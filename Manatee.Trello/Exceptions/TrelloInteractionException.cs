using System;

namespace Manatee.Trello.Exceptions
{
	/// <summary>
	/// Raised when an error originates from Trello.
	/// </summary>
	public class TrelloInteractionException : Exception
	{
		/// <summary>
		/// Creates a new instance of the TrelloInteractionException class.
		/// </summary>
		/// <param name="innerException">The exception which occurred during the call.</param>
		public TrelloInteractionException(Exception innerException)
			: base("Trello has reported an error with the request.", innerException) {}
	}
}
