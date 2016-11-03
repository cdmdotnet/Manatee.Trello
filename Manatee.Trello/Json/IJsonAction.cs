using System;

namespace Manatee.Trello.Json
{
	/// <summary>
	/// Defines the JSON structure for the Action object.
	/// </summary>
	public interface IJsonAction : IJsonCacheable
	{
		/// <summary>
		/// Gets or sets the ID of the member who performed the action.
		/// </summary>
		[JsonDeserialize]
		IJsonMember MemberCreator { get; set; }
		/// <summary>
		/// Gets or sets the data associated with the action.  Contents depend upon the action's type.
		/// </summary>
		[JsonDeserialize]
		IJsonActionData Data { get; set; }
		/// <summary>
		/// Gets or sets the action's type.
		/// </summary>
		[JsonDeserialize]
		ActionType? Type { get; set; }
		///<summary>
		/// Gets or sets the date on which the action was performed.
		///</summary>
		[JsonDeserialize]
		DateTime? Date { get; set; }
	}
}