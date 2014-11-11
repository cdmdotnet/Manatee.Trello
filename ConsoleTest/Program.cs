using System;
using System.Linq;
using Manatee.Trello;
using Manatee.Trello.ManateeJson;
using Manatee.Trello.RestSharp;
using Manatee.Trello.Test;

namespace ConsoleTest
{
	class Program
	{
		static void Main(string[] args)
		{
			Run(() =>
			{
				TrelloProcessor.WaitForPendingRequests = true;

				var board = new Board(TrelloIds.BoardId);
				Console.WriteLine(Member.Me.Email);
				var list = board.Lists.FirstOrDefault();
				var card = list.Cards.Add("new card");
				Console.WriteLine(card);
				card.Description = "a new description";
				card.Labels.Add(LabelColor.Blue);

				TrelloProcessor.Shutdown();
			});
		}

		private static void Run(System.Action action)
		{
			var serializer = new ManateeSerializer();
			TrelloConfiguration.Serializer = serializer;
			TrelloConfiguration.Deserializer = serializer;
			TrelloConfiguration.JsonFactory = new ManateeFactory();
			TrelloConfiguration.RestClientProvider = new RestSharpClientProvider();

			TrelloAuthorization.Default.AppKey = TrelloIds.AppKey;
			TrelloAuthorization.Default.UserToken = TrelloIds.UserToken;

			TrelloConfiguration.ThrowOnTrelloError = true;

			action();
		}
	}
}
