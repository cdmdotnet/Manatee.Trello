using System;
using System.Collections.Generic;
using System.Reflection;
using StoryQ.Formatting.Methods;

namespace Manatee.Trello.Test.FunctionalTests
{
	public class GenericMethodFormatAttribute : MethodFormatAttribute
	{
		private readonly string _textFormat;

		public GenericMethodFormatAttribute()
		{
			_textFormat = null;
		}
		public GenericMethodFormatAttribute(string textFormat)
		{
			_textFormat = textFormat;
		}

		public override string Format(MethodInfo method, IEnumerable<string> parameters)
		{
			var generics = method.GetGenericArguments();
			if (_textFormat == null)
			{
				var genericsList = string.Join<Type>(", ", generics);
				return string.Format("{0} ({1})", UnCamel(method.Name), genericsList);
			}
			return string.Format(_textFormat, generics);
		}
	}
}