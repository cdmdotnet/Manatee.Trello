using System;

namespace Manatee.Trello
{
	public class LicenseException : Exception
	{
		public LicenseException()
		{
		}

		public LicenseException(string message) : base(message)
		{
		}

		public LicenseException(string message, Exception innerException) : base(message, innerException)
		{
		}
	}
}