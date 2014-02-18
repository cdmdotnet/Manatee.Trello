/***************************************************************************************

	Copyright 2013 Little Crab Solutions

	   Licensed under the Apache License, Version 2.0 (the "License");
	   you may not use this file except in compliance with the License.
	   You may obtain a copy of the License at

		 http://www.apache.org/licenses/LICENSE-2.0

	   Unless required by applicable law or agreed to in writing, software
	   distributed under the License is distributed on an "AS IS" BASIS,
	   WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
	   See the License for the specific language governing permissions and
	   limitations under the License.
 
	File Name:		ExpiringList.cs
	Namespace:		Manatee.Trello.Internal
	Class Name:		ExpiringList<TSource, TContent>
	Purpose:		A collection of entities which automatically updates.

***************************************************************************************/
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Manatee.Trello.Contracts;
using Manatee.Trello.Internal.DataAccess;

namespace Manatee.Trello.Internal
{
	internal class ListActionDefinition
	{
		public IEnumerable<ActionType> ActionTypes { get; set; }
		public IEnumerable<IEnumerable<string>> OwnerPaths { get; set; }
	}

	internal abstract class ExpiringListBase : ExpiringObject
	{
		internal static readonly Dictionary<Type, ListActionDefinition> ActionTypeMap;

		static ExpiringListBase()
		{
			ActionTypeMap = new Dictionary<Type, ListActionDefinition>
				{
					{
						typeof (Action), new ListActionDefinition
							{
								ActionTypes = new[]
									{
										ActionType.CommentCard,
										ActionType.CopyCommentCard
									},
								OwnerPaths = new[] {new[] {"card", "id"}}
							}
					},
					{
						typeof (Attachment), new ListActionDefinition
							{
								ActionTypes = new[]
									{
										ActionType.AddAttachmentToCard,
										ActionType.DeleteAttachmentFromCard
									},
								OwnerPaths = new[] {new[] {"card", "id"}}
							}
					},
					{
						typeof (Board), new ListActionDefinition
							{
								ActionTypes = new[]
									{
										ActionType.AddToOrganizationBoard,
										ActionType.CopyBoard,
										ActionType.CreateBoard,
										ActionType.RemoveFromOrganizationBoard
									},
								OwnerPaths = new[]
									{
										new[] {"member", "id"},
										new[] {"organization", "id"}
									}
							}
					},
					{
						typeof (BoardMembership), new ListActionDefinition
							{
								ActionTypes = new[]
									{
										ActionType.AddMemberToBoard, ActionType.AddMemberToCard, ActionType.AddMemberToOrganization, ActionType.MakeAdminOfBoard, ActionType.MakeNormalMemberOfBoard,
										ActionType.MakeNormalMemberOfOrganization, ActionType.MakeObserverOfBoard, ActionType.MemberJoinedTrello, ActionType.RemoveAdminFromBoard, ActionType.RemoveAdminFromOrganization,
										ActionType.RemoveMemberFromBoard, ActionType.RemoveMemberFromCard
									},
								OwnerPaths = new[] {new[] {"member", "id"}}
							}
					},
					{
						typeof (Card), new ListActionDefinition
							{
								ActionTypes = new[]
									{
										ActionType.ConvertToCardFromCheckItem,
										ActionType.CopyCard,
										ActionType.CreateCard,
										ActionType.DeleteCard,
										ActionType.MoveCardFromBoard,
										ActionType.MoveCardToBoard
									},
								OwnerPaths = new[]
									{
										new[] {"board", "id"},
										new[] {"list", "id"}
									}
							}
					},
					{
						typeof (CheckItem), new ListActionDefinition
							{
								ActionTypes = new[] {ActionType.ConvertToCardFromCheckItem},
								OwnerPaths = new[] {new[] {"checkList", "id"}}
							}
					},
					{
						typeof (CheckList), new ListActionDefinition
							{
								ActionTypes = new[] {ActionType.AddChecklistToCard},
								OwnerPaths = new[] {new[] {"card", "id"}}
							}
					},
					{
						typeof (List), new ListActionDefinition
							{
								ActionTypes = new[]
									{
										ActionType.CreateList,
										ActionType.MoveListFromBoard,
										ActionType.MoveListToBoard
									},
								OwnerPaths = new[] {new[] {"board", "id"}}
							}
					},
					{
						typeof (Member), new ListActionDefinition
							{
								ActionTypes = new[]
									{
										ActionType.AddMemberToBoard,
										ActionType.AddMemberToCard,
										ActionType.AddMemberToOrganization,
										ActionType.MakeAdminOfBoard,
										ActionType.MakeNormalMemberOfBoard,
										ActionType.MakeNormalMemberOfOrganization,
										ActionType.MakeObserverOfBoard,
										ActionType.MemberJoinedTrello,
										ActionType.RemoveAdminFromBoard,
										ActionType.RemoveAdminFromOrganization,
										ActionType.RemoveMemberFromBoard,
										ActionType.RemoveMemberFromCard
									},
								OwnerPaths = new[]
									{
										new[] {"board", "id"},
										new[] {"card", "id"},
										new[] {"organization", "id"}
									}
							}
					},
					{
						typeof (Organization), new ListActionDefinition
							{
								ActionTypes = new[]
									{
										ActionType.AddMemberToOrganization,
										ActionType.MakeNormalMemberOfOrganization,
										ActionType.MakeObserverOfBoard,
										ActionType.MemberJoinedTrello,
										ActionType.RemoveAdminFromOrganization,
									},
								OwnerPaths = new[] {new[] {"organization", "id"}}
							}
					},
					{
						typeof (OrganizationMembership), new ListActionDefinition
							{
								ActionTypes = new[]
									{
										ActionType.AddMemberToOrganization,
										ActionType.MakeNormalMemberOfOrganization,
										ActionType.MakeObserverOfBoard,
										ActionType.MemberJoinedTrello,
										ActionType.RemoveAdminFromOrganization,
									},
								OwnerPaths = new[] {new[] {"organization", "id"}}
							}
					},
				};
		}
	}

	internal class ExpiringList<T> : ExpiringListBase, IEnumerable<T>, ICanWebhook, IEquatable<ExpiringList<T>>
		where T : ExpiringObject, IEquatable<T>, IComparable<T>
	{
		private readonly EntityRequestType _requestType;
		private readonly List<T> _list;

		public string Filter { get; set; }
		public override bool IsStubbed { get { return (Owner == null) || Owner.IsStubbed; } }

		internal List<T> List { get { return _list; } }

		protected override bool AllowSelfUpdate { get { return true; } }

		public ExpiringList(ExpiringObject owner, EntityRequestType requestType)
		{
			_requestType = requestType;
			Owner = owner;
			_list = new List<T>();
		}
		public IEnumerator<T> GetEnumerator()
		{
			VerifyNotExpired();
			Expires = DateTime.Now;
			return _list.GetEnumerator();
		}
		IEnumerator IEnumerable.GetEnumerator()
		{
			return GetEnumerator();
		}
		public bool Equals(ExpiringList<T> other)
		{
			return Equals(Owner, other.Owner) && (Filter == other.Filter);
		}
		public override bool Equals(object obj)
		{
			if (!(obj is ExpiringList<T>)) return false;
			return Equals((ExpiringList<T>) obj);
		}
		public override int GetHashCode()
		{
			unchecked
			{
				return ((Owner != null ? Owner.GetHashCode() : 0)*397) ^
				       (Filter != null ? Filter.GetHashCode() : 0);
			}
		}
		public override string ToString()
		{
			return _list.ToString();
		}
		public override sealed bool Refresh()
		{
			if (Owner != null)
				Parameters.Add("_id", Owner.Id);
			if (Filter != null)
				Parameters.Add("filter", Filter);
			Parameters.Add("fields", RestParameterRepository.GetParameters<T>()["fields"]);
			EntityRepository.RefreshCollection<T>(this, _requestType);
			return true;
		}
		void ICanWebhook.ApplyAction(Action action)
		{
			if (!ActionTypeMap.ContainsKey(typeof(T))) return;
			var definition = ActionTypeMap[typeof (T)];
			if (!definition.ActionTypes.Contains(action.Type)) return;
			var ids = definition.OwnerPaths.Select(p => action.Data.TryGetString(p.ToArray())).Where(s => s != null);
			if (!ids.Contains(Owner.Id)) return;
			foreach (var entity in _list)
			{
				entity.MarkForUpdate();
			}
			MarkForUpdate();
		}

		internal override void PropagateDependencies()
		{
			foreach (var item in _list)
			{
				item.Owner = Owner;
			}
		}
		internal void Update(IEnumerable<T> items)
		{
			_list.Clear();
			_list.AddRange(items);
			Expires = DateTime.Now + EntityRepository.EntityDuration;
		}
		internal override void ApplyJson(object obj) {}
	}
}
