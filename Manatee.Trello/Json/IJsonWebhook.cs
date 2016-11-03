namespace Manatee.Trello.Json
{
	/// <summary>
	/// Defines the JSON structure for the Webhook object.
	/// </summary>
	public interface IJsonWebhook : IJsonCacheable
	{
		/// <summary>
		/// Gets or sets the description of the webhook.
		/// </summary>
		[JsonDeserialize]
		[JsonSerialize]
		string Description { get; set; }
		/// <summary>
		/// Gets or sets the ID of the entity which the webhook monitors.
		/// </summary>
		[JsonDeserialize]
		[JsonSerialize]
		string IdModel { get; set; }
		/// <summary>
		/// Gets or sets the URL which receives notification messages.
		/// </summary>
		[JsonDeserialize]
		[JsonSerialize]
		string CallbackUrl { get; set; }
		/// <summary>
		/// Gets or sets whether the webhook is active.
		/// </summary>
		[JsonDeserialize]
		[JsonSerialize]
		bool? Active { get; set; }
	}
}
