using System.ComponentModel.DataAnnotations;

namespace Manatee.Trello
{
	public enum BoardBackgroundBrightness
	{
		Unknown,
		Light,
		[Display(Description = "dark")]
		Dark
	}
}