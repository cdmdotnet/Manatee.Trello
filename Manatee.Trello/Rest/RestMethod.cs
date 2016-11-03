namespace Manatee.Trello.Rest
{
	/// <summary>
	/// Enumerates the RESTful call methods required by TrelloService.
	/// </summary>
	public enum RestMethod
	{
		///<summary>
		/// Indicates an HTTP GET operation.
		///</summary>
		Get,
		///<summary>
		/// Indicates an HTTP PUT operation.
		///</summary>
		Put,
		///<summary>
		/// Indicates an HTTP POST operation.
		///</summary>
		Post,
		///<summary>
		/// Indicates an HTTP DELETE operation.
		///</summary>
		Delete
	}
}