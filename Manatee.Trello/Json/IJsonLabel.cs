namespace Manatee.Trello.Json
{
	/// <summary>
	/// Defines the JSON structure for the Label object.
	/// </summary>
	public interface IJsonLabel : IJsonCacheable
	{
		/// <summary>
		/// Gets and sets the board on which the label is defined.
		/// </summary>
		[JsonDeserialize]
		IJsonBoard Board { get; set; }
		/// <summary>
		/// Gets and sets the color of the label.
		/// </summary>
		[JsonDeserialize]
		[JsonSerialize]
		LabelColor? Color { get; set; }
		/// <summary>
		/// Determines if the color property should be submitted even if it is null.
		/// </summary>
		/// <remarks>
		/// This property is not part of the JSON structure.
		/// </remarks>
		bool ForceNullColor { set; }
		/// <summary>
		/// Gets and sets the name of the label.
		/// </summary>
		[JsonDeserialize]
		[JsonSerialize]
		string Name { get; set; }
		/// <summary>
		/// Gets and sets how many cards use this label.
		/// </summary>
		[JsonDeserialize]
		int? Uses { get; set; }
	}
}