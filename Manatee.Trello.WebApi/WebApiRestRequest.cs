using System.Collections.Generic;
using System.Linq;
using Manatee.Trello.Rest;

namespace Manatee.Trello.WebApi
{
	public class WebApiRestRequest : IRestRequest
	{
		private const string TrelloApiBaseUrl = @"https://api.trello.com/1";

		private readonly Dictionary<string, object> _parameters; 

		public RestMethod Method { get; set; }
		public string Resource { get; set; }
		public IRestResponse Response { get; set; }

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

		internal string GetFullResource()
		{
			return string.Format("{0}/{1}?{2}", TrelloApiBaseUrl, Resource, string.Join("&", _parameters.Select(kvp => string.Format("{0}={1}", kvp.Key, kvp.Value))));
		}
		public void AddFile(string key, byte[] contentBytes, string fileName)
		{
			
		}
	}
}