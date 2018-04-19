using System;

namespace Manatee.Trello
{
	/// <summary>
	/// Thrown when an invalid license is registered.
	/// </summary>
	public class LicenseException : Exception
	{
		internal LicenseException(string message)
			: base(message)
		{
		}
	}
}