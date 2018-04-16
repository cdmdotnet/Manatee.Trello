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
	/// A read-only collection of custom fields.
	/// </summary>
	public class ReadOnlyCustomFieldCollection : ReadOnlyCollection<CustomField>
	{
		/// <summary>
		/// Creates a new instance of the <see cref="ReadOnlyCustomFieldCollection"/> class.
		/// </summary>
		/// <param name="getOwnerId"></param>
		/// <param name="auth"></param>
		public ReadOnlyCustomFieldCollection(Func<string> getOwnerId, TrelloAuthorization auth)
			: base(getOwnerId, auth)
		{
		}

		/// <summary>
		/// Refreshes the collection.
		/// </summary>
		/// <returns>A task.</returns>
		public sealed override async Task Refresh(CancellationToken ct = default(CancellationToken))
		{
			var endpoint = EndpointFactory.Build(EntityRequestType.Card_Read_CustomFields,
			                                     new Dictionary<string, object> {{"_id", OwnerId}});
			var newData = await JsonRepository.Execute<List<IJsonCustomField>>(Auth, endpoint, ct);

			Items.Clear();
			Items.AddRange(newData.Select(ja =>
				{
					var field = ja.GetFromCache<CustomField>(Auth);
					field.Json = ja;
					return field;
				}));
		}
	}
}