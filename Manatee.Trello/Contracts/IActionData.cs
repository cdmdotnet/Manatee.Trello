using System;

namespace Manatee.Trello
{
	/// <summary>
	/// Exposes any data associated with an action.
	/// </summary>
	public interface IActionData
	{
		/// <summary>
		/// Gets an assocated attachment.
		/// </summary>
		IAttachment Attachment { get; }

		/// <summary>
		/// Gets an assocated board.
		/// </summary>
		IBoard Board { get; }

		/// <summary>
		/// Gets an assocated board.
		/// </summary>
		IBoard BoardSource { get; }

		/// <summary>
		/// Gets an assocated board.
		/// </summary>
		IBoard BoardTarget { get; }

		/// <summary>
		/// Gets an assocated card.
		/// </summary>
		ICard Card { get; }

		/// <summary>
		/// Gets an assocated card.
		/// </summary>
		ICard CardSource { get; }

		/// <summary>
		/// Gets an assocated checklist item.
		/// </summary>
		ICheckItem CheckItem { get; }

		/// <summary>
		/// Gets an assocated checklist.
		/// </summary>
		ICheckList CheckList { get; }
		/// <summary>
		/// Gets the associated custom field definition.
		/// </summary>
		ICustomFieldDefinition CustomField { get; }

		/// <summary>
		/// Gets the associated label.
		/// </summary>
		ILabel Label { get; }

		/// <summary>
		/// Gets the date/time a comment was last edited.
		/// </summary>
		DateTime? LastEdited { get; }

		/// <summary>
		/// Gets an assocated list.
		/// </summary>
		IList List { get; }

		/// <summary>
		/// Gets the current list.
		/// </summary>
		/// <remarks>
		/// For some action types, this information may be in the <see cref="List"/> or <see cref="OldList"/> properties.
		/// </remarks>
		IList ListAfter { get; }

		/// <summary>
		/// Gets the previous list.
		/// </summary>
		/// <remarks>
		/// For some action types, this information may be in the <see cref="List"/> or <see cref="OldList"/> properties.
		/// </remarks>
		IList ListBefore { get; }

		/// <summary>
		/// Gets an assocated member.
		/// </summary>
		IMember Member { get; }

		/// <summary>
		/// Gets the previous description.
		/// </summary>
		string OldDescription { get; }

		/// <summary>
		/// Gets the previous list.
		/// </summary>
		/// <remarks>
		/// For some action types, this information may be in the <see cref="ListAfter"/> or <see cref="ListBefore"/> properties.
		/// </remarks>
		IList OldList { get; }

		/// <summary>
		/// Gets the previous position.
		/// </summary>
		Position OldPosition { get; }

		/// <summary>
		/// Gets the previous text value. 
		/// </summary>
		string OldText { get; }

		/// <summary>
		/// Gets an associated organization.
		/// </summary>
		IOrganization Organization { get; }

		/// <summary>
		/// Gets an associated power-up.
		/// </summary>
		IPowerUp PowerUp { get; }

		/// <summary>
		/// Gets assocated text.
		/// </summary>
		string Text { get; set; }

		/// <summary>
		/// Gets whether the object was previously archived.
		/// </summary>
		bool? WasArchived { get; }

		/// <summary>
		/// Gets a custom value associate with the action if any.
		/// </summary>
		string Value { get; }
	}
}