using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Net.Http;
using System.Net.Http.Formatting;
using System.Net.Http.Headers;
using System.Reflection;
using System.Threading;
using System.Threading.Tasks;
using Manatee.Trello.Json;

namespace Manatee.Trello.WebApi
{
	public class WebApiFormatter : MediaTypeFormatter
	{
		private readonly Dictionary<Type, MethodInfo> _deserializeMethods;

		public WebApiFormatter()
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
			return Task.Run(() =>
				{
					using (var sr = new StreamReader(readStream))
					{
						return Deserialize(type, sr.ReadToEnd());
					}
				});
		}

		public override Task<object> ReadFromStreamAsync(Type type, Stream readStream, HttpContent content, IFormatterLogger formatterLogger, CancellationToken cancellationToken)
		{
			return Task.Run(() =>
				{
					using (var sr = new StreamReader(readStream))
					{
						return Deserialize(type, sr.ReadToEnd());
					}
				}, cancellationToken);
		}
		public override Task WriteToStreamAsync(Type type, object value, Stream writeStream, HttpContent content, TransportContext transportContext)
		{
			return Task.Run(() =>
				{
					if (value == null) return;
					using (var sw = new StreamWriter(writeStream))
					{
						var json = TrelloConfiguration.Serializer.Serialize(value);
						sw.Write(json);
					}
				});
		}
		public override Task WriteToStreamAsync(Type type, object value, Stream writeStream, HttpContent content, TransportContext transportContext, CancellationToken cancellationToken)
		{
			return Task.Run(() =>
				{
					if (value == null) return;
					using (var sw = new StreamWriter(writeStream))
					{
						var json = TrelloConfiguration.Serializer.Serialize(value);
						sw.Write(json);
					}
				}, cancellationToken);
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
