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

namespace Manatee.Trello
{
	/// <summary>
	/// A label.
	/// </summary>
	public class ReadOnlyLabel
	{
		/// <summary>
		/// Gets the label's color.
		/// </summary>
		public LabelColor Color { get; private set; }
		/// <summary>
		/// Gets the label's name.
		/// </summary>
		public string Name { get; internal set; }

		internal ReadOnlyLabel(LabelColor color, string name)
		{
			Color = color;
			Name = name;
		}
	}

	/// <summary>
	/// A label.
	/// </summary>
	public class Label : ReadOnlyLabel
	{
		/// <summary>
		/// Gets or sets the label's name.
		/// </summary>
		public new string Name
		{
			get { return base.Name; }
			set { base.Name = Name; }
		}

		internal Label(LabelColor color, string name)
			: base(color, name) {}
	}
}