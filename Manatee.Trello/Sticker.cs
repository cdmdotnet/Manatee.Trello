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
 
	File Name:		Sticker.cs
	Namespace:		Manatee.Trello
	Class Name:		Sticker
	Purpose:		Represents a sticker on a card.

***************************************************************************************/

using System;
using System.Collections.Generic;
using Manatee.Trello.Contracts;
using Manatee.Trello.Internal;
using Manatee.Trello.Internal.Synchronization;
using Manatee.Trello.Internal.Validation;
using Manatee.Trello.Json;

namespace Manatee.Trello
{
	/// <summary>
	/// Represents a sticker on a card.
	/// </summary>
	public class Sticker : ICacheable
	{
		/// <summary>
		/// Represents the stock Check sticker.
		/// </summary>
		public const string Check = "check";
		/// <summary>
		/// Represents the stock Heart sticker.
		/// </summary>
		public const string Heart = "heart";
		/// <summary>
		/// Represents the stock Warning sticker.
		/// </summary>
		public const string Warning = "warning";
		/// <summary>
		/// Represents the stock Clock sticker.
		/// </summary>
		public const string Clock = "clock";
		/// <summary>
		/// Represents the stock Smile sticker.
		/// </summary>
		public const string Smile = "smile";
		/// <summary>
		/// Represents the stock Laugh sticker.
		/// </summary>
		public const string Laugh = "laugh";
		/// <summary>
		/// Represents the stock Huh sticker.
		/// </summary>
		public const string Huh = "huh";
		/// <summary>
		/// Represents the stock Frown sticker.
		/// </summary>
		public const string Frown = "frown";
		/// <summary>
		/// Represents the stock ThumbsUp sticker.
		/// </summary>
		public const string ThumbsUp = "thumbsup";
		/// <summary>
		/// Represents the stock ThumbsDown sticker.
		/// </summary>
		public const string ThumbsDown = "thumbsdown";
		/// <summary>
		/// Represents the stock Star sticker.
		/// </summary>
		public const string Star = "star";
		/// <summary>
		/// Represents the stock RocketShip sticker.
		/// </summary>
		public const string RocketShip = "rocketship";

		private readonly Field<double?> _left;
		private readonly Field<string> _name;
		private readonly Field<int?> _rotation;
		private readonly Field<double?> _top;
		private readonly Field<string> _url;
		private readonly Field<int?> _zIndex;
		private readonly StickerContext _context;

		/// <summary>
		/// Gets the checklist's ID.
		/// </summary>
		public string Id { get; private set; }
		/// <summary>
		/// Gets or sets the position of the left edge.
		/// </summary>
		public double? Left
		{
			get { return _left.Value; }
			set { _left.Value = value; }
		}
		/// <summary>
		/// Gets the name of the sticker.
		/// </summary>
		public string Name => _name.Value;
		/// <summary>
		/// Gets the collection of previews.
		/// </summary>
		public ReadOnlyStickerPreviewCollection Previews { get; }
		/// <summary>
		/// Gets or sets the rotation.
		/// </summary>
		/// <remarks>
		/// Rotation is clockwise and in degrees.
		/// </remarks>
		public int? Rotation
		{
			get { return _rotation.Value; }
			set { _rotation.Value = value; }
		}
		/// <summary>
		/// Gets or sets the position of the top edge.
		/// </summary>
		public double? Top
		{
			get { return _top.Value; }
			set { _top.Value = value; }
		}
		/// <summary>
		/// Gets the URL for the sticker's image.
		/// </summary>
		public string ImageUrl => _url.Value;
		/// <summary>
		/// Gets or sets the z-index.
		/// </summary>
		public int? ZIndex
		{
			get { return _zIndex.Value; }
			set { _zIndex.Value = value; }
		}

		internal IJsonSticker Json
		{
			get { return _context.Data; }
			set { _context.Merge(value); }
		}

#if IOS
		private Action<Sticker, IEnumerable<string>> _updatedInvoker;

		/// <summary>
		/// Raised when data on the attachment is updated.
		/// </summary>
		public event Action<Sticker, IEnumerable<string>> Updated
		{
			add { _updatedInvoker += value; }
			remove { _updatedInvoker -= value; }
		}
#else
		/// <summary>
		/// Raised when data on the attachment is updated.
		/// </summary>
		public event Action<Sticker, IEnumerable<string>> Updated;
#endif

		internal Sticker(IJsonSticker json, string ownerId, TrelloAuthorization auth)
		{
			Id = json.Id;
			_context = new StickerContext(Id, ownerId, auth);
			_context.Synchronized += Synchronized;

			_left = new Field<double?>(_context, nameof(Left));
			_left.AddRule(NullableHasValueRule<double>.Instance);
			_name = new Field<string>(_context, nameof(Name));
			Previews = new ReadOnlyStickerPreviewCollection(_context, auth);
			_rotation = new Field<int?>(_context, nameof(Rotation));
			_rotation.AddRule(NullableHasValueRule<int>.Instance);
			_rotation.AddRule(new NumericRule<int> {Min = 0, Max = 359});
			_top = new Field<double?>(_context, nameof(Top));
			_top.AddRule(NullableHasValueRule<double>.Instance);
			_url = new Field<string>(_context, nameof(ImageUrl));
			_zIndex = new Field<int?>(_context, nameof(ZIndex));
			_zIndex.AddRule(NullableHasValueRule<int>.Instance);

			_context.Merge(json);
			TrelloConfiguration.Cache.Add(this);
		}
		/// <summary>
		/// Deletes the card.
		/// </summary>
		/// <remarks>
		/// This permanently deletes the card from Trello's server, however, this object will
		/// remain in memory and all properties will remain accessible.
		/// </remarks>
		public void Delete()
		{
			_context.Delete();
			TrelloConfiguration.Cache.Remove(this);
		}
		/// <summary>
		/// Marks the card to be refreshed the next time data is accessed.
		/// </summary>
		public void Refresh()
		{
			_context.Expire();
		}
		/// <summary>
		/// Returns a string that represents the current object.
		/// </summary>
		/// <returns>
		/// A string that represents the current object.
		/// </returns>
		/// <filterpriority>2</filterpriority>
		public override string ToString()
		{
			return Name;
		}

		private void Synchronized(IEnumerable<string> properties)
		{
			Id = _context.Data.Id;
#if IOS
			var handler = _updatedInvoker;
#else
			var handler = Updated;
#endif
			handler?.Invoke(this, properties);
		}
	}
}