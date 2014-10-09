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
 
	File Name:		CardAgingStyle.cs
	Namespace:		Manatee.Trello
	Class Name:		CardAgingStyle
	Purpose:		Enumerates the various styles of aging for the Card Aging power up.

***************************************************************************************/

using System.ComponentModel;

namespace Manatee.Trello
{
	public enum CardAgingStyle
	{
		/// <summary>
		/// Not recognized.  May have been created since the current version of this API.
		/// </summary>
		Unknown,
		[Description("regular")]
		Regular,
		[Description("pirate")]
		Pirate
	}
}