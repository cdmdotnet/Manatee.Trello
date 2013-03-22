using System;
using System.Collections.Generic;
using System.Linq;
using Manatee.Trello.Implementation;
using RestSharp;

namespace Manatee.Trello.Rest
{
	internal class Request<T> : RequestBase
		where T : ExpiringObject
	{
		public ExpiringObject ParameterSource { get; private set; }

		public Request()
			: base(GetPath()) {}
		public Request(ExpiringObject obj)
			: this(obj.Id)
		{
			ParameterSource = obj;
		}
		public Request(string id)
			: base(GetPathWithId())
		{
			AddParameter("id", id, ParameterType.UrlSegment);
		}
		public Request(IEnumerable<ExpiringObject> tokens, ExpiringObject entity = null, string urlExtension = null)
			: base(GetPath(tokens, urlExtension))
		{
			ParameterSource = entity;
		}

		public void AddParameters()
		{
			if (ParameterSource == null) return;
			foreach (var parameter in ParameterSource.Parameters)
			{
				AddParameter(parameter.Name, parameter.Value);
			}
			ParameterSource.Parameters.Clear();
		}

		private static string GetPath()
		{
			var section = SectionStrings[typeof(T)];
			return string.Format("{0}", section);
		}
		private static string GetPathWithId()
		{
			var section = SectionStrings[typeof (T)];
			return string.Format("{0}/{{id}}", section);
		}
		private static string GetPath(IEnumerable<ExpiringObject> tokens, string urlExtension)
		{
			var segments = new List<string>();
			foreach (var token in tokens)
			{
				segments.Add(SectionStrings[token.GetType()]);
				if (token.Id != null)
					segments.Add(token.Id);
			}
			if (urlExtension != null)
				segments.Add(urlExtension);
			return string.Join("/", segments);
		}
	}
}