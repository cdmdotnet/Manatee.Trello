/***************************************************************************************

	Copyright 2014 Greg Dennis

	   Licensed under the Apache License, Version 2.0 (the "License");
	   you may not use this file except in compliance with the License.
	   You may obtain a copy of the License at

		 http://www.apache.org/licenses/LICENSE-2.0

	   Unless required by applicable law or agreed to in writing, software
	   distributed under the License is distributed on an "AS IS" BASIS,
	   WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
	   See the License for the specific language governing permissions and
	   limitations under the License.
 
	File Name:		Property.cs
	Namespace:		Manatee.Trello.Internal
	Class Name:		Property
	Purpose:		Provides generic access to properties for the synchronization
					context.

***************************************************************************************/

using System;

namespace Manatee.Trello.Internal
{
	internal class Property<T>
	{
		public Func<T, object> Get { get; private set; }
		public Action<T, object> Set { get; private set; }

		protected Property(Func<T, object> get, Action<T, object> set)
		{
			Get = get;
			Set = set;
		}
	}

	internal class Property<TJson, T> : Property<TJson>
	{
		public Property(Func<TJson, T> get, Action<TJson, T> set)
			: base(j => get(j), (j, o) => set(j, (T) o)) {}
	}
}