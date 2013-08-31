using System;
using System.Text;
using System.Collections.Generic;
using System.Linq;
using Manatee.Trello.ManateeJson;
using Manatee.Trello.RestSharp;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Manatee.Trello.Test.Functional
{
	[TestClass]
	public class ListTest
	{
		[TestMethod]
		public void RetrieveAndModify()
		{
			var options = new TrelloServiceConfiguration();
			var serializer = new ManateeSerializer();
			options.Serializer = serializer;
			options.Deserializer = serializer;
			options.RestClientProvider = new RestSharpClientProvider(options);
			var auth = new TrelloAuthorization(TrelloIds.AppKey, TrelloIds.UserToken);
			var service = new TrelloService(options, auth);
			var list = service.Retrieve<List>(TrelloIds.ListId);
			var oldName = list.Name;
			var oldPos = list.Position;
			var oldSubscribed = list.IsSubscribed;
			try
			{
				const string name = "Manatee.Trello.ListTest.RetrieveAndModify";
				var board = list.Board;
				
				Assert.AreNotEqual(0, list.Actions.Count());
				Assert.AreEqual(TrelloIds.BoardId, board.Id);
				Assert.AreNotEqual(0, list.Cards);
				Assert.AreEqual(false, list.IsClosed);

				list.Name = name;
				list.Position = Position.Bottom;
				list.IsSubscribed = !list.IsSubscribed;

				list.MarkForUpdate();

				Assert.AreEqual(name, list.Name);
				Assert.AreEqual(list, board.Lists.OrderBy(l => l.Position).Last());
				Assert.AreNotEqual(oldSubscribed, list.IsSubscribed);

				list.IsClosed = true;

				list.MarkForUpdate();
	
				Assert.AreEqual(true, list.IsClosed);
			}
			finally
			{
				list.IsClosed = false;

				list.MarkForUpdate();

				Assert.AreEqual(false, list.IsClosed);

				list.Name = oldName;
				list.Position = oldPos;
				list.IsSubscribed = oldSubscribed;

				list.MarkForUpdate();

				Assert.AreEqual(oldName, list.Name);
				// It's not necessarily the case that the same value is populated in this field.
				//Assert.AreEqual(oldPos, list.Position);
				Assert.AreEqual(false, list.IsSubscribed);
			}
		}
	}
}
