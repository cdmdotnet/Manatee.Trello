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
 
	File Name:		GeneralExtensions.cs
	Namespace:		Manatee.Trello.Internal
	Class Name:		GeneralExtensions
	Purpose:		A set of general extension methods used throughout the project.

***************************************************************************************/
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using Manatee.Trello.Exceptions;

namespace Manatee.Trello.Internal
{
	internal static class GeneralExtensions
	{
		public static string ToLowerString<T>(this T item)
		{
			return item.ToString().ToLower();
		}
		public static void Replace<T>(this List<T> list, T replace, T with)
		{
			var i = list.IndexOf(replace);
			if (i == -1) return;
			list[i] = with;
		}
		public static bool BeginsWith(this string str, string beginning)
		{
			return (beginning.Length > str.Length) || (str.Substring(0, beginning.Length) == beginning);
		}
		public static bool IsNullOrWhiteSpace(this string value)
		{
#if NET35 || NET35C
			return string.IsNullOrEmpty(value) || string.IsNullOrEmpty(value.Trim());
#elif NET4 || NET4C || NET45
			return string.IsNullOrWhiteSpace(value);
#endif
		}
		public static string Join(this IEnumerable<string> segments, string separator)
		{
#if NET35 || NET35C
			return string.Join(separator, segments.ToArray());
#elif NET4 || NET4C || NET45
			return string.Join(separator, segments);
#endif
		}
		// source: http://stackoverflow.com/questions/479410/enum-tostring
		public static string GetDescription<T>(this T enumerationValue)
			where T : struct
		{
			var type = enumerationValue.GetType();
			if (!type.IsEnum)
				throw new ArgumentException("EnumerationValue must be of Enum type", "enumerationValue");

			//Tries to find a DescriptionAttribute for a potential friendly name
			//for the enum
			var memberInfo = type.GetMember(enumerationValue.ToString());
			if (memberInfo.Length > 0)
			{
				object[] attrs = memberInfo[0].GetCustomAttributes(typeof (DescriptionAttribute), false);

				if (attrs.Length > 0)
				{
					//Pull out the description value
					return ((DescriptionAttribute) attrs[0]).Description;
				}
			}
			//If we have no description attribute, just return the ToString of the enum
			return enumerationValue.ToString();

		}
		public static string GetName<T>(this Expression<Func<T>> property)
		{
			var lambda = (LambdaExpression)property;

			MemberExpression memberExpression;
			var body = lambda.Body as UnaryExpression;
			if (body != null)
			{
				var unaryExpression = body;
				memberExpression = (MemberExpression)unaryExpression.Operand;
			}
			else
				memberExpression = (MemberExpression)lambda.Body;

			return memberExpression.Member.Name;
		}
		public static bool IsNotFoundError(this TrelloInteractionException e)
		{
			return e.InnerException != null && e.InnerException.Message.ToLower().Contains("not found");
		}
		public static string FlagsEnumToCommaList<T>(this T value)
		{
			return value.ToLowerString().Replace(" ", string.Empty);
		}
		public static bool In<T>(this T obj, params T[] values)
		{
			return values.Contains(obj);
		}
	}
}
