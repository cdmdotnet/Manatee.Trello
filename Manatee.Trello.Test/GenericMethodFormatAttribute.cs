using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using Moq;
using StoryQ.Formatting.Methods;

namespace Manatee.Trello.Test
{
	[AttributeUsage(AttributeTargets.Method)]
	public class GenericMethodFormatAttribute : MethodFormatAttribute
	{
		private readonly string _textFormat;
		private readonly Times _times;

		public GenericMethodFormatAttribute()
			: this(null) {}
		public GenericMethodFormatAttribute(string textFormat)
		{
			_textFormat = textFormat;
		}

		public override string Format(MethodInfo method, IEnumerable<string> parameters)
		{
			var generics = method.GetGenericArguments();
			var genericsList = string.Join<Type>(", ", generics);
			parameters = parameters ?? Enumerable.Empty<string>();
			var valuesList = parameters.Any() ? string.Join(", ", parameters) : null;
			if (_textFormat == null)
			{
				return valuesList != null
				       	? string.Format("{0} ({1}) [{2}]", UnCamel(method.Name), genericsList, valuesList)
				       	: string.Format("{0} ({1})", UnCamel(method.Name), genericsList);
			}
			return string.Format(_textFormat, genericsList, valuesList);
		}
	}
}