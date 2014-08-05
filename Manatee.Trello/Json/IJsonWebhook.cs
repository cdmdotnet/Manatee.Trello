/***************************************************************************************

	Copyright 2013 Little Crab Solutions

	   Licensed under the Apache License, Version 2.0 (the "License");
	   you may not use this file except in compliance with the License.
	   You may obtain a copy of the License at

		 http://www.apache.org/licenses/LICENSE-2.0

	   Unless required by applicable law or agreed to in writing, software
	   distributed under the License is distributed on an "AS IS" BASIS,
	   WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
	   See the License for the specific language governing permissions and
	   limitations under the License.
 
	File Name:		IJsonToken.cs
	Namespace:		Manatee.Trello.Json
	Class Name:		IJsonToken
	Purpose:		Defines the JSON structure for the Webhook object.

***************************************************************************************/

namespace Manatee.Trello.Json
{
	/// <summary>
	/// Defines the JSON structure for the Webhook object.
	/// </summary>
	public interface IJsonWebhook : IJsonCacheable
	{
		/// <summary>
		/// Gets or sets the description of the webhook.
		/// </summary>
		[JsonDeserialize]
		[JsonSerialize]
		string Description { get; set; }
		/// <summary>
		/// Gets or sets the ID of the entity which the webhook monitors.
		/// </summary>
		[JsonDeserialize]
		[JsonSerialize]
		string IdModel { get; set; }
		/// <summary>
		/// Gets or sets the URL which receives notification messages.
		/// </summary>
		[JsonDeserialize]
		[JsonSerialize]
		string CallbackUrl { get; set; }
		/// <summary>
		/// Gets or sets whether the webhook is active.
		/// </summary>
		[JsonDeserialize]
		[JsonSerialize]
		bool? Active { get; set; }
	}
}
