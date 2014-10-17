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
 
	File Name:		ILog.cs
	Namespace:		Manatee.Trello.Contracts
	Class Name:		ILog
	Purpose:		Defines methods required to log information, events, and errors
					generated throughout Manatee.Trello.

***************************************************************************************/
using System;
using JetBrains.Annotations;

namespace Manatee.Trello.Contracts
{
	/// <summary>
	/// Defines methods required to log information, events, and errors generated
	/// throughout Manatee.Trello.
	/// </summary>
	public interface ILog
	{
		/// <summary>
		/// Writes a debug level log entry.
		/// </summary>
		/// <param name="message">The message or message format.</param>
		/// <param name="parameters">A list of parameters.</param>
		void Debug(string message, params object[] parameters);
		/// <summary>
		/// Writes an information level log entry.
		/// </summary>
		/// <param name="message">The message or message format.</param>
		/// <param name="parameters">A list of paramaters.</param>
		void Info(string message, params object[] parameters);
		//void Warning(string message, params object[] parameters);
		//void Warning(Exception e);
		//void Error(string message, params object[] parameters);
		/// <summary>
		/// Writes an error level log entry.
		/// </summary>
		/// <param name="e">The exception that will be or was thrown.</param>
		/// <param name="shouldThrow">true if the exception should be thrown; false otherwise.</param>
		/// <remarks>
		/// Manatee.Trello relies on the logger to throw any exceptions.  Not implmenting this functionality
		/// may result in undesired behavior.
		/// </remarks>
		[ContractAnnotation("shouldThrow:true => halt")]
		void Error(Exception e, bool shouldThrow = true);
	}
}