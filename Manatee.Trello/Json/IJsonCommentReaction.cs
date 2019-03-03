namespace Manatee.Trello.Json
{
	public interface IJsonCommentReaction : IJsonCacheable
	{
		IJsonMember Member { get; set; }
		IJsonAction Comment { get; set; }
		Emoji Emoji { get; set; }
	}
}