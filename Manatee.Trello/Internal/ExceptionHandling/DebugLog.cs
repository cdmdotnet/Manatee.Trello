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
 
	File Name:		DebugLog.cs
	Namespace:		Manatee.Trello.Internal.ExceptionHandling
	Class Name:		DebugLog
	Purpose:		Implements ILog, writing messages only to the debug window.

***************************************************************************************/

using System;
using System.Text;
using Manatee.Trello.Contracts;

namespace Manatee.Trello.Internal.ExceptionHandling
{
	internal class DebugLog : ILog
	{
		public void Debug(string message, params object[] parameters)
		{
			var output = string.Format(string.Format("Debug: {0}", message), parameters);
			Post(output);
		}
		public void Info(string message, params object[] parameters)
		{
			var output = string.Format(string.Format("Info: {0}", message), parameters);
			Post(output);
		}
		public void Warning(string message, params object[] parameters)
		{
			var output = string.Format(string.Format("Warning: {0}", message), parameters);
			Post(output);
		}
		public void Warning(Exception e)
		{
			var output = BuildMessage(string.Format("Warning: An exception of type {0} occurred:", e.GetType().Name),
			                          e.Message,
			                          e.StackTrace);
			Post(output);
		}
		public void Error(string message, params object[] parameters)
		{
			var output = string.Format(string.Format("Error: {0}", message), parameters);
			Post(output);
		}
		public void Error(Exception e, bool shouldThrow = true)
		{
			var output = BuildMessage(string.Format("Error: An exception of type {0} occurred:", e.GetType().Name),
									  e.Message,
									  e.StackTrace);
			Post(output);
			if (shouldThrow)
				throw e;
		}

		private static string BuildMessage(params string[] lines)
		{
			var sb = new StringBuilder();
			foreach (var line in lines)
			{
				sb.AppendLine(line);
			}
			return sb.ToString();
		}
		private static void Post(string output)
		{
			System.Diagnostics.Debug.WriteLine(output);
		}
	}
}