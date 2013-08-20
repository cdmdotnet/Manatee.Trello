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
 
	File Name:		NewtonsoftTokenPermission.cs
	Namespace:		Manatee.Trello.NewtonsoftJson.Entities
	Class Name:		NewtonsoftTokenPermission
	Purpose:		Implements IJsonTokenPermission for Newtonsoft's Json.Net.

***************************************************************************************/

using Manatee.Trello.Json;
using Newtonsoft.Json;

namespace Manatee.Trello.NewtonsoftJson.Entities
{
	internal class NewtonsoftTokenPermission : IJsonTokenPermission
	{
		[JsonProperty("idModel")]
		public string IdModel { get; set; }
		[JsonProperty("modelType")]
		public string ModelType { get; set; }
		[JsonProperty("Read")]
		public bool? Read { get; set; }
		[JsonProperty("Write")]
		public bool? Write { get; set; }
	}
}