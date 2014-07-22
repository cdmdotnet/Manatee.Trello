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
					return string.Format("Value must be between {0} and {1}.", Min, Max);
				return string.Format("Value must be greater than {0}.", Min);
			}
			if (Max.HasValue && newValue.Value.CompareTo(Max.Value) > 0)
				return string.Format("Value must be less than {0}.", Max);
			return null;
		}
	}
}