namespace Manatee.Trello.CardRepeater
{
	public class Time
	{
		public int Hour { get; }
		public int Minute { get; }

		public Time(int hour, int minute)
		{
			Hour = hour;
			Minute = minute;
		}
	}
}