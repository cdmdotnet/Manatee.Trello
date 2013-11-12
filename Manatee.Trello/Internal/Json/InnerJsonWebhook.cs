/***************************************************************************************

	Copyright 2012 Greg Dennis

	   Licensed under the Apache License, Version 2.0 (the "License");
	   you may not use this file except in compliance with the License.
	   You may obtain a copy of the License at

		 http://www.apache.org/licenses/LICENSE-2.0

	   Unless required by applicable law or agreed to in writing, software
	   distributed under the License is distributed on an "AS IS" BASIS,
	   WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
	   See the License for the specific language governing permissions and
	   limitations under the License.
 
	File Name:		InnerJsonWebhook.cs
	Namespace:		Manatee.Trello.Internal.Json
	Class Name:		InnerJsonWebhook
	Purpose:		Internal implementation of IJsonWebhook.

***************************************************************************************/

using Manatee.Trello.Json;

namespace Manatee.Trello.Internal.Json
{
	internal class InnerJsonWebhook : IJsonWebhook
	{
		public string Id { get; set; }
		public string Description { get; set; }
		public string IdModel { get; set; }
		public string CallbackUrl { get; set; }
		public bool? Active { get; set; }
	}
}