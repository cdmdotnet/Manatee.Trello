using System.Globalization;

namespace Manatee.Trello.Internal.Searching
{
	internal class CreatedSearchParameter : ISearchParameter
	{
		public static readonly CreatedSearchParameter Day = new CreatedSearchParameter("day");
		public static readonly CreatedSearchParameter Week = new CreatedSearchParameter("week");
		public static readonly CreatedSearchParameter Month = new CreatedSearchParameter("month");

		public string Query { get; }

		public CreatedSearchParameter(int days)
		{
			Query = "created:" + days.ToString(CultureInfo.InvariantCulture);
		}
		private CreatedSearchParameter(string named)
		{
			Query = "created:" + named;
		}
	}
}