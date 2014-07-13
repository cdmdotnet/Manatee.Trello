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
 
	File Name:		ModelScope.cs
	Namespace:		Manatee.Trello
	Class Name:		ModelScope
	Purpose:		Represents the scope of a user token for a given model type.

***************************************************************************************/
using Manatee.Trello.Contracts;

namespace Manatee.Trello
{
	/// <summary>
	/// Represents the scope of a user token for a given model type.
	/// </summary>
	/// <typeparam name="T">The type of the model.</typeparam>
	public class ModelScope<T> where T : ExpiringObject
	{
		private readonly T _model;

		/// <summary>
		/// Defines global access to a particular model type.
		/// </summary>
		public static ModelScope<T> All { get; private set; }
		/// <summary>
		/// Gets the model entity.
		/// </summary>
		public T Model { get { return _model; } }

		static ModelScope()
		{
			All = new ModelScope<T>();
		}
		internal ModelScope(T model)
		{
			_model = model;
		}
		private ModelScope() {}

		/// <summary>
		/// Returns a string that represents the current object.
		/// </summary>
		/// <returns>
		/// A string that represents the current object.
		/// </returns>
		/// <filterpriority>2</filterpriority>
		public override string ToString()
		{
			return ReferenceEquals(this, All) ? "*" : Model.ToString();
		}
	}
}