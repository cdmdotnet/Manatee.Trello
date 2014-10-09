/***************************************************************************************

	Copyright 2014 Greg Dennis

	   Licensed under the Apache License, Version 2.0 (the "License");
	   you may not use this file except in compliance with the License.
	   You may obtain a copy of the License at

		 http://www.apache.org/licenses/LICENSE-2.0

	   Unless required by applicable law or agreed to in writing, software
	   distributed under the License is distributed on an "AS IS" BASIS,
	   WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
	   See the License for the specific language governing permissions and
	   limitations under the License.
 
	File Name:		CachingObjectFactory.cs
	Namespace:		Manatee.Trello.Internal.Caching
	Class Name:		CachingObjectFactory
	Purpose:		Checks the cache and builds new objects when nothing is found.

***************************************************************************************/

using System;
using System.Collections.Generic;
using Manatee.Trello.Contracts;
using Manatee.Trello.Json;

namespace Manatee.Trello.Internal.Caching
{
	internal static class CachingObjectFactory
	{
		private static readonly Dictionary<Type, Type> _jsonTypeMap;
		private static readonly Dictionary<Type, Func<IJsonCacheable, ICacheable>> _jsonFactory;

		static CachingObjectFactory()
		{
			_jsonTypeMap = new Dictionary<Type, Type>
				{
					{typeof (IJsonAction), typeof (Action)},
					{typeof (IJsonAttachment), typeof (Attachment)},
					{typeof (IJsonImagePreview), typeof (ImagePreview)},
					{typeof (IJsonBoard), typeof (Board)},
					{typeof (IJsonCard), typeof (Card)},
					{typeof (IJsonCheckList), typeof (CheckList)},
					{typeof (IJsonList), typeof (List)},
					{typeof (IJsonMember), typeof (Member)},
					{typeof (IJsonOrganization), typeof (Organization)},
					{typeof (IJsonNotification), typeof (Notification)},
					{typeof (IJsonToken), typeof (Token)},
				};
			_jsonFactory = new Dictionary<Type, Func<IJsonCacheable, ICacheable>>
				{
					{typeof (Action), j => new Action((IJsonAction) j)},
					{typeof (ImagePreview), j => new ImagePreview((IJsonImagePreview) j)},
					{typeof (Board), j => new Board((IJsonBoard) j)},
					{typeof (Card), j => new Card((IJsonCard) j)},
					{typeof (CheckList), j => new CheckList((IJsonCheckList) j)},
					{typeof (List), j => new List((IJsonList) j)},
					{typeof (Member), j => new Member((IJsonMember) j)},
					{typeof (Organization), j => new Organization((IJsonOrganization) j)},
					{typeof (Notification), j => new Notification((IJsonNotification) j)},
					{typeof (Token), j => new Token((IJsonToken) j)},
				};
		}

		public static ICacheable GetFromCache(this IJsonCacheable json)
		{
			return json == null ? null : TrelloConfiguration.Cache.Find<ICacheable>(o => o.Id == json.Id) ?? _jsonFactory[_jsonTypeMap[json.GetType()]](json);
		}
		public static T GetFromCache<T>(this IJsonCacheable json)
			where T : class, ICacheable
		{
			return json == null ? null : TrelloConfiguration.Cache.Find<T>(o => o.Id == json.Id) ?? (T) _jsonFactory[typeof (T)](json);
		}
	}
}