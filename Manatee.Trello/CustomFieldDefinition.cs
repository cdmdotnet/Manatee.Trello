using System.Collections.Generic;
using System.Linq;
using Manatee.Trello.Internal.Caching;
using Manatee.Trello.Json;

namespace Manatee.Trello
{
	public class CustomFieldDefinition : ICustomFieldDefinition
	{
		private readonly TrelloAuthorization _auth;

		public string Id => Json.Id;
		public Board Board => Json.Board.GetFromCache<Board, IJsonBoard>(_auth);
		public string FieldGroup => Json.FieldGroup;
		public string Name => Json.Name;
		public Position Position => Position.GetPosition(Json.Pos);
		public CustomFieldType? Type => Json.Type;
		public IEnumerable<DropDownOption> Options => Json.Options?.Select(o => o.GetFromCache<DropDownOption>(_auth));

		internal IJsonCustomFieldDefinition Json { get; set; }

		internal CustomFieldDefinition(IJsonCustomFieldDefinition json, TrelloAuthorization auth)
		{
			_auth = auth;
			Json = json;

			TrelloConfiguration.Cache.Add(this);

			// we need to enumerate the collection to cache all of the values
			// ReSharper disable once ReturnValueOfPureMethodIsNotUsed
			Options?.ToList();
		}

		public override string ToString()
		{
			return Name;
		}
	}
}
