﻿using System;
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
					{typeof(Action), (j, a) => new Action((IJsonAction) j, a)},
					{typeof(ImagePreview), (j, a) => new ImagePreview((IJsonImagePreview) j)},
					{typeof(Board), (j, a) => new Board((IJsonBoard) j, a)},
					{typeof(BoardBackground), (j, a) => new BoardBackground((IJsonBoardBackground) j, a)},
					{typeof(Card), (j, a) => new Card((IJsonCard) j, a)},
					{typeof(CheckList), (j, a) => new CheckList((IJsonCheckList) j, a)},
					{typeof(Label), (j, a) => new Label((IJsonLabel) j, a)},
					{typeof(List), (j, a) => new List((IJsonList) j, a)},
					{typeof(Member), (j, a) => new Member((IJsonMember) j, a)},
					{typeof(Organization), (j, a) => new Organization((IJsonOrganization) j, a)},
					{typeof(Notification), (j, a) => new Notification((IJsonNotification) j, a)},
					{typeof(Token), (j, a) => new Token((IJsonToken) j, a)},
					{typeof(IPowerUp), (j, a) => BuildConfiguredPowerUp((IJsonPowerUp) j, a) ?? new UnknownPowerUp((IJsonPowerUp) j, a)},
					{typeof(PowerUpData), (j, a) => new PowerUpData((IJsonPowerUpData) j, a)},
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

		private static IPowerUp BuildConfiguredPowerUp(IJsonPowerUp json, TrelloAuthorization auth)
		{
			Func<IJsonPowerUp, TrelloAuthorization, IPowerUp> factory;
			if (!TrelloConfiguration.RegisteredPowerUps.TryGetValue(json.Id, out factory)) return null;

			return factory(json, auth);
		}
	}
}