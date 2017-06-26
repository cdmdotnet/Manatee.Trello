using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using Manatee.Trello.Json;

namespace Manatee.Trello.Internal.DataAccess
{
	internal static class RestParameterRepository
	{
		private static readonly Dictionary<Type, Func<string>> _fieldFuncs;

		static RestParameterRepository()
		{
			_fieldFuncs = new Dictionary<Type, Func<string>>
				{
					[typeof(IJsonAction)] = () => Action.DownloadedFields.GetDescription(),
					[typeof(IJsonAttachment)] = () => Attachment.DownloadedFields.GetDescription(),
					[typeof(IJsonBoard)] = () => Board.DownloadedFields.GetDescription(),
					[typeof(IJsonCard)] = () => Card.DownloadedFields.GetDescription(),
					[typeof(IJsonCheckItem)] = () => CheckItem.DownloadedFields.GetDescription(),
					[typeof(IJsonCheckList)] = () => CheckList.DownloadedFields.GetDescription(),
					[typeof(IJsonLabel)] = () => Label.DownloadedFields.GetDescription(),
					[typeof(IJsonList)] = () => List.DownloadedFields.GetDescription(),
					[typeof(IJsonMember)] = () => Member.DownloadedFields.GetDescription(),
					[typeof(IJsonNotification)] = () => Notification.DownloadedFields.GetDescription(),
					[typeof(IJsonOrganization)] = () => Organization.DownloadedFields.GetDescription(),
					// Search is a special case.  Need to consider...
					//[typeof(IJsonSearch)] = () => Search.DownloadedFields.GetDescription(),
					[typeof(IJsonSticker)] = () => Sticker.DownloadedFields.GetDescription(),
					[typeof(IJsonToken)] = () => Token.DownloadedFields.GetDescription()
				};
		}

		public static Dictionary<string, string> GetParameters<T>()
		{
			var fieldList = _TryGetFields<T>();
			return fieldList != null
				? new Dictionary<string, string> {["fields"] = fieldList}
				: new Dictionary<string, string>();
		}

		private static string _TryGetFields<T>()
		{
			var type = typeof(T);
			if (type.GetTypeInfo().IsGenericType)
			{
				var generic = type.GetGenericTypeDefinition();
				if (generic == typeof(List<>))
					type = type.GetTypeInfo().GenericTypeArguments.First();
			}
			Func<string> getKey;
			if (!_fieldFuncs.TryGetValue(type, out getKey)) return null;
			var key = getKey();
			return key;
		}
	}
}