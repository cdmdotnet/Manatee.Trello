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
 
	File Name:		ManateeNotificationData.cs
	Namespace:		Manatee.Trello.ManateeJson.Entities
	Class Name:		ManateeNotificationData
	Purpose:		Implements IJsonNotificationData for Manatee.Json.

***************************************************************************************/
using System.Collections.Generic;
using System.Linq;
using Manatee.Json;
using Manatee.Trello.Json;

namespace Manatee.Trello.ManateeJson.Entities
{
	internal class ManateeNotificationData : IJsonNotificationData
	{
		private JsonObject _obj;

		public object RawData
		{
			get { return _obj; }
			set { _obj = (JsonObject)value; }
		}

		public string TryGetString(params string[] path)
		{
			var obj = DrillDown(path);
			if (obj == null) return null;
			return obj.TryGetString(path.Last());
		}
		public double? TryGetNumber(params string[] path)
		{
			var obj = DrillDown(path);
			if (obj == null) return null;
			return obj.TryGetNumber(path.Last());
		}
		public bool? TryGetBoolean(params string[] path)
		{
			var obj = DrillDown(path);
			if (obj == null) return null;
			return obj.TryGetBoolean(path.Last());
		}

		private JsonObject DrillDown(IEnumerable<string> path)
		{
			var leadingPath = path.Take(path.Count() - 1);
			var obj = _obj;
			foreach (var key in leadingPath)
			{
				obj = obj.TryGetObject(key);
				if (obj == null)
					return null;
			}
			return obj;
		}
	}
}