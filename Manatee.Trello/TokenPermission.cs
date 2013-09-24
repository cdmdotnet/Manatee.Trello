/***************************************************************************************

	Copyright 2013 Little Crab Solutions

	   Licensed under the Apache License, Version 2.0 (the "License");
	   you may not use this file except in compliance with the License.
	   You may obtain a copy of the License at

		 http://www.apache.org/licenses/LICENSE-2.0

	   Unless required by applicable law or agreed to in writing, software
	   distributed under the License is distributed on an "AS IS" BASIS,
	   WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
	   See the License for the specific language governing permissions and
	   limitations under the License.
 
	File Name:		TokenPermission.cs
	Namespace:		Manatee.Trello
	Class Name:		TokenPermission
	Purpose:		Represents permissions granted to a particular entity type
					by a user token.

***************************************************************************************/
using System.Linq;
using Manatee.Trello.Contracts;
using Manatee.Trello.Internal;
using Manatee.Trello.Json;

namespace Manatee.Trello
{
	/// <summary>
	/// Non-generic base class to hold static members.  Performs no other function.
	/// </summary>
	public abstract class TokenPermission
	{
		private static readonly OneToOneMap<TokenModelType, string> _typeMap;

		internal static OneToOneMap<TokenModelType, string> TypeMap { get { return _typeMap; } }

		static TokenPermission()
		{
			_typeMap = new OneToOneMap<TokenModelType, string>
			           	{
			           		{TokenModelType.Board, "Board"},
			           		{TokenModelType.Member, "Member"},
			           		{TokenModelType.Organization, "Organization"},
			           	};
		}
	}

	/// <summary>
	/// Represents permissions granted to a particular entity type by a user token.
	/// </summary>
	/// <typeparam name="T"></typeparam>
	public class TokenPermission<T> : TokenPermission
		where T : ExpiringObject
	{
		private readonly IJsonTokenPermission _jsonTokenPermission;
		private readonly TokenModelType _modelType = TokenModelType.Unknown;
		private readonly ModelScope<T> _scope;

		/// <summary>
		/// Gets whether read access is granted by a token.
		/// </summary>
		public bool? CanRead { get { return _jsonTokenPermission.Read; } }
		/// <summary>
		/// Gets whether write access is granted by a token.
		/// </summary>
		public bool? CanWrite { get { return _jsonTokenPermission.Write; } }
		/// <summary>
		/// Gets the type of the model.
		/// </summary>
		public TokenModelType ModelType { get { return _modelType; } }
		/// <summary>
		/// Gets the model, if well-defined, to which a token grants permissions.
		/// </summary>
		public ModelScope<T> Scope { get { return _scope; } }

		internal TokenPermission(IJsonTokenPermission jsonTokenPermission, T model)
		{
			_jsonTokenPermission = jsonTokenPermission;
			_scope = (model != null) ? new ModelScope<T>(model) : ModelScope<T>.All;
			_modelType = TypeMap.Any(kvp => kvp.Value == _jsonTokenPermission.ModelType)
							? TypeMap[_jsonTokenPermission.ModelType]
							: TokenModelType.Unknown;
		}
	}
}