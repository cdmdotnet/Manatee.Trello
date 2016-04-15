/***************************************************************************************

	Copyright 2016 Greg Dennis

	   Licensed under the Apache License, Version 2.0 (the "License");
	   you may not use this file except in compliance with the License.
	   You may obtain a copy of the License at

		 http://www.apache.org/licenses/LICENSE-2.0

	   Unless required by applicable law or agreed to in writing, software
	   distributed under the License is distributed on an "AS IS" BASIS,
	   WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
	   See the License for the specific language governing permissions and
	   limitations under the License.
 
	File Name:		WebApiTextFormatter.cs
	Namespace:		Manatee.Trello.WebApi
	Class Name:		WebApiTextFormatter
	Purpose:		Provides a formatter for error messages from Trello.

***************************************************************************************/

using System;
using System.IO;
using System.Net.Http;
using System.Net.Http.Formatting;
using System.Net.Http.Headers;
using System.Threading;
using System.Threading.Tasks;

namespace Manatee.Trello.WebApi
{
	internal class WebApiTextFormatter : MediaTypeFormatter
	{
		public WebApiTextFormatter()
		{
			SupportedMediaTypes.Add(new MediaTypeHeaderValue("application/text"));
			SupportedMediaTypes.Add(new MediaTypeHeaderValue("text/html"));
			SupportedMediaTypes.Add(new MediaTypeHeaderValue("text/plain"));
		}

		public override bool CanReadType(Type type)
		{
			return true;
		}
		public override bool CanWriteType(Type type)
		{
			return false;
		}

		public override Task<object> ReadFromStreamAsync(Type type, Stream readStream, HttpContent content, IFormatterLogger formatterLogger)
		{
			return Task.Run(() => Log(readStream));
		}

		public override Task<object> ReadFromStreamAsync(Type type, Stream readStream, HttpContent content, IFormatterLogger formatterLogger, CancellationToken cancellationToken)
		{
			return Task.Run(() => Log(readStream), cancellationToken);
		}

		private static object Log(Stream readStream)
		{
			string message;
			using (var sr = new StreamReader(readStream))
			{
				message = sr.ReadToEnd();
			}
			TrelloConfiguration.Log.Error(new HttpRequestException($"Trello reported an error: '{message}'"));
			// This should never happen since the line above should throw an exception.
			return null;
		}
	}
}