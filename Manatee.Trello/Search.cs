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
 
	File Name:		Search.cs
	Namespace:		Manatee.Trello
	Class Name:		Search
	Purpose:		Performs a search.

***************************************************************************************/

using System.Collections.Generic;
using System.Linq;
using Manatee.Trello.Internal;
using Manatee.Trello.Internal.Synchronization;
using Manatee.Trello.Internal.Validation;
using IQueryable = Manatee.Trello.Contracts.IQueryable;

namespace Manatee.Trello
{
	/// <summary>
	/// Performs a search.
	/// </summary>
	public class Search
	{
		private readonly Field<IEnumerable<Action>> _actions;
		private readonly Field<IEnumerable<Board>> _boards;
		private readonly Field<IEnumerable<Card>> _cards;
		private readonly Field<IEnumerable<Member>> _members;
		private readonly Field<IEnumerable<Organization>> _organizations;
		private readonly Field<string> _query;
		private readonly Field<List<IQueryable>> _queryContext;
		private readonly Field<SearchModelType?> _modelTypes;
		private readonly Field<int?> _limit; 
		private readonly SearchContext _context;

		/// <summary>
		/// Gets the collection of actions returned by the search.
		/// </summary>
		public IEnumerable<Action> Actions => _actions.Value;
		/// <summary>
		/// Gets the collection of boards returned by the search.
		/// </summary>
		public IEnumerable<Board> Boards => _boards.Value;
		/// <summary>
		/// Gets the collection of cards returned by the search.
		/// </summary>
		public IEnumerable<Card> Cards => _cards.Value;
		/// <summary>
		/// Gets the collection of members returned by the search.
		/// </summary>
		public IEnumerable<Member> Members => _members.Value;
		/// <summary>
		/// Gets the collection of organizations returned by the search.
		/// </summary>
		public IEnumerable<Organization> Organizations => _organizations.Value;
		/// <summary>
		/// Gets the query.
		/// </summary>
		public string Query
		{
			get { return _query.Value; }
			private set { _query.Value = value; }
		}

		private List<IQueryable> Context
		{
			get { return _queryContext.Value; }
			set { _queryContext.Value = value; }
		}
		private SearchModelType? Types
		{
			get { return _modelTypes.Value; }
			set { _modelTypes.Value = value; }
		}
		private int? Limit
		{
			get { return _limit.Value; }
			set { _limit.Value = value; }
		}

		/// <summary>
		/// Creates a new instance of the <see cref="Search"/> object and performs the search.
		/// </summary>
		/// <param name="query">The query.</param>
		/// <param name="limit">The maximum number of results to return.</param>
		/// <param name="modelTypes">Optional - The desired model types to return.  Can be joined using the | operator.  Default is All.</param>
		/// <param name="context">Optional - A collection of queryable items to serve as a context in which to search.</param>
		/// <param name="auth">(Optional) Custom authorization parameters. When not provided,
		/// <see cref="TrelloAuthorization.Default"/> will be used.</param>
		public Search(SearchFor query, int? limit = null, SearchModelType modelTypes = SearchModelType.All, IEnumerable<IQueryable> context = null, TrelloAuthorization auth = null)
			: this(query.ToString(), limit, modelTypes, context, auth) { }
		/// <summary>
		/// Creates a new instance of the <see cref="Search"/> object and performs the search.
		/// </summary>
		/// <param name="query">The query.</param>
		/// <param name="limit">The maximum number of results to return.</param>
		/// <param name="modelTypes">Optional - The desired model types to return.  Can be joined using the | operator.  Default is All.</param>
		/// <param name="context">Optional - A collection of queryable items to serve as a context in which to search.</param>
		/// <param name="auth">(Optional) Custom authorization parameters. When not provided,
		/// <see cref="TrelloAuthorization.Default"/> will be used.</param>
		public Search(string query, int? limit = null, SearchModelType modelTypes = SearchModelType.All, IEnumerable<IQueryable> context = null, TrelloAuthorization auth = null)
		{
			_context = new SearchContext(auth);

			_actions = new Field<IEnumerable<Action>>(_context, () => Actions);
			_boards = new Field<IEnumerable<Board>>(_context, () => Boards);
			_cards = new Field<IEnumerable<Card>>(_context, () => Cards);
			_members = new Field<IEnumerable<Member>>(_context, () => Members);
			_organizations = new Field<IEnumerable<Organization>>(_context, () => Organizations);
			_query = new Field<string>(_context, () => Query);
			_query.AddRule(NotNullOrWhiteSpaceRule.Instance);
			_queryContext = new Field<List<IQueryable>>(_context, () => Context);
			_modelTypes = new Field<SearchModelType?>(_context, () => Types);
			_limit = new Field<int?>(_context, () => Limit);
			_limit.AddRule(NullableHasValueRule<int>.Instance);
			_limit.AddRule(new NumericRule<int> { Min = 1, Max = 1000 });

			Query = query;
			if (context != null)
				Context = context.ToList();
			Types = modelTypes;
			Limit = limit;
		}

		/// <summary>
		/// Marks the search to be refreshed the next time data is accessed.
		/// </summary>
		public void Refresh()
		{
			_context.Expire();
		}
	}
}