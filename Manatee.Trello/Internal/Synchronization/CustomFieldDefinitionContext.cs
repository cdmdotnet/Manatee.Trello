using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Manatee.Trello.Internal.Caching;
using Manatee.Trello.Internal.DataAccess;
using Manatee.Trello.Json;

namespace Manatee.Trello.Internal.Synchronization
{
	internal class CustomFieldDefinitionContext : SynchronizationContext<IJsonCustomFieldDefinition>
	{
		private bool _deleted;

		public DropDownOptionCollection DropDownOptions { get; }

		static CustomFieldDefinitionContext()
		{
			Properties = new Dictionary<string, Property<IJsonCustomFieldDefinition>>
				{
					{
						nameof(CustomFieldDefinition.Board),
						new Property<IJsonCustomFieldDefinition, Board>((d, a) => d.Board?.GetFromCache<Board, IJsonBoard>(a),
						                                                (d, o) =>
							                                                {
								                                                if (o != null) d.Board = o.Json;
							                                                })
					},
					{
						nameof(CustomFieldDefinition.FieldGroup),
						new Property<IJsonCustomFieldDefinition, string>((d, a) => d.FieldGroup,
						                                                 (d, o) =>
							                                                 {
								                                                 if (o != null) d.FieldGroup = o;
							                                                 })
					},
					{
						nameof(CustomFieldDefinition.Id),
						new Property<IJsonCustomFieldDefinition, string>((d, a) => d.Id, (d, o) => d.Id = o)
					},
					{
						nameof(CustomFieldDefinition.Name),
						new Property<IJsonCustomFieldDefinition, string>((d, a) => d.Name,
						                                                 (d, o) =>
							                                                 {
								                                                 if (o != null) d.Name = o;
							                                                 })
					},
					{
						nameof(CustomFieldDefinition.Position),
						new Property<IJsonCustomFieldDefinition, Position>((d, a) => Position.GetPosition(d.Pos),
						                                                   (d, o) => d.Pos = Position.GetJson(o))
					},
					{
						nameof(CustomFieldDefinition.Type),
						new Property<IJsonCustomFieldDefinition,CustomFieldType?>((d,a) => d.Type,
							(d, o) =>
								{
									if (o != null) d.Type = o;
								})
					},
				};
		}

		public CustomFieldDefinitionContext(string id, TrelloAuthorization auth)
			: base(auth)
		{
			Data.Id = id;

			DropDownOptions = new DropDownOptionCollection(() => Data.Id, auth);
		}

		public async Task Delete(CancellationToken ct)
		{
			if (_deleted) return;
			CancelUpdate();

			var endpoint = EndpointFactory.Build(EntityRequestType.CustomFieldDefinition_Write_Delete,
			                                     new Dictionary<string, object> { { "_id", Data.Id } });
			await JsonRepository.Execute(Auth, endpoint, ct);

			_deleted = true;
		}

		protected override async Task<IJsonCustomFieldDefinition> GetData(CancellationToken ct)
		{
			try
			{
				var endpoint = EndpointFactory.Build(EntityRequestType.CustomFieldDefinition_Read_Refresh,
				                                     new Dictionary<string, object> {{"_id", Data.Id}});
				var newData = await JsonRepository.Execute<IJsonCustomFieldDefinition>(Auth, endpoint, ct);

				return newData;
			}
			catch (TrelloInteractionException e)
			{
				if (!e.IsNotFoundError() || !IsInitialized) throw;
				_deleted = true;
				return Data;
			}
		}
		protected override async Task SubmitData(IJsonCustomFieldDefinition json, CancellationToken ct)
		{
			var endpoint = EndpointFactory.Build(EntityRequestType.CustomFieldDefinition_Write_Update,
			                                     new Dictionary<string, object> {{"_id", Data.Id}});
			var newData = await JsonRepository.Execute(Auth, endpoint, json, ct);

			Merge(newData);
		}

		protected override IEnumerable<string> MergeDependencies(IJsonCustomFieldDefinition json)
		{
			var properties = new List<string>();

			if (json.Options != null)
			{
				DropDownOptions.Update(json.Options.Select(a => a.GetFromCache<DropDownOption, IJsonCustomDropDownOption>(Auth)));
				properties.Add(nameof(Board.Cards));
			}

			return properties;
		}
	}
}
