using System;

namespace Manatee.Trello.Internal.Validation
{
	internal class NumericRule<T> : IValidationRule<T?>
		where T : struct, IComparable<T>
	{
		public T? Min { get; set; }
		public T? Max { get; set; }

		public string Validate(T? oldValue, T? newValue)
		{
			if (!newValue.HasValue) return null;
			if (Min.HasValue && newValue.Value.CompareTo(Min.Value) < 0)
			{
				if (Max.HasValue && newValue.Value.CompareTo(Max.Value) > 0)
					return $"Value must be between {Min} and {Max}.";
				return $"Value must be greater than {Min}.";
			}
			if (Max.HasValue && newValue.Value.CompareTo(Max.Value) > 0)
				return $"Value must be less than {Max}.";
			return null;
		}
	}
}