﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
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
		[Flags]
		public enum Fields
		{
			[Display(Description="left")]
			Left = 1,
			[Display(Description="image")]
			Name = 1 << 1,
			[Display(Description="imageScaled")]
			Previews = 1 << 2,
			[Display(Description="rotate")]
			Rotation = 1 << 3,
			[Display(Description="top")]
			Top = 1 << 4,
			[Display(Description="url")]
			Url = 1 << 5,
			[Display(Description="zIndex")]
			ZIndex = 1 << 6
		}

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

		public static Fields DownloadedFields { get; set; } = (Fields)Enum.GetValues(typeof(Fields)).Cast<int>().Sum();

		/// <summary>
		/// Gets the checklist's ID.
		/// </summary>
		public virtual string Id { get; private set; }
		/// <summary>
		/// Gets or sets the position of the left edge.
		/// </summary>
		public virtual double? Left
		{
			get { return _left.Value; }
			set { _left.Value = value; }
		}
		/// <summary>
		/// Gets the name of the sticker.
		/// </summary>
		public virtual string Name => _name.Value;
		/// <summary>
		/// Gets the collection of previews.
		/// </summary>
		public virtual ReadOnlyStickerPreviewCollection Previews { get; }
		/// <summary>
		/// Gets or sets the rotation.
		/// </summary>
		/// <remarks>
		/// Rotation is clockwise and in degrees.
		/// </remarks>
		public virtual int? Rotation
		{
			get { return _rotation.Value; }
			set { _rotation.Value = value; }
		}
		/// <summary>
		/// Gets or sets the position of the top edge.
		/// </summary>
		public virtual double? Top
		{
			get { return _top.Value; }
			set { _top.Value = value; }
		}
		/// <summary>
		/// Gets the URL for the sticker's image.
		/// </summary>
		public virtual string ImageUrl => _url.Value;
		/// <summary>
		/// Gets or sets the z-index.
		/// </summary>
		public virtual int? ZIndex
		{
			get { return _zIndex.Value; }
			set { _zIndex.Value = value; }
		}

		internal IJsonSticker Json
		{
			get { return _context.Data; }
			set { _context.Merge(value); }
		}

		/// <summary>
		/// Raised when data on the attachment is updated.
		/// </summary>
		public virtual event Action<Sticker, IEnumerable<string>> Updated;

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
		public virtual void Delete()
		{
			_context.Delete();
			TrelloConfiguration.Cache.Remove(this);
		}
		/// <summary>
		/// Marks the card to be refreshed the next time data is accessed.
		/// </summary>
		public virtual void Refresh()
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
			var handler = Updated;
			handler?.Invoke(this, properties);
		}
	}
}