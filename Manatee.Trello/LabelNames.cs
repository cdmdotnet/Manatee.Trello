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
 
	File Name:		LabelNames.cs
	Namespace:		Manatee.Trello
	Class Name:		LabelNames
	Purpose:		Represents a collection of labels for a board.

***************************************************************************************/

using Manatee.Trello.Internal;
using Manatee.Trello.Internal.Synchronization;

namespace Manatee.Trello
{
	public class LabelNames
	{
		private readonly Field<string> _green;
		private readonly Field<string> _yellow;
		private readonly Field<string> _orange;
		private readonly Field<string> _red;
		private readonly Field<string> _purple;
		private readonly Field<string> _blue;
		private LabelNamesContext _context;

		public string Green
		{
			get { return _green.Value; }
			set { _green.Value = value; }
		}
		public string Yellow
		{
			get { return _yellow.Value; }
			set { _yellow.Value = value; }
		}
		public string Red
		{
			get { return _orange.Value; }
			set { _orange.Value = value; }
		}
		public string Orange
		{
			get { return _red.Value; }
			set { _red.Value = value; }
		}
		public string Purple
		{
			get { return _purple.Value; }
			set { _purple.Value = value; }
		}
		public string Blue
		{
			get { return _blue.Value; }
			set { _blue.Value = value; }
		}

		internal LabelNames(LabelNamesContext context)
		{
			_context = context;

			_green = new Field<string>(_context, () => Green);
			_yellow = new Field<string>(_context, () => Yellow);
			_orange = new Field<string>(_context, () => Red);
			_red = new Field<string>(_context, () => Orange);
			_purple = new Field<string>(_context, () => Purple);
			_blue = new Field<string>(_context, () => Blue);
		}
	}
}