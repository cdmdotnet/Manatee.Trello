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
 
	File Name:		PostComment.cs
	Namespace:		Manatee.Trello.Implementation
	Class Name:		PostComment
	Purpose:		Represents a card comment on Trello.com.  Implemented solely
					as a name-holder in the EntityService class.

***************************************************************************************/
using System;
using Manatee.Json;
using Manatee.Trello.Rest;

namespace Manatee.Trello.Implementation
{
	internal class PostComment : OwnedEntityBase<Card>
	{
		internal override void Refresh(ExpiringObject entity)
		{
			throw new NotImplementedException();
		}
		internal override bool Match(string id)
		{
			throw new NotImplementedException();
		}
		protected override void Refresh()
		{
			throw new NotImplementedException();
		}
		protected override void PropigateSerivce()
		{
			throw new NotImplementedException();
		}
		public override void FromJson(JsonValue json)
		{
			throw new NotImplementedException();
		}
		public override JsonValue ToJson()
		{
			throw new NotImplementedException();
		}
	}
}
