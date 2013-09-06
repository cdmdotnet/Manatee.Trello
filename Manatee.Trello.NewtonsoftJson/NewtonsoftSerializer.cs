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
 
	File Name:		Serializer.cs
	Namespace:		Manatee.Trello.NewtonsoftJson
	Class Name:		Serializer
	Purpose:		Wrapper class for the Newtonsoft.Json.Serializer for use with
					RestSharp.

***************************************************************************************/
using System.IO;
using Manatee.Trello.Json;
using Manatee.Trello.NewtonsoftJson.Converters;
using Manatee.Trello.Rest;
using Newtonsoft.Json;

namespace Manatee.Trello.NewtonsoftJson
{
	/// <summary>
	/// Wrapper class for the Newtonsoft.Json.Serializer for use with RestSharp.
	/// </summary>
	public class NewtonsoftSerializer : ISerializer, IDeserializer
	{
		private readonly JsonSerializer _serializer;

		/// <summary>
		/// Creates and initializes a new instance of the ManateeJsonSerializer class.
		/// </summary>
		public NewtonsoftSerializer()
		{
			_serializer = new JsonSerializer();
			SetupTypeConverters();
		}

		/// <summary>
		/// Attempts to deserialize a RESTful response to the indicated type.
		/// </summary>
		/// <typeparam name="T">The type of object expected.</typeparam>
		/// <param name="response">The response object which contains the JSON to deserialize.</param>
		/// <returns>The requested object, if JSON is valid; null otherwise.</returns>
		public T Deserialize<T>(IRestResponse<T> response)
		{
			var stringReader = new StringReader(response.Content);
			var textReader = new JsonTextReader(stringReader);
			return _serializer.Deserialize<T>(textReader);
		}
		/// <summary>
		/// Serializes an object to JSON.
		/// </summary>
		/// <param name="obj">The object to serialize.</param>
		/// <returns>An equivalent JSON string.</returns>
		public string Serialize(object obj)
		{
			return Serialize(obj);
		}

		private void SetupTypeConverters()
		{
			_serializer.Converters.Add(new ActionConverter());
			_serializer.Converters.Add(new ActionDataConverter());
			_serializer.Converters.Add(new AttachmentConverter());
			_serializer.Converters.Add(new AttachmentPreviewConverter());
			_serializer.Converters.Add(new BadgesConverter());
			_serializer.Converters.Add(new BoardConverter());
			_serializer.Converters.Add(new BoardMembershipConverter());
			_serializer.Converters.Add(new BoardPersonalPreferencesConverter());
			_serializer.Converters.Add(new BoardPreferencesConverter());
			_serializer.Converters.Add(new BoardVisibilityRestrictConverter());
			_serializer.Converters.Add(new CardConverter());
			_serializer.Converters.Add(new CheckItemConverter());
			_serializer.Converters.Add(new CheckListConverter());
			_serializer.Converters.Add(new LabelConverter());
			_serializer.Converters.Add(new LabelNamesConverter());
			_serializer.Converters.Add(new ListConverter());
			_serializer.Converters.Add(new MemberConverter());
			_serializer.Converters.Add(new MemberPreferencesConverter());
			_serializer.Converters.Add(new MemberSessionConverter());
			_serializer.Converters.Add(new NotificationConverter());
			_serializer.Converters.Add(new NotificationDataConverter());
			_serializer.Converters.Add(new OrganizationConverter());
			_serializer.Converters.Add(new OrganizationMembershipConverter());
			_serializer.Converters.Add(new OrganizationPreferencesConverter());
			_serializer.Converters.Add(new SearchResultsConverter());
			_serializer.Converters.Add(new TokenConverter());
			_serializer.Converters.Add(new TokenPermissionConverter());
		}
	}
}
