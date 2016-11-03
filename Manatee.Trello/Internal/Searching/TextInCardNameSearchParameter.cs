namespace Manatee.Trello.Internal.Searching
{
	internal class TextInCardNameSearchParameter : ISearchParameter
	{
		public string Query { get; }

		public TextInCardNameSearchParameter(string text)
		{
			Query = "name:" + text;
		}
	}
}