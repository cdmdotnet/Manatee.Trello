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
 
	File Name:		UpdateCardIdListAction.cs
	Namespace:		Manatee.Trello
	Class Name:		UpdateCardIdListAction
	Purpose:		Indicates a card was moved from one list to another.

***************************************************************************************/
namespace Manatee.Trello
{
	/// <summary>
	/// Indicates a card was moved from one list to another.
	/// </summary>
	public class UpdateCardIdListAction : Action
	{
		/// <summary>
		/// Creates a new instance of the UpdateCardIdListAction class.
		/// </summary>
		/// <param name="action"></param>
		public UpdateCardIdListAction(Action action)
		{
			VerifyNotExpired();
		}
	}
}