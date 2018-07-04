using System.Collections.Generic;

namespace Manatee.Trello.Json
{
	public interface IJsonBatch
	{
		List<IJsonBatchItem> Items { get; set; }
	}
}