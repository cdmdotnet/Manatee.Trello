/***************************************************************************************

	Copyright 2015 Greg Dennis

	   Licensed under the Apache License, Version 2.0 (the "License");
	   you may not use this file except in compliance with the License.
	   You may obtain a copy of the License at

		 http://www.apache.org/licenses/LICENSE-2.0

	   Unless required by applicable law or agreed to in writing, software
	   distributed under the License is distributed on an "AS IS" BASIS,
	   WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
	   See the License for the specific language governing permissions and
	   limitations under the License.
 
	File Name:		ActionDataContext.cs
	Namespace:		Manatee.Trello.Internal.Synchronization
	Class Name:		ActionDataContext
	Purpose:		Provides a data context for action data.

***************************************************************************************/

using System;
using System.Collections.Generic;
using Manatee.Trello.Internal.Caching;
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
						"Attachment", new Property<IJsonActionData, Attachment>((d, a) => d.Attachment == null
							                                                             ? null
							                                                             : new Attachment(d.Attachment, d.Card.Id, a),
						                                                        (d, o) => { if (o != null) d.Attachment = o.Json; })
					},
					{
						"Board", new Property<IJsonActionData, Board>((d, a) => d.Board == null
							                                                   ? null
							                                                   : new Board(d.Board, a),
						                                              (d, o) => { if (o != null) d.Board = o.Json; })
					},
					{
						"BoardSource", new Property<IJsonActionData, Board>((d, a) => d.BoardSource == null
							                                                         ? null
							                                                         : new Board(d.BoardSource, a),
						                                                    (d, o) => { if (o != null) d.BoardSource = o.Json; })
					},
					{
						"BoardTarget", new Property<IJsonActionData, Board>((d, a) => d.BoardTarget == null
							                                                         ? null
							                                                         : new Board(d.BoardTarget, a),
						                                                    (d, o) => { if (o != null) d.BoardTarget = o.Json; })
					},
					{
						"Card", new Property<IJsonActionData, Card>((d, a) => d.Card == null
							                                                 ? null
							                                                 : new Card(d.Card, a),
						                                            (d, o) => { if (o != null) d.Card = o.Json; })
					},
					{
						"CardSource", new Property<IJsonActionData, Card>((d, a) => d.CardSource == null
							                                                       ? null
							                                                       : new Card(d.CardSource, a),
						                                                  (d, o) => { if (o != null) d.CardSource = o.Json; })
					},
					{
						"CheckItem", new Property<IJsonActionData, CheckItem>((d, a) => d.CheckItem == null || d.CheckList == null
							                                                           ? null
							                                                           : new CheckItem(d.CheckItem, d.CheckList.Id),
						                                                      (d, o) => { if (o != null) d.CheckItem = o.Json; })
					},
					{
						"CheckList", new Property<IJsonActionData, CheckList>((d, a) => d.CheckList == null
							                                                           ? null
							                                                           : new CheckList(d.CheckList, a),
						                                                      (d, o) => { if (o != null) d.CheckList = o.Json; })
					},
					{"LastEdited", new Property<IJsonActionData, DateTime?>((d, a) => d.DateLastEdited, (d, o) => { if (o != null) d.DateLastEdited = o; })},
					{
						"List", new Property<IJsonActionData, List>((d, a) => d.List == null
							                                                 ? null
							                                                 : new List(d.List, a),
						                                            (d, o) => { if (o != null) d.List = o.Json; })
					},
					{
						"ListAfter", new Property<IJsonActionData, List>((d, a) => d.ListAfter == null
							                                                      ? null
							                                                      : new List(d.ListAfter, a),
						                                                 (d, o) => { if (o != null) d.ListAfter = o.Json; })
					},
					{
						"ListBefore", new Property<IJsonActionData, List>((d, a) => d.ListBefore == null
							                                                       ? null
							                                                       : new List(d.ListBefore, a),
						                                                  (d, o) => { if (o != null) d.ListBefore = o.Json; })
					},
					{
						"Member", new Property<IJsonActionData, Member>((d, a) => d.Member?.GetFromCache<Member>(a),
						                                                (d, o) => { if (o != null) d.Member = o.Json; })
					},
					{"WasArchived", new Property<IJsonActionData, bool?>((d, a) => d.Old?.Closed, (d, o) => { if (d.Old != null && o != null) d.Old.Closed = o; })},
					{"OldDesc", new Property<IJsonActionData, string>((d, a) => d.Old?.Desc, (d, o) => { if (d.Old != null && o != null) d.Old.Desc = o; })},
					{
						"OldList", new Property<IJsonActionData, List>((d, a) => d.Old?.List == null
							                                                    ? null
							                                                    : new List(d.Old.List, a),
						                                               (d, o) => { if (d.Old != null) d.Old.List = o.Json; })
					},
					{"OldPos", new Property<IJsonActionData, Position>((d, a) => d.Old?.Pos, (d, o) => { if (d.Old != null) d.Old.Pos = o.Value; })},
					{"OldText", new Property<IJsonActionData, string>((d, a) => d.Old?.Text, (d, o) => { if (d.Old != null) d.Old.Text = o; })},
					{
						"Organization", new Property<IJsonActionData, Organization>((d, a) => d.Org == null
							                                                                 ? null
							                                                                 : new Organization(d.Org, a),
						                                                            (d, o) => { if (o != null) d.Org = o.Json; })
					},
					{"Text", new Property<IJsonActionData, string>((d, a) => d.Text, (d, o) => d.Text = o)},
					{"Value", new Property<IJsonActionData, string>((d, a) => d.Value, (d, o) => d.Value = o)},
				};
		}
		public ActionDataContext(TrelloAuthorization auth)
			: base(auth) {}
	}
}