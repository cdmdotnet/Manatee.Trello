/***************************************************************************************

	Copyright 2015 Greg Dennis

	   Licensed under the Apache License, Version 2.0 (the "License");
	   you may not use this file except in compliance with the License.
	   You may obtain a copy of the License at

		 http://www.apache.org/licenses/LICENSE-2.0

	   Unless required by applicable law or agreed to in writing, software
	   distributed under the License is distributed on an "AS IS" BASIS,
	   WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
	   See the License for the specific language governing permissions and
	   limitations under the License.
 
	File Name:		TokenPermission.cs
	Namespace:		Manatee.Trello
	Class Name:		TokenPermission
	Purpose:		Represents permissions granted by a token.

***************************************************************************************/
using Manatee.Trello.Internal;
using Manatee.Trello.Internal.Synchronization;

namespace Manatee.Trello
{
	/// <summary>
	/// Represents permissions granted by a token.
	/// </summary>
	public class TokenPermission
	{
		private readonly Field<bool?> _canRead;
		private readonly Field<bool?> _canWrite; 
		private readonly TokenPermissionContext _context;

		/// <summary>
		/// Gets whether a token can read values.
		/// </summary>
		public bool? CanRead => _canRead.Value;
		/// <summary>
		/// Gets whether a token can write values.
		/// </summary>
		public bool? CanWrite => _canWrite.Value;

		internal TokenPermission(TokenPermissionContext context)
		{
			_context = context;

			_canRead = new Field<bool?>(_context, nameof(CanRead));
			_canWrite = new Field<bool?>(_context, nameof(CanWrite));
		}
	}
}