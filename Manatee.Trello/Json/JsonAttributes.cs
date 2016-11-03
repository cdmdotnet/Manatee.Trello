using System;

namespace Manatee.Trello.Json
{
	/// <summary>
	/// Declares that the JSON property should be deserialized.
	/// </summary>
	[AttributeUsage(AttributeTargets.Property)]
	public class JsonDeserializeAttribute : Attribute {}

	/// <summary>
	/// Declares that the JSON property should be serialized and whether it
	/// is optional or required.
	/// </summary>
	[AttributeUsage(AttributeTargets.Property)]
	public class JsonSerializeAttribute : Attribute
	{
		/// <summary>
		/// Gets or sets whether this property is required by the Trello API.
		/// </summary>
		public bool IsRequired { get; set; }
	}

	/// <summary>
	/// Declares that the JSON property has a special serialization.
	/// </summary>
	[AttributeUsage(AttributeTargets.Property)]
	public class JsonSpecialSerializationAttribute : Attribute { }
}