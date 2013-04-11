using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Manatee.Trello.Exceptions
{
	public class UsernameInUseException : Exception
	{
		public string Username { get; private set; }

		public UsernameInUseException(string username)
		{
			Username = Username;
		}
	}
}
