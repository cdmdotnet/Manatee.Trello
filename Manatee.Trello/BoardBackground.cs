using Manatee.Trello.Contracts;
using Manatee.Trello.Internal;
using Manatee.Trello.Internal.Synchronization;
using Manatee.Trello.Json;

namespace Manatee.Trello
{
	public class BoardBackground : ICacheable
	{
		private readonly Field<WebColor> _color;
		private readonly Field<string> _image;
		private readonly Field<bool?> _isTiled;
		private readonly BoardBackgroundContext _context;

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
		public ReadOnlyBoardBackgroundScalesCollection ScaledImages { get; private set; }
		/// <summary>
		/// Gets the background's ID.
		/// </summary>
		public string Id { get; private set; }

		internal IJsonBoardBackground Json
		{
			get { return _context.Data; }
			set { _context.Merge(value); }
		}

		internal BoardBackground(IJsonBoardBackground json, TrelloAuthorization auth)
		{
			Id = json.Id;
			_context = new BoardBackgroundContext(auth);
			_context.Merge(json);

			_color = new Field<WebColor>(_context, () => Color);
			_image = new Field<string>(_context, () => Image);
			_isTiled = new Field<bool?>(_context, () => IsTiled);
			ScaledImages = new ReadOnlyBoardBackgroundScalesCollection(_context, auth);

			TrelloConfiguration.Cache.Add(this);
		}
	}
}
