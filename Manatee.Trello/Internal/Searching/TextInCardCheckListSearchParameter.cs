namespace Manatee.Trello.Internal.Searching
{
	internal class TextInCardCheckListSearchParameter : ISearchParameter
	{
		public string Query { get; }

		public TextInCardCheckListSearchParameter(string text)
		{
			Query = "checklist:" + text;
		}
	}
}