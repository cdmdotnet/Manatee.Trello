using System.ComponentModel.DataAnnotations;

namespace Manatee.Trello
{
	public enum BoardBackgroundBrightness
	{
		Unknown,
		[Display(Description = "light")]
		Light,
		[Display(Description = "dark")]
		Dark
	}
}
