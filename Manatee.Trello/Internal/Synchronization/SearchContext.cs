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
						"Actions", new Property<IJsonSearch, IEnumerable<Action>>(d => d.Actions == null
							                                                        ? Enumerable.Empty<Action>()
							                                                        : d.Actions.Select(j => j.GetFromCache<Action>()).ToList(),
						                                                   (d, o) => d.Actions = o == null ? null : o.Select(a => a.Json).ToList())
					},
					{
						"Boards", new Property<IJsonSearch, IEnumerable<Board>>(d => d.Boards == null
							                                                      ? Enumerable.Empty<Board>()
							                                                      : d.Boards.Select(j => j.GetFromCache<Board>()).ToList(),
						                                                 (d, o) => d.Boards = o == null ? null : o.Select(a => a.Json).ToList())
					},
					{
						"Cards", new Property<IJsonSearch, IEnumerable<Card>>(d => d.Cards == null
							                                                    ? Enumerable.Empty<Card>()
							                                                    : d.Cards.Select(j => j.GetFromCache<Card>()).ToList(),
						                                               (d, o) => d.Cards = o == null ? null : o.Select(a => a.Json).ToList())
					},
					{
						"Members", new Property<IJsonSearch, IEnumerable<Member>>(d => d.Members == null
							                                                        ? Enumerable.Empty<Member>()
							                                                        : d.Members.Select(j => j.GetFromCache<Member>()).ToList(),
						                                                   (d, o) => d.Members = o == null ? null : o.Select(a => a.Json).ToList())
					},
					{
						"Organizations", new Property<IJsonSearch, IEnumerable<Organization>>(d => d.Organizations == null
							                                                                    ? Enumerable.Empty<Organization>()
							                                                                    : d.Organizations.Select(j => j.GetFromCache<Organization>()).ToList(),
						                                                               (d, o) => d.Organizations = o == null ? null : o.Select(a => a.Json).ToList())
					},
					{"Query", new Property<IJsonSearch, string>(d => d.Query, (d, o) => { if (!o.IsNullOrWhiteSpace()) d.Query = o; })},
					{
						"Context", new Property<IJsonSearch, List<IQueryable>>(d => d.Context == null
							                                                            ? null
							                                                            : d.Context.Select(j => j.GetFromCache()).Cast<IQueryable>().ToList(),
						                                                       (d, o) => { if (o != null) d.Context = o.Select(ExtractData).ToList(); })
					},
					{"Types", new Property<IJsonSearch, SearchModelType?>(d => d.Types, (d, o) => { if (o != 0) d.Types = o; })},
					{"Limit", new Property<IJsonSearch, int?>(d => d.Limit, (d, o) => { if (o != 0) d.Limit = o; })},
				};
		}

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
			var endpoint = EndpointFactory.Build(EntityRequestType.Service_Read_Search);
			var newData = JsonRepository.Execute<IJsonSearch>(TrelloAuthorization.Default, endpoint, parameters);

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
