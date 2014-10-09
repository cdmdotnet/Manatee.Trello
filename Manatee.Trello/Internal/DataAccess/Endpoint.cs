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
 
	File Name:		Endpoint.cs
	Namespace:		Manatee.Trello.Internal.DataAccess
	Class Name:		Endpoint
	Purpose:		Represents a series of URL segments which together represent
					a REST method in Trello's API.

***************************************************************************************/

using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Manatee.Trello.Rest;

namespace Manatee.Trello.Internal.DataAccess
{
	internal class Endpoint : IEnumerable<string>
	{
		private readonly List<string> _segments;

		public RestMethod Method { get; private set; }

		public Endpoint(RestMethod method, params string[] segments)
		{
			Method = method;
			_segments = segments.Where(s => !s.IsNullOrWhiteSpace()).ToList();
		}

		public void AddSegment(string segment)
		{
			_segments.Add(segment);
		}
		public void Resolve(string key, string variable)
		{
			_segments.Replace(key, variable);
		}
		public IEnumerator<string> GetEnumerator()
		{
			return _segments.GetEnumerator();
		}
		public override string ToString()
		{
			return _segments.Join("/");
		}
		IEnumerator IEnumerable.GetEnumerator()
		{
			return GetEnumerator();
		}
	}
}