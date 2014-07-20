using Manatee.Trello.Internal;
using Manatee.Trello.Internal.Synchronization;

namespace Manatee.Trello
{
	public class TokenPermission
	{
		private readonly Field<bool?> _canRead;
		private readonly Field<bool?> _canWrite; 
		private readonly TokenPermissionContext _context;

		public bool? CanRead { get { return _canRead.Value; } }
		public bool? CanWrite { get { return _canWrite.Value; } }

		internal TokenPermission(TokenPermissionContext context)
		{
			_context = context;

			_canRead = new Field<bool?>(_context, () => CanRead);
			_canWrite = new Field<bool?>(_context, () => CanWrite);
		}
	}
}