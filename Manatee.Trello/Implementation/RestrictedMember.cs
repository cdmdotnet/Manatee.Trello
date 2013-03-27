using System;
using Manatee.Json.Enumerations;

namespace Manatee.Trello.Implementation
{
	internal class RestrictedMember : Member, IEquatable<RestrictedMember>
	{
		public RestrictedMember() { }
		internal RestrictedMember(TrelloService svc, string id)
			: base(svc, id) { }

		public bool Equals(RestrictedMember other)
		{
			return base.Equals(this);
		}
		public override void FromJson(Manatee.Json.JsonValue json)
		{
			if (json == null) return;
			if (json.Type != JsonValueType.String) return;
			Id = json.String;
		}
	}
}