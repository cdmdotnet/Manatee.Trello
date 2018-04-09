using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Manatee.Trello.Internal.Caching;
using Manatee.Trello.Internal.DataAccess;
using Manatee.Trello.Json;

namespace Manatee.Trello.Internal.Synchronization
{
	internal class BoardPersonalPreferencesContext : SynchronizationContext<IJsonBoardPersonalPreferences>
	{
		private readonly Func<string> _getOwnerId;
		private string _ownerId;

		private string OwnerId => _ownerId ?? (_ownerId = _getOwnerId());

		static BoardPersonalPreferencesContext()
		{
			_properties = new Dictionary<string, Property<IJsonBoardPersonalPreferences>>
				{
					{"ShowSidebar", new Property<IJsonBoardPersonalPreferences, bool?>((d, a) => d.ShowSidebar, (d, o) => d.ShowSidebar = o)},
					{"ShowSidebarMembers", new Property<IJsonBoardPersonalPreferences, bool?>((d, a) => d.ShowSidebarMembers, (d, o) => d.ShowSidebarMembers = o)},
					{"ShowSidebarBoardActions", new Property<IJsonBoardPersonalPreferences, bool?>((d, a) => d.ShowSidebarBoardActions, (d, o) => d.ShowSidebarBoardActions = o)},
					{"ShowSidebarActivity", new Property<IJsonBoardPersonalPreferences, bool?>((d, a) => d.ShowSidebarActivity, (d, o) => d.ShowSidebarActivity = o)},
					{"ShowListGuide", new Property<IJsonBoardPersonalPreferences, bool?>((d, a) => d.ShowListGuide, (d, o) => d.ShowListGuide = o)},
					{
						"EmailPosition", new Property<IJsonBoardPersonalPreferences, Position>((d, a) => Position.GetPosition(d.EmailPosition),
																							   (d, o) => d.EmailPosition = Position.GetJson(o))
					},
					{
						"EmailListId", new Property<IJsonBoardPersonalPreferences, List>((d, a) => d.EmailList?.GetFromCache<List>(a),
																						 (d, o) => d.EmailList = o?.Json)
					},
				};
		}
		public BoardPersonalPreferencesContext(Func<string> getOwnerId, TrelloAuthorization auth)
			: base(auth)
		{
			_getOwnerId = getOwnerId;
		}

		protected override async Task<IJsonBoardPersonalPreferences> GetData(CancellationToken ct)
		{
			var endpoint = EndpointFactory.Build(EntityRequestType.Board_Read_PersonalPrefs, new Dictionary<string, object> {{"_id", OwnerId}});
			var newData = await JsonRepository.Execute<IJsonBoardPersonalPreferences>(TrelloAuthorization.Default, endpoint, ct);

			return newData;
		}
		protected override Task SubmitData(IJsonBoardPersonalPreferences json, CancellationToken ct)
		{
			return Task.WhenAll(
				json.EmailList == null
					? Task.CompletedTask
					: SubmitDataPoint(json.EmailList.Id, "emailList", ct),
				SubmitDataPoint(json.EmailPosition, "emailPosition", ct),
				SubmitDataPoint(json.ShowListGuide, "showListGuide", ct),
				SubmitDataPoint(json.ShowSidebar, "showSidebar", ct),
				SubmitDataPoint(json.ShowSidebarActivity, "showSidebarActivity", ct),
				SubmitDataPoint(json.ShowSidebarBoardActions, "showSidebarBoardActions", ct),
				SubmitDataPoint(json.ShowSidebarMembers, "showSidebarMembers", ct));
		}

		private async Task SubmitDataPoint<T>(T value, string segment, CancellationToken ct)
		{
			if (Equals(value, default(T))) return;

			var json = TrelloConfiguration.JsonFactory.Create<IJsonParameter>();
			if (typeof (T) == typeof (bool?))
				json.Boolean = (bool?) (object) value;
			else
				json.String = value.ToString();

			var endpoint = EndpointFactory.Build(EntityRequestType.Board_Write_PersonalPrefs, new Dictionary<string, object> {{"_id", _ownerId}});
			endpoint.AddSegment(segment);
			await JsonRepository.Execute(Auth, endpoint, json, ct);
		}
	}
}