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
 
	File Name:		Action.cs
	Namespace:		Manatee.Trello
	Class Name:		Action
	Purpose:		Represents an action.

***************************************************************************************/

using System;
using System.Collections.Generic;
using Manatee.Trello.Contracts;
using Manatee.Trello.Internal;
using Manatee.Trello.Internal.Synchronization;
using Manatee.Trello.Json;

namespace Manatee.Trello
{
	public class Action : ICacheable
	{
		private readonly Field<Member> _creator;
		private readonly Field<DateTime?> _date;
		private readonly Field<ActionType> _type;
		private readonly ActionContext _context;

		public Member Creator { get { return _creator.Value; } }
		public ActionData Data { get; private set; }
		public DateTime? Date { get { return _date.Value; } }
		public string Id { get; private set; }
		public ActionType Type { get { return _type.Value; } }

		internal IJsonAction Json { get { return _context.Data; } }

		public event Action<Action, IEnumerable<string>> Updated;

		public Action(string id)
		{
			Id = id;
			_context = new ActionContext(id);
			_context.Synchronized += Synchronized;

			_creator = new Field<Member>(_context, () => Creator);
			_date = new Field<DateTime?>(_context, () => Date);
			Data = new ActionData(_context.ActionDataContext);
			_type = new Field<ActionType>(_context, () => Type);

			TrelloConfiguration.Cache.Add(this);
		}
		internal Action(IJsonAction json)
			: this(json.Id)
		{
			_context.Merge(json);
		}

		private void Synchronized(IEnumerable<string> properties)
		{
			Id = _context.Data.Id;
			var handler = Updated;
			if (handler != null)
				handler(this, properties);
		}
	}
}