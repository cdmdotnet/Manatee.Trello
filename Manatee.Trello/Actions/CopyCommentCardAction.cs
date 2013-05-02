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
 
	File Name:		CopyCommentCardAction.cs
	Namespace:		Manatee.Trello
	Class Name:		CopyCommentCardAction
	Purpose:		Indicates a comment was copied from another card.

***************************************************************************************/
namespace Manatee.Trello
{
	/// <summary>
	/// Indicates a comment was copied from another card.
	/// </summary>
	public class CopyCommentCardAction : Action
	{
		/// <summary>
		/// Creates a new instance of the CopyCommentCardAction class.
		/// </summary>
		/// <param name="action"></param>
		public CopyCommentCardAction(Action action)
		{
			VerifyNotExpired();
		}
	}
}