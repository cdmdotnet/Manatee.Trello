using System;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using Manatee.Trello.Internal;
using Manatee.Trello.Internal.Synchronization;
using Manatee.Trello.Internal.Validation;
using Manatee.Trello.Json;

namespace Manatee.Trello
{
	/// <summary>
	/// A label.
	/// </summary>
	public class Label : ILabel
	{
		/// <summary>
		/// Enumerates the data which can be pulled for labels.
		/// </summary>
		[Flags]
		public enum Fields
		{
			/// <summary>
			/// Indicates the Board property should be populated.
			/// </summary>
			[Display(Description="idBoard")]
			Board = 1,
			/// <summary>
			/// Indicates the Color property should be populated.
			/// </summary>
			[Display(Description="color")]
			Color = 1 << 1,
			/// <summary>
			/// Indicates the Name property should be populated.
			/// </summary>
			[Display(Description="name")]
			Name = 1 << 2,
			/// <summary>
			/// Indicates the Uses property should be populated.
			/// </summary>
			[Display(Description="uses")]
			Uses = 1 << 3
		}

		private readonly Field<Board> _board;
		private readonly Field<LabelColor?> _color;
		private readonly Field<string> _name;
		private readonly Field<int?> _uses;
		private readonly LabelContext _context;
		private DateTime? _creation;

		/// <summary>
		/// Specifies which fields should be downloaded.
		/// </summary>
		public static Fields DownloadedFields { get; set; } = (Fields)Enum.GetValues(typeof(Fields)).Cast<int>().Sum();

		/// <summary>
		/// Gets the board on which the label is defined.
		/// </summary>
		public IBoard Board => _board.Value;
		/// <summary>
		/// Gets and sets the color.  Use null for no color.
		/// </summary>
		public LabelColor? Color
		{
			get { return _color.Value; }
			set { _color.Value = value; }
		}
		/// <summary>
		/// Gets the creation date of the label.
		/// </summary>
		public DateTime CreationDate
		{
			get
			{
				if (_creation == null)
					_creation = Id.ExtractCreationDate();
				return _creation.Value;
			}
		}
		/// <summary>
		/// Gets the label's ID.
		/// </summary>
		public string Id { get; }
		/// <summary>
		/// Gets and sets the label's name.
		/// </summary>
		public string Name
		{
			get { return _name.Value; }
			set { _name.Value = value; }
		}
		/// <summary>
		/// Gets the number of cards which use this label.
		/// </summary>
		public int? Uses => _uses.Value;

		internal IJsonLabel Json
		{
			get { return _context.Data; }
			set { _context.Merge(value); }
		}

		internal Label(IJsonLabel json, TrelloAuthorization auth)
		{
			Id = json.Id;
			_context = new LabelContext(Id, auth);
			_board = new Field<Board>(_context, nameof(Board));
			_color = new Field<LabelColor?>(_context, nameof(Color));
			_color.AddRule(EnumerationRule<LabelColor?>.Instance);
			_name = new Field<string>(_context, nameof(Name));
			_uses = new Field<int?>(_context, nameof(Uses));
			
			_context.Merge(json);

			TrelloConfiguration.Cache.Add(this);
		}

		/// <summary>
		/// Permanently deletes the label and all of its usages from Trello.
		/// </summary>
		/// <remarks>
		/// This instance will remain in memory and all properties will remain accessible.  Any cards that have the label assigned will update as normal.
		/// </remarks>
		public async Task Delete()
		{
			await _context.Delete();
			TrelloConfiguration.Cache.Remove(this);
		}
		/// <summary>
		/// Marks the label to be refreshed the next time data is accessed.
		/// </summary>
		public async Task Refresh()
		{
			await _context.Expire();
		}
		/// <summary>
		/// Returns the <see cref="Name"/> and <see cref="Color"/>.
		/// </summary>
		/// <returns>
		/// A string that represents the current object.
		/// </returns>
		public override string ToString()
		{
			if (Name.IsNullOrWhiteSpace() && !Color.HasValue)
				return string.Empty;
			return $"{Name} ({Color?.ToString() ?? "No color"})";
		}
	}
}