namespace Manatee.Trello.Tests.Common
{
	public static class TrelloIds
	{
		// To re-authorize and receive new token (must have login access for the user to approve the token)
		// Other developers working on this should create their own sandbox boards and use their user accounts during the tests
		// Functional tests will not run successfully without a token with read/write/account access.
		// https://trello.com/1/authorize?key=062109670e7f56b88783721892f8f66f&name=Manatee.Trello&expiration=1day&response_type=token&scope=read,write,account

		public const string AppKey = "062109670e7f56b88783721892f8f66f";
		public const string UserToken = "f0e93074ba9726081eb4aeabb958048a6e0ce0f8f9e98ccd3294dcc080a08fea";
		public const string UserName = "s_littlecrabsolutions";
		public const string MemberId = "514464db3fa062da6e00254f";
		public const string BoardId = "51478f6469fd3d9341001dae";
		public const string ListId = "51478f6469fd3d9341001daf";
		public const string CardId = "5a72b7ab3711a44643c5ed49";
		public const string CheckListId = "51478f72231d38143c0057e1";
		public const string OrganizationId = "50d4eb07a1b0902152003329";
		public const string ActionId = "51446f605061aeb832002655";
		public const string NotificationId = "51832de023195de57800095c";
		public const string FakeId = "12345a123456acb123456789";
		public const string ShortFakeId = "12345a";
		public const string AttachmentUrl = "http://i.imgur.com/H7ybFd0.png";
	}
}
