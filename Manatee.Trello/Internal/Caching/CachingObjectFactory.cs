using System;
using System.Collections.Generic;
using Manatee.Trello.Json;

namespace Manatee.Trello.Internal.Caching
{
	internal static class CachingObjectFactory
	{
		private static readonly Dictionary<Type, Type> JsonTypeMap;
		private static readonly Dictionary<Type, Func<IJsonCacheable, TrelloAuthorization, ICacheable>> JsonFactory;

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
					{typeof (IJsonLabel), typeof (Label)},
					{typeof (IJsonList), typeof (List)},
					{typeof (IJsonMember), typeof (Member)},
					{typeof (IJsonOrganization), typeof (Organization)},
					{typeof (IJsonNotification), typeof (Notification)},
					{typeof (IJsonToken), typeof (Token)},
				};
			JsonFactory = new Dictionary<Type, Func<IJsonCacheable, TrelloAuthorization, ICacheable>>
				{
					{typeof(Action), (j, a) => new Action((IJsonAction) j, a)},
					{typeof(Board), (j, a) => new Board((IJsonBoard) j, a)},
					{typeof(BoardBackground), (j, a) => new BoardBackground((IJsonBoardBackground) j, a)},
					{typeof(Card), (j, a) => new Card((IJsonCard) j, a)},
					{typeof(CheckList), (j, a) => new CheckList((IJsonCheckList) j, a)},
					{typeof(CustomField), (j, a) => _BuildCustomField((IJsonCustomField) j, a)},
					{typeof(CustomFieldDefinition), (j, a) => new CustomFieldDefinition((IJsonCustomFieldDefinition) j, a)},
					{typeof(DropDownOption), (j, a) => new DropDownOption((IJsonCustomDropDownOption) j, a)},
					{typeof(ImagePreview), (j, a) => new ImagePreview((IJsonImagePreview) j)},
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
			if (json == null) return null;

			return TrelloConfiguration.Cache.Find<ICacheable>(o => o.Id == json.Id) ??
			       JsonFactory[JsonTypeMap[json.GetType()]](json, auth);
		}
		public static T GetFromCache<T>(this IJsonCacheable json, TrelloAuthorization auth)
			where T : class, ICacheable
		{
			if (json == null) return null;

			return TryGetFromCache<T>(json) ?? (T) JsonFactory[typeof(T)](json, auth);
		}

		public static T TryGetFromCache<T>(this IJsonCacheable json)
			where T : class, ICacheable
		{
			return TrelloConfiguration.Cache.Find<T>(o => o.Id == json.Id);
		}

		private static IPowerUp BuildConfiguredPowerUp(IJsonPowerUp json, TrelloAuthorization auth)
		{
			if (!TrelloConfiguration.RegisteredPowerUps.TryGetValue(json.Id, out var factory)) return null;

			return factory(json, auth);
		}

		private static CustomField _BuildCustomField(IJsonCustomField json, TrelloAuthorization auth)
		{
			switch (json.Type)
			{
				case CustomFieldType.Text:
					return new TextField(json, auth);
				case CustomFieldType.DropDown:
					return new DropDownField(json, auth);
				case CustomFieldType.CheckBox:
					return new CheckBoxField(json, auth);
				case CustomFieldType.DateTime:
					return new DateTimeField(json, auth);
				case CustomFieldType.Number:
					return new NumberField(json, auth);
				default:
					throw new ArgumentOutOfRangeException();
			}
		}
	}
}