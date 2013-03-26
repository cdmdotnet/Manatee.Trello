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
 
	File Name:		Parameter.cs
	Namespace:		Manatee.Trello.Rest
	Class Name:		Parameter
	Purpose:		Wraps RestSharp.Parameter in a class which implements
					IRestParameter.

***************************************************************************************/
using Manatee.Trello.Contracts;

namespace Manatee.Trello.Rest
{
	internal class RestSharpParameter : RestSharp.Parameter, IRestParameter { }
}