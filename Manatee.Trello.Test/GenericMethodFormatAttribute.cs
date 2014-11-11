using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
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
			var genericsList = string.Join(", ", generics.Select(CSharpName));
			var inputs = new List<object> { genericsList };
			return string.Format(_textFormat, inputs.ToArray());
		}

		private static string CSharpName(Type type)
		{
			var sb = new StringBuilder();
			var name = type.Name;
			if (!type.IsGenericType)
				return name;
			sb.Append(name.Substring(0, name.IndexOf('`')));
			sb.Append("<");
			sb.Append(string.Join(", ", type.GetGenericArguments()
											.Select(CSharpName)));
			sb.Append(">");
			return sb.ToString();
		}
	}
}