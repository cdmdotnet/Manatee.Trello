namespace Manatee.Trello
{
	public enum NotificationType
	{
		Unknown = -1,
		AddedAttachmentToCard,
		AddedToBoard,
		AddedToCard,
		AddedToOrganization,
		AddedMemberToCard,
		AddAdminToBoard,
		AddAdminToOrganization,
		ChangeCard,
		CloseBoard,
		CommentCard,
		CreatedCard,
		InvitedToBoard,
		InvitedToOrganization,
		RemovedFromBoard,
		RemovedFromCard,
		RemovedMemberFromCard,
		RemovedFromOrganization,
		MentionedOnCard,
		UnconfirmedInvitedToBoard,
		UnconfirmedInvitedToOrganization,
		UpdateCheckItemStateOnCard,
		MakeAdminOfBoard,
		MakeAdminOfOrganization,
		CardDueSoon		
	}
}