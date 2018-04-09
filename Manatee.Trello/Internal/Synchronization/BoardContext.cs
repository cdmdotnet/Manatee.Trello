using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Manatee.Trello.Internal.Caching;
using Manatee.Trello.Internal.DataAccess;
using Manatee.Trello.Internal.Validation;
using Manatee.Trello.Json;

namespace Manatee.Trello.Internal.Synchronization
{
	internal class BoardContext : SynchronizationContext<IJsonBoard>
	{
		private bool _deleted;

		public BoardPreferencesContext BoardPreferencesContext { get; }
		public virtual bool HasValidId => IdRule.Instance.Validate(Data.Id, null) == null;

		static BoardContext()
		{
			_properties = new Dictionary<string, Property<IJsonBoard>>
				{
					{"Description", new Property<IJsonBoard, string>((d, a) => d.Desc, (d, o) => d.Desc = o)},
					{"Id", new Property<IJsonBoard, string>((d, a) => d.Id, (d, o) => d.Id = o)},
					{"IsClosed", new Property<IJsonBoard, bool?>((d, a) => d.Closed, (d, o) => d.Closed = o)},
					{"IsSubscribed", new Property<IJsonBoard, bool?>((d, a) => d.Subscribed, (d, o) => d.Subscribed = o)},
					{"Name", new Property<IJsonBoard, string>((d, a) => d.Name, (d, o) => d.Name = o)},
					{
						"Organization", new Property<IJsonBoard, Organization>((d, a) => d.Organization?.GetFromCache<Organization>(a),
																			   (d, o) => d.Organization = o?.Json)
					},
					{"Preferences", new Property<IJsonBoard, IJsonBoardPreferences>((d, a) => d.Prefs, (d, o) => d.Prefs = o)},
					{"Url", new Property<IJsonBoard, string>((d, a) => d.Url, (d, o) => d.Url = o)},
					{nameof(Board.IsPinned), new Property<IJsonBoard, bool?>((d, a) => d.Pinned, (d, o) => d.Pinned = o)},
					{nameof(Board.IsStarred), new Property<IJsonBoard, bool?>((d, a) => d.Starred, (d, o) => d.Starred = o)},
					{nameof(Board.LastViewed), new Property<IJsonBoard, DateTime?>((d, a) => d.DateLastView, (d, o) => d.DateLastView = o)},
					{nameof(Board.LastActivity), new Property<IJsonBoard, DateTime?>((d, a) => d.DateLastActivity, (d, o) => d.DateLastActivity = o)},
					{nameof(Board.ShortUrl), new Property<IJsonBoard, string>((d, a) => d.ShortUrl, (d, o) => d.ShortUrl = o)},
					{nameof(Board.ShortLink), new Property<IJsonBoard, string>((d, a) => d.ShortLink, (d, o) => d.ShortLink = o)},
				};
		}
		public BoardContext(string id, TrelloAuthorization auth)
			: base(auth)
		{
			Data.Id = id;
			BoardPreferencesContext = new BoardPreferencesContext(Auth);
			BoardPreferencesContext.SynchronizeRequested += ct => Synchronize(ct);
			BoardPreferencesContext.SubmitRequested += ct => HandleSubmitRequested("Preferences", ct);
			Data.Prefs = BoardPreferencesContext.Data;
		}

		public async Task Delete(CancellationToken ct)
		{
			if (_deleted) return;
			CancelUpdate();

			var endpoint = EndpointFactory.Build(EntityRequestType.Board_Write_Delete,
			                                     new Dictionary<string, object> {{"_id", Data.Id}});
			await JsonRepository.Execute(Auth, endpoint, ct);

			_deleted = true;
		}
		public override async Task Expire(CancellationToken ct)
		{
			await BoardPreferencesContext.Expire(ct);
			await base.Expire(ct);
		}

		protected override async Task<IJsonBoard> GetData(CancellationToken ct)
		{
			var endpoint = EndpointFactory.Build(EntityRequestType.Board_Read_Refresh,
			                                     new Dictionary<string, object> {{"_id", Data.Id}});
			var newData = await JsonRepository.Execute<IJsonBoard>(Auth, endpoint, ct);

			return newData;
		}
		protected override async Task SubmitData(IJsonBoard json, CancellationToken ct)
		{
			var endpoint = EndpointFactory.Build(EntityRequestType.Board_Write_Update,
			                                     new Dictionary<string, object> {{"_id", Data.Id}});
			var newData = await JsonRepository.Execute(Auth, endpoint, json, ct);

			Merge(newData);
			Data.Prefs = BoardPreferencesContext.Data;
		}
		protected override void ApplyDependentChanges(IJsonBoard json)
		{
			Data.Prefs = BoardPreferencesContext.Data;
			if (BoardPreferencesContext.HasChanges)
			{
				json.Prefs = BoardPreferencesContext.GetChanges();
				BoardPreferencesContext.ClearChanges();
			}
		}
		protected override IEnumerable<string> MergeDependencies(IJsonBoard json)
		{
			return BoardPreferencesContext.Merge(json.Prefs);
		}
		protected override bool IsDataComplete => !Data.Name.IsNullOrWhiteSpace();
	}
}