using System;
using System.Collections.Generic;

namespace Manatee.Trello
{
	/// <summary>
	/// Provides a base for <see cref="ICustomField{T}"/>.
	/// </summary>
	public interface ICustomField : ICacheable, IRefreshable
	{
		/// <summary>
		/// Gets the custom field definition.
		/// </summary>
		ICustomFieldDefinition Definition { get; }

		/// <summary>
		/// Raised when data on the custom field is updated.
		/// </summary>
		event Action<ICustomField, IEnumerable<string>> Updated;
	}

	/// <summary>
	/// Represents a custom field instance.
	/// </summary>
	/// <typeparam name="T">The type of data in the field.</typeparam>
	public interface ICustomField<T> : ICustomField
	{
		/// <summary>
		/// Gets or sets the value.
		/// </summary>
		T Value { get; set; }
	}
}