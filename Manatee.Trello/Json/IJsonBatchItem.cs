using System.Net;

namespace Manatee.Trello.Json
{
	/// <summary>
	/// Defines the JSON structure for a single item in a batch request.
	/// </summary>
	public interface IJsonBatchItem
	{
		/// <summary>
		/// Gets or sets the status code.
		/// </summary>
		HttpStatusCode StatusCode { get; set; }
		/// <summary>
		/// Gets or sets the entity's ID.
		/// </summary>
		string EntityId { get; set; }
		/// <summary>
		/// Gets or sets the JSON content as a string.
		/// </summary>
		string Content { get; set; }
		/// <summary>
		/// Gets or sets an error message.
		/// </summary>
		string Error { get; set; }
	}
}