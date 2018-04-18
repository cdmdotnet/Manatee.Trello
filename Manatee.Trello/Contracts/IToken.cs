using System;
using System.Threading;
using System.Threading.Tasks;

namespace Manatee.Trello
{
	/// <summary>
	/// Represents a user token.
	/// </summary>
	public interface IToken : ICacheable
	{
		/// <summary>
		/// Gets the name of the application associated with the token.
		/// </summary>
		string AppName { get; }

		/// <summary>
		/// Gets the permissions on boards granted by the token.
		/// </summary>
		ITokenPermission BoardPermissions { get; }

		/// <summary>
		/// Gets the creation date of the token.
		/// </summary>
		DateTime CreationDate { get; }

		/// <summary>
		/// Gets the date and time the token was created.
		/// </summary>
		DateTime? DateCreated { get; }

		/// <summary>
		/// Gets the date and time the token expires, if any.
		/// </summary>
		DateTime? DateExpires { get; }

		/// <summary>
		/// Gets the member for which the token was issued.
		/// </summary>
		IMember Member { get; }

		/// <summary>
		/// Gets the permissions on members granted by the token.
		/// </summary>
		ITokenPermission MemberPermissions { get; }

		/// <summary>
		/// Gets the permissions on organizations granted by the token.
		/// </summary>
		ITokenPermission OrganizationPermissions { get; }

		/// <summary>
		/// Deletes the token.
		/// </summary>
		/// <param name="ct">(Optional) A cancellation token for async processing.</param>
		/// <remarks>
		/// This permanently deletes the token from Trello's server, however, this object will remain in memory and all properties will remain accessible.
		/// </remarks>
		Task Delete(CancellationToken ct = default(CancellationToken));

		/// <summary>
		/// Marks the token to be refreshed the next time data is accessed.
		/// </summary>
		/// <param name="ct">(Optional) A cancellation token for async processing.</param>
		Task Refresh(CancellationToken ct = default(CancellationToken));
	}
}