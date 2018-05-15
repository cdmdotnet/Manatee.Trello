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
	/// Represents a checklist item.
	/// </summary>
	public class CheckItem : ICheckItem, IMergeJson<IJsonCheckItem>
	{
		/// <summary>
		/// Enumerates the data which can be pulled for check items.
		/// </summary>
		[Flags]
		public enum Fields
		{
			/// <summary>
			/// Indicates the State property should be populated.
			/// </summary>
			[Display(Description="state")]
			State = 1,
			/// <summary>
			/// Indicates the Name property should be populated.
			/// </summary>
			[Display(Description="name")]
			Name = 1 << 1,
			/// <summary>
			/// Indicates the Position property should be populated.
			/// </summary>
			[Display(Description="pos")]
			Position = 1 << 2
		}

		private readonly Field<CheckList> _checkList;
		private readonly Field<string> _name;
		private readonly Field<Position> _position;
		private readonly Field<CheckItemState?> _state;
		private readonly CheckItemContext _context;
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
				CheckItemContext.UpdateParameters();
			}
		}

		/// <summary>
		/// Gets or sets the checklist to which the item belongs.
		/// </summary>
		/// <remarks>
		/// Trello only supports moving a check item between lists on the same card.
		/// </remarks>
		public ICheckList CheckList
		{
			get { return _checkList.Value; }
			set { _checkList.Value = (CheckList) value; }
		}
		/// <summary>
		/// Gets the creation date of the checklist item.
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
		/// Gets or sets the checklist item's ID.
		/// </summary>
		public string Id { get; private set; }
		/// <summary>
		/// Gets or sets the checklist item's name.
		/// </summary>
		public string Name
		{
			get { return _name.Value; }
			set { _name.Value = value; }
		}
		/// <summary>
		/// Gets or sets the checklist item's position.
		/// </summary>
		public Position Position
		{
			get { return _position.Value; }
			set { _position.Value = value; }
		}
		/// <summary>
		/// Gets or sets the checklist item's state.
		/// </summary>
		public CheckItemState? State
		{
			get { return _state.Value; }
			set { _state.Value = value; }
		}

		internal IJsonCheckItem Json
		{
			get { return _context.Data; }
			set { _context.Merge(value); }
		}

		/// <summary>
		/// Raised when data on the checklist item is updated.
		/// </summary>
		public event Action<ICheckItem, IEnumerable<string>> Updated;

		static CheckItem()
		{
			DownloadedFields = (Fields)Enum.GetValues(typeof(Fields)).Cast<int>().Sum();
		}

		internal CheckItem(IJsonCheckItem json, string checkListId, TrelloAuthorization auth = null)
		{
			Id = json.Id;
			_context = new CheckItemContext(Id, checkListId, auth);

			_checkList = new Field<CheckList>(_context, nameof(CheckList));
			_checkList.AddRule(NotNullRule<CheckList>.Instance);
			_name = new Field<string>(_context, nameof(Name));
			_name.AddRule(NotNullOrWhiteSpaceRule.Instance);
			_position = new Field<Position>(_context, nameof(Position));
			_position.AddRule(NotNullRule<Position>.Instance);
			_position.AddRule(PositionRule.Instance);
			_state = new Field<CheckItemState?>(_context, nameof(State));
			_state.AddRule(NullableHasValueRule<CheckItemState>.Instance);
			_state.AddRule(EnumerationRule<CheckItemState?>.Instance);

			TrelloConfiguration.Cache.Add(this);

			_context.Merge(json);
			_context.Synchronized += Synchronized;
		}

		/// <summary>
		/// Deletes the checklist item.
		/// </summary>
		/// <param name="ct">(Optional) A cancellation token for async processing.</param>
		/// <remarks>
		/// This permanently deletes the checklist item from Trello's server, however, this object will remain in memory and all properties will remain accessible.
		/// </remarks>
		public async Task Delete(CancellationToken ct = default(CancellationToken))
		{
			await _context.Delete(ct);
			if (TrelloConfiguration.RemoveDeletedItemsFromCache)
				TrelloConfiguration.Cache.Remove(this);
		}

		/// <summary>
		/// Refreshes the checklist item data.
		/// </summary>
		/// <param name="force">Indicates that the refresh should ignore the value in <see cref="TrelloConfiguration.RefreshThrottle"/> and make the call to the API.</param>
		/// <param name="ct">(Optional) A cancellation token for async processing.</param>
		public async Task Refresh(bool force = false, CancellationToken ct = default(CancellationToken))
		{
			await _context.Synchronize(force, ct);
		}

		void IMergeJson<IJsonCheckItem>.Merge(IJsonCheckItem json, bool overwrite)
		{
			_context.Merge(json, overwrite);
		}

		/// <summary>Returns a string that represents the current object.</summary>
		/// <returns>A string that represents the current object.</returns>
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