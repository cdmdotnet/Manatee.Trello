using Manatee.Trello.Contracts;
using Manatee.Trello.Internal.Caching;
using Manatee.Trello.Json;

namespace Manatee.Trello
{
	/// <summary>
	/// Extension methods for ICache implementations.
	/// </summary>
	public static class CacheExtensions
	{
		/// <summary>
		/// Gets an already-instantiated object or creates one.
		/// </summary>
		/// <typeparam name="T">The type of object to get.</typeparam>
		/// <param name="cache">The ICache implementation.</param>
		/// <param name="id">The ID of the object.</param>
		/// <param name="auth">(Optional) Custom authorization parameters. When not provided,
		/// <see cref="TrelloAuthorization.Default"/> will be used.</param>
		/// <returns>The object requested.</returns>
		public static T GetOrCreate<T>(this ICache cache, string id, TrelloAuthorization auth = null)
			where T : class, ICacheable
		{
			var json = TrelloConfiguration.JsonFactory.Create<IJsonCard>();
			json.Id = id;
			return json.GetFromCache<T>(auth ?? TrelloAuthorization.Default);
		}
	}
}