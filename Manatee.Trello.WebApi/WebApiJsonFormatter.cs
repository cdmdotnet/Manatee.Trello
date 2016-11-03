using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Net.Http;
using System.Net.Http.Formatting;
using System.Net.Http.Headers;
using System.Reflection;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Manatee.Trello.Json;

namespace Manatee.Trello.WebApi
{
	internal class WebApiJsonFormatter : MediaTypeFormatter
	{
		private readonly Dictionary<Type, MethodInfo> _deserializeMethods;

		public WebApiJsonFormatter()
		{
			_deserializeMethods = new Dictionary<Type, MethodInfo>();

			SupportedMediaTypes.Add(new MediaTypeHeaderValue("application/json"));
		}

		public override bool CanReadType(Type type)
		{
			return true;
		}
		public override bool CanWriteType(Type type)
		{
			return true;
		}
		public override Task<object> ReadFromStreamAsync(Type type, Stream readStream, HttpContent content, IFormatterLogger formatterLogger)
		{
			return Task.Run(() => Read(type, readStream));
		}

		public override Task<object> ReadFromStreamAsync(Type type, Stream readStream, HttpContent content, IFormatterLogger formatterLogger, CancellationToken cancellationToken)
		{
			return Task.Run(() => Read(type, readStream), cancellationToken);
		}
		public override Task WriteToStreamAsync(Type type, object value, Stream writeStream, HttpContent content, TransportContext transportContext)
		{
			return Task.Run(() => Write(value, writeStream));
		}
		public override Task WriteToStreamAsync(Type type, object value, Stream writeStream, HttpContent content, TransportContext transportContext, CancellationToken cancellationToken)
		{
			return Task.Run(() => Write(value, writeStream), cancellationToken);
		}

		private object Read(Type type, Stream readStream)
		{
			using (var sr = new StreamReader(readStream))
			{
				return Deserialize(type, sr.ReadToEnd());
			}
		}
		private static void Write(object value, Stream writeStream)
		{
			if (value == null) return;
			var json = TrelloConfiguration.Serializer.Serialize(value);
			var bytes = Encoding.UTF8.GetBytes(json);
			writeStream.Write(bytes, 0, bytes.Length);
		}
		private object Deserialize(Type type, string content)
		{
			var method = GetDeserializeMethod(type);
			return method.Invoke(TrelloConfiguration.Deserializer, new object[] {content});
		}
		private MethodInfo GetDeserializeMethod(Type type)
		{
			MethodInfo method;
			if (!_deserializeMethods.TryGetValue(type, out method))
			{
				method = typeof (IDeserializer).GetMethod("Deserialize", new[] {typeof (string)})
				                               .MakeGenericMethod(type);
				_deserializeMethods[type] = method;
			}
			return method;
		}
	}
}
