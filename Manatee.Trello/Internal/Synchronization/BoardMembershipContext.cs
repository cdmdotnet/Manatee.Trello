/***************************************************************************************

	Copyright 2014 Greg Dennis

	   Licensed under the Apache License, Version 2.0 (the "License");
	   you may not use this file except in compliance with the License.
	   You may obtain a copy of the License at

		 http://www.apache.org/licenses/LICENSE-2.0

	   Unless required by applicable law or agreed to in writing, software
	   distributed under the License is distributed on an "AS IS" BASIS,
	   WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
	   See the License for the specific language governing permissions and
	   limitations under the License.
 
	File Name:		BoardMembershipContext.cs
	Namespace:		Manatee.Trello.Internal.Synchronization
	Class Name:		BoardMembershipContext
	Purpose:		Provides a data context for a board membership.

***************************************************************************************/
using System.Collections.Generic;
using Manatee.Trello.Internal.Caching;
using Manatee.Trello.Internal.DataAccess;
using Manatee.Trello.Internal.Validation;
using Manatee.Trello.Json;

namespace Manatee.Trello.Internal.Synchronization
{
	internal class BoardMembershipContext : SynchronizationContext<IJsonBoardMembership>
	{
		private readonly string _ownerId;

		public virtual bool HasValidId { get { return IdRule.Instance.Validate(Data.Id, null) == null; } }

		static BoardMembershipContext()
		{
			_properties = new Dictionary<string, Property<IJsonBoardMembership>>
				{
					{"Id", new Property<IJsonBoardMembership, string>(d => d.Id, (d, o) => d.Id = o)},
					{"IsDeactivated", new Property<IJsonBoardMembership, bool?>(d => d.Deactivated, (d, o) => d.Deactivated = o)},
					{
						"Member", new Property<IJsonBoardMembership, Member>(d => d.Member.GetFromCache<Member>(),
						                                                     (d, o) => d.Member = o != null ? o.Json : null)
					},
					{"MemberType", new Property<IJsonBoardMembership, BoardMembershipType?>(d => d.MemberType, (d, o) => d.MemberType = o)},
				};
		}
		public BoardMembershipContext(string id, string ownerId)
		{
			_ownerId = ownerId;
			Data.Id = id;
		}
		protected override IJsonBoardMembership GetData()
		{
			var endpoint = EndpointFactory.Build(EntityRequestType.BoardMembership_Read_Refresh, new Dictionary<string, object> {{"_boardId", _ownerId}, {"_id", Data.Id}});
			var newData = JsonRepository.Execute<IJsonBoardMembership>(TrelloAuthorization.Default, endpoint);

			return newData;
		}
		protected override void SubmitData(IJsonBoardMembership json)
		{
			var endpoint = EndpointFactory.Build(EntityRequestType.BoardMembership_Write_Update, new Dictionary<string, object> {{"_boardId", _ownerId}, {"_id", Data.Id}});
			var newData = JsonRepository.Execute(TrelloAuthorization.Default, endpoint, json);
			Merge(newData);
		}
	}
}