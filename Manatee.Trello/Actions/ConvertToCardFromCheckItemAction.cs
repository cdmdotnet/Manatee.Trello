/***************************************************************************************

	Copyright 2013 Little Crab Solutions

	   Licensed under the Apache License, Version 2.0 (the "License");
	   you may not use this file except in compliance with the License.
	   You may obtain a copy of the License at

		 http://www.apache.org/licenses/LICENSE-2.0

	   Unless required by applicable law or agreed to in writing, software
	   distributed under the License is distributed on an "AS IS" BASIS,
	   WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
	   See the License for the specific language governing permissions and
	   limitations under the License.
 
	File Name:		ConvertToCardFromCheckItemAction.cs
	Namespace:		Manatee.Trello
	Class Name:		ConvertToCardFromCheckItemAction
	Purpose:		Indicates a check list item was converted to a card.

***************************************************************************************/
namespace Manatee.Trello
{
	/// <summary>
	/// Indicates a check list item was converted to a card.
	/// </summary>
	public class ConvertToCardFromCheckItemAction : Action
	{
		/// <summary>
		/// Creates a new instance of the ConvertToCardFromCheckItemAction class.
		/// </summary>
		/// <param name="action"></param>
		public ConvertToCardFromCheckItemAction(Action action)
		{
			Refresh(action);
		}
	}
}