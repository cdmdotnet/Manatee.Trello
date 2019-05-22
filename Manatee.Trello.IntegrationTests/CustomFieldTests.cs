using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using FluentAssertions;
using Manatee.Trello.Tests.Common;
using NUnit.Framework;
using DateTime = System.DateTime;

namespace Manatee.Trello.IntegrationTests
{
	[TestFixture]
	public class CustomFieldTests
	{
		[Test]
		public Task NumberFieldsHaveNames()
		{
			return TestEnvironment.RunClean(async () =>
				{
					await TestEnvironment.Current.Board.EnsurePowerUp(new CustomFieldsPowerUp());

					var card = await TestEnvironment.Current.BuildCard();
					var fieldDef = await TestEnvironment.Current.Board.CustomFields.Add($"{nameof(NumberFieldsHaveNames)}TooYouKnow", CustomFieldType.Number);

					var field = await fieldDef.SetValueForCard(card, 9.54);

					Assert.AreEqual($"{nameof(NumberFieldsHaveNames)}TooYouKnow", field.Definition.Name);
					Assert.AreEqual(9.54, field.Value);
				});
		}
		[Test]
		public Task TextFieldsHaveNames()
		{
			return TestEnvironment.RunClean(async () =>
				{
					await TestEnvironment.Current.Board.EnsurePowerUp(new CustomFieldsPowerUp());

					var card = await TestEnvironment.Current.BuildCard();
					var fieldDef = await TestEnvironment.Current.Board.CustomFields.Add($"{nameof(TextFieldsHaveNames)}TooYouKnow", CustomFieldType.Text);

					var field = await fieldDef.SetValueForCard(card, "test");

					Assert.AreEqual($"{nameof(TextFieldsHaveNames)}TooYouKnow", field.Definition.Name);
					Assert.AreEqual("test", field.Value);
				});
		}
		[Test]
		public Task DateTimeFieldsHaveNames()
		{
			return TestEnvironment.RunClean(async () =>
				{
					await TestEnvironment.Current.Board.EnsurePowerUp(new CustomFieldsPowerUp());

					var card = await TestEnvironment.Current.BuildCard();
					var fieldDef = await TestEnvironment.Current.Board.CustomFields.Add($"{nameof(DateTimeFieldsHaveNames)}TooYouKnow", CustomFieldType.DateTime);

					var field = await fieldDef.SetValueForCard(card, DateTime.Today);

					Assert.AreEqual($"{nameof(DateTimeFieldsHaveNames)}TooYouKnow", field.Definition.Name);
					Assert.AreEqual(DateTime.Today, field.Value);
				});
		}
		[Test]
		public Task CheckboxFieldsHaveNames()
		{
			return TestEnvironment.RunClean(async () =>
				{
					await TestEnvironment.Current.Board.EnsurePowerUp(new CustomFieldsPowerUp());

					var card = await TestEnvironment.Current.BuildCard();
					var fieldDef = await TestEnvironment.Current.Board.CustomFields.Add($"{nameof(CheckboxFieldsHaveNames)}TooYouKnow", CustomFieldType.CheckBox);

					var field = await fieldDef.SetValueForCard(card, true);

					Assert.AreEqual($"{nameof(CheckboxFieldsHaveNames)}TooYouKnow", field.Definition.Name);
					Assert.AreEqual(true, field.Value);
				});
		}
		[Test]
		public Task DropDownFieldsHaveNames()
		{
			return TestEnvironment.RunClean(async () =>
				{
					await TestEnvironment.Current.Board.EnsurePowerUp(new CustomFieldsPowerUp());

					var card = await TestEnvironment.Current.BuildCard();
					var selection = DropDownOption.Create("test1");
					var fieldDef = await TestEnvironment.Current.Board.CustomFields.Add($"{nameof(DropDownFieldsHaveNames)}TooYouKnow", CustomFieldType.DropDown,
					                                                                    CancellationToken.None, selection, DropDownOption.Create("test2"));
					selection = fieldDef.Options.FirstOrDefault(o => o.Text == selection.Text);
					var field = await fieldDef.SetValueForCard(card, selection);

					Assert.AreEqual($"{nameof(DropDownFieldsHaveNames)}TooYouKnow", field.Definition.Name);
					Assert.AreEqual(selection.Text, field.Value.Text);
				});
		}

		[Test]
		public async Task BoardRefreshWithCardsGetsCustomTextFieldDefinitionName()
		{
			await TestEnvironment.Current.Board.EnsurePowerUp(new CustomFieldsPowerUp());
			var card = await TestEnvironment.Current.BuildCard();
			var fieldDef = await TestEnvironment.Current.Board.CustomFields.Add(nameof(BoardRefreshWithCardsGetsCustomTextFieldDefinitionName), CustomFieldType.Text);
			var field = await fieldDef.SetValueForCard(card, "test");
			field.Definition.Name.Should().Be(nameof(BoardRefreshWithCardsGetsCustomTextFieldDefinitionName));

			await card.Refresh();
			card.CustomFields[0].Definition.Name.Should().Be(nameof(BoardRefreshWithCardsGetsCustomTextFieldDefinitionName));

			await TestEnvironment.RunClean(async () =>
				{
					try
					{
						Board.DownloadedFields |= Board.Fields.Cards;
						var board = TestEnvironment.Current.Factory.Board(TestEnvironment.Current.Board.Id);
						await board.Refresh();

						board.Cards[card.Id].CustomFields[0].Definition.Name.Should().Be(nameof(BoardRefreshWithCardsGetsCustomTextFieldDefinitionName));
					}
					finally
					{
						Board.DownloadedFields &= ~Board.Fields.Cards;
					}
				});
		}

		[Test]
		public async Task BoardRefreshWithCardsGetsCustomNumberFieldDefinitionName()
		{
			await TestEnvironment.Current.Board.EnsurePowerUp(new CustomFieldsPowerUp());
			var card = await TestEnvironment.Current.BuildCard();
			var fieldDef = await TestEnvironment.Current.Board.CustomFields.Add(nameof(BoardRefreshWithCardsGetsCustomNumberFieldDefinitionName), CustomFieldType.Number);
			var field = await fieldDef.SetValueForCard(card, 4.5);
			field.Definition.Name.Should().Be(nameof(BoardRefreshWithCardsGetsCustomNumberFieldDefinitionName));

			await card.Refresh();
			card.CustomFields[0].Definition.Name.Should().Be(nameof(BoardRefreshWithCardsGetsCustomNumberFieldDefinitionName));

			await TestEnvironment.RunClean(async () =>
				{
					try
					{
						Board.DownloadedFields |= Board.Fields.Cards;
						var board = TestEnvironment.Current.Factory.Board(TestEnvironment.Current.Board.Id);
						await board.Refresh();

						board.Cards[card.Id].CustomFields[0].Definition.Name.Should().Be(nameof(BoardRefreshWithCardsGetsCustomNumberFieldDefinitionName));
					}
					finally
					{
						Board.DownloadedFields &= ~Board.Fields.Cards;
					}
				});
		}

	}
}
