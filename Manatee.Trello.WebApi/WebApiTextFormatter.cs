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