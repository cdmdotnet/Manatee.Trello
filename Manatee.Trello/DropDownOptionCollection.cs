using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Manatee.Trello.Internal.DataAccess;
using Manatee.Trello.Internal.Validation;
using Manatee.Trello.Json;

namespace Manatee.Trello
{
	public class DropDownOptionCollection : ReadOnlyDropDownOptionCollection,
	                                        IDropDownOptionCollection
	{
		internal DropDownOptionCollection(Func<string> getOwnerId, TrelloAuthorization auth)
			: base(getOwnerId, auth)
		{
		}

		public async Task<IDropDownOption> Add(string text, Position position, LabelColor? color = null, 
		                                       CancellationToken ct = default(CancellationToken))
		{
			var error = NotNullRule<string>.Instance.Validate(null, text);
			if (error != null)
				throw new ValidationException<string>(text, new[] {error});

			var json = TrelloConfiguration.JsonFactory.Create<IJsonCustomDropDownOption>();
			json.Color = color;
			json.Pos = Position.GetJson(position);
			json.Text = text;

			var endpoint = EndpointFactory.Build(EntityRequestType.CustomFieldDefinition_Write_AddOption,
			                                     new Dictionary<string, object>
				                                     {
					                                     {"_id", OwnerId}
				                                     });
			var newData = await JsonRepository.Execute(Auth, endpoint, json, ct);

			return new DropDownOption(newData, Auth);
		}
	}
}