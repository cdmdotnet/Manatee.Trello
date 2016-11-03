namespace Manatee.Trello.Internal.Searching
{
	internal class IsSearchParameter : ISearchParameter
	{
		public static readonly IsSearchParameter Open = new IsSearchParameter("open");
		public static readonly IsSearchParameter Archived = new IsSearchParameter("archived");
		public static readonly IsSearchParameter Starred = new IsSearchParameter("starred");

		public string Query { get; }

		private IsSearchParameter(string p)
		{
			Query = "is:" + p;
		}
	}
}