using System;
using Manatee.Trello.Contracts;
using Moq;

namespace Manatee.Trello.Moq
{
	public class WebhookMock<T> : Mock<Webhook<T>>
		where T : class, ICanWebhook
	{
		public WebhookMock()
			: base(string.Empty, null)
		{
		}
	}
}
