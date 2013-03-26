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
 
	File Name:		NotificationData.cs
	Namespace:		Manatee.Trello
	Class Name:		NotificationData
	Purpose:		Represents relevant data for a notification on Trello.com.

***************************************************************************************/
using Manatee.Json;
using Manatee.Json.Enumerations;
using Manatee.Trello.Implementation;
using Manatee.Trello.Rest;

namespace Manatee.Trello
{
	//      "data":{
	//         "board":{
	//            "name":"Manatee.Json",
	//            "id":"50d227239c7b29575f000f99"
	//         }
	//      },
	/// <summary>
	/// Contains relevant data for an Notification.  Content depends upon the type of Notification.
	/// </summary>
	public class NotificationData : JsonCompatibleExpiringObject
	{
		/// <summary>
		/// Contains the JSON data relevant to the Notification.
		/// </summary>
		public JsonObject Data { get; set; }

		/// <summary>
		/// Creates a new instance of the ActionData class.
		/// </summary>
		public NotificationData() {}
		internal NotificationData(TrelloService svc, Notification owner)
			: base(svc, owner) {}

		/// <summary>
		/// Builds an object from a JsonValue.
		/// </summary>
		/// <param name="json">The JsonValue representation of the object.</param>
		public override void FromJson(JsonValue json)
		{
			if (json == null) return;
			if (json.Type != JsonValueType.Object) return;
			var obj = json.Object;
			Data = obj;
		}
		/// <summary>
		/// Converts an object to a JsonValue.
		/// </summary>
		/// <returns>
		/// The JsonValue representation of the object.
		/// </returns>
		public override JsonValue ToJson()
		{
			return Data;
		}

		internal override bool Match(string id)
		{
			return false;
		}
		internal override void Refresh(ExpiringObject entity)
		{
			var data = entity as ActionData;
			if (data == null) return;
			Data = data.Data;
		}

		/// <summary>
		/// Retrieves updated data from the service instance and refreshes the object.
		/// </summary>
		protected override void Get()
		{
			var entity = Svc.Api.Get(Svc.RequestProvider.Create<NotificationData>(new[] {Owner, this}));
			Refresh(entity);
		}
		/// <summary>
		/// Propigates the service instance to the object's owned objects.
		/// </summary>
		protected override void PropigateSerivce() {}
	}
}