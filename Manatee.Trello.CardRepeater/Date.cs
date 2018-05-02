namespace Manatee.Trello.CardRepeater
{
	public class Date
	{
		public MonthOfYear Month { get; }
		public int Day { get; }

		public Date(MonthOfYear month, int day)
		{
			Month = month;
			Day = day;
		}
	}
}