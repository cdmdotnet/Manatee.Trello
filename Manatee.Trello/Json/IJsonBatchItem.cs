using System.Net;

namespace Manatee.Trello.Json
{
	public interface IJsonBatchItem
	{
		HttpStatusCode StatusCode { get; set; }
		string EntityId { get; set; }
		string Content { get; set; }
		string Error { get; set; }
	}
}