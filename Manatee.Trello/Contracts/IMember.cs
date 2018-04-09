using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace Manatee.Trello
{
	/// <summary>
	/// Represents a member.
	/// </summary>
	public interface IMember : ICanWebhook
	{
		/// <summary>
		/// Gets the collection of actions performed by the member.
		/// </summary>
		IReadOnlyCollection<IAction> Actions { get; }

		/// <summary>
		/// Gets the source type for the member's avatar.
		/// </summary>
		AvatarSource? AvatarSource { get; }

		/// <summary>
		/// Gets the URL to the member's avatar.
		/// </summary>
		string AvatarUrl { get; }

		/// <summary>
		/// Gets the member's bio.
		/// </summary>
		string Bio { get; }

		/// <summary>
		/// Gets the collection of boards owned by the member.
		/// </summary>
		IReadOnlyCollection<IBoard> Boards { get; }

		/// <summary>
		/// Gets the creation date of the member.
		/// </summary>
		DateTime CreationDate { get; }

		/// <summary>
		/// Gets the member's full name.
		/// </summary>
		string FullName { get; }

		/// <summary>
		/// Gets or sets the member's initials.
		/// </summary>
		string Initials { get; }

		/// <summary>
		/// Gets whether the member has actually join or has merely been invited (ghost).
		/// </summary>
		bool? IsConfirmed { get; }

		/// <summary>
		/// Gets a string which can be used in comments or descriptions to mention another
		/// user.  The user will receive notification that they've been mentioned.
		/// </summary>
		string Mention { get; }

		/// <summary>
		/// Gets the collection of organizations to which the member belongs.
		/// </summary>
		IReadOnlyCollection<IOrganization> Organizations { get; }

		/// <summary>
		/// Gets the member's online status.
		/// </summary>
		MemberStatus? Status { get; }

		/// <summary>
		/// Gets the collection of trophies earned by the member.
		/// </summary>
		IEnumerable<string> Trophies { get; }

		/// <summary>
		/// Gets the member's URL.
		/// </summary>
		string Url { get; }

		/// <summary>
		/// Gets the member's username.
		/// </summary>
		string UserName { get; }

		/// <summary>
		/// Raised when data on the member is updated.
		/// </summary>
		event Action<IMember, IEnumerable<string>> Updated;

		/// <summary>
		/// Marks the member to be refreshed the next time data is accessed.
		/// </summary>
		Task Refresh(CancellationToken ct = default(CancellationToken));
	}
}