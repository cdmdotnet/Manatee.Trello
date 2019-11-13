using System;
using System.Threading;
using System.Threading.Tasks;

namespace Manatee.Trello
{
	/// <summary>
	/// Represents a custom field definition.
	/// </summary>
	public interface ICustomFieldDefinition : ICacheable, IRefreshable
	{
		/// <summary>
		/// Gets the board on which the field is defined.
		/// </summary>
		IBoard Board { get; }

		/// <summary>
		/// Gets display information for the custom field.
		/// </summary>
		ICustomFieldDisplayInfo DisplayInfo { get; }

		/// <summary>
		/// Gets an identifier that groups fields across boards.
		/// </summary>
		string FieldGroup { get; }
		/// <summary>
		/// Gets or sets the name of the field.
		/// </summary>
		string Name { get; set; }
		/// <summary>
		/// Gets or sets the position of the field.
		/// </summary>
		Position Position { get; set; }
		/// <summary>
		/// Gets the data type of the field.
		/// </summary>
		CustomFieldType? Type { get; }
		/// <summary>
		/// Gets drop down options, if applicable.
		/// </summary>
		IDropDownOptionCollection Options { get; }

		/// <summary>
		/// Deletes the field definition.
		/// </summary>
		/// <param name="ct">(Optional) A cancellation token for async processing.</param>
		Task Delete(CancellationToken ct = default);

		/// <summary>
		/// Sets a value for a custom field on a card.
		/// </summary>
		/// <param name="card">The card on which to set the value.</param>
		/// <param name="value">The vaue to set.</param>
		/// <param name="ct">(Optional) A cancellation token for async processing.</param>
		/// <returns>The custom field instance.</returns>
		Task<ICustomField<double?>> SetValueForCard(ICard card, double? value,
		                                            CancellationToken ct = default);

		/// <summary>
		/// Sets a value for a custom field on a card.
		/// </summary>
		/// <param name="card">The card on which to set the value.</param>
		/// <param name="value">The vaue to set.</param>
		/// <param name="ct">(Optional) A cancellation token for async processing.</param>
		/// <returns>The custom field instance.</returns>
		Task<ICustomField<bool?>> SetValueForCard(ICard card, bool? value,
		                                          CancellationToken ct = default);

		/// <summary>
		/// Sets a value for a custom field on a card.
		/// </summary>
		/// <param name="card">The card on which to set the value.</param>
		/// <param name="value">The vaue to set.</param>
		/// <param name="ct">(Optional) A cancellation token for async processing.</param>
		/// <returns>The custom field instance.</returns>
		Task<ICustomField<string>> SetValueForCard(ICard card, string value,
		                                           CancellationToken ct = default);

		/// <summary>
		/// Sets a value for a custom field on a card.
		/// </summary>
		/// <param name="card">The card on which to set the value.</param>
		/// <param name="value">The vaue to set.</param>
		/// <param name="ct">(Optional) A cancellation token for async processing.</param>
		/// <returns>The custom field instance.</returns>
		Task<ICustomField<IDropDownOption>> SetValueForCard(ICard card, IDropDownOption value,
		                                                    CancellationToken ct = default);

		/// <summary>
		/// Sets a value for a custom field on a card.
		/// </summary>
		/// <param name="card">The card on which to set the value.</param>
		/// <param name="value">The vaue to set.</param>
		/// <param name="ct">(Optional) A cancellation token for async processing.</param>
		/// <returns>The custom field instance.</returns>
		Task<ICustomField<DateTime?>> SetValueForCard(ICard card, DateTime? value,
		                                              CancellationToken ct = default);
	}
}