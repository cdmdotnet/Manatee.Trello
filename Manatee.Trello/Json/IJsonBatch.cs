using System.Collections.Generic;

namespace Manatee.Trello.Json
{
	/// <summary>
	/// Defines the JSON structure for a batch request.
	/// </summary>
	public interface IJsonBatch
	{
		/// <summary>
		/// Gets or sets the list of entity data.
		/// </summary>
		List<IJsonBatchItem> Items { get; set; }
	}
}