using System.Collections.Generic;
using System.Linq;
using System.Threading;
using Manatee.Trello.Internal.Validation;
using SynchronizationContext = Manatee.Trello.Internal.Synchronization.SynchronizationContext;

namespace Manatee.Trello.Internal
{
	internal class Field<T>
	{
		private readonly SynchronizationContext _context;
		private readonly List<IValidationRule<T>> _rules;
		private readonly string _property;

		public T Value
		{
			get { return CurrentValue; }
			set
			{
				if (Equals(CurrentValue, value)) return;
				Validate(value);
				_context.SetValue(_property, value, CancellationToken.None);
			}
		}
		private T CurrentValue => _context.GetValue<T>(_property);

		public Field(SynchronizationContext context, string propertyName)
		{
			_context = context;
			_property = propertyName;
			_rules = new List<IValidationRule<T>>();
		}

		public void AddRule(IValidationRule<T> rule)
		{
			_rules.Add(rule);
		}

		private void Validate(T value)
		{
			var errors = _rules.Select(r => r.Validate(CurrentValue, value)).Where(s => s != null).ToList();
			if (errors.Any())
				throw new ValidationException<T>(value, errors);
		}
	}
}