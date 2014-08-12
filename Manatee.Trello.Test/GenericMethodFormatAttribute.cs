using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using StoryQ.Formatting.Methods;

namespace Manatee.Trello.Test
{
	[AttributeUsage(AttributeTargets.Method)]
	public class GenericMethodFormatAttribute : MethodFormatAttribute
	{
		private readonly string _textFormat;

		public GenericMethodFormatAttribute()
			: this(null) { }
		public GenericMethodFormatAttribute(string textFormat)
		{
			_textFormat = textFormat;
		}

		public override string Format(MethodInfo method, IEnumerable<string> parameters)
		{
			var generics = method.GetGenericArguments();
			var genericsList = string.Join<Type>(", ", generics);
			var inputs = new List<object> { genericsList };
			return string.Format(_textFormat, inputs.ToArray());
		}
	}
}