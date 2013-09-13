using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using StoryQ.Formatting.Methods;

namespace Manatee.Trello.Test
{
	public class ParameterizedMethodFormatAttribute : MethodFormatAttribute
	{
		private readonly string _textFormat;

		public ParameterizedMethodFormatAttribute(string textFormat)
		{
			_textFormat = textFormat;
		}

		public override string Format(MethodInfo method, IEnumerable<string> parameters)
		{
			return string.Format(_textFormat, parameters.Cast<object>().ToArray());
		}
	}
}