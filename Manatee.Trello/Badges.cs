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
 
	File Name:		Badges2.cs
	Namespace:		Manatee.Trello
	Class Name:		Badges2
	Purpose:		Represents the badges on the card cover.

***************************************************************************************/

using System;
using Manatee.Trello.Internal;
using Manatee.Trello.Internal.Synchronization;

namespace Manatee.Trello
{
	public class Badges
	{
		private readonly Field<int?> _attachments;
		private readonly Field<int?> _checkItems;
		private readonly Field<int?> _checkItemsChecked;
		private readonly Field<int?> _comments;
		private readonly Field<DateTime?> _dueDate;
		private readonly Field<string> _fogBugz;
		private readonly Field<bool?> _hasDescription;
		private readonly Field<bool?> _hasVoted;
		private readonly Field<bool?> _isSubscribed;
		private readonly Field<int?> _votes;
		private readonly BadgesContext _context;

		public int? Attachments { get { return _attachments.Value; } }
		public int? CheckItems { get { return _checkItems.Value; } }
		public int? CheckItemsChecked { get { return _checkItemsChecked.Value; } }
		public int? Comments { get { return _comments.Value; } }
		public DateTime? DueDate { get { return _dueDate.Value; } }
		public string FogBugz { get { return _fogBugz.Value; } }
		public bool? HasDescription { get { return _hasDescription.Value; } }
		public bool? HasVoted { get { return _hasVoted.Value; } }
		public bool? IsSubscribed { get { return _isSubscribed.Value; } }
		public int? Votes { get { return _votes.Value; } }

		internal Badges(BadgesContext context)
		{
			_context = context;

			_attachments = new Field<int?>(_context, () => Attachments);
			_checkItems = new Field<int?>(_context, () => CheckItems);
			_checkItemsChecked = new Field<int?>(_context, () => CheckItemsChecked);
			_comments = new Field<int?>(_context, () => Comments);
			_dueDate = new Field<DateTime?>(_context, () => DueDate);
			_fogBugz = new Field<string>(_context, () => FogBugz);
			_hasDescription = new Field<bool?>(_context, () => HasDescription);
			_hasVoted = new Field<bool?>(_context, () => HasVoted);
			_isSubscribed = new Field<bool?>(_context, () => IsSubscribed);
			_votes = new Field<int?>(_context, () => Votes);
		}
	}
}