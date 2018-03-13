using Manatee.Trello.Contracts;
using Manatee.Trello.Internal;
using Manatee.Trello.Internal.Synchronization;
using Manatee.Trello.Json;

namespace Manatee.Trello
{
	/// <summary>
	/// Represents a background image for a board.
	/// </summary>
	public class BoardBackground : ICacheable
	{
		private static BoardBackground _blue, _orange, _green, _red, _purple, _pink, _lime, _sky, _grey;

		private readonly Field<WebColor> _color;
		private readonly Field<WebColor> _bottomColor;
		private readonly Field<WebColor> _topColor;
		private readonly Field<string> _image;
		private readonly Field<bool?> _isTiled;
		private readonly Field<BoardBackgroundBrightness?> _brightness;
		private readonly BoardBackgroundContext _context;

		/// <summary>
		/// The standard blue board background.
		/// </summary>
		public static BoardBackground Blue => _blue ?? (_blue = new BoardBackground("blue"));
		/// <summary>
		/// The standard orange board background.
		/// </summary>
		public static BoardBackground Orange => _orange ?? (_orange = new BoardBackground("orange"));
		/// <summary>
		/// The standard green board background.
		/// </summary>
		public static BoardBackground Green => _green ?? (_green = new BoardBackground("green"));
		/// <summary>
		/// The standard red board background.
		/// </summary>
		public static BoardBackground Red => _red ?? (_red = new BoardBackground("red"));
		/// <summary>
		/// The standard purple board background.
		/// </summary>
		public static BoardBackground Purple => _purple ?? (_purple = new BoardBackground("purple"));
		/// <summary>
		/// The standard pink board background.
		/// </summary>
		public static BoardBackground Pink => _pink ?? (_pink = new BoardBackground("pink"));
		/// <summary>
		/// The standard bright green board background.
		/// </summary>
		public static BoardBackground Lime => _lime ?? (_lime = new BoardBackground("lime"));
		/// <summary>
		/// The standard light blue board background.
		/// </summary>
		public static BoardBackground Sky => _sky ?? (_sky = new BoardBackground("sky"));
		/// <summary>
		/// The standard grey board background.
		/// </summary>
		public static BoardBackground Grey => _grey ?? (_grey = new BoardBackground("grey"));

		/// <summary>
		/// Gets the bottom color of a gradient background.
		/// </summary>
		public WebColor BottomColor => _bottomColor.Value;
		/// <summary>
		/// Gets the brightness of the background.
		/// </summary>
		public BoardBackgroundBrightness? Brightness => _brightness.Value;
		/// <summary>
		/// Gets the color of a stock solid-color background.
		/// </summary>
		public WebColor Color => _color.Value;
		/// <summary>
		/// Gets the background's ID.
		/// </summary>
		public string Id { get; }
		/// <summary>
		/// Gets the image of a background.
		/// </summary>
		public string Image => _image.Value;
		/// <summary>
		/// Gets whether the image is tiled when displayed.
		/// </summary>
		public bool? IsTiled => _isTiled.Value;
		/// <summary>
		/// Gets a collections of scaled background images.
		/// </summary>
		public ReadOnlyBoardBackgroundScalesCollection ScaledImages { get; }
		/// <summary>
		/// Gets the top color of a gradient background.
		/// </summary>
		public WebColor TopColor => _topColor.Value;

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

			_brightness = new Field<BoardBackgroundBrightness?>(_context, nameof(Brightness));
			_color = new Field<WebColor>(_context, nameof(Color));
			_topColor = new Field<WebColor>(_context, nameof(TopColor));
			_bottomColor = new Field<WebColor>(_context, nameof(BottomColor));
			_image = new Field<string>(_context, nameof(Image));
			_isTiled = new Field<bool?>(_context, nameof(IsTiled));
			ScaledImages = new ReadOnlyBoardBackgroundScalesCollection(_context, auth);

			TrelloConfiguration.Cache.Add(this);
		}
		private BoardBackground(string id)
		{
			Id = id;
			_context = new BoardBackgroundContext(TrelloAuthorization.Default);
			Json.Id = id;

			_brightness = new Field<BoardBackgroundBrightness?>(_context, nameof(Brightness));
			_color = new Field<WebColor>(_context, nameof(Color));
			_topColor = new Field<WebColor>(_context, nameof(TopColor));
			_bottomColor = new Field<WebColor>(_context, nameof(BottomColor));
			_image = new Field<string>(_context, nameof(Image));
			_isTiled = new Field<bool?>(_context, nameof(IsTiled));
			ScaledImages = new ReadOnlyBoardBackgroundScalesCollection(_context, TrelloAuthorization.Default);

			TrelloConfiguration.Cache.Add(this);
		}
	}
}
