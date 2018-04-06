using System.ComponentModel.DataAnnotations;

namespace Manatee.Trello
{
	public enum CustomFieldType
	{
		Unknown,
		[Display(Description = "text")]
		Text,
		[Display(Description = "list")]
		DropDown,
		[Display(Description = "checkbox")]
		CheckBox,
		[Display(Description = "date")]
		DateTime,
		[Display(Description = "number")]
		Number
	}
}