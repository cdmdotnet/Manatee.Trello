using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Manatee.Trello.Internal.DataAccess;
using Manatee.Trello.Json;

namespace Manatee.Trello
{
	/// <summary>
	/// A read-only collection of custom field definitions.
	/// </summary>
	public class ReadOnlyCustomFieldDefinitionCollection : ReadOnlyCollection<CustomFieldDefinition>
	{
		/// <summary>
		/// Creates a new instance of the <see cref="ReadOnlyCustomFieldDefinitionCollection"/> class.
		/// </summary>
		/// <param name="getOwnerId"></param>
		/// <param name="auth"></param>
		public ReadOnlyCustomFieldDefinitionCollection(Func<string> getOwnerId, TrelloAuthorization auth)
			: base(getOwnerId, auth)
		{
		}

		/// <summary>
		/// Refreshes the collection.
		/// </summary>
		/// <returns>A task.</returns>
		public sealed override async Task Refresh(CancellationToken ct = default(CancellationToken))
		{
			var endpoint = EndpointFactory.Build(EntityRequestType.Board_Read_CustomFields,
			                                     new Dictionary<string, object> {{"_id", OwnerId}});
			var newData = await JsonRepository.Execute<List<IJsonCustomFieldDefinition>>(Auth, endpoint, ct);

			Items.Clear();
			Items.AddRange(newData.Select(ja =>
				{
					var field = TrelloConfiguration.Cache.Find<CustomFieldDefinition>(a => a.Id == ja.Id) ?? new CustomFieldDefinition(ja, Auth);
					field.Json = ja;
					return field;
				}));
		}
	}
}