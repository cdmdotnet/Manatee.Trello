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
 
	File Name:		NotificationConverter.cs
	Namespace:		Manatee.Trello.NewtonsoftJson.Converters
	Class Name:		NotificationConverter
	Purpose:		Provides a concrete implementation of IJsonNotification for
					Newtonsoft's Json.Net

***************************************************************************************/
using System;
using Manatee.Trello.Json;
using Manatee.Trello.NewtonsoftJson.Entities;
using Newtonsoft.Json.Linq;

namespace Manatee.Trello.NewtonsoftJson.Converters
{
	internal class NotificationConverter : JsonCreationConverter<IJsonNotification>
	{
		protected override IJsonNotification Create(Type objectType, JObject jObject)
		{
			return new NewtonsoftNotification();
		}
	}
}