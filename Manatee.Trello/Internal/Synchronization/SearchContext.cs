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
 
	File Name:		SearchContext.cs
	Namespace:		Manatee.Trello.Internal.Synchronization
	Class Name:		SearchContext
	Purpose:		Provides a data context for a search.

***************************************************************************************/
using System;
using System.Collections.Generic;
using System.Linq;
using Manatee.Trello.Contracts;
using Manatee.Trello.Internal.Caching;
using Manatee.Trello.Internal.DataAccess;
using Manatee.Trello.Json;
using IQueryable = Manatee.Trello.Contracts.IQueryable;

namespace Manatee.Trello.Internal.Synchronization
{
	internal class SearchContext : SynchronizationContext<IJsonSearch>
	{
		private static readonly Dictionary<Type, Func<ICacheable, IJsonCacheable>> _jsonExtraction;

		static SearchContext()
		{
			_jsonExtraction = new Dictionary<Type, Func<ICacheable, IJsonCacheable>>
				{
					{typeof (Action), o => ((Action) o).Json},
					{typeof (Board), o => ((Board) o).Json},
					{typeof (Card), o => ((Card) o).Json},
					{typeof (List), o => ((List) o).Json},
					{typeof (Member), o => ((Member) o).Json},
					{typeof (Organization), o => ((Organization) o).Json},
				};
			_properties = new Dictionary<string, Property<IJsonSearch>>
				{
					{
						"Actions", new Property<IJsonSearch, IEnumerable<Action>>((d, a) => d.Actions?.Select(j => j.GetFromCache<Action>(a)).ToList() ?? Enumerable.Empty<Action>(),
						                                                   (d, o) => d.Actions = o?.Select(a => a.Json).ToList())
					},
					{
						"Boards", new Property<IJsonSearch, IEnumerable<Board>>((d, a) => d.Boards?.Select(j => j.GetFromCache<Board>(a)).ToList() ?? Enumerable.Empty<Board>(),
						                                                 (d, o) => d.Boards = o?.Select(a => a.Json).ToList())
					},
					{
						"Cards", new Property<IJsonSearch, IEnumerable<Card>>((d, a) => d.Cards?.Select(j => j.GetFromCache<Card>(a)).ToList() ?? Enumerable.Empty<Card>(),
						                                               (d, o) => d.Cards = o?.Select(a => a.Json).ToList())
					},
					{
						"Members", new Property<IJsonSearch, IEnumerable<Member>>((d, a) => d.Members?.Select(j => j.GetFromCache<Member>(a)).ToList() ?? Enumerable.Empty<Member>(),
						                                                   (d, o) => d.Members = o?.Select(a => a.Json).ToList())
					},
					{
						"Organizations", new Property<IJsonSearch, IEnumerable<Organization>>((d, a) => d.Organizations?.Select(j => j.GetFromCache<Organization>(a)).ToList() ?? Enumerable.Empty<Organization>(),
						                                                               (d, o) => d.Organizations = o?.Select(a => a.Json).ToList())
					},
					{"Query", new Property<IJsonSearch, string>((d, a) => d.Query, (d, o) => { if (!o.IsNullOrWhiteSpace()) d.Query = o; })},
					{
						"Context", new Property<IJsonSearch, List<IQueryable>>((d, a) => d.Context?.Select(j => j.GetFromCache(a)).Cast<IQueryable>().ToList(),
						                                                       (d, o) => { if (o != null) d.Context = o.Select(ExtractData).ToList(); })
					},
					{"Types", new Property<IJsonSearch, SearchModelType?>((d, a) => d.Types, (d, o) => { if (o != 0) d.Types = o; })},
					{"Limit", new Property<IJsonSearch, int?>((d, a) => d.Limit, (d, o) => { if (o != 0) d.Limit = o; })},
					{"IsPartial", new Property<IJsonSearch, bool?>((d, a) => d.Partial, (d, o) => { if (o.HasValue) d.Partial = o.Value; })},
				};
		}
		public SearchContext(TrelloAuthorization auth)
			: base(auth) {}

		protected override IJsonSearch GetData()
		{
			// NOTE: Cannot place these parameters in a JSON object because it's a GET operation.
			var parameters = new Dictionary<string, object>
				{
					{"query", Data.Query},
					{"modelTypes", Data.Types.FlagsEnumToCommaList()}
				};
			if (Data.Context != null)
			{
				TryAddContext<IJsonCard>(parameters, "idCards");
				TryAddContext<IJsonBoard>(parameters, "idBoards");
				TryAddContext<IJsonOrganization>(parameters, "idOrganizations");
			}
			if (Data.Limit.HasValue)
			{
				parameters.Add("boards_limit", Data.Limit);
				parameters.Add("cards_limit", Data.Limit);
				parameters.Add("organizations_limit", Data.Limit);
				parameters.Add("members_limit", Data.Limit);
			}
			if (Data.Partial)
			{
				parameters.Add("partial", Data.Partial.ToLowerString());
			}
			var endpoint = EndpointFactory.Build(EntityRequestType.Service_Read_Search);
			var newData = JsonRepository.Execute<IJsonSearch>(Auth, endpoint, parameters);

			return newData;
		}

		private static IJsonCacheable ExtractData(ICacheable obj)
		{
			return _jsonExtraction[obj.GetType()](obj);
		}

		private void TryAddContext<T>(Dictionary<string, object> json, string key)
			where T : IJsonCacheable
		{
			var ids = Data.Context.OfType<T>().Select(o => o.Id).Take(24).Join(",");
			if (!ids.IsNullOrWhiteSpace())
				json[key] = ids;
		}
	}
}
