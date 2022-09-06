﻿using System;
using System.Collections.Generic;
using System.Linq;
using Manatee.Trello.Json;

namespace Manatee.Trello.Internal.Caching
{
	internal static class CachingObjectFactory
	{
		private static readonly Dictionary<Type, Type> JsonTypeMap;
		private static readonly Dictionary<Type, Func<IJsonCacheable, TrelloAuthorization, object[], ICacheable>> JsonFactory;

		static CachingObjectFactory()
		{
			JsonTypeMap = new Dictionary<Type, Type>
				{
					{typeof (IJsonAction), typeof (Action)},
					{typeof (IJsonAttachment), typeof (Attachment)},
					{typeof (IJsonImagePreview), typeof (ImagePreview)},
					{typeof (IJsonBoard), typeof (Board)},
					{typeof (IJsonBoardBackground), typeof (BoardBackground)},
					{typeof (IJsonCard), typeof (Card)},
					{typeof (IJsonCheckList), typeof (CheckList)},
					{typeof (IJsonCommentReaction), typeof (CommentReaction)},
					{typeof (IJsonLabel), typeof (Label)},
					{typeof (IJsonList), typeof (List)},
					{typeof (IJsonCollaborator), typeof (Collaborator)},
					{typeof (IJsonMember), typeof (Member)},
					{typeof (IJsonOrganization), typeof (Organization)},
					{typeof (IJsonNotification), typeof (Notification)},
					{typeof (IJsonStarredBoard), typeof (StarredBoard)},
					{typeof (IJsonToken), typeof (Token)},
				};
			JsonFactory = new Dictionary<Type, Func<IJsonCacheable, TrelloAuthorization, object[], ICacheable>>
				{
					{typeof(Action), (j, a, p) => new Action((IJsonAction) j, a)},
					{typeof(Board), (j, a, p) => new Board((IJsonBoard) j, a)},
					{typeof(BoardBackground), (j, a, p) => new BoardBackground((string) p?[0], (IJsonBoardBackground) j, a)},
					{typeof(Card), (j, a, p) => new Card((IJsonCard) j, a)},
					{typeof(CheckList), (j, a, p) => new CheckList((IJsonCheckList) j, a)},
					{typeof(CommentReaction), (j, a, p) => _BuildCommentReaction((IJsonCommentReaction) j, a, p)},
					{typeof(CustomField), (j, a, p) => _BuildCustomField((IJsonCustomField) j, a, p)},
					{typeof(CustomFieldDefinition), (j, a, p) => new CustomFieldDefinition((IJsonCustomFieldDefinition) j, a)},
					{typeof(DropDownOption), (j, a, p) => new DropDownOption((IJsonCustomDropDownOption) j, a)},
					{typeof(ImagePreview), (j, a, p) => new ImagePreview((IJsonImagePreview) j)},
					{typeof(Label), (j, a, p) => new Label((IJsonLabel) j, a)},
					{typeof(List), (j, a, p) => new List((IJsonList) j, a)},
					{typeof(Collaborator), (j, a, p) => new Collaborator((IJsonCollaborator) j, a)},
					{typeof(Member), (j, a, p) => new Member((IJsonMember) j, a)},
					{typeof(Organization), (j, a, p) => new Organization((IJsonOrganization) j, a)},
					{typeof(StarredBoard), (j, a, p) => new StarredBoard((string) p[0], (IJsonStarredBoard) j, a)},
					{typeof(Notification), (j, a, p) => new Notification((IJsonNotification) j, a)},
					{typeof(Token), (j, a, p) => new Token((IJsonToken) j, a)},
					{typeof(IPowerUp), (j, a, p) => _BuildConfiguredPowerUp((IJsonPowerUp) j, a) ?? new UnknownPowerUp((IJsonPowerUp) j, a)},
					{typeof(PowerUpData), (j, a, p) => new PowerUpData((IJsonPowerUpData) j, a)},
				};
		}

		public static ICacheable GetFromCache(this IJsonCacheable json, TrelloAuthorization auth)
		{
			if (json == null) return null;
			Type jsonType = json.GetType();
			if (!jsonType.IsInterface)
			{
				jsonType = JsonTypeMap.Keys.Intersect(jsonType.GetInterfaces()).FirstOrDefault();
				if (jsonType == null)
					throw new InvalidOperationException($"Type `{json.GetType().Name}` implements more than one Manatee.Trello.Json interface.  " +
					                                    $"Please provide separate classes for each interface.");
			}

			return TrelloConfiguration.Cache.Find<ICacheable>(json.Id) ??
			       JsonFactory[JsonTypeMap[jsonType]](json, auth, null);
		}

		public static T GetFromCache<T>(this IJsonCacheable json, TrelloAuthorization auth)
			where T : class, ICacheable
		{
			if (json == null) return null;

			var cache = TryGetFromCache<T>(json);
			if (cache == null)
			{
				TrelloConfiguration.Log.Debug($"{typeof(T).Name} with ID {json.Id} not found.  Building...");
				return (T) JsonFactory[typeof(T)](json, auth, null);
			}
			return cache;

		}
		public static T GetFromCache<T, TJson>(this TJson json, TrelloAuthorization auth, bool overwrite = true, params object[] parameters)
			where T : class, ICacheable, IMergeJson<TJson>
			where TJson : IJsonCacheable
		{
			if (json == null) return null;

			var cache = TryGetFromCache<T, TJson>(json, overwrite);
			if (cache == null)
			{
				TrelloConfiguration.Log.Debug($"{typeof(T).Name} with ID {json.Id} not found.  Building...");
				return (T) JsonFactory[typeof(T)](json, auth, parameters);
			}
			return cache;

		}

		public static T TryGetFromCache<T>(this IJsonCacheable json)
			where T : class, ICacheable
		{
			return TrelloConfiguration.Cache.Find<T>(json.Id);
		}

		public static T TryGetFromCache<T, TJson>(this TJson json, bool overwrite = true)
			where T : class, ICacheable, IMergeJson<TJson>
			where TJson : IJsonCacheable
		{
			var obj = TrelloConfiguration.Cache.Find<T>(json.Id);
			obj?.Merge(json, overwrite);

			return obj;
		}

		private static IPowerUp _BuildConfiguredPowerUp(IJsonPowerUp json, TrelloAuthorization auth)
		{
			if (!TrelloConfiguration.RegisteredPowerUps.TryGetValue(json.Id, out var factory)) return null;

			return factory(json, auth);
		}

		private static CommentReaction _BuildCommentReaction(IJsonCommentReaction json, TrelloAuthorization auth, object[] parameters)
		{
			var ownerId = parameters[0] as string;
			return new CommentReaction(json, ownerId, auth);
		}

		private static CustomField _BuildCustomField(this IJsonCustomField json, TrelloAuthorization auth, object[] parameters)
		{
			var cardId = (string) parameters[0];

			switch (json.Type)
			{
				case CustomFieldType.Text:
					return new TextField(json, cardId, auth);
				case CustomFieldType.DropDown:
					return new DropDownField(json, cardId, auth);
				case CustomFieldType.CheckBox:
					return new CheckBoxField(json, cardId, auth);
				case CustomFieldType.DateTime:
					return new DateTimeField(json, cardId, auth);
				case CustomFieldType.Number:
					return new NumberField(json, cardId, auth);
				default:
					return null;
			}
		}
	}
}