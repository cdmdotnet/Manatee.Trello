using System;
using System.Collections.Generic;
using Manatee.Trello.Contracts;

namespace Manatee.Trello
{
	/// <summary>
	/// Represents a webhook.
	/// </summary>
	/// <typeparam name="T">The type of object to which the webhook is attached.</typeparam>
	public interface IWebhook<T> where T : class, ICanWebhook
	{
		/// <summary>
		/// Gets or sets a callback URL for the webhook.
		/// </summary>
		string CallBackUrl { get; set; }

		/// <summary>
		/// Gets the creation date of the webhook.
		/// </summary>
		DateTime CreationDate { get; }

		/// <summary>
		/// Gets or sets a description for the webhook.
		/// </summary>
		string Description { get; set; }

		/// <summary>
		/// Gets the webhook's ID>
		/// </summary>
		string Id { get; }

		/// <summary>
		/// Gets or sets whether the webhook is active.
		/// </summary>
		bool? IsActive { get; set; }

		/// <summary>
		/// Gets or sets the webhook's target.
		/// </summary>
		T Target { get; set; }

		/// <summary>
		/// Raised when data on the webhook is updated.
		/// </summary>
		event Action<IWebhook<T>, IEnumerable<string>> Updated;

		/// <summary>
		/// Deletes the webhook.
		/// </summary>
		/// <remarks>
		/// This permanently deletes the card from Trello's server, however, this object will
		/// remain in memory and all properties will remain accessible.
		/// </remarks>
		void Delete();

		/// <summary>
		/// Marks the webhook to be refreshed the next time data is accessed.
		/// </summary>
		void Refresh();
	}
}