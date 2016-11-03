using System.Globalization;

namespace Manatee.Trello.Internal.Searching
{
	internal class DueSearchParameter : ISearchParameter
	{
		public static readonly DueSearchParameter Day = new DueSearchParameter("day");
		public static readonly DueSearchParameter Week = new DueSearchParameter("week");
		public static readonly DueSearchParameter Month = new DueSearchParameter("month");
		public static readonly DueSearchParameter Overdue = new DueSearchParameter("overdue");

		public string Query { get; }

		public DueSearchParameter(int days)
		{
			Query = "due:" + days.ToString(CultureInfo.InvariantCulture);
		}
		private DueSearchParameter(string named)
		{
			Query = "due:" + named;
		}
	}
}