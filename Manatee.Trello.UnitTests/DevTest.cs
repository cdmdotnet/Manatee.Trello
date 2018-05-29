using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Threading;
using System.Threading.Tasks;
using Manatee.Trello.Rest;
using Manatee.Trello.Tests.Common;
using Moq;
using NUnit.Framework;

namespace Manatee.Trello.UnitTests
{
	[TestFixture]
	//[Ignore("This test fixture for development purposes only.")]
	public class DevTest
	{
		private readonly TrelloFactory _factory = new TrelloFactory();

		[Test]
		public async Task TestMethod1()
		{
			await Run(async ct =>
			{
				var card = _factory.Card(TrelloIds.CardId);
				await card.Refresh(ct);

				//var webhook = await Webhook<ICard>.Create(card, "https://webhook.site/a980d5d4-cdaf-407b-b486-b0c74b553894", ct: ct);
				//var webhook = new Webhook<ICard>("5aff6a4505cae24096bd2487");

				//Console.WriteLine(webhook.Id);

				var content = "{\"model\":{\"id\":\"5a72b7ab3711a44643c5ed49\",\"badges\":{\"votes\":0,\"attachmentsByType\":{\"trello\":{\"board\":0,\"card\":0}},\"viewingMemberVoted\":false,\"subscribed\":false,\"fogbugz\":\"\",\"checkItems\":2,\"checkItemsChecked\":1,\"comments\":1,\"attachments\":1,\"description\":true,\"due\":null,\"dueComplete\":false},\"checkItemStates\":[{\"idCheckItem\":\"5aa7a08854b35d7eb0c497db\",\"state\":\"complete\"}],\"closed\":false,\"dueComplete\":false,\"dateLastActivity\":\"2018-05-19T00:06:58.640Z\",\"desc\":\"changing the description to trigger a webhook\\n\",\"descData\":{\"emoji\":{}},\"due\":null,\"email\":null,\"idBoard\":\"51478f6469fd3d9341001dae\",\"idChecklists\":[\"5aa7a081fa623ac86b7856e1\"],\"idList\":\"51478f6469fd3d9341001daf\",\"idMembers\":[\"50b693ad6f122b4310000a3c\"],\"idMembersVoted\":[],\"idShort\":1344,\"idAttachmentCover\":\"5a72b801baac36a68ea5c10b\",\"labels\":[{\"id\":\"5475f8a629b56a928f0b20fa\",\"idBoard\":\"51478f6469fd3d9341001dae\",\"name\":\"Colorless Label\",\"color\":null},{\"id\":\"545b37fa74d650d567d4c15a\",\"idBoard\":\"51478f6469fd3d9341001dae\",\"name\":\"orange\",\"color\":\"orange\"},{\"id\":\"54737da6c22a888c1f5bfa7e\",\"idBoard\":\"51478f6469fd3d9341001dae\",\"name\":\"Other Blue Label\",\"color\":\"blue\"}],\"idLabels\":[\"54737da6c22a888c1f5bfa7e\",\"5475f8a629b56a928f0b20fa\",\"545b37fa74d650d567d4c15a\"],\"manualCoverAttachment\":false,\"name\":\"Card\",\"pos\":163839.5,\"shortLink\":\"3rm0AZg5\",\"shortUrl\":\"https://trello.com/c/3rm0AZg5\",\"subscribed\":false,\"url\":\"https://trello.com/c/3rm0AZg5/1344-card\"},\"action\":{\"id\":\"5aff6aa258f856835962fd6f\",\"idMemberCreator\":\"50b693ad6f122b4310000a3c\",\"data\":{\"list\":{\"name\":\"List\",\"id\":\"51478f6469fd3d9341001daf\"},\"board\":{\"shortLink\":\"VHHdzCU0\",\"name\":\"Sandbox\",\"id\":\"51478f6469fd3d9341001dae\"},\"card\":{\"shortLink\":\"3rm0AZg5\",\"idShort\":1344,\"name\":\"Card\",\"id\":\"5a72b7ab3711a44643c5ed49\",\"desc\":\"changing the description to trigger a webhook\\n\"},\"old\":{\"desc\":\"a description\"}},\"type\":\"updateCard\",\"date\":\"2018-05-19T00:06:58.644Z\",\"limits\":{},\"display\":{\"translationKey\":\"action_changed_description_of_card\",\"entities\":{\"card\":{\"type\":\"card\",\"desc\":\"changing the description to trigger a webhook\\n\",\"id\":\"5a72b7ab3711a44643c5ed49\",\"shortLink\":\"3rm0AZg5\",\"text\":\"Card\"},\"memberCreator\":{\"type\":\"member\",\"id\":\"50b693ad6f122b4310000a3c\",\"username\":\"gregsdennis\",\"text\":\"Greg Dennis\"}}},\"memberCreator\":{\"id\":\"50b693ad6f122b4310000a3c\",\"avatarHash\":\"cfd323494c6c01459001e53c35e88e41\",\"avatarUrl\":\"https://trello-avatars.s3.amazonaws.com/cfd323494c6c01459001e53c35e88e41\",\"fullName\":\"Greg Dennis\",\"initials\":\"GSD\",\"username\":\"gregsdennis\"}}}";

				card.Updated += (c, list) => OutputCollection("properties", list);
				TrelloProcessor.ProcessNotification(content);
			});
		}

		private static async Task Run(Func<CancellationToken, Task> action)
		{
			TrelloAuthorization.Default.AppKey = TrelloIds.AppKey;
			//TrelloAuthorization.Default.UserToken = TrelloIds.UserToken;

			await action(CancellationToken.None);

			await TrelloProcessor.Flush();
		}

		private static void OutputCollection<T>(string section, IEnumerable<T> collection)
		{
			Console.WriteLine();
			Console.WriteLine(section);
			foreach (var item in collection)
			{
				Console.WriteLine($"    {item}");
			}
		}
	}
}
