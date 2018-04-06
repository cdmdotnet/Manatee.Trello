using Manatee.Trello.Internal;
using Manatee.Trello.Internal.Synchronization;

namespace Manatee.Trello
{
	/// <summary>
	/// Represents permissions granted by a token.
	/// </summary>
	public class TokenPermission : ITokenPermission
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