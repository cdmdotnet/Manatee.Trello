﻿namespace Manatee.Trello.Test
{
	internal static class TrelloIds
	{
		// To re-authorize and receive new token (must have login access for the user to approve the token)
		// Other developers working on this should create their own sandbox boards and use their user accounts during the tests
		// https://trello.com/1/authorize?key=062109670e7f56b88783721892f8f66f&name=Manatee.Trello&expiration=never&response_type=token&scope=read,write,account

		public const string AppKey = "062109670e7f56b88783721892f8f66f";
		public const string UserToken = "93b012d8f6902a2ea0c915eca65ba82232aacb4c0843b8747a0d119a07501a4d";
		public const string UserName = "s_littlecrabsolutions";
		public const string MemberId = "514464db3fa062da6e00254f";
		public const string BoardId = "51478f6469fd3d9341001dae";
		public const string ListId = "51478f6469fd3d9341001daf";
		public const string CardId = "51478f6ce7d2d11751005681";
		public const string CheckListId = "51478f72231d38143c0057e1";
		public const string OrganizationId = "514a7dd9321c387f2600059e";
		public const string ActionId = "51446f605061aeb832002655";
		public const string NotificationId = "514465dd6d16d0481a000baa";
		public const string Invalid = "12345w123456wvb123456789";
		public const string AttachmentUrl = "http://i.imgur.com/H7ybFd0.png";
	}
}