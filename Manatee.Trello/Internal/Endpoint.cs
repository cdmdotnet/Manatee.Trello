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
	Namespace:		Manatee.Trello.Internal
	Class Name:		Endpoint
	Purpose:		Represents a series of URL segments which together represent
					a REST method in Trello's API.

***************************************************************************************/
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Manatee.Trello.Rest;

namespace Manatee.Trello.Internal
{
	/// <summary>
	/// Represents a series of URL segments which together represent a REST
	/// method in Trello's API.
	/// </summary>
	/// <remarks>
	/// This class is only exposes for unit testing purposes.
	/// </remarks>
	public class Endpoint : IEnumerable<string>
	{
		private readonly List<string> _segments;

		/// <summary />
		public RestMethod Method { get; private set; }

		/// <summary />
		public Endpoint(RestMethod method, IEnumerable<string> segments)
		{
			Method = method;
			_segments = segments.ToList();
		}
		/// <summary />
		public Endpoint(RestMethod method, params string[] segments)
		{
			Method = method;
			_segments = segments.Where(s => !string.IsNullOrWhiteSpace(s)).ToList();
		}

		/// <summary />
		public void Resolve(string key, string variable)
		{
			_segments.Replace(key, variable);
		}

		/// <summary>
		/// Returns an enumerator that iterates through the collection.
		/// </summary>
		/// <returns>
		/// A <see cref="T:System.Collections.Generic.IEnumerator`1"/> that can be used to iterate through the collection.
		/// </returns>
		/// <filterpriority>1</filterpriority>
		public IEnumerator<string> GetEnumerator()
		{
			return _segments.GetEnumerator();
		}
		/// <summary>
		/// Returns a string that represents the current object.
		/// </summary>
		/// <returns>
		/// A string that represents the current object.
		/// </returns>
		/// <filterpriority>2</filterpriority>
		public override string ToString()
		{
			return string.Join("/", _segments);
		}
		/// <summary>
		/// Returns an enumerator that iterates through a collection.
		/// </summary>
		/// <returns>
		/// An <see cref="T:System.Collections.IEnumerator"/> object that can be used to iterate through the collection.
		/// </returns>
		/// <filterpriority>2</filterpriority>
		IEnumerator IEnumerable.GetEnumerator()
		{
			return GetEnumerator();
		}
	}
}