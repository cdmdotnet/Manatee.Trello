using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Formatting;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;

namespace Manatee.Trello.WebApi
{
	public class TrelloFormatter : BufferedMediaTypeFormatter
	{
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
			return base.ReadFromStream(type, readStream, content, formatterLogger);
		}
		public override void WriteToStream(Type type, object value, Stream writeStream, HttpContent content)
		{
			base.WriteToStream(type, value, writeStream, content);
		}
	}
}
