using System;
using System.Collections.Generic;
using Manatee.Trello.Json;

namespace Manatee.Trello.Internal.Synchronization
{
	internal class BadgesContext : LinkedSynchronizationContext<IJsonBadges>
	{
		static BadgesContext()
		{
			Properties = new Dictionary<string, Property<IJsonBadges>>
				{
					{
						nameof(Badges.Attachments),
						new Property<IJsonBadges, int?>((d, a) => d.Attachments, (d, o) => d.Attachments = o)
					},
					{
						nameof(Badges.CheckItems),
						new Property<IJsonBadges, int?>((d, a) => d.CheckItems, (d, o) => d.CheckItems = o)
					},
					{
						nameof(Badges.CheckItemsChecked),
						new Property<IJsonBadges, int?>((d, a) => d.CheckItemsChecked, (d, o) => d.CheckItemsChecked = o)
					},
					{
						nameof(Badges.Comments),
						new Property<IJsonBadges, int?>((d, a) => d.Comments, (d, o) => d.Comments = o)
					},
					{
						nameof(Badges.DueDate),
						new Property<IJsonBadges, DateTime?>((d, a) => d.Due, (d, o) => d.Due = o)
					},
					{
						nameof(Badges.FogBugz),
						new Property<IJsonBadges, string>((d, a) => d.Fogbugz, (d, o) => d.Fogbugz = o)
					},
					{
						nameof(Badges.HasDescription),
						new Property<IJsonBadges, bool?>((d, a) => d.Description, (d, o) => d.Description = o)
					},
					{
						nameof(Badges.HasVoted),
						new Property<IJsonBadges, bool?>((d, a) => d.ViewingMemberVoted, (d, o) => d.ViewingMemberVoted = o)
					},
					{
						nameof(Badges.IsComplete),
						new Property<IJsonBadges, bool?>((d, a) => d.DueComplete, (d, o) => d.DueComplete = o)
					},
					{
						nameof(Badges.IsSubscribed),
						new Property<IJsonBadges, bool?>((d, a) => d.Subscribed, (d, o) => d.Subscribed = o)
					},
					{
						nameof(Badges.Votes),
						new Property<IJsonBadges, int?>((d, a) => d.Votes, (d, o) => d.Votes = o)
					},
				};
		}
		public BadgesContext(TrelloAuthorization auth)
			: base(auth) {}
	}
}