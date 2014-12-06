/***************************************************************************************

	Copyright 2014 Greg Dennis

	   Licensed under the Apache License, Version 2.0 (the "License");
	   you may not use this file except in compliance with the License.
	   You may obtain a copy of the License at

		 http://www.apache.org/licenses/LICENSE-2.0

	   Unless required by applicable law or agreed to in writing, software
	   distributed under the License is distributed on an "AS IS" BASIS,
	   WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
	   See the License for the specific language governing permissions and
	   limitations under the License.
 
	File Name:		Label.cs
	Namespace:		Manatee.Trello
	Class Name:		Label
	Purpose:		Represents a label.

***************************************************************************************/

using Manatee.Trello.Contracts;
using Manatee.Trello.Internal;
using Manatee.Trello.Internal.Synchronization;
using Manatee.Trello.Internal.Validation;
using Manatee.Trello.Json;

namespace Manatee.Trello
{
	/// <summary>
	/// A label.
	/// </summary>
	public class Label : ICacheable
	{
		private readonly Field<Board> _board;
		private readonly Field<LabelColor?> _color;
		private readonly Field<string> _name;
		private readonly Field<int?> _uses;
		private readonly LabelContext _context;

		/// <summary>
		/// Gets the <see cref="Board"/> on which the label is defined.
		/// </summary>
		public Board Board { get { return _board.Value; } }
		/// <summary>
		/// Gets and sets the color.  Use null for no color.
		/// </summary>
		public LabelColor? Color
		{
			get { return _color.Value; }
			set { _color.Value = value; }
		}
		/// <summary>
		/// Gets the label's ID.
		/// </summary>
		public string Id { get; private set; }
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
		public int? Uses { get { return _uses.Value; } }

		internal IJsonLabel Json
		{
			get { return _context.Data; }
			set { _context.Merge(value); }
		}

		internal Label(IJsonLabel json)
		{
			Id = json.Id;
			_context = new LabelContext(Id);
			_board = new Field<Board>(_context, () => Board);
			_color = new Field<LabelColor?>(_context, () => Color);
			_color.AddRule(EnumerationRule<LabelColor?>.Instance);
			_name = new Field<string>(_context, () => Name);
			_uses = new Field<int?>(_context, () => Uses);
			
			_context.Merge(json);
		}

		/// <summary>
		/// Deletes the label.  All usages of the label will also be removed.
		/// </summary>
		/// <remarks>
		/// This permanently deletes the label from Trello's server, however, this object will
		/// remain in memory and all properties will remain accessible.
		/// </remarks>
		public void Delete()
		{
			_context.Delete();
			TrelloConfiguration.Cache.Remove(this);
		}
		/// <summary>
		/// Marks the label to be refreshed the next time data is accessed.
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
			if (Name.IsNullOrWhiteSpace() && !Color.HasValue)
				return string.Empty;
			return string.Format("{0} ({1})", Name, Color.HasValue ? Color.Value.ToString() : "No color");
		}
	}
}