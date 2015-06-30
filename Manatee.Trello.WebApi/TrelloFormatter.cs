using System;
using System.Collections.Generic;
using System.IO;
using System.Net.Http;
using System.Net.Http.Formatting;
using System.Net.Http.Headers;
using System.Reflection;

namespace Manatee.Trello.WebApi
{
	public class TrelloFormatter : BufferedMediaTypeFormatter
	{
		private static readonly Dictionary<Type, MethodInfo> _methodCache;
		private static MethodInfo _baseMethod;
		private static bool _isInitialized;

		static TrelloFormatter()
		{
			_methodCache = new Dictionary<Type, MethodInfo>();
		}
		public TrelloFormatter()
		{
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
		public override object ReadFromStream(Type type, Stream readStream, HttpContent content, IFormatterLogger formatterLogger)
		{
			Initialize();
			using (var reader = new StreamReader(readStream))
			{
				var text = reader.ReadToEnd();
				var value = GetDeserializeMethod(type).Invoke(TrelloConfiguration.Deserializer, new object[] {text});
				return value;
			}
		}
		public override void WriteToStream(Type type, object value, Stream writeStream, HttpContent content)
		{
			using (var writer = new StreamWriter(writeStream))
			{
				var jsonText = TrelloConfiguration.Serializer.Serialize(value);
				writer.Write(jsonText);
			}
		}

		private static void Initialize()
		{
			if (_isInitialized) return;
			_isInitialized = true;
			_baseMethod = TrelloConfiguration.Deserializer.GetType().GetMethod("Deserialize");
		}
		private static MethodInfo GetDeserializeMethod(Type type)
		{
			MethodInfo method;
			if (!_methodCache.TryGetValue(type, out method))
				_methodCache[type] = method = _baseMethod.MakeGenericMethod(type);
			return method;
		}
	}
}
