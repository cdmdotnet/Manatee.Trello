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
					{"Attachments", new Property<IJsonBadges>(d => d.Attachments, (d, o) => d.Attachments = (int?) o)},
					{"CheckItems", new Property<IJsonBadges>(d => d.CheckItems, (d, o) => d.CheckItems = (int?) o)},
					{"CheckItemsChecked", new Property<IJsonBadges>(d => d.CheckItemsChecked, (d, o) => d.CheckItemsChecked = (int?) o)},
					{"Comments", new Property<IJsonBadges>(d => d.Comments, (d, o) => d.Comments = (int?) o)},
					{"DueDate", new Property<IJsonBadges>(d => d.Due, (d, o) => d.Due = (DateTime?) o)},
					{"FogBugz", new Property<IJsonBadges>(d => d.Fogbugz, (d, o) => d.Fogbugz = (string) o)},
					{"HasDescription", new Property<IJsonBadges>(d => d.Description, (d, o) => d.Description = (bool?) o)},
					{"HasVoted", new Property<IJsonBadges>(d => d.ViewingMemberVoted, (d, o) => d.ViewingMemberVoted = (bool?) o)},
					{"IsSubscribed", new Property<IJsonBadges>(d => d.Subscribed, (d, o) => d.Subscribed = (bool?) o)},
					{"Votes", new Property<IJsonBadges>(d => d.Votes, (d, o) => d.Votes = (int?) o)},
				};
		}
	}
}