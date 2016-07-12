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
 
	File Name:		MemberSearch.cs
	Namespace:		Manatee.Trello
	Class Name:		MemberSearch
	Purpose:		Performs a search for members.

***************************************************************************************/

using System.Collections.Generic;
using Manatee.Trello.Internal;
using Manatee.Trello.Internal.Synchronization;
using Manatee.Trello.Internal.Validation;

namespace Manatee.Trello
{
	/// <summary>
	/// Performs a search for members.
	/// </summary>
	public class MemberSearch
	{
		private readonly Field<Board> _board;
		private readonly Field<int?> _limit;
		private readonly Field<Organization> _organization;
		private readonly Field<string> _query;
		private readonly Field<bool?> _restrictToOrganization;
		private readonly Field<IEnumerable<MemberSearchResult>> _results;
		private readonly MemberSearchContext _context;

		/// <summary>
		/// Gets the collection of results returned by the search.
		/// </summary>
		public IEnumerable<MemberSearchResult> Results => _results.Value;

		private Board Board
		{
			get { return _board.Value; }
			set { _board.Value = value; }
		}
		private int? Limit
		{
			get { return _limit.Value; }
			set { _limit.Value = value; }
		}
		private Organization Organization
		{
			get { return _organization.Value; }
			set { _organization.Value = value; }
		}
		private string Query
		{
			get { return _query.Value; }
			set { _query.Value = value; }
		}
		private bool? RestrictToOrganization
		{
			get { return _restrictToOrganization.Value; }
			set { _restrictToOrganization.Value = value; }
		}

		/// <summary>
		/// Creates a new instance of the <see cref="MemberSearch"/> object and performs the search.
		/// </summary>
		/// <param name="query">The query.</param>
		/// <param name="limit">Optional - The result limit.  Can be a value from 1 to 20. The default is 8.</param>
		/// <param name="board">Optional - A board to which the search should be limited.</param>
		/// <param name="organization">Optional - An organization to which the search should be limited.</param>
		/// <param name="restrictToOrganization">Optional - Restricts the search to only organization members.</param>
		/// <param name="auth">(Optional) Custom authorization parameters. When not provided,
		/// <see cref="TrelloAuthorization.Default"/> will be used.</param>
		public MemberSearch(string query, int? limit = null, Board board = null, Organization organization = null, bool? restrictToOrganization = null, TrelloAuthorization auth = null)
		{
			_context = new MemberSearchContext(auth);

			_board = new Field<Board>(_context, nameof(Board));
			_limit = new Field<int?>(_context, nameof(Limit));
			_limit.AddRule(NullableHasValueRule<int>.Instance);
			_limit.AddRule(new NumericRule<int> {Min = 1, Max = 20});
			_organization = new Field<Organization>(_context, nameof(Organization));
			_query = new Field<string>(_context, nameof(Query));
			_query.AddRule(NotNullOrWhiteSpaceRule.Instance);
			_restrictToOrganization = new Field<bool?>(_context, nameof(RestrictToOrganization));
			_results = new Field<IEnumerable<MemberSearchResult>>(_context, nameof(Results));

			Query = query;
			Limit = limit;
			Board = board;
			Organization = organization;
			RestrictToOrganization = restrictToOrganization;
		}

		/// <summary>
		/// Marks the member search to be refreshed the next time data is accessed.
		/// </summary>
		public void Refresh()
		{
			_context.Expire();
		}
	}
}