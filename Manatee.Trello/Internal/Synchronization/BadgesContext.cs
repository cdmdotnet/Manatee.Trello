using System;
using System.Collections.Generic;
using Manatee.Trello.Json;

namespace Manatee.Trello.Internal.Synchronization
{
	internal class BadgesContext : LinkedSynchronizationContext<IJsonBadges>
	{
		static BadgesContext()
		{
			_properties = new Dictionary<string, Property<IJsonBadges>>
				{
					{"Attachments", new Property<IJsonBadges, int?>((d, a) => d.Attachments, (d, o) => d.Attachments = o)},
					{"CheckItems", new Property<IJsonBadges, int?>((d, a) => d.CheckItems, (d, o) => d.CheckItems = o)},
					{"CheckItemsChecked", new Property<IJsonBadges, int?>((d, a) => d.CheckItemsChecked, (d, o) => d.CheckItemsChecked = o)},
					{"Comments", new Property<IJsonBadges, int?>((d, a) => d.Comments, (d, o) => d.Comments = o)},
					{"DueDate", new Property<IJsonBadges, DateTime?>((d, a) => d.Due, (d, o) => d.Due = o)},
					{"FogBugz", new Property<IJsonBadges, string>((d, a) => d.Fogbugz, (d, o) => d.Fogbugz = o)},
					{"HasDescription", new Property<IJsonBadges, bool?>((d, a) => d.Description, (d, o) => d.Description = o)},
					{"HasVoted", new Property<IJsonBadges, bool?>((d, a) => d.ViewingMemberVoted, (d, o) => d.ViewingMemberVoted = o)},
					{"IsSubscribed", new Property<IJsonBadges, bool?>((d, a) => d.Subscribed, (d, o) => d.Subscribed = o)},
					{"Votes", new Property<IJsonBadges, int?>((d, a) => d.Votes, (d, o) => d.Votes = o)},
				};
		}
		public BadgesContext(TrelloAuthorization auth)
			: base(auth) {}
	}
}