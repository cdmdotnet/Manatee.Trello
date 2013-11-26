/***************************************************************************************

	Copyright 2013 Greg Dennis

	   Licensed under the Apache License, Version 2.0 (the "License");
	   you may not use this file except in compliance with the License.
	   You may obtain a copy of the License at

		 http://www.apache.org/licenses/LICENSE-2.0

	   Unless required by applicable law or agreed to in writing, software
	   distributed under the License is distributed on an "AS IS" BASIS,
	   WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
	   See the License for the specific language governing permissions and
	   limitations under the License.
 
	File Name:		GeneralExtensions.cs
	Namespace:		Manatee.Trello
	Class Name:		GeneralExtensions
	Purpose:		Contains extension methods for any ExpiringObject implementation.

***************************************************************************************/

using System;
using System.Collections.Generic;
using Manatee.Trello.Contracts;
using Manatee.Trello.Internal;

namespace Manatee.Trello
{
	/// <summary>
	/// Contains extension methods for any ExpiringObject implementation.
	/// </summary>
	public static class ExpiringExtensions
	{
		/// <summary>
		/// Creates a webhook for the specified entity.
		/// </summary>
		/// <typeparam name="T">The type of the entity.</typeparam>
		/// <param name="entity">The entity.</param>
		/// <param name="callbackUrl">The callback URL to receive webhook messages.</param>
		/// <returns>A <see cref="Webhook&lt;T&gt;"/> object containing the details of the webhook.</returns>
		public static Webhook<T> CreateWebhook<T>(this T entity, string callbackUrl)
			where T : ExpiringObject, ICanWebhook
		{
			if (entity == null)
				throw new ArgumentNullException("entity");
			entity.Validator.Url(callbackUrl);
			var parameters = new Dictionary<string, object>
				{
					{"callbackURL", callbackUrl},
					{"idModel", entity.Id}
				};
			var webhook = entity.EntityRepository.Download<Webhook<T>>(EntityRequestType.Webhook_Write_Entity, parameters);
			return webhook;
		}
	}
}