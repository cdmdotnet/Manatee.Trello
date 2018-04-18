using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Manatee.Trello.Internal;
using Manatee.Trello.Internal.Synchronization;
using Manatee.Trello.Internal.Validation;
using Manatee.Trello.Json;

namespace Manatee.Trello
{
	/// <summary>
	/// Represents a sticker on a card.
	/// </summary>
	public class Sticker : ISticker, IMergeJson<IJsonSticker>
	{
		/// <summary>
		/// Enumerates the data which can be pulled for stickers.
		/// </summary>
		[Flags]
		public enum Fields
		{
			/// <summary>
			/// Indicates the Left property should be populated.
			/// </summary>
			[Display(Description="left")]
			Left = 1,
			/// <summary>
			/// Indicates the Name property should be populated.
			/// </summary>
			[Display(Description="image")]
			Name = 1 << 1,
			/// <summary>
			/// Indicates the previews will be downloaded.
			/// </summary>
			[Display(Description="imageScaled")]
			Previews = 1 << 2,
			/// <summary>
			/// Indicates the Rotation property should be populated.
			/// </summary>
			[Display(Description="rotate")]
			Rotation = 1 << 3,
			/// <summary>
			/// Indicates the Top property should be populated.
			/// </summary>
			[Display(Description="top")]
			Top = 1 << 4,
			/// <summary>
			/// Indicates the Url property should be populated.
			/// </summary>
			[Display(Description="url")]
			Url = 1 << 5,
			/// <summary>
			/// Indicates the ZIndex property should be populated.
			/// </summary>
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
		private static Fields _downloadedFields;

		/// <summary>
		/// Specifies which fields should be downloaded.
		/// </summary>
		public static Fields DownloadedFields
		{
			get { return _downloadedFields; }
			set
			{
				_downloadedFields = value;
				StickerContext.UpdateParameters();
			}
		}

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
		public IReadOnlyCollection<IImagePreview> Previews => _context.Previews;
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

		/// <summary>
		/// Raised when data on the attachment is updated.
		/// </summary>
		public event Action<ISticker, IEnumerable<string>> Updated;

		static Sticker()
		{
			DownloadedFields = (Fields)Enum.GetValues(typeof(Fields)).Cast<int>().Sum();
		}

		internal Sticker(IJsonSticker json, string ownerId, TrelloAuthorization auth)
		{
			Id = json.Id;
			_context = new StickerContext(Id, ownerId, auth);
			_context.Synchronized += Synchronized;

			_left = new Field<double?>(_context, nameof(Left));
			_left.AddRule(NullableHasValueRule<double>.Instance);
			_name = new Field<string>(_context, nameof(Name));
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
		/// Deletes the sticker.
		/// </summary>
		/// <param name="ct">(Optional) A cancellation token for async processing.</param>
		/// <remarks>
		/// This permanently deletes the sticker from Trello's server, however, this object will remain in memory and all properties will remain accessible.
		/// </remarks>
		public async Task Delete(CancellationToken ct = default(CancellationToken))
		{
			await _context.Delete(ct);
			if (TrelloConfiguration.RemoveDeletedItemsFromCache)
				TrelloConfiguration.Cache.Remove(this);
		}

		/// <summary>
		/// Refreshes the sticker data.
		/// </summary>
		/// <param name="ct">(Optional) A cancellation token for async processing.</param>
		public async Task Refresh(CancellationToken ct = default(CancellationToken))
		{
			await _context.Synchronize(ct);
		}

		void IMergeJson<IJsonSticker>.Merge(IJsonSticker json)
		{
			_context.Merge(json);
		}

		/// <summary>Returns a string that represents the current object.</summary>
		/// <returns>A string that represents the current object.</returns>
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