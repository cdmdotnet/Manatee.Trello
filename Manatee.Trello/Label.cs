using System;
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
	/// A label.
	/// </summary>
	public class Label : ILabel, IMergeJson<IJsonLabel>
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
			[Display(Description="board")]
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
			[Obsolete("Trello no longer supports this feature.")]
			Uses = 1 << 3
		}

		private readonly Field<Board> _board;
		private readonly Field<LabelColor?> _color;
		private readonly Field<string> _name;
		private readonly LabelContext _context;
		private DateTime? _creation;
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
				LabelContext.UpdateParameters();
			}
		}

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
		[Obsolete("Trello no longer supports this feature.")]
		public int? Uses => null;

		internal IJsonLabel Json
		{
			get { return _context.Data; }
			set { _context.Merge(value); }
		}

		static Label()
		{
			DownloadedFields = (Fields) Enum.GetValues(typeof(Fields)).Cast<int>().Sum() &
			                   ~Fields.Board;
		}

		internal Label(IJsonLabel json, TrelloAuthorization auth)
		{
			Id = json.Id;
			_context = new LabelContext(Id, auth);
			_board = new Field<Board>(_context, nameof(Board));
			_color = new Field<LabelColor?>(_context, nameof(Color));
			_color.AddRule(EnumerationRule<LabelColor?>.Instance);
			_name = new Field<string>(_context, nameof(Name));
			
			_context.Merge(json);

			TrelloConfiguration.Cache.Add(this);
		}

		/// <summary>
		/// Deletes the label.  All usages of the label will also be removed.
		/// </summary>
		/// <param name="ct">(Optional) A cancellation token for async processing.</param>
		/// <remarks>
		/// This permanently deletes the label from Trello's server, however, this object will remain in memory and all properties will remain accessible.
		/// </remarks>
		public async Task Delete(CancellationToken ct = default(CancellationToken))
		{
			await _context.Delete(ct);
			if (TrelloConfiguration.RemoveDeletedItemsFromCache)
				TrelloConfiguration.Cache.Remove(this);
		}

		/// <summary>
		/// Refreshes the label data.
		/// </summary>
		/// <param name="force">Indicates that the refresh should ignore the value in <see cref="TrelloConfiguration.RefreshThrottle"/> and make the call to the API.</param>
		/// <param name="ct">(Optional) A cancellation token for async processing.</param>
		public async Task Refresh(bool force = false, CancellationToken ct = default(CancellationToken))
		{
			await _context.Synchronize(force, ct);
		}

		void IMergeJson<IJsonLabel>.Merge(IJsonLabel json, bool overwrite)
		{
			_context.Merge(json, overwrite);
		}

		/// <summary>Returns a string that represents the current object.</summary>
		/// <returns>A string that represents the current object.</returns>
		public override string ToString()
		{
			return Name.IsNullOrWhiteSpace()
				       ? $"({Color?.ToString() ?? "No color"})"
				       : $"{Name} ({Color?.ToString() ?? "No color"})";
		}
	}
}