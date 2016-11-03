using System.Globalization;

namespace Manatee.Trello.Internal.Searching
{
	internal class EditedSearchParameter : ISearchParameter
	{
		public static readonly EditedSearchParameter Day = new EditedSearchParameter("day");
		public static readonly EditedSearchParameter Week = new EditedSearchParameter("week");
		public static readonly EditedSearchParameter Month = new EditedSearchParameter("month");

		public string Query { get; }

		public EditedSearchParameter(int days)
		{
			Query = "edited:" + days.ToString(CultureInfo.InvariantCulture);
		}
		private EditedSearchParameter(string named)
		{
			Query = "edited:" + named;
		}
	}
}