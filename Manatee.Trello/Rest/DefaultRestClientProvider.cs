using System;
using System.Net.Http;

namespace Manatee.Trello.Rest
{
	/// <summary>
	/// Implements <see cref="IRestClientProvider"/> using ASP.Net's <see cref="HttpClient"/>.
	/// </summary>
	public class DefaultRestClientProvider : IRestClientProvider, IDisposable
	{
		private WebApiClient _client;

		/// <summary>
		/// Singleton instance of <see cref="DefaultRestClientProvider"/>.
		/// </summary>
		public static DefaultRestClientProvider Instance { get; } = new DefaultRestClientProvider();

		/// <summary>
		/// Creates requests for the client.
		/// </summary>
		public virtual IRestRequestProvider RequestProvider { get; }

		/// <summary>
		/// Creates a new instance of the <see cref="DefaultRestClientProvider"/> class.
		/// </summary>
		protected DefaultRestClientProvider()
		{
			RequestProvider = new WebApiRequestProvider();
		}

		/// <summary>
		/// Allows an object to try to free resources and perform other cleanup operations before it is reclaimed by garbage collection.
		/// </summary>
		~DefaultRestClientProvider()
		{
			Dispose(false);
		}

		/// <summary>
		/// Creates an instance of <see cref="IRestClient"/>.
		/// </summary>
		/// <param name="apiBaseUrl">The base URL to be used by the client</param>
		/// <returns>An instance of <see cref="IRestClient"/>.</returns>
		public virtual IRestClient CreateRestClient(string apiBaseUrl)
		{
			return _client ?? (_client = new WebApiClient(apiBaseUrl));
		}

		/// <summary>
		/// Performs application-defined tasks associated with freeing, releasing, or resetting unmanaged resources.
		/// </summary>
		public void Dispose()
		{
			Dispose(true);
			GC.SuppressFinalize(this);
		}

		/// <summary>
		/// Performs application-defined tasks associated with freeing, releasing, or resetting unmanaged resources.
		/// </summary>
		/// <param name="disposing">Pass true if explicitly disposing; false if being called from the finalizer.</param>
		protected virtual void Dispose(bool disposing)
		{
			if (disposing)
			{
				_client?.Dispose();
			}
		}
	}
}