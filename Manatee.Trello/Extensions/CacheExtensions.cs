/***************************************************************************************

	Copyright 2015 Greg Dennis

	   Licensed under the Apache License, Version 2.0 (the "License");
	   you may not use this file except in compliance with the License.
	   You may obtain a copy of the License at

		 http://www.apache.org/licenses/LICENSE-2.0

	   Unless required by applicable law or agreed to in writing, software
	   distributed under the License is distributed on an "AS IS" BASIS,
	   WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
	   See the License for the specific language governing permissions and
	   limitations under the License.
 
	File Name:		CacheExtensions.cs
	Namespace:		Manatee.Trello.Extensions
	Class Name:		CacheExtensions
	Purpose:		Extension methods for ICache implementations.

***************************************************************************************/

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