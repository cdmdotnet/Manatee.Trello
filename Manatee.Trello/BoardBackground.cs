using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Manatee.Trello.Contracts;
using Manatee.Trello.Internal;
using Manatee.Trello.Internal.Synchronization;
using Manatee.Trello.Json;

namespace Manatee.Trello
{
	public class BoardBackground : ICacheable
	{
		private readonly TrelloAuthorization _auth;
		private readonly Field<WebColor> _color;
		private readonly Field<string> _image;
		private readonly Field<bool?> _isTiled;
		private readonly BoardBackgroundContext _context;

		private string _id;

		public WebColor Color
		{
			get { return _color.Value; }
		}
		public string Image
		{
			get { return _image.Value; }
		}
		public bool? IsTiled
		{
			get { return _isTiled.Value; }
		}
		public BoardBackgroundScalesCollection ScaledImages { get; private set; }
		/// <summary>
		/// Gets the background's ID.
		/// </summary>
		public string Id { get; private set; }

		internal IJsonBoardBackground Json
		{
			get { return _context.Data; }
			set { _context.Merge(value); }
		}
		internal TrelloAuthorization Auth { get { return _auth; } }

#if IOS
		private Action<Board, IEnumerable<string>> _updatedInvoker;

		/// <summary>
		/// Raised when data on the board is updated.
		/// </summary>
		public event Action<Board, IEnumerable<string>> Updated
		{
			add { _updatedInvoker += value; }
			remove { _updatedInvoker -= value; }
		}
#else
		/// <summary>
		/// Raised when data on the board is updated.
		/// </summary>
		public event Action<BoardBackground, IEnumerable<string>> Updated;
#endif

		internal BoardBackground(IJsonBoardBackground json, TrelloAuthorization auth)
		{
			Id = json.Id;
			_context = new BoardBackgroundContext(auth);
		}

		/// <summary>
		/// Marks the board to be refreshed the next time data is accessed.
		/// </summary>
		public void Refresh()
		{
			_context.Expire();
		}

		private void Synchronized(IEnumerable<string> properties)
		{
			Id = _context.Data.Id;
#if IOS
			var handler = _updatedInvoker;
#else
			var handler = Updated;
#endif
			if (handler != null)
				handler(this, properties);
		}
	}
}
