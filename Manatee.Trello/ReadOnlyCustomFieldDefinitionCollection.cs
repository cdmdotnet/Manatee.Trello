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
	public class ReadOnlyCustomFieldDefinitionCollection : ReadOnlyCollection<ICustomFieldDefinition>
	{
		/// <summary>
		/// Creates a new instance of the <see cref="ReadOnlyCustomFieldDefinitionCollection"/> class.
		/// </summary>
		/// <param name="getOwnerId"></param>
		/// <param name="auth"></param>
		internal ReadOnlyCustomFieldDefinitionCollection(Func<string> getOwnerId, TrelloAuthorization auth)
			: base(getOwnerId, auth)
		{
		}

		internal sealed override async Task PerformRefresh(CancellationToken ct)
		{
			var endpoint = EndpointFactory.Build(EntityRequestType.Board_Read_CustomFields,
			                                     new Dictionary<string, object> {{"_id", OwnerId}});
			var newData = await JsonRepository.Execute<List<IJsonCustomFieldDefinition>>(Auth, endpoint, ct, AdditionalParameters);

			Items.Clear();
			Items.AddRange(newData.Select(ja =>
				{
					var field = TrelloConfiguration.Cache.Find<CustomFieldDefinition>(ja.Id) ?? new CustomFieldDefinition(ja, Auth);
					field.Json = ja;
					return field;
				}));
		}
	}
}