namespace Manatee.Trello.Json
{
	/// <summary>
	/// Defines methods required by the IRestClient to serialize an object to JSON.
	/// </summary>
	public interface ISerializer
	{
		/// <summary>
		/// Serializes an object to JSON.
		/// </summary>
		/// <param name="obj">The object to serialize.</param>
		/// <returns>An equivalent JSON string.</returns>
		string Serialize(object obj);
	}
}
