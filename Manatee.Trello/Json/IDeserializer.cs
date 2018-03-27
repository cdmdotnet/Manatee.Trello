using Manatee.Trello.Rest;

namespace Manatee.Trello.Json
{
	/// <summary>
	/// Defines methods required by the IRestClient to deserialize a response from JSON to an object.
	/// </summary>
	public interface IDeserializer
	{
		/// <summary>
		/// Attempts to deserialize a RESTful response to the indicated type.
		/// </summary>
		/// <typeparam name="T">The type of object expected.</typeparam>
		/// <param name="response">The response object which contains the JSON to deserialize.</param>
		/// <returns>The requested object, if JSON is valid; null otherwise.</returns>
		T Deserialize<T>(IRestResponse<T> response);
		/// <summary>
		/// Attempts to deserialize a RESTful response to the indicated type.
		/// </summary>
		/// <typeparam name="T">The type of object expected.</typeparam>
		/// <param name="content">A string which contains the JSON to deserialize.</param>
		/// <returns>The requested object, if JSON is valid; null otherwise.</returns>
		T Deserialize<T>(string content);
	}
}