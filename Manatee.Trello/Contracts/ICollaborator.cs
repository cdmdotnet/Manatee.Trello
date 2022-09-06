namespace Manatee.Trello
{
	/// <summary>
	/// Associates a <see cref="Collaborator"/> to an <see cref="Organization"/> and indicates any permissions the collaborator has in the organization.
	/// </summary>
	public interface ICollaborator : ICacheable, IRefreshable
	{
		/// <summary>
		/// Gets the member's bio.
		/// </summary>
		string Bio { get; }

		/// <summary>
		/// Gets the member's full name.
		/// </summary>
		string FullName { get; }

		/// <summary>
		/// Gets or sets the member's initials.
		/// </summary>
		string Initials { get; }

		/// <summary>
		/// Gets the member's online status.
		/// </summary>
		MemberStatus? Status { get; }

		/// <summary>
		/// Gets the member's URL.
		/// </summary>
		string Url { get; }

		/// <summary>
		/// Gets the member's username.
		/// </summary>
		string UserName { get; }
	}
}