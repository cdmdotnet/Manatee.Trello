using System;
using Manatee.Trello;
using Manatee.Trello.ManateeJson;
using Manatee.Trello.WebApi;

namespace NugetTest
{
    class Program
    {
        static void Main(string[] args)
        {
            var serializer = new ManateeSerializer();
            TrelloConfiguration.Serializer = serializer;
            TrelloConfiguration.Deserializer = serializer;
            TrelloConfiguration.JsonFactory = new ManateeFactory();
            TrelloConfiguration.RestClientProvider = new WebApiClientProvider();
            TrelloAuthorization.Default.AppKey = "062109670e7f56b88783721892f8f66f";
            TrelloAuthorization.Default.UserToken = "eb16c70a145d75e5ef75bdbf465ee494ac4efca148e4efcfc41060c53beb450a";

            var card = new Card("ar2EsVVg");

            Console.WriteLine(card);
            Console.WriteLine(card.Description);

            Console.ReadLine();
        }
    }
}
