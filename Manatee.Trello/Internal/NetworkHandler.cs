//	Source: http://blog.dotnetclr.com/archive/2007/09/24/Check-for-internet-connection---Method-2.aspx
//	Modified as follows:
//		- Elimiated redundant class nesting.
//		- Change access modifier to internal.

using System.Runtime.InteropServices;
using System.Security;

namespace Manatee.Trello.Internal
{
	/// <summary>
	/// Performs actions on the network
	/// </summary>
	[SuppressUnmanagedCodeSecurity]
	internal static class NetworkHandler
	{
		// Extern Library
		// UnManaged code - be careful.
		[DllImport("wininet.dll", CharSet = CharSet.Auto)]
		[return: MarshalAs(UnmanagedType.Bool)]
		private static extern bool InternetGetConnectedState(out int description, int reservedValue);

		/// <summary>
		/// Determines if there is an active connection on this computer
		/// </summary>
		/// <returns></returns>
		public static bool HasActiveConnection()
		{
			int desc;
			return InternetGetConnectedState(out desc, 0);
		}
	}
}