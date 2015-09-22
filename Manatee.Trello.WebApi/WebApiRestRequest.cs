using System.Collections.Generic;
using System.Linq;
using Manatee.Trello.Rest;

namespace Manatee.Trello.WebApi
{
	public class WebApiRestRequest : IRestRequest
	{
		private readonly Dictionary<string, object> _parameters; 

		public RestMethod Method { get; set; }
		public string Resource { get; set; }
		public IRestResponse Response { get; set; }

		internal Dictionary<string, object> Parameters { get { return _parameters; } } 
		internal object Body { get; private set; }
		internal byte[] File { get; private set; }
		internal string FileName { get; private set; }

		public WebApiRestRequest()
		{
			_parameters = new Dictionary<string, object>();
		}

		public void AddParameter(string name, object value)
		{
			_parameters[name] = value;
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