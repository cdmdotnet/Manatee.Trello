using System.Collections.Generic;

namespace Manatee.Trello.Rest
{
	internal class WebApiRestRequest : IRestRequest
	{
		public RestMethod Method { get; set; }
		public string Resource { get; set; }
		public IRestResponse Response { get; set; }

		internal Dictionary<string, object> Parameters { get; }
		internal object Body { get; private set; }
		internal byte[] File { get; private set; }
		internal string FileName { get; private set; }

		public WebApiRestRequest()
		{
			Parameters = new Dictionary<string, object>();
		}

		public void AddParameter(string name, object value)
		{
			Parameters[name] = value;
		}
		public void AddBody(object body)
		{
			Body = body;
		}

		public void AddFile(string key, byte[] contentBytes, string fileName)
		{
			File = contentBytes;
			FileName = fileName;
		}
	}
}