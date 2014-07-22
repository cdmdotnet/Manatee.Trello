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
 
	File Name:		Search.cs
	Namespace:		Manatee.Trello
	Class Name:		Search
	Purpose:		Represents a search.

***************************************************************************************/

using System.Collections.Generic;
using System.Linq;
using Manatee.Trello.Internal;
using Manatee.Trello.Internal.Synchronization;
using Manatee.Trello.Internal.Validation;
using IQueryable = Manatee.Trello.Contracts.IQueryable;

namespace Manatee.Trello
{
	public class Search
	{
		private readonly Field<IEnumerable<Action>> _actions;
		private readonly Field<IEnumerable<Board>> _boards;
		private readonly Field<IEnumerable<Card>> _cards;
		private readonly Field<IEnumerable<Member>> _members;
		private readonly Field<IEnumerable<Organization>> _organizations;
		private readonly Field<string> _query;
		private readonly Field<List<IQueryable>> _queryContext;
		private readonly Field<SearchModelType> _modelTypes;
		private readonly SearchContext _context;

		public IEnumerable<Action> Actions { get { return _actions.Value; } }
		public IEnumerable<Board> Boards { get { return _boards.Value; } }
		public IEnumerable<Card> Cards { get { return _cards.Value; } }
		public IEnumerable<Member> Members { get { return _members.Value; } }
		public IEnumerable<Organization> Organizations { get { return _organizations.Value; } }

		private string Query
		{
			get { return _query.Value; }
			set { _query.Value = value; }
		}
		private List<IQueryable> Context
		{
			get { return _queryContext.Value; }
			set { _queryContext.Value = value; }
		}
		private SearchModelType Types
		{
			get { return _modelTypes.Value; }
			set { _modelTypes.Value = value; }
		}

		public Search(string query, SearchModelType modelTypes = SearchModelType.All, IEnumerable<IQueryable> context = null)
		{
			_context = new SearchContext();

			_actions = new Field<IEnumerable<Action>>(_context, () => Actions);
			_boards = new Field<IEnumerable<Board>>(_context, () => Boards);
			_cards = new Field<IEnumerable<Card>>(_context, () => Cards);
			_members = new Field<IEnumerable<Member>>(_context, () => Members);
			_organizations = new Field<IEnumerable<Organization>>(_context, () => Organizations);
			_query = new Field<string>(_context, () => Query);
			_query.AddRule(NotNullOrWhiteSpaceRule.Instance);
			_queryContext = new Field<List<IQueryable>>(_context, () => Context);
			_modelTypes = new Field<SearchModelType>(_context, () => Types);

			Query = query;
			if (context != null)
				Context = context.ToList();
			Types = modelTypes;
		}
	}
}