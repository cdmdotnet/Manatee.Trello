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
	/// A read-only collection of custom drop down options.
	/// </summary>
	public class ReadOnlyDropDownOptionCollection : ReadOnlyCollection<IDropDownOption>,
	                                                IHandle<EntityDeletedEvent<IJsonCustomDropDownOption>>
	{
		internal ReadOnlyDropDownOptionCollection(Func<string> getOwnerId, TrelloAuthorization auth)
			: base(getOwnerId, auth)
		{
		}

		internal sealed override async Task PerformRefresh(bool force, CancellationToken ct)
		{
			var endpoint = EndpointFactory.Build(EntityRequestType.CustomFieldDefinition_Read_Options,
			                                     new Dictionary<string, object> {{"_id", OwnerId}});
			var newData = await JsonRepository.Execute<List<IJsonCustomDropDownOption>>(Auth, endpoint, ct, AdditionalParameters);

			Items.Clear();
			Items.AddRange(newData.Select(jb =>
				{
					var option = jb.GetFromCache<DropDownOption, IJsonCustomDropDownOption>(Auth);
					option.Json = jb;
					return option;
				}));
		}

		void IHandle<EntityDeletedEvent<IJsonCustomDropDownOption>>.Handle(EntityDeletedEvent<IJsonCustomDropDownOption> message)
		{
			var item = Items.FirstOrDefault(c => c.Id == message.Data.Id);
			Items.Remove(item);
		}
	}
}