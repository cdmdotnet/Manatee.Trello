using System;
using System.Net;

namespace Manatee.Trello.RestSharp
{
	/// <summary>
	/// Implements <see cref="Rest.IRestResponse"/> for RestSharp.
	/// </summary>
	public class RestSharpResponse : Rest.IRestResponse
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
	/// Implements <see cref="Rest.IRestResponse{T}"/> for RestSharp.
	/// </summary>
	/// <typeparam name="T">The type expected to be returned by the call.</typeparam>
	public class RestSharpResponse<T> : RestSharpResponse, Rest.IRestResponse<T>
	{
		/// <summary>
		/// The deserialized data.
		/// </summary>
		public T Data { get; set; }
	}
}