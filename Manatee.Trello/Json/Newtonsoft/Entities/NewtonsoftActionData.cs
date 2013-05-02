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
 
	File Name:		NewtonsoftActionData.cs
	Namespace:		Manatee.Trello.Json.Newtonsoft.Entities
	Class Name:		NewtonsoftActionData
	Purpose:		Implements IJsonActionData for Newtonsoft's Json.Net.

***************************************************************************************/
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Newtonsoft.Json.Linq;

namespace Manatee.Trello.Json.Newtonsoft.Entities
{
	internal class NewtonsoftActionData : IJsonActionData
	{
		private JObject _obj;

		public object RawData
		{
			get { return _obj; }
			set { _obj = (JObject) value; }
		}
		public string TryGetString(params string[] path)
		{
			var obj = DrillDown(path);
			if (obj == null) return null;
			var last = obj[path.Last()];
			return (last == null) ? null : last.Value<string>();
		}
		public double? TryGetNumber(params string[] path)
		{
			var obj = DrillDown(path);
			if (obj == null) return null;
			var last = obj[path.Last()];
			return (last == null) ? null : last.Value<double?>();
		}
		public bool? TryGetBoolean(params string[] path)
		{
			var obj = DrillDown(path);
			if (obj == null) return null;
			var last = obj[path.Last()];
			return (last == null) ? null : last.Value<bool?>();
		}

		private JObject DrillDown(IEnumerable<string> path)
		{
			var leadingPath = path.Take(path.Count() - 1);
			var obj = _obj;
			foreach (var key in leadingPath)
			{
				obj = obj[key] as JObject;
				if (obj == null)
					return null;
			}
			return obj;
		}
	}
}
