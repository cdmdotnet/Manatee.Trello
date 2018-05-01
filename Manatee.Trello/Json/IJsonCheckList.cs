using System.Collections.Generic;

namespace Manatee.Trello.Json
{
	/// <summary>
	/// Defines the JSON structure for the CheckList object.
	/// </summary>
	public interface IJsonCheckList : IJsonCacheable, IAcceptId
	{
		/// <summary>
		/// Gets or sets the name of this checklist.
		/// </summary>
		[JsonDeserialize]
		[JsonSerialize]
		string Name { get; set; }
		/// <summary>
		/// Gets or sets the ID of the board which contains this checklist.
		/// </summary>
		[JsonDeserialize]
		IJsonBoard Board { get; set; }
		/// <summary>
		/// Gets or sets the ID of the card which contains this checklist.
		/// </summary>
		[JsonDeserialize]
		[JsonSerialize]
		IJsonCard Card { get; set; }
		/// <summary>
		/// Gets or sets the collection of items in this checklist.
		/// </summary>
		[JsonDeserialize]
		List<IJsonCheckItem> CheckItems { get; set; }
		/// <summary>
		/// Gets or sets the position of this checklist.
		/// </summary>
		[JsonDeserialize]
		[JsonSerialize]
		IJsonPosition Pos { get; set; }
		/// <summary>
		/// Gets or sets a checklist to copy during creation.
		/// </summary>
		[JsonSerialize]
		IJsonCheckList CheckListSource { get; set; }
	}
}