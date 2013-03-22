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
 
	File Name:		List.cs
	Namespace:		Manatee.Trello
	Class Name:		List
	Purpose:		Represents a list on Trello.com.

***************************************************************************************/
using System;
using System.Collections.Generic;
using Manatee.Json;
using Manatee.Json.Enumerations;
using Manatee.Trello.Implementation;
using Manatee.Trello.Rest;
using RestSharp;
using JsonObject = Manatee.Json.JsonObject;

namespace Manatee.Trello
{
	//{
	//   "id":"5144051cbd0da6681200201f",
	//   "name":"Current Version Backlog",
	//   "closed":false,
	//   "idBoard":"5144051cbd0da6681200201e",
	//   "pos":16384
	//}
	public class List : JsonCompatibleExpiringObject, IEquatable<List>
	{
		private readonly ExpiringList<List, Action> _actions;
		private string _boardId;
		private Board _board;
		private readonly ExpiringList<List, Card> _cards;
		private bool? _isClosed;
		private bool? _isSubscribed;
		private string _name;
		private int? _position;

		public IEnumerable<Action> Actions { get { return _actions; } }
		public Board Board
		{
			get
			{
				VerifyNotExpired();
				return ((_board == null) || (_board.Id != _boardId)) && (Svc != null) ? (_board = Svc.Retrieve<Board>(_boardId)) : _board;
			}
		}
		public IEnumerable<Card> Cards { get { return _cards; } }
		public bool? IsClosed
		{
			get
			{
				VerifyNotExpired();
				return _isClosed;
			}
			set
			{
				_isClosed = value;
				Parameters.Add("closed", _isClosed.ToLowerString());
				Put();
			}
		}
		public bool? IsSubscribed
		{
			get
			{
				VerifyNotExpired();
				return _isSubscribed;
			}
			set
			{
				_isSubscribed = value;
				Parameters.Add("subscribed", _isSubscribed.ToLowerString());
				Put();
			}
		}
		public string Name
		{
			get
			{
				VerifyNotExpired();
				return _name;
			}
			set
			{
				_name = value;
				Parameters.Add("name", _name);
				Put();
			}
		}
		public int? Position
		{
			get
			{
				VerifyNotExpired();
				return _position;
			}
			set
			{
				_position = value;
				Parameters.Add("pos", _position);
				Put();
			}
		}

		public List()
		{
			_actions = new ExpiringList<List, Action>(this);
			_cards = new ExpiringList<List, Card>(this);
		}
		internal List(TrelloService svc, string id)
			: base(svc, id)
		{
			_actions = new ExpiringList<List, Action>(svc, this);
			_cards = new ExpiringList<List, Card>(svc, this);
		}

		public Card AddCard(string name, string description = null, Position position = null)
		{
			var request = new Request<Card>(this);
			Parameters.Add("name", name);
			Parameters.Add("idList", Id);
			if (description != null)
				Parameters.Add("desc", description);
			if ((position != null) && position.IsValid)
				Parameters.Add("pos", position);
			var card = Svc.PostAndCache(request);
			_cards.MarkForUpdate();
			return card;
		}
		private void Delete()
		{
			throw new NotSupportedException("Deleting lists is not yet supported by Trello.");
		}
		public void Move(Board board, int? position = null)
		{
			Parameters.Add(new Parameter { Name = "idBoard", Value = board.Id });
			if (position != null)
				Parameters.Add(new Parameter { Name = "pos", Value = position });
			Svc.PutAndCache(new Request<List>(this));
			_actions.MarkForUpdate();
		}
		public override void FromJson(JsonValue json)
		{
			if (json == null) return;
			if (json.Type != JsonValueType.Object) return;
			var obj = json.Object;
			Id = obj.TryGetString("id");
			_boardId = obj.TryGetString("idBoard");
			_isClosed = obj.TryGetBoolean("closed");
			_isSubscribed = obj.TryGetBoolean("subscribed");
			_name = obj.TryGetString("name");
			_position = (int?) obj.TryGetNumber("pos");
		}
		public override JsonValue ToJson()
		{
			var json = new JsonObject
			           	{
			           		{"id", Id},
			           		{"state", _boardId},
			           		{"closed", _isClosed.HasValue ? _isClosed.Value : JsonValue.Null},
			           		{"subscribed", _isSubscribed.HasValue ? _isSubscribed.Value : JsonValue.Null},
			           		{"name", _name},
			           		{"pos", _position.HasValue ? _position.Value : JsonValue.Null}
			           	};
			return json;
		}
		public bool Equals(List other)
		{
			return Id == other.Id;
		}

		internal override void Refresh(ExpiringObject entity)
		{
			var list = entity as List;
			if (list == null) return;
			_boardId = list._boardId;
			_isClosed = list._isClosed;
			_isSubscribed = list._isSubscribed;
			_name = list._name;
			_position = list._position;
		}
		internal override bool Match(string id)
		{
			return Id == id;
		}

		protected override void Get()
		{
			var entity = Svc.Api.Get(new Request<List>(Id));
			Refresh(entity);
		}
		protected override void PropigateSerivce()
		{
			_actions.Svc = Svc;
			_cards.Svc = Svc;
			if (_board != null) _board.Svc = Svc;
		}

		private void Put()
		{
			Svc.PutAndCache(new Request<List>(this));
			_actions.MarkForUpdate();
		}
	}
}
