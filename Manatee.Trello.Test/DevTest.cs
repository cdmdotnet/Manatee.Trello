using System;
using System.Linq;
using System.Text;
using System.Threading;
using Manatee.Trello.ManateeJson;
using Manatee.Trello.RestSharp;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Manatee.Trello.Test
{
	[TestClass]
	public class DevTest
	{
		[TestMethod]
		//[Ignore]
		public void TestMethod1()
		{
			var options = new TrelloServiceConfiguration();
			var serializer = new ManateeSerializer();
			options.Serializer = serializer;
			options.Deserializer = serializer;
			options.RestClientProvider = new RestSharpClientProvider(options);

			var auth = new TrelloAuthorization(TrelloIds.AppKey, TrelloIds.UserToken);
			var service = new TrelloService(options, auth);

			var start = DateTime.Now;
			var member = service.Retrieve<Member>("gregsdennis");

			//Console.WriteLine(member);
			//foreach (var board in member.Boards)
			//{
			//	Console.WriteLine("  {0}", board);
			//	foreach (var list in board.Lists)
			//	{
			//		Console.WriteLine("    {0}", list);
			//		foreach (var card in list.Cards)
			//		{
			//			Console.WriteLine("      {0}", card);
			//			foreach (var checkList in card.CheckLists)
			//			{
			//				Console.WriteLine("        {0}", checkList);
			//				foreach (var checkItem in checkList.CheckItems)
			//				{
			//					Console.WriteLine("          {0}", checkItem);
			//				}
			//			}
			//		}
			//	}
			//}
			foreach (var action in member.Actions)
			{
				Console.WriteLine(action);
			}


			var end = DateTime.Now;
			Console.WriteLine(end - start);
		}
		[TestMethod]
		public void TestMethod2()
		{
			var options = new TrelloServiceConfiguration();
			var serializer = new ManateeSerializer();
			options.Serializer = serializer;
			options.Deserializer = serializer;
			options.RestClientProvider = new RestSharpClientProvider(options);

			var auth = new TrelloAuthorization(TrelloIds.AppKey, TrelloIds.UserToken);
			var service = new TrelloService(options, auth);

			var list = service.Retrieve<List>(TrelloIds.ListId);
			var card = list.AddCard("this card was created by the new Manatee.Trello");
			var checklist = card.AddCheckList("a checklist");
			var checkitem = checklist.AddCheckItem("a new checkItem");
			var checkitem2 = checklist.AddCheckItem("another new checkItem", CheckItemStateType.Complete);
			var member = service.Retrieve<Member>("gregsdennis");
			card.AssignMember(member);

			Console.WriteLine(list);
			Console.WriteLine(card);
			Console.WriteLine(checklist);
			Console.WriteLine(checkitem);
			Console.WriteLine(checkitem2);
			card.Delete();
		}
	}
}
