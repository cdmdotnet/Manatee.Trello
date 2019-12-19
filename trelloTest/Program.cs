using System;
using System.IO;
using System.Linq;
using Manatee.Trello;
namespace trelloTest
{
    class Program
    {
        static async System.Threading.Tasks.Task Main(string[] args)
        {
            ITrelloFactory _factory;
            IBoard board;
            _factory = new TrelloFactory();

            //String your board id
            String boardId = "8OgbO1Xt";
            board = _factory.Board(boardId, util.auth); //util.auth add your app key and token
            await board.Lists.Refresh();
            var backlog = board.Lists.FirstOrDefault(l => l.Name == "asdf");
            var card = await backlog.Cards.Add("attachment test");

        
            var attachment = await card.Attachments.AddAttachment(@"C:\Users\Kavtech\Desktop\Shoaib.docx", "test");
        }
    }
}
