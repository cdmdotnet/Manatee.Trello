namespace Manatee.Trello.Internal.Searching
{
	internal class TextInCardDescriptionSearchParameter : ISearchParameter
	{
		public string Query { get; }

		public TextInCardDescriptionSearchParameter(string text)
		{
			Query = "description:" + text;
		}
	}
}