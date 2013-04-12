using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Manatee.Trello.Exceptions
{
	public class ReadOnlyAccessException : Exception
	{
		public ReadOnlyAccessException()
			: base("A valid authorization token must be supplied to perform write operations.") {}
	}
}
