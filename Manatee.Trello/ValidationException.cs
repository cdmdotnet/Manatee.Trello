using System;
using System.Collections.Generic;

namespace Manatee.Trello
{
	/// <summary>
	/// Thrown when validation fails on a Trello object property.
	/// </summary>
	public class ValidationException<T> : Exception
	{
		/// <summary>
		/// Gets a collection of errors that occurred while validating the value.
		/// </summary>
		public IEnumerable<string> Errors { get; private set; }

		/// <summary>
		/// Creates a new instance of the <see cref="ValidationException{T}"/> object.
		/// </summary>
		/// <param name="value">The value that was passed.</param>
		/// <param name="errors">The reasons the value did not pass validation.</param>
		public ValidationException(T value, IEnumerable<string> errors)
			: base($"'{value}' is not a valid value.")
		{
			Errors = errors;
		}
	}
}