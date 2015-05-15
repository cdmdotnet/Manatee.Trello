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
		private static readonly Dictionary<Type, Func<IJsonCacheable, TrelloAuthorization, ICacheable>> _jsonFactory;

		static CachingObjectFactory()
		{
			_jsonTypeMap = new Dictionary<Type, Type>
				{
					{typeof (IJsonAction), typeof (Action)},
					{typeof (IJsonAttachment), typeof (Attachment)},
					{typeof (IJsonImagePreview), typeof (ImagePreview)},
					{typeof (IJsonBoard), typeof (Board)},
					{typeof (IJsonBoardBackground), typeof (BoardBackground)},
					{typeof (IJsonCard), typeof (Card)},
					{typeof (IJsonCheckList), typeof (CheckList)},
					{typeof (IJsonLabel), typeof (Label)},
					{typeof (IJsonList), typeof (List)},
					{typeof (IJsonMember), typeof (Member)},
					{typeof (IJsonOrganization), typeof (Organization)},
					{typeof (IJsonNotification), typeof (Notification)},
					{typeof (IJsonToken), typeof (Token)},
				};
			_jsonFactory = new Dictionary<Type, Func<IJsonCacheable, TrelloAuthorization, ICacheable>>
				{
					{typeof (Action), (j, a) => new Action((IJsonAction) j, a)},
					{typeof (ImagePreview), (j, a) => new ImagePreview((IJsonImagePreview) j)},
					{typeof (Board), (j, a) => new Board((IJsonBoard) j, a)},
					{typeof (BoardBackground), (j, a) => new BoardBackground((IJsonBoardBackground) j, a)},
					{typeof (Card), (j, a) => new Card((IJsonCard) j, a)},
					{typeof (CheckList), (j, a) => new CheckList((IJsonCheckList) j, a)},
					{typeof (Label), (j, a) => new Label((IJsonLabel) j, a)},
					{typeof (List), (j, a) => new List((IJsonList) j, a)},
					{typeof (Member), (j, a) => new Member((IJsonMember) j, a)},
					{typeof (Organization), (j, a) => new Organization((IJsonOrganization) j, a)},
					{typeof (Notification), (j, a) => new Notification((IJsonNotification) j, a)},
					{typeof (Token), (j, a) => new Token((IJsonToken) j, a)},
				};
		}

		public static ICacheable GetFromCache(this IJsonCacheable json, TrelloAuthorization auth)
		{
			return json == null ? null : TrelloConfiguration.Cache.Find<ICacheable>(o => o.Id == json.Id) ?? _jsonFactory[_jsonTypeMap[json.GetType()]](json, auth);
		}
		public static T GetFromCache<T>(this IJsonCacheable json, TrelloAuthorization auth)
			where T : class, ICacheable
		{
			return json == null ? null : TrelloConfiguration.Cache.Find<T>(o => o.Id == json.Id) ?? (T) _jsonFactory[typeof (T)](json, auth);
		}
	}
}