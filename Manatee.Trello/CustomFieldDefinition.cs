using System;
using System.Collections.Generic;
using System.Text;

namespace Manatee.Trello
{
	public class CustomFieldDefinition
	{
		public string Id { get; set; }
		public Board Board { get; set; }
		public string FieldGroup { get; set; }
		public string Name { get; set; }
		public Position Position { get; set; }
		public CustomFieldType Type { get; set; }
		public IEnumerable<DropDownOption> Options { get; set; }
	}

	public class DropDownOption
	{
		public string Id { get; set; }
		public DropDownField Field { get; set; }
		public string Text { get; set; }
		public LabelColor? Color { get; set; }
		public Position Position { get; set; }
	}

	public enum CustomFieldType
	{
		Unknown,
		Text,
		DropDown,
		CheckBox,
		DateTime,
		Number
	}

	public abstract class CustomField
	{
		public string Id { get; set; }
		public CustomFieldDefinition Definition { get; set; }
	}

	public abstract class CustomField<T> : CustomField
	{
		public T Value { get; set; }
	}

	public class TextField : CustomField<string>
	{
	}

	public class DropDownField : CustomField<DropDownOption>
	{
	}

	public class CheckBoxField : CustomField<bool?>
	{
	}

	public class DateTimeCustomField : CustomField<DateTime?>
	{
	}

	public class NumberCustomField : CustomField<double?>
	{
	}
}
