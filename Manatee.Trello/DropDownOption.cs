using Manatee.Trello.Contracts;
using Manatee.Trello.Internal.Caching;
using Manatee.Trello.Json;

namespace Manatee.Trello
{
	public class DropDownOption : ICacheable
	{
		private readonly TrelloAuthorization _auth;

		public string Id => Json.Id;
		public DropDownField Field => Json.Field.GetFromCache<DropDownField>(_auth);
		public string Text => Json.Text;
		public LabelColor? Color => Json.Color;
		public Position Position => Position.GetPosition(Json.Pos);

		internal IJsonCustomDropDownOption Json { get; }

		internal DropDownOption(IJsonCustomDropDownOption json, TrelloAuthorization auth)
		{
			_auth = auth;
			Json = json;

			TrelloConfiguration.Cache.Add(this);
		}

		public override string ToString()
		{
			return Text;
		}
	}
}