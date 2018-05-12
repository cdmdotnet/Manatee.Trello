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
	/// <summary>
	/// A read-only collection of custom drop down options.
	/// </summary>
	public class ReadOnlyDropDownOptionCollection : ReadOnlyCollection<IDropDownOption>
	{
		internal ReadOnlyDropDownOptionCollection(Func<string> getOwnerId, TrelloAuthorization auth)
			: base(getOwnerId, auth)
		{
		}

		/// <summary>
		/// Manually updates the collection's data.
		/// </summary>
		/// <param name="ct">(Optional) A cancellation token for async processing.</param>
		public sealed override async Task Refresh(CancellationToken ct = default(CancellationToken))
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
	}
}