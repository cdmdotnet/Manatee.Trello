using System;
using System.Collections.Generic;
using Manatee.Trello.Exceptions;
using Manatee.Trello.Internal.Caching;
using Manatee.Trello.Internal.DataAccess;
using Manatee.Trello.Internal.Validation;
using Manatee.Trello.Json;

namespace Manatee.Trello.Internal.Synchronization
{
	internal class CardContext : SynchronizationContext<IJsonCard>
	{
		private bool _deleted;

		public BadgesContext BadgesContext { get; }
		protected override bool IsDataComplete => !Data.Name.IsNullOrWhiteSpace();
		public virtual bool HasValidId => IdRule.Instance.Validate(Data.Id, null) == null;

		static CardContext()
		{
			_properties = new Dictionary<string, Property<IJsonCard>>
				{
					{
						"Board", new Property<IJsonCard, Board>((d, a) => d.Board?.GetFromCache<Board>(a),
						                                        (d, o) => d.Board = o?.Json)
					},
					{"Description", new Property<IJsonCard, string>((d, a) => d.Desc, (d, o) => d.Desc = o)},
					{"DueDate", new Property<IJsonCard, DateTime?>((d, a) => d.Due?.Decode(), (d, o) =>
						{
							d.Due = o?.Encode();
							d.ForceDueDate = o == null;
						})},
					{"Id", new Property<IJsonCard, string>((d, a) => d.Id, (d, o) => d.Id = o)},
					{"IsArchived", new Property<IJsonCard, bool?>((d, a) => d.Closed, (d, o) => d.Closed = o)},
					{"IsComplete", new Property<IJsonCard, bool?>((d, a) => d.DueComplete, (d, o) => d.DueComplete = o)},
					{"IsSubscribed", new Property<IJsonCard, bool?>((d, a) => d.Subscribed, (d, o) => d.Subscribed = o)},
					{"Labels", new Property<IJsonCard, List<IJsonLabel>>((d, a) => d.Labels, (d, o) => d.Labels = o)},
					{"LastActivity", new Property<IJsonCard, DateTime?>((d, a) => d.DateLastActivity, (d, o) => d.DateLastActivity = o)},
					{
						"List", new Property<IJsonCard, List>((d, a) => d.List?.GetFromCache<List>(a),
						                                      (d, o) => d.List = o?.Json)
					},
					{"Name", new Property<IJsonCard, string>((d, a) => d.Name, (d, o) => d.Name = o)},
					{
						"Position", new Property<IJsonCard, Position>((d, a) => Position.GetPosition(d.Pos),
						                                              (d, o) => d.Pos = Position.GetJson(o))
					},
					{"ShortId", new Property<IJsonCard, int?>((d, a) => d.IdShort, (d, o) => d.IdShort = o)},
					{"ShortUrl", new Property<IJsonCard, string>((d, a) => d.ShortUrl, (d, o) => d.ShortUrl = o)},
					{"Url", new Property<IJsonCard, string>((d, a) => d.Url, (d, o) => d.Url = o)},
				};
		}
		public CardContext(string id, TrelloAuthorization auth)
			: base(auth)
		{
			Data.Id = id;
			BadgesContext = new BadgesContext(Auth);
			BadgesContext.SynchronizeRequested += () => Synchronize();
			Data.Badges = BadgesContext.Data;
		}

		public void Delete()
		{
			if (_deleted) return;
			CancelUpdate();

			var endpoint = EndpointFactory.Build(EntityRequestType.Card_Write_Delete, new Dictionary<string, object> {{"_id", Data.Id}});
			JsonRepository.Execute(Auth, endpoint);

			_deleted = true;
		}

		protected override IJsonCard GetData()
		{
			try
			{
				var endpoint = EndpointFactory.Build(EntityRequestType.Card_Read_Refresh, new Dictionary<string, object> {{"_id", Data.Id}});
				var newData = JsonRepository.Execute<IJsonCard>(Auth, endpoint);
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
		protected override void SubmitData(IJsonCard json)
		{
			var endpoint = EndpointFactory.Build(EntityRequestType.Card_Write_Update, new Dictionary<string, object> {{"_id", Data.Id}});
			var newData = JsonRepository.Execute(Auth, endpoint, json);
			Merge(newData);
		}
		protected override IEnumerable<string> MergeDependencies(IJsonCard json)
		{
			return BadgesContext.Merge(json.Badges);
		}
		protected override bool CanUpdate()
		{
			return !_deleted;
		}
		public override void Expire()
		{
			BadgesContext.Expire();
			base.Expire();
		}
	}
}