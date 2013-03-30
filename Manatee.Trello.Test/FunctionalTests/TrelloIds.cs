namespace Manatee.Trello.Test.FunctionalTests
{
	internal static class TrelloIds
	{
		// To re-authorize and receive new token (must have login access for the user to approve the token)
		// Other developers working on this should create their own sandbox boards and use their user accounts during the tests
		// https://trello.com/1/authorize?key=062109670e7f56b88783721892f8f66f&name=Manatee.Trello&expiration=never&response_type=token&scope=read,write,account

		public const string Key = "062109670e7f56b88783721892f8f66f";
		public const string Token = "bd944b694c84d53c411f25e7866d5b5860eb91fa5097bbb13bac8d5c79d611fe";
		public const string UserName = "s_littlecrabsolutions";
		public const string BoardId = "51478f6469fd3d9341001dae";
		public const string ListId = "51478f6469fd3d9341001daf";
		public const string CardId = "51478f6ce7d2d11751005681";
		public const string CheckListId = "51478f72231d38143c0057e1";
		public const string OrganizationId = "50d4eb07a1b0902152003329";
		public const string ActionId = "51446f605061aeb832002655";
	}
}
