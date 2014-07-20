/***************************************************************************************

	Copyright 2014 Greg Dennis

	   Licensed under the Apache License, Version 2.0 (the "License");
	   you may not use this file except in compliance with the License.
	   You may obtain a copy of the License at

		 http://www.apache.org/licenses/LICENSE-2.0

	   Unless required by applicable law or agreed to in writing, software
	   distributed under the License is distributed on an "AS IS" BASIS,
	   WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
	   See the License for the specific language governing permissions and
	   limitations under the License.
 
	File Name:		Field.cs
	Namespace:		Manatee.Trello.Internal
	Class Name:		Field<T>
	Purpose:		Serves as a backing field and handles synchronization
					and validation.

***************************************************************************************/

using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using Manatee.Trello.Internal.Synchronization;
using Manatee.Trello.Internal.Validation;

namespace Manatee.Trello.Internal
{
	internal class Field<T>
	{
		private readonly SynchronizationContext _context;
		private readonly List<IValidationRule<T>> _rules;
		private readonly string _property;

		public T Value
		{
			get
			{
				_context.Synchronize();
				return CurrentValue;
			}
			set
			{
				Validate(value);
				_context.SetValue(_property, value);
			}
		}
		private T CurrentValue { get { return _context.GetValue<T>(_property); } }

		public Field(SynchronizationContext context, Expression<Func<T>> property)
		{
			_context = context;
			_property = property.GetName();
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
				throw new Exception(string.Format("'{0}' is not a valid value:\n - {1}", value, errors.Join("\n - ")));
		}
	}
}