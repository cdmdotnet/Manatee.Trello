using System.ComponentModel.DataAnnotations;

namespace Manatee.Trello.CardRepeater
{
	public enum RepetitionPeriod
	{
		Unknown,
		[Display(Description = "weekly")]
		Weekly,
		[Display(Description = "monthly")]
		Monthly,
		[Display(Description = "yearly")]
		Yearly
	}
}