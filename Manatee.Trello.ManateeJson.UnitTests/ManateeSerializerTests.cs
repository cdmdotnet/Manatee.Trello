using System;
using FluentAssertions;
using Manatee.Json;
using Manatee.Trello.Json;
using Manatee.Trello.Tests.Common;
using NUnit.Framework;

namespace Manatee.Trello.ManateeJson.UnitTests
{
	[TestFixture]
	public class ManateeSerializerTests
	{
		[Test]
		public void ActionIdDeserialized()
		{
			var json = new JsonObject {["id"] = TrelloIds.FakeId};
			IDeserializer serializer = new ManateeSerializer();

			var action = serializer.Deserialize<IJsonAction>(json.ToString());

			action.Id.Should().Be(TrelloIds.FakeId);
		}

		[Test]
		public void ActionDateDeserialized()
		{
			var yesterday = DateTime.Today.AddDays(-1);
			var json = new JsonObject {["date"] = yesterday.ToUniversalTime().ToString("yyyy-MM-ddTHH:mm:ss.fffZ") };
			IDeserializer serializer = new ManateeSerializer();

			var action = serializer.Deserialize<IJsonAction>(json.ToString());

			action.Date.Should().Be(yesterday);
		}

		[Test]
		public void ActionTypeDeserialized()
		{
			var json = new JsonObject {["type"] = ActionType.DeleteAttachmentFromCard.ToString() };
			IDeserializer serializer = new ManateeSerializer();

			var action = serializer.Deserialize<IJsonAction>(json.ToString());

			action.Type.Should().Be(ActionType.DeleteAttachmentFromCard);
		}

		[Test]
		public void ActionTextNotDeserialized()
		{
			var json = new JsonObject {["text"] = "some text here" };
			IDeserializer serializer = new ManateeSerializer();

			var action = serializer.Deserialize<IJsonAction>(json.ToString());

			action.Text.Should().BeNull();
		}
	}
}
