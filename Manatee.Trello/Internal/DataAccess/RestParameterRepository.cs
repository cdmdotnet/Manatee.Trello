/***************************************************************************************

	Copyright 2013 Greg Dennis

	   Licensed under the Apache License, Version 2.0 (the "License");
	   you may not use this file except in compliance with the License.
	   You may obtain a copy of the License at

		 http://www.apache.org/licenses/LICENSE-2.0

	   Unless required by applicable law or agreed to in writing, software
	   distributed under the License is distributed on an "AS IS" BASIS,
	   WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
	   See the License for the specific language governing permissions and
	   limitations under the License.
 
	File Name:		RestParameterRepository.cs
	Namespace:		Manatee.Trello.Internal.DataAccess
	Class Name:		RestParameterRepository
	Purpose:		Manages default REST parameters for all entity types.

***************************************************************************************/

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