using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FluentAssertions;
using Manatee.Trello.Json;
using Manatee.Trello.Tests.Common;
using NUnit.Framework;

namespace Manatee.Trello.UnitTests
{
	[TestFixture]
	public class CardTests
	{
		private readonly TrelloFactory _factory = new TrelloFactory();

		[Test]
		public async Task CardProcessesWebhook()
		{
			List<string> properties = null;
			try
			{
				var cardData = "{\"id\":\"5a72b7ab3711a44643c5ed49\",\"badges\":{\"votes\":0,\"attachmentsByType\":{\"trello\":{\"board\":0,\"card\":0}},\"viewingMemberVoted\":false,\"subscribed\":false,\"fogbugz\":\"\",\"checkItems\":2,\"checkItemsChecked\":1,\"comments\":1,\"attachments\":1,\"description\":true,\"due\":null,\"dueComplete\":false},\"checkItemStates\":[{\"idCheckItem\":\"5aa7a08854b35d7eb0c497db\",\"state\":\"complete\"}],\"closed\":false,\"dueComplete\":false,\"dateLastActivity\":\"2018-05-19T00:06:58.640Z\",\"desc\":\"the original description\",\"descData\":{\"emoji\":{}},\"due\":null,\"email\":null,\"idBoard\":\"51478f6469fd3d9341001dae\",\"idChecklists\":[\"5aa7a081fa623ac86b7856e1\"],\"idList\":\"51478f6469fd3d9341001daf\",\"idMembers\":[\"50b693ad6f122b4310000a3c\"],\"idMembersVoted\":[],\"idShort\":1344,\"idAttachmentCover\":\"5a72b801baac36a68ea5c10b\",\"labels\":[{\"id\":\"5475f8a629b56a928f0b20fa\",\"idBoard\":\"51478f6469fd3d9341001dae\",\"name\":\"Colorless Label\",\"color\":null},{\"id\":\"545b37fa74d650d567d4c15a\",\"idBoard\":\"51478f6469fd3d9341001dae\",\"name\":\"orange\",\"color\":\"orange\"},{\"id\":\"54737da6c22a888c1f5bfa7e\",\"idBoard\":\"51478f6469fd3d9341001dae\",\"name\":\"Other Blue Label\",\"color\":\"blue\"}],\"idLabels\":[\"54737da6c22a888c1f5bfa7e\",\"5475f8a629b56a928f0b20fa\",\"545b37fa74d650d567d4c15a\"],\"manualCoverAttachment\":false,\"name\":\"Card\",\"pos\":163839.5,\"shortLink\":\"3rm0AZg5\",\"shortUrl\":\"https://trello.com/c/3rm0AZg5\",\"subscribed\":false,\"url\":\"https://trello.com/c/3rm0AZg5/1344-card\"}";
				var webhookData = "{\"model\":{\"id\":\"5a72b7ab3711a44643c5ed49\",\"badges\":{\"votes\":0,\"attachmentsByType\":{\"trello\":{\"board\":0,\"card\":0}},\"viewingMemberVoted\":false,\"subscribed\":false,\"fogbugz\":\"\",\"checkItems\":2,\"checkItemsChecked\":1,\"comments\":1,\"attachments\":1,\"description\":true,\"due\":null,\"dueComplete\":false},\"checkItemStates\":[{\"idCheckItem\":\"5aa7a08854b35d7eb0c497db\",\"state\":\"complete\"}],\"closed\":false,\"dueComplete\":false,\"dateLastActivity\":\"2018-05-19T00:06:58.640Z\",\"desc\":\"changing the description to trigger a webhook\\n\",\"descData\":{\"emoji\":{}},\"due\":null,\"email\":null,\"idBoard\":\"51478f6469fd3d9341001dae\",\"idChecklists\":[\"5aa7a081fa623ac86b7856e1\"],\"idList\":\"51478f6469fd3d9341001daf\",\"idMembers\":[\"50b693ad6f122b4310000a3c\"],\"idMembersVoted\":[],\"idShort\":1344,\"idAttachmentCover\":\"5a72b801baac36a68ea5c10b\",\"labels\":[{\"id\":\"5475f8a629b56a928f0b20fa\",\"idBoard\":\"51478f6469fd3d9341001dae\",\"name\":\"Colorless Label\",\"color\":null},{\"id\":\"545b37fa74d650d567d4c15a\",\"idBoard\":\"51478f6469fd3d9341001dae\",\"name\":\"orange\",\"color\":\"orange\"},{\"id\":\"54737da6c22a888c1f5bfa7e\",\"idBoard\":\"51478f6469fd3d9341001dae\",\"name\":\"Other Blue Label\",\"color\":\"blue\"}],\"idLabels\":[\"54737da6c22a888c1f5bfa7e\",\"5475f8a629b56a928f0b20fa\",\"545b37fa74d650d567d4c15a\"],\"manualCoverAttachment\":false,\"name\":\"Card\",\"pos\":163839.5,\"shortLink\":\"3rm0AZg5\",\"shortUrl\":\"https://trello.com/c/3rm0AZg5\",\"subscribed\":false,\"url\":\"https://trello.com/c/3rm0AZg5/1344-card\"},\"action\":{\"id\":\"5aff6aa258f856835962fd6f\",\"idMemberCreator\":\"50b693ad6f122b4310000a3c\",\"data\":{\"list\":{\"name\":\"List\",\"id\":\"51478f6469fd3d9341001daf\"},\"board\":{\"shortLink\":\"VHHdzCU0\",\"name\":\"Sandbox\",\"id\":\"51478f6469fd3d9341001dae\"},\"card\":{\"shortLink\":\"3rm0AZg5\",\"idShort\":1344,\"name\":\"Card\",\"id\":\"5a72b7ab3711a44643c5ed49\",\"desc\":\"changing the description to trigger a webhook\\n\"},\"old\":{\"desc\":\"a description\"}},\"type\":\"updateCard\",\"date\":\"2018-05-19T00:06:58.644Z\",\"limits\":{},\"display\":{\"translationKey\":\"action_changed_description_of_card\",\"entities\":{\"card\":{\"type\":\"card\",\"desc\":\"changing the description to trigger a webhook\\n\",\"id\":\"5a72b7ab3711a44643c5ed49\",\"shortLink\":\"3rm0AZg5\",\"text\":\"Card\"},\"memberCreator\":{\"type\":\"member\",\"id\":\"50b693ad6f122b4310000a3c\",\"username\":\"gregsdennis\",\"text\":\"Greg Dennis\"}}},\"memberCreator\":{\"id\":\"50b693ad6f122b4310000a3c\",\"avatarHash\":\"cfd323494c6c01459001e53c35e88e41\",\"avatarUrl\":\"https://trello-avatars.s3.amazonaws.com/cfd323494c6c01459001e53c35e88e41\",\"fullName\":\"Greg Dennis\",\"initials\":\"GSD\",\"username\":\"gregsdennis\"}}}";

				MockHost.MockRest<IJsonCard>(cardData);

				var card = _factory.Card(TrelloIds.CardId);
				await card.Refresh();

				card.Updated += (c, list) => properties = list.ToList();

				TrelloProcessor.ProcessNotification(webhookData);

				properties.Count.Should().Be(1);
				properties.Should().Contain(new[] {"Description"});
			}
			finally
			{
				if (properties != null)
					Console.WriteLine(string.Join("\n", properties));
				MockHost.ResetRest();
				TrelloConfiguration.Cache.Clear();
			}
		}

		[Test]
		public async Task CardListChangeProvokesUpdateInListCardCollection()
		{
			try
			{
				TrelloConfiguration.EnableConsistencyProcessing = true;

				var cardData = "{\"id\":\"5a72b7ab3711a44643c5ed49\",\"idList\":\"51478f6469fd3d9341001dad\"}";
				var listData = "{\"id\":\"51478f6469fd3d9341001daf\",\"cards\":[{\"id\":\"5a72b7ab3711a44643c5ed50\"}]}";

				MockHost.MockRest<IJsonCard>(cardData);
				MockHost.MockRest<IJsonList>(listData);

				var card = _factory.Card("5a72b7ab3711a44643c5ed49");
				var list = _factory.List("51478f6469fd3d9341001daf");

				await card.Refresh();
				await list.Refresh();

				list.Cards.Count().Should().Be(1);

				card.List = list;

				list.Cards.Count().Should().Be(2);
			}
			finally
			{
				MockHost.ResetRest();
				TrelloConfiguration.EnableConsistencyProcessing = false;
				TrelloConfiguration.Cache.Clear();
			}
		}

		[Test]
		public async Task CardDeleteProvokesRemovalFromListCardCollection()
		{
			try
			{
				TrelloConfiguration.EnableConsistencyProcessing = true;

				var cardData = "{\"id\":\"5a72b7ab3711a44643c5ed49\",\"idList\":\"51478f6469fd3d9341001dad\"}";
				var listData = "{\"id\":\"51478f6469fd3d9341001daf\",\"cards\":[{\"id\":\"5a72b7ab3711a44643c5ed49\"}]}";

				MockHost.MockRest<IJsonCard>(cardData);
				MockHost.MockRest<IJsonList>(listData);

				var card = _factory.Card("5a72b7ab3711a44643c5ed49");
				var list = _factory.List("51478f6469fd3d9341001daf");

				await card.Refresh();
				await list.Refresh();

				list.Cards.Count().Should().Be(1);

				await card.Delete();

				list.Cards.Count().Should().Be(0);
			}
			finally
			{
				MockHost.ResetRest();
				TrelloConfiguration.EnableConsistencyProcessing = false;
				TrelloConfiguration.Cache.Clear();
			}
		}
	}
}