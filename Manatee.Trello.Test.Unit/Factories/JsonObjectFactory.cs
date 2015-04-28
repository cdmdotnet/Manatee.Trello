using System;
using System.Collections.Generic;
using Manatee.Trello.Json;
using Moq;

namespace Manatee.Trello.Test.Unit.Factories
{
	public static class JsonObjectFactory
	{
		private static readonly Dictionary<Type, Func<object>> _factories = new Dictionary<Type, Func<object>>
			{
				{typeof (IJsonAction), Action},
				{typeof (IJsonAttachment), Attachment},
				{typeof (IJsonBoard), Board},
				{typeof (IJsonCard), Card},
			};

		public static Mock<T> Get<T>()
			where T : class
		{
			return _factories.ContainsKey(typeof (T))
					   ? (Mock<T>) _factories[typeof (T)]()
					   : CreateMock<T>();
		}

		private static Mock<IJsonAction> Action()
		{
			var action = CreateMock<IJsonAction>();
			var data = CreateMock<IJsonActionData>();
			data.Object.Attachment = CreateMock<IJsonAttachment>().Object;
			data.Object.Board = CreateMock<IJsonBoard>().Object;
			data.Object.BoardSource = CreateMock<IJsonBoard>().Object;
			data.Object.BoardTarget = CreateMock<IJsonBoard>().Object;
			data.Object.Card = CreateMock<IJsonCard>().Object;
			data.Object.CardSource = CreateMock<IJsonCard>().Object;
			data.Object.CheckItem = CreateMock<IJsonCheckItem>().Object;
			data.Object.CheckList = CreateMock<IJsonCheckList>().Object;
			data.Object.List = CreateMock<IJsonList>().Object;
			data.Object.ListAfter = CreateMock<IJsonList>().Object;
			data.Object.ListBefore = CreateMock<IJsonList>().Object;
			data.Object.Member = CreateMock<IJsonMember>().Object;
			var oldData = CreateMock<IJsonActionOldData>();
			oldData.Object.Closed = false;
			oldData.Object.Desc = "an old description";
			oldData.Object.List = CreateMock<IJsonList>().Object;
			oldData.Object.Pos = 50;
			oldData.Object.Text = "some old text";
			data.Object.Old = oldData.Object;
			data.Object.Org = CreateMock<IJsonOrganization>().Object;
			data.Object.Text = "some new text";
			action.Object.Data = data.Object;
			action.Object.Date = DateTime.Now;
			action.Object.Id = Guid.NewGuid().ToString();
			action.Object.MemberCreator = CreateMock<IJsonMember>().Object;
			action.Object.Type = ActionType.CommentCard;
			return action;
		}
		private static Mock<IJsonAttachment> Attachment()
		{
			var attachment = CreateMock<IJsonAttachment>();
			attachment.Object.Bytes = 256;
			attachment.Object.Date = DateTime.Now;
			attachment.Object.Id = Guid.NewGuid().ToString();
			attachment.Object.IsUpload = false;
			var member = CreateMock<IJsonMember>();
			member.Object.FullName = "member";
			member.Object.Id = Guid.NewGuid().ToString();
			attachment.Object.Member = member.Object;
			attachment.Object.MimeType = "image/png";
			attachment.Object.Name = "attachment 1";
			var preview1 = CreateMock<IJsonImagePreview>();
			preview1.Object.Height = 600;
			preview1.Object.Id = Guid.NewGuid().ToString();
			preview1.Object.Scaled = false;
			preview1.Object.Url = "http://manatee.trello.com/attachmentPreview1";
			preview1.Object.Width = 800;
			var preview2 = CreateMock<IJsonImagePreview>();
			preview2.Object.Height = 300;
			preview2.Object.Id = Guid.NewGuid().ToString();
			preview2.Object.Scaled = true;
			preview2.Object.Url = "http://manatee.trello.com/attachmentPreview2";
			preview2.Object.Width = 400;
			attachment.Object.Previews = new List<IJsonImagePreview> {preview1.Object, preview2.Object};
			attachment.Object.Url = "http://manatee.trello.com/attachment";
			return attachment;
		}
		private static Mock<IJsonBoard> Board()
		{
			var board = CreateMock<IJsonBoard>();
			board.Object.Closed = false;
			board.Object.Desc = "a board description";
			board.Object.Id = Guid.NewGuid().ToString();
			//var labels = CreateMock<IJsonLabelNames>();
			//labels.Object.Green = "green label";
			//labels.Object.Yellow = "yellow label";
			//labels.Object.Orange = "orange label";
			//labels.Object.Red = "red label";
			//labels.Object.Blue = "blue label";
			//labels.Object.Purple = "purple label";
			//board.Object.LabelNames = labels.Object;
			board.Object.Name = "board name";
			var org = CreateMock<IJsonOrganization>();
			board.Object.Organization = org.Object;
			//var personalPrefs = Configure<IJsonBoardPersonalPreferences>(mock);
			var prefs = CreateMock<IJsonBoardPreferences>();
			prefs.Object.CardCovers = true;
			prefs.Object.Comments = BoardCommentPermission.Public;
			prefs.Object.Invitations = BoardInvitationPermission.Admins;
			prefs.Object.PermissionLevel = BoardPermissionLevel.Public;
			prefs.Object.SelfJoin = false;
			prefs.Object.Voting = BoardVotingPermission.Public;
			board.Object.Prefs = prefs.Object;
			board.Object.Subscribed = true;
			board.Object.Url = "http://manatee.trello.com/board";
			return board;
		}
		private static Mock<IJsonCard> Card()
		{
			var card = CreateMock<IJsonCard>();
			var badges = CreateMock<IJsonBadges>();
			badges.Object.Attachments = 1;
			badges.Object.CheckItems = 2;
			badges.Object.CheckItemsChecked = 0;
			badges.Object.Comments = 3;
			badges.Object.Description = true;
			badges.Object.Due = DateTime.Now;
			badges.Object.Subscribed = false;
			badges.Object.ViewingMemberVoted = true;
			badges.Object.Votes = 4;
			card.Object.Badges = badges.Object;
			var board = CreateMock<IJsonBoard>();
			board.Object.Id = Guid.NewGuid().ToString();
			board.Object.Name = "board name";
			card.Object.Board = board.Object;
			card.Object.Closed = false;
			card.Object.DateLastActivity = DateTime.Now;
			card.Object.Desc = "card description";
			card.Object.Due = DateTime.Now;
			card.Object.Id = Guid.NewGuid().ToString();
			card.Object.IdAttachmentCover = Guid.NewGuid().ToString();
			card.Object.IdShort = 14;
			card.Object.Labels = new List<IJsonLabel>();
			var list = CreateMock<IJsonList>();
			list.Object.Id = Guid.NewGuid().ToString();
			list.Object.Name = "list name";
			card.Object.List = list.Object;
			card.Object.ManualCoverAttachment = false;
			card.Object.Name = "card name";
			var pos = CreateMock<IJsonPosition>();
			pos.SetupGet(p => p.Explicit)
			   .Returns(30);
			card.Object.Pos = pos.Object;
			card.Object.ShortUrl = "http://manatee.trello.com/card";
			card.Object.Subscribed = true;
			card.Object.Url = "http://manatee.trello.com/card";
			return card;
		}

		private static Mock<T> CreateMock<T>()
			where T : class
		{
			var retVal = new Mock<T>();
			retVal.SetupAllProperties();
			return retVal;
		}
	}
}