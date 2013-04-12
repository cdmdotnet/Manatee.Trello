using System;

namespace Manatee.Trello.Exceptions
{
	/// <summary>
	/// Thrown when an attempt is made to set an organization's name to a name which
	/// already belongs to an existing organization on Trello.
	/// </summary>
	public class OrgNameInUseException : Exception
	{
		/// <summary>
		/// The name which is already in use.
		/// </summary>
		public string Name { get; private set; }

		/// <summary>
		/// Creates a new instance of the OrgNameInUseException
		/// </summary>
		/// <param name="name">The name which is in use.</param>
		public OrgNameInUseException(string name)
		{
			Name = name;
		}
	}
}