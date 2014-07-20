using System.Collections.Generic;
using Manatee.Trello.Json;

namespace Manatee.Trello.Internal.Synchronization
{
	internal class ActionDataContext : LinkedSynchronizationContext<IJsonActionData>
	{
		static ActionDataContext()
		{
			_properties = new Dictionary<string, Property<IJsonActionData>>
				{
					{
						"Attachment", new Property<IJsonActionData, Attachment>(d => d.Attachment == null
							                                                             ? null
							                                                             : new Attachment(d.Attachment, d.Card.Id, false),
						                                                        (d, o) => { if (o != null) d.Attachment = o.Json; })
					},
					{
						"Board", new Property<IJsonActionData, Board>(d => d.Board == null
							                                                   ? null
							                                                   : new Board(d.Board, false),
						                                              (d, o) => { if (o != null) d.Board = o.Json; })
					},
					{
						"Card", new Property<IJsonActionData, Card>(d => d.Card == null
							                                                 ? null
							                                                 : new Card(d.Card, false),
						                                            (d, o) => { if (o != null) d.Card = o.Json; })
					},
					{
						"CheckItem", new Property<IJsonActionData, CheckItem>(d => d.CheckItem == null
							                                                           ? null
							                                                           : new CheckItem(d.CheckItem, d.CheckList.Id, false),
						                                                      (d, o) => { if (o != null) d.CheckItem = o.Json; })
					},
					{
						"CheckList", new Property<IJsonActionData, CheckList>(d => d.CheckList == null
							                                                           ? null
							                                                           : new CheckList(d.CheckList, false),
						                                                      (d, o) => { if (o != null) d.CheckList = o.Json; })
					},
					{
						"List", new Property<IJsonActionData, List>(d => d.List == null
							                                                 ? null
							                                                 : new List(d.List, false),
						                                            (d, o) => { if (o != null) d.List = o.Json; })
					},
					{
						"ListAfter", new Property<IJsonActionData, List>(d => d.ListAfter == null
							                                                      ? null
							                                                      : new List(d.ListAfter, false),
						                                                 (d, o) => { if (o != null) d.ListAfter = o.Json; })
					},
					{
						"ListBefore", new Property<IJsonActionData, List>(d => d.ListBefore == null
							                                                       ? null
							                                                       : new List(d.ListBefore, false),
						                                                  (d, o) => { if (o != null) d.ListBefore = o.Json; })
					},
					{
						"Member", new Property<IJsonActionData, Member>(d => d.Member == null
							                                                     ? null
							                                                     : new Member(d.Member, false),
						                                                (d, o) => { if (o != null) d.Member = o.Json; })
					},
					{"OldClosed", new Property<IJsonActionData, bool?>(d => d.Old == null ? null : d.Old.Closed, (d, o) => { if (d.Old != null && o != null) d.Old.Closed = o; })},
					{"OldDesc", new Property<IJsonActionData, string>(d => d.Old == null ? null : d.Old.Desc, (d, o) => { if (d.Old != null && o != null) d.Old.Desc = o; })},
					{
						"OldList", new Property<IJsonActionData, List>(d => d.Old == null || d.Old.List == null
							                                                    ? null
							                                                    : new List(d.Old.List, false),
						                                               (d, o) => { if (d.Old != null) d.Old.List = o.Json; })
					},
					{"OldPos", new Property<IJsonActionData, Position>(d => d.Old == null ? null : d.Old.Pos, (d, o) => { if (d.Old != null) d.Old.Pos = o.Value; })},
					{"OldText", new Property<IJsonActionData, string>(d => d.Old == null ? null : d.Old.Text, (d, o) => { if (d.Old != null) d.Old.Text = o; })},
					{
						"Organization", new Property<IJsonActionData, Organization>(d => d.Org == null
							                                                                 ? null
							                                                                 : new Organization(d.Org, false),
						                                                            (d, o) => { if (o != null) d.Org = o.Json; })
					},
					{"Text", new Property<IJsonActionData, string>(d => d.Text, (d, o) => d.Text = o)},
				};
		}
	}
}