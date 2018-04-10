using System.Collections.Generic;
using Manatee.Trello.Json;

namespace Manatee.Trello.Internal.Synchronization
{
	internal class TokenPermissionContext : LinkedSynchronizationContext<IJsonTokenPermission>
	{
		static TokenPermissionContext()
		{
			Properties = new Dictionary<string, Property<IJsonTokenPermission>>
				{
					{
						"ModelType",
						new Property<IJsonTokenPermission, TokenModelType?>((d, a) => d.ModelType, (d, o) => d.ModelType = o)
					},
					{
						nameof(TokenPermission.CanRead),
						new Property<IJsonTokenPermission, bool?>((d, a) => d.Read, (d, o) => d.Read = o)
					},
					{
						nameof(TokenPermission.CanWrite),
						new Property<IJsonTokenPermission, bool?>((d, a) => d.Write, (d, o) => d.Write = o)
					},
				};
		}
		public TokenPermissionContext(TrelloAuthorization auth)
			: base(auth) {}
	}
}