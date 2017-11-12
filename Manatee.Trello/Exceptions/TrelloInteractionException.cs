using System;

namespace Manatee.Trello.Exceptions
{
	/// <summary>
	/// Thrown when Trello reports an error with a request.
	/// </summary>
	public class TrelloInteractionException : Exception
	{
		/// <summary>
		/// Creates a new instance of the TrelloInteractionException class.
		/// </summary>
		/// <param name="innerException">The exception which occurred during the call.</param>
		public TrelloInteractionException(Exception innerException)
			: base("Trello has reported an error with the request.", innerException) {}
		/// <summary>
		/// Creates a new instance of the TrelloInteractionException class.
		/// </summary>
		/// <param name="innerException">The exception which occurred during the call.</param>
		public TrelloInteractionException(string message, Exception innerException = null)
			: base($"Trello has reported an error with the request: {message}", innerException) {}
	}
}
