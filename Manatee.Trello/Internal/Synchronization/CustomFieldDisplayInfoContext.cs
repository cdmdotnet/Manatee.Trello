using System.Collections.Generic;
using Manatee.Trello.Json;

namespace Manatee.Trello.Internal.Synchronization
{
	internal class CustomFieldDisplayInfoContext : LinkedSynchronizationContext<IJsonCustomFieldDisplayInfo>
	{
		static CustomFieldDisplayInfoContext()
		{
			Properties = new Dictionary<string, Property<IJsonCustomFieldDisplayInfo>>
				{
					{
						nameof(CustomFieldDisplayInfo.CardFront),
						new Property<IJsonCustomFieldDisplayInfo, bool?>((d, a) => d.CardFront,
						                                                 (d, o) => d.CardFront = o)
					}
				};
		}

		public CustomFieldDisplayInfoContext(TrelloAuthorization auth)
			: base(auth)
		{

		}
	}
}