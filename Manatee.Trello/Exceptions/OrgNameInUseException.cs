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
 
	File Name:		OrgNameInUseException.cs
	Namespace:		Manatee.Trello.Exceptions
	Class Name:		OrgNameInUseException
	Purpose:		Thrown when an attempt is made to set an organization's name
					to a name which already belongs to an existing organization
					on Trello.

***************************************************************************************/
using System;

namespace Manatee.Trello.Exceptions
{
	/// <summary>
	/// Thrown when an attempt is made to set an organization's name to a name which
	/// already belongs to an existing organization on Trello.
	/// </summary>
	public class OrgNameInUseException : Exception
	{
		/// <summary>
		/// The name which is already in use.
		/// </summary>
		public string Name { get; private set; }

		/// <summary>
		/// Creates a new instance of the OrgNameInUseException
		/// </summary>
		/// <param name="name">The name which is in use.</param>
		public OrgNameInUseException(string name)
		{
			Name = name;
		}
	}
}