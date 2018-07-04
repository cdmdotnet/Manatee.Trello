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
	internal class CheckItemContext : SynchronizationContext<IJsonCheckItem>
	{
		private static readonly Dictionary<string, object> Parameters;
		private static readonly CheckItem.Fields MemberFields;

		public static Dictionary<string, object> CurrentParameters
		{
			get
			{
				lock (Parameters)
				{
					if (!Parameters.Any())
						GenerateParameters();

					return new Dictionary<string, object>(Parameters);
				}
			}
		}

		private string _ownerId;
		private bool _deleted;

		static CheckItemContext()
		{
			Parameters = new Dictionary<string, object>();
			MemberFields = CheckItem.Fields.Name |
			               CheckItem.Fields.Position |
			               CheckItem.Fields.State;
			Properties = new Dictionary<string, Property<IJsonCheckItem>>
				{
					{
						nameof(CheckItem.CheckList),
						new Property<IJsonCheckItem, CheckList>((d, a) => d.CheckList?.GetFromCache<CheckList, IJsonCheckList>(a),
						                                        (d, o) => d.CheckList = o?.Json)
					},
					{
						nameof(CheckItem.Id),
						new Property<IJsonCheckItem, string>((d, a) => d.Id, (d, o) => d.Id = o)
					},
					{
						nameof(CheckItem.Name),
						new Property<IJsonCheckItem, string>((d, a) => d.Name, (d, o) => d.Name = o)
					},
					{
						nameof(CheckItem.Position),
						new Property<IJsonCheckItem, Position>((d, a) => Position.GetPosition(d.Pos),
						                                       (d, o) => d.Pos = Position.GetJson(o))
					},
					{
						nameof(CheckItem.State),
						new Property<IJsonCheckItem, CheckItemState?>((d, a) => d.State, (d, o) => d.State = o)
					},
				};
		}
		public CheckItemContext(string id, string ownerId, TrelloAuthorization auth)
			: base(auth)
		{
			_ownerId = ownerId;
			Data.Id = id;
		}

		public static void UpdateParameters()
		{
			lock (Parameters)
			{
				Parameters.Clear();
			}
		}

		private static void GenerateParameters()
		{
			lock (Parameters)
			{
				Parameters.Clear();
				var flags = Enum.GetValues(typeof(CheckItem.Fields)).Cast<CheckItem.Fields>().ToList();
				var availableFields = (CheckItem.Fields)flags.Cast<int>().Sum();

				var memberFields = availableFields & MemberFields & CheckItem.DownloadedFields;
				Parameters["fields"] = memberFields.GetDescription();
			}
		}

		public async Task Delete(CancellationToken ct)
		{
			if (_deleted) return;
			CancelUpdate();

			var endpoint = EndpointFactory.Build(EntityRequestType.CheckItem_Write_Delete,
			                                     new Dictionary<string, object> {{"_checklistId", _ownerId}, {"_id", Data.Id}});
			await JsonRepository.Execute(Auth, endpoint, ct);

			_deleted = true;
			RaiseDeleted();
		}

		public override Endpoint GetRefreshEndpoint()
		{
			return EndpointFactory.Build(EntityRequestType.CheckItem_Read_Refresh,
			                             new Dictionary<string, object> {{"_checklistId", _ownerId}, {"_id", Data.Id}});
		}

		protected override async Task<IJsonCheckItem> GetData(CancellationToken ct)
		{
			try
			{
				var endpoint = EndpointFactory.Build(EntityRequestType.CheckItem_Read_Refresh,
				                                     new Dictionary<string, object> {{"_checklistId", _ownerId}, {"_id", Data.Id}});
				var newData = await JsonRepository.Execute<IJsonCheckItem>(Auth, endpoint, ct, CurrentParameters);

				MarkInitialized();
				return newData;
			}
			catch (TrelloInteractionException e)
			{
				if (!e.IsNotFoundError() || !IsInitialized) throw;
				_deleted = true;
				return Data;
			}
		}
		protected override async Task SubmitData(IJsonCheckItem json, CancellationToken ct)
		{
			// Checklist should be downloaded already since CheckItem ctor is internal,
			// but allow for the case where it has not been anyway.
			var checkListJson = TrelloConfiguration.JsonFactory.Create<IJsonCheckList>();
			checkListJson.Id = _ownerId;
			var checkList = checkListJson.GetFromCache<CheckList, IJsonCheckList>(Auth);
			if (string.IsNullOrEmpty(checkList.Json.Card?.Id))
				await checkList.Refresh(ct: ct);

			// This may make a call to get the card, but it can't be avoided.  We need its ID.
			var endpoint = EndpointFactory.Build(EntityRequestType.CheckItem_Write_Update, new Dictionary<string, object>
				{
					{"_cardId", checkList.Card.Id},
					{"_checklistId", _ownerId},
					{"_id", Data.Id},
				});
			var newData = await JsonRepository.Execute(Auth, endpoint, json, ct);
			if (!string.IsNullOrEmpty(newData.CheckList?.Id))
				_ownerId = newData.CheckList.Id;
			Merge(newData);
		}
		protected override bool CanUpdate()
		{
			return !_deleted;
		}
	}
}