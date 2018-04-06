using Manatee.Trello.Internal.Caching;
using Manatee.Trello.Json;

namespace Manatee.Trello
{
	public abstract class CustomField : ICacheable
	{
		protected readonly TrelloAuthorization _auth;

		public string Id => Json.Id;
		public CustomFieldDefinition Definition => Json.Definition.GetFromCache<CustomFieldDefinition>(_auth);

		internal IJsonCustomField Json { get; }

		protected CustomField(IJsonCustomField json, TrelloAuthorization auth)
		{
			_auth = auth;
			Json = json;

			TrelloConfiguration.Cache.Add(this);
		}
	}

	public abstract class CustomField<T> : CustomField
	{
		public abstract T Value { get; }

		protected CustomField(IJsonCustomField json, TrelloAuthorization auth)
			: base(json, auth)
		{
		}

		public override string ToString()
		{
			return $"{Definition} - {Value}";
		}
	}
}