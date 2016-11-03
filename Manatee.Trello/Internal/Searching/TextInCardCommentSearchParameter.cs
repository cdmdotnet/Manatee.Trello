namespace Manatee.Trello.Internal.Searching
{
	internal class TextInCardCommentSearchParameter : ISearchParameter
	{
		public string Query { get; }

		public TextInCardCommentSearchParameter(string text)
		{
			Query = "comment:" + text;
		}
	}
}