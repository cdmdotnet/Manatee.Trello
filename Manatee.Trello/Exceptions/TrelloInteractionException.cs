using System;

namespace Manatee.Trello.Exceptions
{
	public class TrelloInteractionException : Exception
	{
		public TrelloInteractionException(Exception innerException)
			: base("Trello has reported an error with the request.", innerException) {}
	}
}
