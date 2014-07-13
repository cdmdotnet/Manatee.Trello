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
 
	File Name:		BoardCollection.cs
	Namespace:		Manatee.Trello
	Class Name:		BoardCollection
	Purpose:		Represents a collection of boards.

***************************************************************************************/

using System;
using System.Collections.Generic;
using System.Linq;
using Manatee.Trello.Enumerations;
using Manatee.Trello.Internal.DataAccess;
using Manatee.Trello.Internal.Genesis;
using Manatee.Trello.Json;

namespace Manatee.Trello
{
	public class ReadOnlyBoardCollection : ReadOnlyCollection<Board>
	{
		private readonly EntityRequestType _updateRequestType;

		internal ReadOnlyBoardCollection(Type type, string ownerId)
			: base(ownerId)
		{
			_updateRequestType = type == typeof (Organization)
				                     ? EntityRequestType.Organization_Read_Boards
				                     : EntityRequestType.Member_Read_Boards;
		}

		protected override sealed void Update()
		{
			var endpoint = EndpointFactory.Build(_updateRequestType, new Dictionary<string, object> {{"_id", OwnerId}});
			var newData = JsonRepository.Execute<List<IJsonBoard>>(TrelloAuthorization.Default, endpoint);

			Items.Clear();
			Items.AddRange(newData.Select(jc => TrelloConfiguration.Cache.Find<Board>(b => b.Id == jc.Id) ?? new Board(jc.Id)));
		}
	}

	public class BoardCollection : ReadOnlyBoardCollection
	{
		private readonly EntityRequestType _addRequestType;

		internal BoardCollection(Type type, string ownerId)
			: base(type, ownerId)
		{
			_addRequestType = type == typeof (Organization)
				                  ? EntityRequestType.Organization_Write_CreateBoard
				                  : EntityRequestType.Member_Write_CreateBoard;
		}

		public Board Add(string name)
		{
			var json = TrelloConfiguration.JsonFactory.Create<IJsonBoard>();
			json.Name = name;
			if (_addRequestType == EntityRequestType.Organization_Write_CreateBoard)
			{
				json.Organization = TrelloConfiguration.JsonFactory.Create<IJsonOrganization>();
				json.Organization.Id = OwnerId;
			}

			var endpoint = EndpointFactory.Build(_addRequestType);
			var newData = JsonRepository.Execute(TrelloAuthorization.Default, endpoint, json);

			return new Board(newData);
		}
	}
}