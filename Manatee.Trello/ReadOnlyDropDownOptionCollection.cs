using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Manatee.Trello.Internal.Caching;
using Manatee.Trello.Internal.DataAccess;
using Manatee.Trello.Json;

namespace Manatee.Trello
{
	public class ReadOnlyDropDownOptionCollection : ReadOnlyCollection<DropDownOption>
	{
		internal ReadOnlyDropDownOptionCollection(Func<string> getOwnerId, TrelloAuthorization auth)
			: base(getOwnerId, auth)
		{
		}

		public override async Task Refresh(CancellationToken ct = default(CancellationToken))
		{
			var endpoint = EndpointFactory.Build(EntityRequestType.Board_Read_CustomFields,
			                                     new Dictionary<string, object> { { "_id", OwnerId } });
			var newData = await JsonRepository.Execute<List<IJsonCustomDropDownOption>>(Auth, endpoint, ct);

			Items.Clear();
			Items.AddRange(newData.Select(jb =>
				{
					var option = jb.GetFromCache<DropDownOption, IJsonCustomDropDownOption>(Auth);
					option.Json = jb;
					return option;
				}));
		}
	}
}