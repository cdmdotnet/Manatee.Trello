using System.Threading;
using System.Threading.Tasks;

namespace Manatee.Trello.Rest
{
	/// <summary>
	/// Defines methods required to make RESTful calls.
	/// </summary>
	public interface IRestClient
	{
		/// <summary>
		/// Makes a RESTful call and ignores any return data.
		/// </summary>
		/// <param name="request">The request.</param>
		/// <param name="ct">(Optional) A cancellation token for async processing.</param>
		Task<IRestResponse> Execute(IRestRequest request, CancellationToken ct);

		/// <summary>
		/// Makes a RESTful call and expects a single object to be returned.
		/// </summary>
		/// <typeparam name="T">The expected type of object to receive in response.</typeparam>
		/// <param name="request">The request.</param>
		/// <param name="ct">(Optional) A cancellation token for async processing.</param>
		/// <returns>The response.</returns>
		Task<IRestResponse<T>> Execute<T>(IRestRequest request, CancellationToken ct)
			where T : class;
	}
}