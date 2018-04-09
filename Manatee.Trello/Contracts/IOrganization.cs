using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Manatee.Trello
{
	/// <summary>
	/// Represents an organization.
	/// </summary>
	public interface IOrganization : ICanWebhook, IQueryable
	{
		/// <summary>
		/// Gets the collection of actions performed on the organization.
		/// </summary>
		IReadOnlyCollection<IAction> Actions { get; }

		/// <summary>
		/// Gets the collection of boards owned by the organization.
		/// </summary>
		IBoardCollection Boards { get; }

		/// <summary>
		/// Gets the creation date of the organization.
		/// </summary>
		DateTime CreationDate { get; }

		/// <summary>
		/// Gets or sets the organization's description.
		/// </summary>
		string Description { get; set; }

		/// <summary>
		/// Gets or sets the organization's display name.
		/// </summary>
		string DisplayName { get; set; }

		/// <summary>
		/// Gets whether the organization has business class status.
		/// </summary>
		bool IsBusinessClass { get; }

		/// <summary>
		/// Gets the collection of members who belong to the organization.
		/// </summary>
		IReadOnlyCollection<IMember> Members { get; }

		/// <summary>
		/// Gets the collection of members and their priveledges on this organization.
		/// </summary>
		IOrganizationMembershipCollection Memberships { get; }

		/// <summary>
		/// Gets the organization's name.
		/// </summary>
		string Name { get; set; }

		/// <summary>
		/// Gets specific data regarding power-ups.
		/// </summary>
		IReadOnlyCollection<IPowerUpData> PowerUpData { get; }

		/// <summary>
		/// Gets the set of preferences for the organization.
		/// </summary>
		IOrganizationPreferences Preferences { get; }

		/// <summary>
		/// Gets the organization's URL.
		/// </summary>
		string Url { get; }

		/// <summary>
		/// Gets or sets the organization's website.
		/// </summary>
		string Website { get; set; }

		/// <summary>
		/// Raised when data on the organization is updated.
		/// </summary>
		event Action<IOrganization, IEnumerable<string>> Updated;

		/// <summary>
		/// Deletes the organization.
		/// </summary>
		/// <remarks>
		/// This permanently deletes the organization from Trello's server, however, this
		/// object will remain in memory and all properties will remain accessible.
		/// </remarks>
		Task Delete();

		/// <summary>
		/// Marks the organization to be refreshed the next time data is accessed.
		/// </summary>
		Task Refresh();
	}
}