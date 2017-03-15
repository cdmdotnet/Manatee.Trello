using System;
using System.Net;

namespace Manatee.Trello.Rest
{
	/// <summary>
	/// Defines properties required for objects returned by RESTful calls.
	/// </summary>
	public interface IRestResponse
	{
		/// <summary>
		/// The JSON content returned by the call.
		/// </summary>
		string Content { get; }
		/// <summary>
		/// Gets any exception that was thrown during the call.
		/// </summary>
		Exception Exception { get; set; }
		/// <summary>
		/// Gets the status code.
		/// </summary>
		HttpStatusCode StatusCode { get; }
	}
	/// <summary>
	/// Defines required properties returned by RESTful calls.
	/// </summary>
	/// <typeparam name="T">The type expected to be returned by the call.</typeparam>
	public interface IRestResponse<out T> : IRestResponse
	{
		/// <summary>
		/// The deserialized data.
		/// </summary>
		T Data { get; }
	}
}