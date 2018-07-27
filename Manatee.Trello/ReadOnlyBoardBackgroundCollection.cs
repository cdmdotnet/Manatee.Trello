using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Manatee.Trello.Internal.Caching;
using Manatee.Trello.Internal.DataAccess;
using Manatee.Trello.Internal.Eventing;
using Manatee.Trello.Json;

namespace Manatee.Trello
{
	/// <summary>
	/// A read-only collection of custom board backgrounds.
	/// </summary>
	public class ReadOnlyBoardBackgroundCollection : ReadOnlyCollection<IBoardBackground>,
	                                                 IHandle<EntityDeletedEvent<IJsonBoardBackground>>
	{
		internal ReadOnlyBoardBackgroundCollection(Func<string> getOwnerId, TrelloAuthorization auth)
			: base(getOwnerId, auth)
		{
			EventAggregator.Subscribe(this);
		}

		internal override async Task PerformRefresh(bool force, CancellationToken ct)
		{
			IncorporateLimit();

			AdditionalParameters["filter"] = "custom";

			var endpoint = EndpointFactory.Build(EntityRequestType.Member_Read_BoardBackgrounds, new Dictionary<string, object> {{"_id", OwnerId}});
			var newData = await JsonRepository.Execute<List<IJsonBoardBackground>>(Auth, endpoint, ct, AdditionalParameters);

			Items.Clear();
			Items.AddRange(newData.Select(jb =>
				{
					var background = jb.GetFromCache<BoardBackground, IJsonBoardBackground>(Auth, true, OwnerId);
					background.Json = jb;
					return background;
				}));
		}

		void IHandle<EntityDeletedEvent<IJsonBoardBackground>>.Handle(EntityDeletedEvent<IJsonBoardBackground> message)
		{
			var item = Items.FirstOrDefault(c => c.Id == message.Data.Id);
			Items.Remove(item);
		}
	}
}