using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace Manatee.Trello
{
	/// <summary>
	/// Represents a member.
	/// </summary>
	public interface IMember : ICanWebhook, IRefreshable
	{
		/// <summary>
		/// Gets the collection of actions performed by the member.
		/// </summary>
		IReadOnlyActionCollection Actions { get; }

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
		IReadOnlyBoardCollection Boards { get; }

		/// <summary>
		/// Gets the collection of custom board backgrounds uploaded by the member.
		/// </summary>
		IReadOnlyCollection<IBoardBackground> BoardBackgrounds { get; }
		
		/// <summary>
		/// Gets the collection of cards assigned to the member.
		/// </summary>
		IReadOnlyCardCollection Cards { get; }

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
		/// Gets a string which can be used in comments or descriptions to mention this user.  The user will receive notification that they've been mentioned.
		/// </summary>
		string Mention { get; }

		/// <summary>
		/// Gets the collection of organizations to which the member belongs.
		/// </summary>
		IReadOnlyOrganizationCollection Organizations { get; }

		/// <summary>
		/// Gets the collection of the member's board stars.
		/// </summary>
		IReadOnlyCollection<IStarredBoard> StarredBoards { get; }

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
	}
}