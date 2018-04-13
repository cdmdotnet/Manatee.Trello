namespace Manatee.Trello.Rest
{
	/// <summary>
	/// Defines properties and methods required to make RESTful requests.
	/// </summary>
	public interface IRestRequest
	{
		/// <summary>
		/// Gets and sets the method to be used in the call.
		/// </summary>
		RestMethod Method { get; set; }
		/// <summary>
		/// Gets the URI enpoint for the request.
		/// </summary>
		string Resource { get; }
		/// <summary>
		/// Explicitly adds a parameter to the request.
		/// </summary>
		/// <param name="name">The name.</param>
		/// <param name="value">The value.</param>
		void AddParameter(string name, object value);
		/// <summary>
		/// Adds a body to the request.
		/// </summary>
		/// <param name="body">The body.</param>
		void AddBody(object body);
	}
}