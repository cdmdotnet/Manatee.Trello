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
 
	File Name:		TokenModelType.cs
	Namespace:		Manatee.Trello
	Class Name:		TokenModelType
	Purpose:		Enumerates the model types to which a user token may
					grant access.

***************************************************************************************/

using System.ComponentModel;

namespace Manatee.Trello
{
	/// <summary>
	/// Enumerates the model types to which a user token may grant access.
	/// </summary>
	public enum TokenModelType
	{
		/// <summary>
		/// Assigned when the model type is not recognized.
		/// </summary>
		Unknown,
		/// <summary>
		/// Indicates the model is one or more Members.
		/// </summary>
		[Description("Member")]
		Member,
		/// <summary>
		/// Indicates the model is one or more Boards.
		/// </summary>
		[Description("Board")]
		Board,
		/// <summary>
		/// Indicates the model is one or more Organizations.
		/// </summary>
		[Description("Organization")]
		Organization
	}
}