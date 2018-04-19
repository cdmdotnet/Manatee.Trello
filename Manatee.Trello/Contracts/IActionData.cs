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
		/// <associated-action-types>
		/// - AddAttachmentToCard
		///	- DeleteAttachmentFromCard
		/// </associated-action-types>
		IAttachment Attachment { get; }

		/// <summary>
		/// Gets an assocated board.
		/// </summary>
		/// <associated-action-types>
		/// - AddMemberToBoard
		/// - AddToOrganizationBoard
		/// - CreateBoard
		/// - DeleteBoardInvitation
		/// - MakeAdminOfBoard
		/// - MakeNormalMemberOfBoard
		/// - MakeObserverOfBoard
		/// - RemoveFromOrganizationBoard
		/// - UnconfirmedBoardInvitation
		/// - UpdateBoard
		/// </associated-action-types>
		IBoard Board { get; }

		/// <summary>
		/// Gets an assocated board.
		/// </summary>
		/// <associated-action-types>
		/// - CopyBoard
		/// </associated-action-types>
		IBoard BoardSource { get; }

		/// <summary>
		/// Gets an assocated board.
		/// </summary>
		/// <associated-action-types>
		/// - CopyBoardx
		/// </associated-action-types>
		IBoard BoardTarget { get; }

		/// <summary>
		/// Gets an assocated card.
		/// </summary>
		/// <associated-action-types>
		/// - AddAttachmentToCard
		/// - AddChecklistToCard
		/// - AddMemberToCard
		/// - CommentCard
		/// - ConvertToCardFromCheckItem
		/// - CopyCommentCard
		/// - CreateCard
		/// - DeleteAttachmentFromCard
		/// - DeleteCard
		/// - EmailCard
		/// - MoveCardFromBoard
		/// - MoveCardToBoard
		/// - RemoveChecklistFromCard
		/// - RemoveMemberFromCard
		/// - UpdateCard
		/// - UpdateCardClosed
		/// - UpdateCardDesc
		/// - UpdateCardIdList
		/// - UpdateCardName
		/// - UpdateCheckItemStateOnCard
		/// </associated-action-types>
		ICard Card { get; }

		/// <summary>
		/// Gets an assocated card.
		/// </summary>
		/// <associated-action-types>
		/// - CopyCard
		/// </associated-action-types>
		ICard CardSource { get; }

		/// <summary>
		/// Gets an assocated checklist item.
		/// </summary>
		/// <associated-action-types>
		/// - ConvertToCardFromCheckItem
		/// - UpdateCheckItemStateOnCard
		/// </associated-action-types>
		ICheckItem CheckItem { get; }

		/// <summary>
		/// Gets an assocated checklist.
		/// </summary>
		/// <associated-action-types>
		/// - AddChecklistToCard
		/// - RemoveChecklistFromCard
		/// - UpdateChecklist
		/// </associated-action-types>
		ICheckList CheckList { get; }

		/// <summary>
		/// Gets an associated custom field definition.
		/// </summary>
		/// <associated-action-types>
		/// - UpdateCustomField
		/// - UpdateCustomFieldItem
		/// </associated-action-types>
		ICustomFieldDefinition CustomField { get; }

		/// <summary>
		/// Gets the associated label.
		/// </summary>
		/// <associated-action-types>
		/// - AddLabelToCard
		/// - CreateLabel
		/// - DeleteLabel
		/// - RemoveLabelFromCard
		/// - UpdateLabel
		/// </associated-action-types>
		ILabel Label { get; }

		/// <summary>
		/// Gets the date/time a comment was last edited.
		/// </summary>
		DateTime? LastEdited { get; }

		/// <summary>
		/// Gets an assocated list.
		/// </summary>
		/// <associated-action-types>
		/// - CreateList
		/// - MoveListFromBoard
		/// - MoveListToBoard
		/// - UpdateList
		/// - UpdateListClosed
		/// - UpdateListName
		/// </associated-action-types>
		IList List { get; }

		/// <summary>
		/// Gets the current list.
		/// </summary>
		/// <associated-action-types>
		/// - UpdateCardIdList
		/// </associated-action-types>
		/// <remarks>
		/// For some action types, this information may be in the <see cref="List"/> or <see cref="OldList"/> properties.
		/// </remarks>
		IList ListAfter { get; }

		/// <summary>
		/// Gets the previous list.
		/// </summary>
		/// <associated-action-types>
		/// - UpdateCardIdList
		/// </associated-action-types>
		/// <remarks>
		/// For some action types, this information may be in the <see cref="List"/> or <see cref="OldList"/> properties.
		/// </remarks>
		IList ListBefore { get; }

		/// <summary>
		/// Gets an assocated member.
		/// </summary>
		/// <associated-action-types>
		/// - AddMemberToBoard
		/// - AddMemberToCard
		/// - AddMemberToOrganization
		/// - MakeNormalMemberOfBoard
		/// - MakeNormalMemberOfOrganization
		/// - MemberJoinedTrello
		/// - RemoveMemberFromCard
		/// - UpdateMember
		/// </associated-action-types>
		IMember Member { get; }

		/// <summary>
		/// Gets the previous description.
		/// </summary>
		/// <associated-action-types>
		/// - UpdateCard
		/// - UpdateCardDesc
		/// </associated-action-types>
		string OldDescription { get; }

		/// <summary>
		/// Gets the previous list.
		/// </summary>
		/// <associated-action-types>
		/// - UpdateCard
		/// - UpdateCardIdList
		/// </associated-action-types>
		/// <remarks>
		/// For some action types, this information may be in the <see cref="ListAfter"/> or <see cref="ListBefore"/> properties.
		/// </remarks>
		IList OldList { get; }

		/// <summary>
		/// Gets the previous position.
		/// </summary>
		/// <associated-action-types>
		/// - UpdateCard
		/// - UpdateList
		/// - UpdateCustomField
		/// </associated-action-types>
		Position OldPosition { get; }

		/// <summary>
		/// Gets the previous text value. 
		/// </summary>
		/// <associated-action-types>
		/// - UpdateCard
		/// - CommentCard
		/// </associated-action-types>
		string OldText { get; }

		/// <summary>
		/// Gets an associated organization.
		/// </summary>
		/// <associated-action-types>
		/// - AddMemberToOrganization
		/// - AddToOrganizationBoard
		/// - CreateOrganization
		/// - DeleteOrganizationInvitation
		/// - MakeNormalMemberOfOrganization
		/// - RemoveFromOrganizationBoard
		/// - UnconfirmedOrganizationInvitation
		/// - UpdateOrganization
		/// </associated-action-types>
		IOrganization Organization { get; }

		/// <summary>
		/// Gets an associated power-up.
		/// </summary>
		/// <associated-action-types>
		/// - DisablePowerUp
		/// - EnablePowerUp
		/// </associated-action-types>
		IPowerUp PowerUp { get; }

		/// <summary>
		/// Gets assocated text.
		/// </summary>
		/// <associated-action-types>
		/// - CommentCard
		/// </associated-action-types>
		string Text { get; set; }

		/// <summary>
		/// Gets whether the object was previously archived.
		/// </summary>
		/// <associated-action-types>
		/// - UpdateCardClosed
		/// - UpdateListClosed
		/// </associated-action-types>
		bool? WasArchived { get; }

		/// <summary>
		/// Gets a custom value associate with the action if any.
		/// </summary>
		string Value { get; }
	}
}