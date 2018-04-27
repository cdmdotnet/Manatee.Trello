namespace Manatee.Trello
{
	/// <summary>
	/// Represents permissions granted by a token.
	/// </summary>
	public interface ITokenPermission
	{
		/// <summary>
		/// Gets whether a token can read values.
		/// </summary>
		bool? CanRead { get; }

		/// <summary>
		/// Gets whether a token can write values.
		/// </summary>
		bool? CanWrite { get; }
	}
}