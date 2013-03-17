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
 
	File Name:		Options.cs
	Namespace:		Manatee.Trello
	Class Name:		Options
	Purpose:		Exposes a set of run-time options for the Manatee.Trello project.

***************************************************************************************/
using System;

namespace Manatee.Trello
{
	public static class Options
	{
		public static readonly TimeSpan DefaultItemDuration;

		public static TimeSpan ItemDuration { get; set; }
		public static bool AutoRefresh { get; set; }

		static Options()
		{
			DefaultItemDuration = TimeSpan.FromSeconds(60);
			ItemDuration = DefaultItemDuration;
			AutoRefresh = true;
		}
	}
}
