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
			var member = service.Retrieve<Member>(TrelloIds.MemberId);
			var sb = new StringBuilder();

			sb.AppendLine(member.ToString());
			foreach (var board in member.Boards)
			{
				sb.AppendLine(string.Format("  {0}", board));
				foreach (var list in board.Lists)
				{
					sb.AppendLine(string.Format("    {0}", list));
					foreach (var card in list.Cards)
					{
						sb.AppendLine(string.Format("      {0}", card));
					}
				}
				foreach (var membership in board.Memberships)
				{
					sb.AppendLine(string.Format("    {0}", membership));
				}
			}
			sb.AppendLine();
			foreach (var action in member.Actions)
			{
				sb.AppendLine(action.ToString());
			}

			SpinWait.SpinUntil(() => !member.IsStubbed);

			var end = DateTime.Now;
			Console.WriteLine(sb);
			Console.WriteLine(end - start);
		}

		[TestMethod]
		//[Ignore]
		public void TestMethod2()
		{
			var options = new TrelloServiceConfiguration();
			var serializer = new ManateeSerializer();
			options.Serializer = serializer;
			options.Deserializer = serializer;
			options.RestClientProvider = new RestSharpClientProvider(options);

			var auth = new TrelloAuthorization(TrelloIds.AppKey, TrelloIds.UserToken);
			var service = new TrelloService(options, auth);

			var start = DateTime.Now;
			var list = service.Retrieve<List>(TrelloIds.ListId);
			var card = list.AddCard("this is a new card for TestMethod2");

			Console.WriteLine("Cards in list: {0}", list.Cards.Count());

			SpinWait.SpinUntil(() => !card.IsStubbed);

			Console.WriteLine(list);
			Console.WriteLine(card);
			var end = DateTime.Now;
			Console.WriteLine(end - start);

			card.Delete();
		}
	}
}
