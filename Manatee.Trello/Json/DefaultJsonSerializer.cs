using System;
using System.Globalization;
using System.Linq;
using System.Reflection;
using Manatee.Json;
using Manatee.Json.Serialization;
using Manatee.Trello.Json.Entities;
using Manatee.Trello.Rest;

namespace Manatee.Trello.Json
{
	/// <summary>
	/// Wrapper class for the Manatee.Json.Serializer for use with RestSharp.
	/// </summary>
	public class DefaultJsonSerializer : ISerializer, IDeserializer
	{
		private readonly JsonSerializer _serializer;
		private readonly MethodInfo _method;

		/// <summary>
		/// Provides a singleton instance.
		/// </summary>
		public static DefaultJsonSerializer Instance { get; } = new DefaultJsonSerializer();

		/// <summary>
		/// Creates and initializes a new instance of the ManateeJsonSerializer class.
		/// </summary>
		private DefaultJsonSerializer()
		{
			_serializer = new JsonSerializer
				{
					Options =
						{
							EnumSerializationFormat = EnumSerializationFormat.AsName,
							FlagsEnumSeparator = ","
						}
				};
			_serializer.CustomSerializations.RegisterType(DateTimeToJson, JsonToDateTime);
			_serializer.CustomSerializations.RegisterType(DateTimeToJson2, JsonToDateTime2);
			_serializer.CustomSerializations.RegisterType(ActionTypeToJson, JsonToActionType);
			InitializeAbstractionMap(_serializer);
			_method = _serializer.GetType().GetTypeInfo().DeclaredMethods
			                     .First(m => m.Name == nameof(Serialize))
			                     .GetGenericMethodDefinition();
		}

		/// <summary>
		/// Serializes an object to JSON.
		/// </summary>
		/// <param name="obj">The object to serialize.</param>
		/// <returns>An equivalent JSON string.</returns>
		public string Serialize(object obj)
		{
			var method = _method.MakeGenericMethod(obj.GetType());
			var json = method.Invoke(_serializer, new[] {obj});
			var text = json.ToString();
			return text;
		}
		/// <summary>
		/// Attempts to deserialize a RESTful response to the indicated type.
		/// </summary>
		/// <typeparam name="T">The type of object expected.</typeparam>
		/// <param name="response">The response object which contains the JSON to deserialize.</param>
		/// <returns>The requested object, if JSON is valid; null otherwise.</returns>
		public T Deserialize<T>(IRestResponse<T> response)
		{
			var json = JsonValue.Parse(response.Content);
			T obj = _serializer.Deserialize<T>(json);
			return obj;
		}
		/// <summary>
		/// Attempts to deserialize a RESTful response to the indicated type.
		/// </summary>
		/// <typeparam name="T">The type of object expected.</typeparam>
		/// <param name="content">A string which contains the JSON to deserialize.</param>
		/// <returns>The requested object, if JSON is valid; null otherwise.</returns>
		public T Deserialize<T>(string content)
		{
			var json = JsonValue.Parse(content);
			T obj = _serializer.Deserialize<T>(json);
			return obj;
		}

		private static void InitializeAbstractionMap(JsonSerializer serializer)
		{
			serializer.AbstractionMap.Map<IJsonAction, ManateeAction>();
			serializer.AbstractionMap.Map<IJsonActionData, ManateeActionData>();
			serializer.AbstractionMap.Map<IJsonActionOldData, ManateeActionOldData>();
			serializer.AbstractionMap.Map<IJsonAttachment, ManateeAttachment>();
			serializer.AbstractionMap.Map<IJsonBadges, ManateeBadges>();
			serializer.AbstractionMap.Map<IJsonBoard, ManateeBoard>();
			serializer.AbstractionMap.Map<IJsonBoardBackground, ManateeBoardBackground>();
			serializer.AbstractionMap.Map<IJsonBoardMembership, ManateeBoardMembership>();
			serializer.AbstractionMap.Map<IJsonBoardPersonalPreferences, ManateeBoardPersonalPreferences>();
			serializer.AbstractionMap.Map<IJsonBoardPreferences, ManateeBoardPreferences>();
			serializer.AbstractionMap.Map<IJsonBoardVisibilityRestrict, ManateeBoardVisibilityRestrict>();
			serializer.AbstractionMap.Map<IJsonCard, ManateeCard>();
			serializer.AbstractionMap.Map<IJsonCheckItem, ManateeCheckItem>();
			serializer.AbstractionMap.Map<IJsonCheckList, ManateeCheckList>();
			serializer.AbstractionMap.Map<IJsonCustomField, ManateeCustomField>();
			serializer.AbstractionMap.Map<IJsonCustomDropDownOption, ManateeCustomDropDownOption>();
			serializer.AbstractionMap.Map<IJsonCustomFieldDefinition, ManateeCustomFieldDefinition>();
			serializer.AbstractionMap.Map<IJsonImagePreview, ManateeImagePreview>();
			serializer.AbstractionMap.Map<IJsonLabel, ManateeLabel>();
			serializer.AbstractionMap.Map<IJsonList, ManateeList>();
			serializer.AbstractionMap.Map<IJsonMember, ManateeMember>();
			serializer.AbstractionMap.Map<IJsonMemberSearch, ManateeMemberSearch>();
			serializer.AbstractionMap.Map<IJsonMemberPreferences, ManateeMemberPreferences>();
			serializer.AbstractionMap.Map<IJsonNotification, ManateeNotification>();
			serializer.AbstractionMap.Map<IJsonNotificationData, ManateeNotificationData>();
			serializer.AbstractionMap.Map<IJsonNotificationOldData, ManateeNotificationOldData>();
			serializer.AbstractionMap.Map<IJsonOrganization, ManateeOrganization>();
			serializer.AbstractionMap.Map<IJsonOrganizationMembership, ManateeOrganizationMembership>();
			serializer.AbstractionMap.Map<IJsonOrganizationPreferences, ManateeOrganizationPreferences>();
			serializer.AbstractionMap.Map<IJsonParameter, ManateeParameter>();
			serializer.AbstractionMap.Map<IJsonPosition, ManateePosition>();
			serializer.AbstractionMap.Map<IJsonPowerUp, ManateePowerUp>();
			serializer.AbstractionMap.Map<IJsonPowerUpData, ManateePowerUpData>();
			serializer.AbstractionMap.Map<IJsonSearch, ManateeSearch>();
			serializer.AbstractionMap.Map<IJsonSticker, ManateeSticker>();
			serializer.AbstractionMap.Map<IJsonToken, ManateeToken>();
			serializer.AbstractionMap.Map<IJsonTokenPermission, ManateeTokenPermission>();
			serializer.AbstractionMap.Map<IJsonWebhook, ManateeWebhook>();
			serializer.AbstractionMap.Map<IJsonWebhookNotification, ManateeWebhookNotification>();
		}

		private static JsonValue DateTimeToJson(DateTime? date, JsonSerializer serializer)
		{
			var dateString = date?.ToUniversalTime()
			                     .ToString("yyyy-MM-ddTHH:mm:ss.fffZ") ?? JsonValue.Null;

			return dateString;
		}

		private static DateTime? JsonToDateTime(JsonValue json, JsonSerializer serializer)
		{
			var dateString = json.String;
			if (DateTime.TryParseExact(dateString, "yyyy-MM-ddTHH:mm:ss.fffZ", CultureInfo.InvariantCulture, DateTimeStyles.AssumeUniversal, out var date))
			{
				var localDate = date.ToLocalTime();

				return localDate;
			}

			return null;
		}

		private static JsonValue DateTimeToJson2(DateTime date, JsonSerializer serializer)
		{
			var dateString = date.ToUniversalTime()
			                     .ToString("yyyy-MM-ddTHH:mm:ss.fffZ");

			return dateString;
		}

		private static DateTime JsonToDateTime2(JsonValue json, JsonSerializer serializer)
		{
			var dateString = json.String;
			if (DateTime.TryParseExact(dateString, "yyyy-MM-ddTHH:mm:ss.fffZ", CultureInfo.InvariantCulture, DateTimeStyles.AssumeUniversal, out var date))
			{
				var localDate = date.ToLocalTime();

				return localDate;
			}

			return DateTime.MinValue;
		}

		private static JsonValue ActionTypeToJson(ActionType? actionType, JsonSerializer serializer)
		{
			return actionType?.ToString();
		}

		private static ActionType? JsonToActionType(JsonValue json, JsonSerializer serializer)
		{
			return ActionType.TryParse(json.String, out var actionType) ? actionType : (ActionType?) null;
		}
	}
}
