using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Manatee.Trello.Internal.Caching;
using Manatee.Trello.Internal.DataAccess;
using Manatee.Trello.Json;
using IQueryable = Manatee.Trello.IQueryable;

namespace Manatee.Trello.Internal.Synchronization
{
	internal class SearchContext : SynchronizationContext<IJsonSearch>
	{
		private static readonly Dictionary<Type, Func<ICacheable, IJsonCacheable>> JsonExtraction;

		static SearchContext()
		{
			JsonExtraction = new Dictionary<Type, Func<ICacheable, IJsonCacheable>>
				{
					{typeof (Action), o => ((Action) o).Json},
					{typeof (Board), o => ((Board) o).Json},
					{typeof (Card), o => ((Card) o).Json},
					{typeof (List), o => ((List) o).Json},
					{typeof (Member), o => ((Member) o).Json},
					{typeof (Organization), o => ((Organization) o).Json},
				};
			Properties = new Dictionary<string, Property<IJsonSearch>>
				{
					{
						nameof(Search.Actions),
						new Property<IJsonSearch, IEnumerable<Action>>(
							(d, a) => d.Actions?.Select(j => j.GetFromCache<Action>(a)).ToList() ??
							          Enumerable.Empty<Action>(),
							(d, o) => d.Actions = o?.Select(a => a.Json).ToList())
					},
					{
						nameof(Search.Boards),
						new Property<IJsonSearch, IEnumerable<Board>>(
							(d, a) => d.Boards?.Select(j => j.GetFromCache<Board>(a)).ToList() ??
							          Enumerable.Empty<Board>(),
							(d, o) => d.Boards = o?.Select(a => a.Json).ToList())
					},
					{
						nameof(Search.Cards),
						new Property<IJsonSearch, IEnumerable<Card>>(
							(d, a) => d.Cards?.Select(j => j.GetFromCache<Card>(a)).ToList() ??
							          Enumerable.Empty<Card>(),
							(d, o) => d.Cards = o?.Select(a => a.Json).ToList())
					},
					{
						nameof(Search.Members),
						new Property<IJsonSearch, IEnumerable<Member>>(
							(d, a) => d.Members?.Select(j => j.GetFromCache<Member>(a)).ToList() ??
							          Enumerable.Empty<Member>(),
							(d, o) => d.Members = o?.Select(a => a.Json).ToList())
					},
					{
						nameof(Search.Organizations),
						new Property<IJsonSearch, IEnumerable<Organization>>(
							(d, a) => d.Organizations
							           ?.Select(j => j.GetFromCache<Organization>(a)).ToList() ??
							          Enumerable.Empty<Organization>(),
							(d, o) => d.Organizations = o?.Select(a => a.Json).ToList())
					},
					{
						nameof(Search.Query),
						new Property<IJsonSearch, string>((d, a) => d.Query,
						                                  (d, o) =>
							                                  {
								                                  if (!o.IsNullOrWhiteSpace()) d.Query = o;
							                                  })
					},
					{
						nameof(Search.Context),
						new Property<IJsonSearch, List<IQueryable>>(
							(d, a) => d.Context?.Select(j => j.GetFromCache(a)).Cast<IQueryable>().ToList(),
							(d, o) =>
								{
									if (o != null) d.Context = o.Select(ExtractData).ToList();
								})
					},
					{
						nameof(Search.Types),
						new Property<IJsonSearch, SearchModelType?>((d, a) => d.Types,
						                                            (d, o) =>
							                                            {
								                                            if (o != 0) d.Types = o;
							                                            })
					},
					{
						nameof(Search.Limit),
						new Property<IJsonSearch, int?>((d, a) => d.Limit,
						                                (d, o) =>
							                                {
								                                if (o != 0) d.Limit = o;
							                                })
					},
					{
						nameof(Search.IsPartial),
						new Property<IJsonSearch, bool?>((d, a) => d.Partial,
						                                 (d, o) =>
							                                 {
								                                 if (o.HasValue) d.Partial = o.Value;
							                                 })
					},
				};
		}
		public SearchContext(TrelloAuthorization auth)
			: base(auth) {}

		protected override async Task<IJsonSearch> GetData(CancellationToken ct)
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
			var newData = await JsonRepository.Execute<IJsonSearch>(Auth, endpoint, ct, parameters);

			return newData;
		}

		private static IJsonCacheable ExtractData(ICacheable obj)
		{
			return JsonExtraction[obj.GetType()](obj);
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
