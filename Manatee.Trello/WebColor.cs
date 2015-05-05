using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;

namespace Manatee.Trello
{
	public class WebColor
	{
		private static readonly Regex _pattern = new Regex(@"^#(?<Red>[0-9a-fA-F]{2})(?<Green>[0-9a-fA-F]{2})(?<Blue>[0-9a-fA-F]{2})$");

		public ushort Red { get; private set; }
		public ushort Green { get; private set; }
		public ushort Blue { get; private set; }

		public WebColor(ushort red, ushort green, ushort blue)
		{
			Red = red;
			Green = green;
			Blue = blue;
		}
		public WebColor(string serialized)
		{
			var matches = _pattern.Matches(serialized);
			if (matches.Count == 0)
				throw new ArgumentException("'{}' is not a valid web color", "serialized");

			Red = ushort.Parse(matches[0].Groups["Red"].Value, NumberStyles.HexNumber);
			Green = ushort.Parse(matches[0].Groups["Green"].Value, NumberStyles.HexNumber);
			Blue = ushort.Parse(matches[0].Groups["Blue"].Value, NumberStyles.HexNumber);
		}

		public override string ToString()
		{
			return string.Format("#{0:X}{1:X}{2:X}", Red, Green, Blue);
		}
	}
}
