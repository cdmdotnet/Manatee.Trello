﻿/***************************************************************************************

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
 
	File Name:		IJsonParameter.cs
	Namespace:		Manatee.Trello.Json
	Class Name:		IJsonParameter
	Purpose:		Defines the JSON structure for a single-value parameter.

***************************************************************************************/

namespace Manatee.Trello.Json
{
	/// <summary>
	/// Defines the JSON structure for a single-value parameter.
	/// </summary>
	public interface IJsonParameter
	{
		/// <summary>
		/// Gets or sets the parameter value;
		/// </summary>
		[JsonSerialize(IsRequired = true)]
		string Value { get; set; } 
	}
}