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