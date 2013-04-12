using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Manatee.Trello.Exceptions
{
	/// <summary>
	/// Thrown when an attempt is made to set a member's username to a username
	/// which already belongs to an existing member on Trello.
	/// </summary>
	public class UsernameInUseException : Exception
	{
		/// <summary>
		/// The username which is already in use.
		/// </summary>
		public string Username { get; private set; }

		/// <summary>
		/// Creates a new instance of the OrgNameInUseException
		/// </summary>
		/// <param name="username">The username which is in use.</param>
		public UsernameInUseException(string username)
		{
			Username = Username;
		}
	}
}
