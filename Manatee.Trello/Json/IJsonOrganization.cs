using System.Collections.Generic;

namespace Manatee.Trello.Json
{
	/// <summary>
	/// Defines the JSON structure for the Organization object.
	/// </summary>
	public interface IJsonOrganization : IJsonCacheable, IAcceptId
	{
		/// <summary>
		/// Gets or sets the name of the organization.
		/// </summary>
		[JsonDeserialize]
		[JsonSerialize]
		string Name { get; set; }
		/// <summary>
		/// Gets or sets the name to be displayed for the organization.
		/// </summary>
		[JsonDeserialize]
		[JsonSerialize]
		string DisplayName { get; set; }
		/// <summary>
		/// Gets or sets the description for the organization.
		/// </summary>
		[JsonDeserialize]
		[JsonSerialize]
		string Desc { get; set; }
		/// <summary>
		/// Gets or sets the URL to the organization's profile.
		/// </summary>
		[JsonDeserialize]
		string Url { get; set; }
		/// <summary>
		/// Gets or sets the organization's website.
		/// </summary>
		[JsonDeserialize]
		[JsonSerialize]
		string Website { get; set; }
		/// <summary>
		/// Gets or sets the organization's logo hash.
		/// </summary>
		[JsonDeserialize]
		string LogoHash { get; set; }
		/// <summary>
		/// Enumerates the powerups obtained by the organization.
		/// </summary>
		[JsonDeserialize]
		List<int> PowerUps { get; set; }
		/// <summary>
		/// Gets or sets whether the organization is a paid account.
		/// </summary>
		[JsonDeserialize]
		bool? PaidAccount { get; set; }
		/// <summary>
		/// Gets or sets a set of preferences for the organization.
		/// </summary>
		[JsonDeserialize]
		[JsonSerialize]
		[JsonSpecialSerialization]
		IJsonOrganizationPreferences Prefs { get; set; }
		/// <summary>
		/// Gets or sets a collection of premium features available to the organization.
		/// </summary>
		[JsonDeserialize]
		List<string> PremiumFeatures { get; set; }
		/// <summary>
		/// Gets or sets a collection of actions.
		/// </summary>
		[JsonDeserialize]
		List<IJsonAction> Actions { get; set; }
		/// <summary>
		/// Gets or sets a collection of boards.
		/// </summary>
		[JsonDeserialize]
		List<IJsonBoard> Boards { get; set; }
		/// <summary>
		/// Gets or sets a collection of members.
		/// </summary>
		[JsonDeserialize]
		List<IJsonMember> Members { get; set; }
		/// <summary>
		/// Gets or sets a collection of memberships.
		/// </summary>
		[JsonDeserialize]
		List<IJsonOrganizationMembership> Memberships { get; set; }
		/// <summary>
		/// Gets or sets a collection of power-up data.
		/// </summary>
		[JsonDeserialize]
		List<IJsonPowerUpData> PowerUpData { get; set; }
	}
}