using System;
using System.Linq;
using System.Reflection;

namespace Manatee.Trello.Internal.Validation
{
	internal class EnumerationRule<T> : IValidationRule<T>
	{
		public static IValidationRule<T> Instance { get; }

		private readonly Type _enumType;
		private readonly bool _isNullable;

		static EnumerationRule()
		{
			Instance = new EnumerationRule<T>();
		}
		private EnumerationRule()
		{
			_enumType = typeof (T);
			if (_enumType.GetTypeInfo().IsGenericType)
			{
				if (_enumType.GetGenericTypeDefinition() != typeof(Nullable<>))
					throw new ArgumentException($"Type {_enumType} must be an enumeration or a nullable enumeration.");
				_enumType = _enumType.GetTypeInfo().GetGenericArguments().First();
				_isNullable = true;
			}
			if (!_enumType.GetTypeInfo().IsEnum)
				throw new ArgumentException($"Type {_enumType} must be an enumeration or a nullable enumeration.");
		}

		public string Validate(T oldValue, T newValue)
		{
			if (_isNullable && Equals(newValue, default(T))) return null;
			var validValues = Enum.GetValues(_enumType).Cast<T>();
			return !validValues.Contains(newValue)
					   ? $"{newValue} is not defined in type {_enumType.Name}."
				       : null;
		}
	}
}