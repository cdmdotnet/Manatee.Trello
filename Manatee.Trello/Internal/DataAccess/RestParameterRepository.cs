using System;
using System.Collections.Generic;
using Manatee.Trello.Json;

namespace Manatee.Trello.Internal.DataAccess
{
	internal static class RestParameterRepository
	{
		private static readonly Dictionary<Type, Dictionary<string, string>> _parameterSets;

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
		}

		public static Dictionary<string, string> GetParameters<T>()
		{
			var type = typeof (T);
			if (type == typeof (Me))
				type = typeof (Member);
			return _parameterSets.ContainsKey(type)
				       ? _parameterSets[type]
				       : new Dictionary<string, string>();
		}
	}
}