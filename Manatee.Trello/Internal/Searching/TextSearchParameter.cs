namespace Manatee.Trello.Internal.Searching
{
	internal class TextSearchParameter : ISearchParameter
	{
		public string Query { get; }

		public TextSearchParameter(string text)
		{
			Query = text;
		}
	}
}