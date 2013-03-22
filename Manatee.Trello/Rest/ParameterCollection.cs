using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using RestSharp;

namespace Manatee.Trello.Rest
{
	internal class ParameterCollection : List<Parameter>
	{
		public void Add(string name, object value)
		{
			Add(new Parameter {Name = name, Value = value});
		}
	}
}
