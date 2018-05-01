namespace Manatee.Trello.Json
{
	/// <summary>
	/// Defines the JSON structure for the CheckItem object.
	/// </summary>
	public interface IJsonCheckItem : IJsonCacheable
	{
		/// <summary>
		/// Gets or sets the check list for the check item.
		/// </summary>
		[JsonDeserialize]
		[JsonSerialize]
		IJsonCheckList CheckList { get; set; }
		/// <summary>
		/// Gets or sets the check state of the checklist item.
		/// </summary>
		[JsonDeserialize]
		[JsonSerialize]
		CheckItemState? State { get; set; }
		/// <summary>
		/// Gets or sets the name of the checklist item.
		/// </summary>
		[JsonDeserialize]
		[JsonSerialize]
		string Name { get; set; }
		/// <summary>
		/// Gets or sets the position of the checklist item.
		/// </summary>
		[JsonDeserialize]
		[JsonSerialize]
		IJsonPosition Pos { get; set; }
	}
}