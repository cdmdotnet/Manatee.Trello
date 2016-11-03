namespace Manatee.Trello.Json
{
	/// <summary>
	/// Defines the JSON structure for the WebhookNotification object.
	/// </summary>
	public interface IJsonWebhookNotification
	{
		/// <summary>
		/// Gets or sets the action associated with the notification.
		/// </summary>
		[JsonDeserialize]
		IJsonAction Action { get; set; }
	}
}