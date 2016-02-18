/***************************************************************************************

	Copyright 2015 Greg Dennis

	   Licensed under the Apache License, Version 2.0 (the "License");
	   you may not use this file except in compliance with the License.
	   You may obtain a copy of the License at

		 http://www.apache.org/licenses/LICENSE-2.0

	   Unless required by applicable law or agreed to in writing, software
	   distributed under the License is distributed on an "AS IS" BASIS,
	   WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
	   See the License for the specific language governing permissions and
	   limitations under the License.
 
	File Name:		JsonAttributes.cs
	Namespace:		Manatee.Trello.Json
	Class Name:		JsonAttributes
	Purpose:		Attributes for JSON interface properties to declare whether
					the property should be included when serializing and
					deserializing.

***************************************************************************************/

using System;

namespace Manatee.Trello.Json
{
	/// <summary>
	/// Declares that the JSON property should be deserialized.
	/// </summary>
	[AttributeUsage(AttributeTargets.Property)]
	public class JsonDeserializeAttribute : Attribute {}

	/// <summary>
	/// Declares that the JSON property should be serialized and whether it
	/// is optional or required.
	/// </summary>
	[AttributeUsage(AttributeTargets.Property)]
	public class JsonSerializeAttribute : Attribute
	{
		/// <summary>
		/// Gets or sets whether this property is required by the Trello API.
		/// </summary>
		public bool IsRequired { get; set; }
	}

	/// <summary>
	/// Declares that the JSON property has a special serialization.
	/// </summary>
	[AttributeUsage(AttributeTargets.Property)]
	public class JsonSpecialSerializationAttribute : Attribute { }
}