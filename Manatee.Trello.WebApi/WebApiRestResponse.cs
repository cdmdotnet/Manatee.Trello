using System;
using System.Net;
using Manatee.Trello.Rest;

namespace Manatee.Trello.WebApi
{
	/// <summary>
	/// Implements <see cref="IRestResponse"/> for WebApi.
	/// </summary>
	public class WebApiRestResponse : IRestResponse
	{
		/// <summary>
		/// The JSON content returned by the call.
		/// </summary>
		public string Content { get; set; }
		/// <summary>
		/// Gets any exception that was thrown during the call.
		/// </summary>
		public Exception Exception { get; set; }
		/// <summary>
		/// Gets the status code.
		/// </summary>
		public HttpStatusCode StatusCode { get; set; }
	}

	/// <summary>
	/// Implements <see cref="IRestResponse{T}"/> for WebApi.
	/// </summary>
	/// <typeparam name="T">The type expected to be returned by the call.</typeparam>
	public class WebApiRestResponse<T> : WebApiRestResponse, IRestResponse<T>
	{
		/// <summary>
		/// The deserialized data.
		/// </summary>
		public T Data { get; set; }
	}
}