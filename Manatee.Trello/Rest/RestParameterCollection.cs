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
 
	File Name:		RestParameterCollection.cs
	Namespace:		Manatee.Trello.Rest
	Class Name:		RestParameterCollection
	Purpose:		Implements a dictionary to automatically concatenate a collection
					of properties into a RESTful parameter list.

***************************************************************************************/
using System.Collections.Generic;
using System.Linq;

namespace Manatee.Trello.Rest
{
	internal class RestParameterCollection : Dictionary<string, object>
	{
		public override string ToString()
		{
			return string.Join("&", this.Select(o => string.Format("{0}={1}", o.Key, o.Value)));
		}
	}
}
