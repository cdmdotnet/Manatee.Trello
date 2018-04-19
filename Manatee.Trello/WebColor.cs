using System;
using System.Globalization;
using System.Text.RegularExpressions;

namespace Manatee.Trello
{
	/// <summary>
	/// Defines a color in the RGB space.
	/// </summary>
	public class WebColor
	{
		private static readonly Regex Pattern = new Regex(@"^#(?<Red>[0-9a-fA-F]{2})(?<Green>[0-9a-fA-F]{2})(?<Blue>[0-9a-fA-F]{2})$");

		/// <summary>
		/// Gets the red component.
		/// </summary>
		public ushort Red { get; }
		/// <summary>
		/// Gets the green component.
		/// </summary>
		public ushort Green { get; }
		/// <summary>
		/// Gets the blue component.
		/// </summary>
		public ushort Blue { get; }

		/// <summary>
		/// Creates a new instance of the <see cref="WebColor"/> class.
		/// </summary>
		/// <param name="red">The red component.</param>
		/// <param name="green">The green component.</param>
		/// <param name="blue">The blue component.</param>
		public WebColor(ushort red, ushort green, ushort blue)
		{
			Red = red;
			Green = green;
			Blue = blue;
		}
		/// <summary>
		/// Creates a new isntance of the <see cref="WebColor"/> class.
		/// </summary>
		/// <param name="serialized">A string representation of RGB values in the format "#RRGGBB".</param>
		public WebColor(string serialized)
		{
			var matches = Pattern.Matches(serialized);
			if (matches.Count == 0)
				throw new ArgumentException($"'{serialized}' is not a valid web color", nameof(serialized));

			Red = ushort.Parse(matches[0].Groups["Red"].Value, NumberStyles.HexNumber);
			Green = ushort.Parse(matches[0].Groups["Green"].Value, NumberStyles.HexNumber);
			Blue = ushort.Parse(matches[0].Groups["Blue"].Value, NumberStyles.HexNumber);
		}

		/// <summary>
		/// Returns an HTML-compatible color code.
		/// </summary>
		/// <returns>
		/// A string that represents the current object.
		/// </returns>
		public override string ToString()
		{
			return $"#{Red:X2}{Green:X2}{Blue:X2}";
		}
	}
}
