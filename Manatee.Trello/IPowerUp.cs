using Manatee.Trello.Contracts;

namespace Manatee.Trello
{
	/// <summary>
	/// Defines the basis of a power-up.
	/// </summary>
	public interface IPowerUp : ICacheable
	{
		/// <summary>
		/// Gets the power-up name.
		/// </summary>
		string Name { get; }
		/// <summary>
		/// Gets whether the power-up is public (Really, I don't know what this is.  Trello's not talking.)
		/// </summary>
		bool? Public { get; }
	}
}
