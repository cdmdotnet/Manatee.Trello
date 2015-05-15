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
 
	File Name:		BoardBackground.cs
	Namespace:		Manatee.Trello
	Class Name:		BoardBackground
	Purpose:		Represents a background image for a board.

***************************************************************************************/
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
		private readonly Field<WebColor> _color;
		private readonly Field<string> _image;
		private readonly Field<bool?> _isTiled;
		private readonly BoardBackgroundContext _context;

		/// <summary>
		/// Gets the color of a stock solid-color background.
		/// </summary>
		public WebColor Color
		{
			get { return _color.Value; }
		}
		/// <summary>
		/// Gets the background's ID.
		/// </summary>
		public string Id { get; private set; }
		/// <summary>
		/// Gets the image of a background.
		/// </summary>
		public string Image
		{
			get { return _image.Value; }
		}
		/// <summary>
		/// Gets whether the image is tiled when displayed.
		/// </summary>
		public bool? IsTiled
		{
			get { return _isTiled.Value; }
		}
		/// <summary>
		/// Gets a collections of scaled background images.
		/// </summary>
		public ReadOnlyBoardBackgroundScalesCollection ScaledImages { get; private set; }

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
