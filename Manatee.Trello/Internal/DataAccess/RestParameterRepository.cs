using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using Manatee.Trello.Json;

namespace Manatee.Trello.Internal.DataAccess
{
	internal static class RestParameterRepository
	{
		private static readonly Dictionary<Type, Dictionary<string, string>> _parameterSets;
		private static readonly Dictionary<Type, Func<string>> _fieldFuncs;

		static RestParameterRepository()
		{
			_parameterSets = new Dictionary<Type, Dictionary<string, string>>
				{
					{
						typeof (IJsonOrganization), new Dictionary<string, string>
							{
								{"fields", "name,displayName,desc,descData,url,website,logoHash,products,powerUps,prefs"},
							}
					},
				};
			_fieldFuncs = new Dictionary<Type, Func<string>>
				{
					[typeof(IJsonCard)] = () => Card.DownloadedFields.GetDescription()
				};
		}

		public static Dictionary<string, string> GetParameters<T>()
		{
			var fieldList = _TryGetFields<T>();
			if (fieldList != null)
				return new Dictionary<string, string> {["fields"] = fieldList};

			var type = typeof (T);
			if (type == typeof (Me))
				type = typeof (Member);
			return _parameterSets.ContainsKey(type)
				       ? _parameterSets[type]
				       : new Dictionary<string, string>();
		}

		private static string _TryGetFields<T>()
		{
			var type = typeof(T);
			Func<string> getKey;
			return !_fieldFuncs.TryGetValue(type, out getKey) ? null : getKey();
		}
	}
}