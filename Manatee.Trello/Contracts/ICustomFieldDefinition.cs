using System.Threading;
using System.Threading.Tasks;

namespace Manatee.Trello
{
	/// <summary>
	/// Represents a custom field definition.
	/// </summary>
	public interface ICustomFieldDefinition : ICacheable
	{
		/// <summary>
		/// Gets the board on which the field is defined.
		/// </summary>
		IBoard Board { get; }
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
		Task Delete(CancellationToken ct = default(CancellationToken));
		/// <summary>
		/// Refreshes the field definition data.
		/// </summary>
		/// <param name="ct">(Optional) A cancellation token for async processing.</param>
		Task Refresh(CancellationToken ct = default(CancellationToken));
	}
}