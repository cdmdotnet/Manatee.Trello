using System;
using System.Collections.Generic;
using Manatee.Trello.Internal.DataAccess;
using Manatee.Trello.Json;

namespace Manatee.Trello.Internal.Synchronization
{
	internal class ActionContext : SynchronizationContext<IJsonAction>
	{
		public ActionDataContext ActionDataContext { get; private set; }

		static ActionContext()
		{
			_properties = new Dictionary<string, Property<IJsonAction>>
				{
					{
						"Creator", new Property<IJsonAction, Member>(d => d.MemberCreator.GetFromCache<Member>(),
						                                             (d, o) => d.MemberCreator = o.Json)
					},
					{"Date", new Property<IJsonAction, DateTime?>(d => d.Date, (d, o) => d.Date = o)},
					{"Id", new Property<IJsonAction, string>(d => d.Id, (d, o) => d.Id = o)},
					{"Type", new Property<IJsonAction, ActionType>(d => d.Type, (d, o) => d.Type = o)},
				};
		}
		public ActionContext(string id)
		{
			Data.Id = id;
			ActionDataContext = new ActionDataContext();
			ActionDataContext.SynchronizeRequested += () => Synchronize();
			Data.Data = ActionDataContext.Data;
		}

		protected override IJsonAction GetData()
		{
			var endpoint = EndpointFactory.Build(EntityRequestType.Action_Read_Refresh, new Dictionary<string, object> {{"_id", Data.Id}});
			var newData = JsonRepository.Execute<IJsonAction>(TrelloAuthorization.Default, endpoint);

			return newData;
		}
		protected override IEnumerable<string> MergeDependencies(IJsonAction json)
		{
			return ActionDataContext.Merge(json.Data);
		}
	}
}