using System;
using System.Threading;
using System.Threading.Tasks;

namespace Manatee.Trello
{
	/// <summary>
	/// A label.
	/// </summary>
	public interface ILabel : ICacheable, IRefreshable
	{
		/// <summary>
		/// Gets the <see cref="Board"/> on which the label is defined.
		/// </summary>
		IBoard Board { get; }

		/// <summary>
		/// Gets and sets the color.  Use null for no color.
		/// </summary>
		LabelColor? Color { get; set; }

		/// <summary>
		/// Gets the creation date of the label.
		/// </summary>
		DateTime CreationDate { get; }

		/// <summary>
		/// Gets and sets the label's name.
		/// </summary>
		string Name { get; set; }

		/// <summary>
		/// Gets the number of cards which use this label.
		/// </summary>
		int? Uses { get; }

		/// <summary>
		/// Deletes the label.  All usages of the label will also be removed.
		/// </summary>
		/// <param name="ct">(Optional) A cancellation token for async processing.</param>
		/// <remarks>
		/// This permanently deletes the label from Trello's server, however, this object will remain in memory and all properties will remain accessible.
		/// </remarks>
		Task Delete(CancellationToken ct = default);
	}
}