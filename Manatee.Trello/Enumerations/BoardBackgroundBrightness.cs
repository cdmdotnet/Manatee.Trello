using System.ComponentModel.DataAnnotations;

namespace Manatee.Trello
{
	public enum BoardBackgroundBrightness
	{
		Unknown,
		[Display(Description = "dark")]
		Dark
	}
}