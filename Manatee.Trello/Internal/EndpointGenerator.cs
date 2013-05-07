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
 
	File Name:		EndpointGenerator.cs
	Namespace:		Manatee.Trello.Internal
	Class Name:		EndpointGenerator
	Purpose:		Creates the request string based on a list of objects
					submitted to it.

***************************************************************************************/
using System.Collections.Generic;
using Manatee.Trello.Contracts;

namespace Manatee.Trello.Internal
{
	internal class EndpointGenerator
	{
		private static readonly EndpointGenerator _default;

		public static EndpointGenerator Default { get { return _default; } }

		static EndpointGenerator()
		{
			_default = new EndpointGenerator();
		}
		private EndpointGenerator() {}

		public Endpoint Generate(params ExpiringObject[] tokens)
		{
			var segments = new List<string>();
			foreach (var token in tokens)
			{
				segments.Add(token.Key);
				if (token.KeyId != null)
					segments.Add(token.KeyId);
			}
			return new Endpoint(segments);
		}
	}
}
