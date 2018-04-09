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
		Task<IRestResponse> Execute(IRestRequest request);

		/// <summary>
		/// Makes a RESTful call and expects a single object to be returned.
		/// </summary>
		/// <typeparam name="T">The expected type of object to receive in response.</typeparam>
		/// <param name="request">The request.</param>
		/// <returns>The response.</returns>
		Task<IRestResponse<T>> Execute<T>(IRestRequest request)
			where T : class;
	}
}