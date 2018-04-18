using System.Threading;
using System.Threading.Tasks;

namespace Manatee.Trello
{
	/// <summary>
	/// Represents a custom field drop down option.
	/// </summary>
	public interface IDropDownOption : ICacheable
	{
		/// <summary>
		/// Gets the custom field definition that defines this option.
		/// </summary>
		ICustomFieldDefinition Field { get; }
		/// <summary>
		/// Gets the option text.
		/// </summary>
		string Text { get; }
		/// <summary>
		/// Gets the option color.
		/// </summary>
		LabelColor? Color { get; }
		/// <summary>
		/// Gets the option position.
		/// </summary>
		Position Position { get; }

		/// <summary>
		/// Deletes the drop down option.
		/// </summary>
		/// <param name="ct">(Optional) A cancellation token for async processing.</param>
		/// <remarks>
		/// This permanently deletes the drop down option from Trello's server, however, this object will remain in memory and all properties will remain accessible.
		/// </remarks>
		Task Delete(CancellationToken ct = default(CancellationToken));
		/// <summary>
		/// Refreshes the drop down option.
		/// </summary>
		/// <param name="ct">(Optional) A cancellation token for async processing.</param>
		Task Refresh(CancellationToken ct = default(CancellationToken));
	}
}