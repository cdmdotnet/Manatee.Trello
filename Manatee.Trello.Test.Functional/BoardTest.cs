using System;
using System.Text;
using System.Collections.Generic;
using System.Linq;
using Manatee.Trello.ManateeJson;
using Manatee.Trello.RestSharp;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Manatee.Trello.Test.Functional
{
	[TestClass]
	public class BoardTest
	{
		[TestMethod]
		public void RetrieveAndModify()
		{
			var options = new TrelloServiceConfiguration();
			var serializer = new ManateeSerializer();
			options.Serializer = serializer;
			options.Deserializer = serializer;
			options.RestClientProvider = new RestSharpClientProvider(options);
			var auth = new TrelloAuthorization(TrelloIds.AppKey, TrelloIds.UserToken);
			var service = new TrelloService(options, auth);
			var board = service.Retrieve<Board>(TrelloIds.BoardId);
			var me = service.Me;
			Member member = null;
			var oldName = board.Name;
			var oldOrg = board.Organization;
			var oldDesc = board.Description;
			//var oldPinned = board.IsPinned;
			var oldSubscribed = board.IsSubscribed;
			var oldRedLabel = board.LabelNames.Red;
			var oldOrangeLabel = board.LabelNames.Orange;
			var oldYellowLabel = board.LabelNames.Yellow;
			var oldGreenLabel = board.LabelNames.Green;
			var oldBlueLabel = board.LabelNames.Blue;
			var oldPurpleLabel = board.LabelNames.Purple;
			var oldPrefSelfJoin = board.Preferences.AllowsSelfJoin;
			var oldPrefComments = board.Preferences.Comments;
			var oldPrefInvitations = board.Preferences.Invitations;
			var oldPrefPermission = board.Preferences.PermissionLevel;
			var oldPrefCovers = board.Preferences.ShowCardCovers;
			var oldPrefVoting = board.Preferences.Voting;
			var oldPrefListGuide = board.PersonalPreferences.ShowListGuide;
			var oldPrefSideBar = board.PersonalPreferences.ShowSidebar;
			var oldPrefActivity = board.PersonalPreferences.ShowSidebarActivity;
			var oldPrefActions = board.PersonalPreferences.ShowSidebarBoardActions;
			var oldPrefMembers = board.PersonalPreferences.ShowSidebarMembers;
			try
			{
				const string name = "Manatee.Trello.BoardTest.RetrieveAndModify";
				const string desc = "a new description set by the test";
				const string red = "red";
				const string orange = "orange";
				const string yellow = "yellow";
				const string green = "green";
				const string blue = "blue";
				const string purple = "purple";

				Assert.AreNotEqual(0, board.Actions);
				Assert.AreNotEqual(0, board.ArchivedCards);
				Assert.AreNotEqual(0, board.ArchivedLists);
				Assert.AreEqual(false, board.IsClosed);
				Assert.AreEqual(true, board.IsPinned);
				Assert.AreEqual(true, board.IsSubscribed);

				board.Name = name;
				board.Description = desc;
				//board.IsPinned = false;
				board.IsSubscribed = false;
				board.LabelNames.Red = red;
				board.LabelNames.Orange = orange;
				board.LabelNames.Yellow = yellow;
				board.LabelNames.Green = green;
				board.LabelNames.Blue = blue;
				board.LabelNames.Purple = purple;
				member = board.AddOrUpdateMember("littlecrab@notarealdomain.com", "Little Crab 2");
				board.Preferences.AllowsSelfJoin = true;
				board.Preferences.Comments = BoardCommentType.Disabled;
				board.Preferences.Invitations = BoardInvitationType.Members;
				board.Preferences.PermissionLevel = BoardPermissionLevelType.Org;
				board.Preferences.ShowCardCovers = false;
				board.Preferences.Voting = BoardVotingType.Disabled;
				board.PersonalPreferences.ShowListGuide = true;
				board.PersonalPreferences.ShowSidebar = true;
				board.PersonalPreferences.ShowSidebarActivity = false;
				board.PersonalPreferences.ShowSidebarBoardActions = false;
				board.PersonalPreferences.ShowSidebarMembers = false;

				board.MarkForUpdate();
				board.Preferences.MarkForUpdate();
				board.PersonalPreferences.MarkForUpdate();

				Assert.AreEqual(name, board.Name);
				Assert.AreEqual(desc, board.Description);
				//Assert.AreEqual(false, board.IsPinned);
				Assert.AreEqual(false, board.IsSubscribed);
				Assert.AreEqual(red, board.LabelNames.Red);
				Assert.AreEqual(orange, board.LabelNames.Orange);
				Assert.AreEqual(yellow, board.LabelNames.Yellow);
				Assert.AreEqual(green, board.LabelNames.Green);
				Assert.AreEqual(blue, board.LabelNames.Blue);
				Assert.AreEqual(purple, board.LabelNames.Purple);
				//Assert.IsFalse(board.Memberships.Select(m => m.Member).Contains(member));
				Assert.AreEqual(true, board.Preferences.AllowsSelfJoin);
				Assert.AreEqual(BoardCommentType.Disabled, board.Preferences.Comments);
				Assert.AreEqual(BoardInvitationType.Members, board.Preferences.Invitations);
				Assert.AreEqual(BoardPermissionLevelType.Org, board.Preferences.PermissionLevel);
				Assert.AreEqual(false, board.Preferences.ShowCardCovers);
				Assert.AreEqual(BoardVotingType.Disabled, board.Preferences.Voting);
				Assert.AreEqual(true, board.PersonalPreferences.ShowListGuide);
				Assert.AreEqual(true, board.PersonalPreferences.ShowSidebar);
				Assert.AreEqual(false, board.PersonalPreferences.ShowSidebarActivity);
				Assert.AreEqual(false, board.PersonalPreferences.ShowSidebarBoardActions);
				Assert.AreEqual(false, board.PersonalPreferences.ShowSidebarMembers);

				board.Organization = null;

				board.MarkForUpdate();

				Assert.IsNull(board.Organization);

				board.IsClosed = true;

				board.MarkForUpdate();
				me.BoardsList.MarkForUpdate();

				Assert.AreEqual(true, board.IsClosed);
				//Assert.IsFalse(me.Boards.Contains(board));
			}
			finally
			{
				board.IsClosed = false;

				board.MarkForUpdate();
				me.BoardsList.MarkForUpdate();

				Assert.AreEqual(false, board.IsClosed);

				board.Organization = oldOrg;

				board.MarkForUpdate();

				Assert.AreEqual(oldOrg, board.Organization);

				board.Name = oldName;
				board.Description = oldDesc;
				//board.IsPinned = oldPinned;
				board.IsSubscribed = oldSubscribed;
				board.LabelNames.Red = oldRedLabel;
				board.LabelNames.Orange = oldOrangeLabel;
				board.LabelNames.Yellow = oldYellowLabel;
				board.LabelNames.Green = oldGreenLabel;
				board.LabelNames.Blue = oldBlueLabel;
				board.LabelNames.Purple = oldPurpleLabel;
				if (member != null)
					board.RemoveMember(member);
				board.Preferences.PermissionLevel = oldPrefPermission;
				board.Preferences.AllowsSelfJoin = oldPrefSelfJoin;
				board.Preferences.Comments = oldPrefComments;
				board.Preferences.Invitations = oldPrefInvitations;
				board.Preferences.ShowCardCovers = oldPrefCovers;
				board.Preferences.Voting = oldPrefVoting;
				board.PersonalPreferences.ShowListGuide = oldPrefListGuide;
				board.PersonalPreferences.ShowSidebar = oldPrefSideBar;
				board.PersonalPreferences.ShowSidebarActivity = oldPrefActivity;
				board.PersonalPreferences.ShowSidebarBoardActions = oldPrefActions;
				board.PersonalPreferences.ShowSidebarMembers = oldPrefMembers;

				board.MarkForUpdate();
				board.Preferences.MarkForUpdate();
				board.PersonalPreferences.MarkForUpdate();

				Assert.AreEqual(oldName, board.Name);
				Assert.AreEqual(oldOrg, board.Organization);
				Assert.IsTrue(me.Boards.Contains(board));
				Assert.AreEqual(oldDesc, board.Description);
				//Assert.AreEqual(oldPinned, board.IsPinned);
				Assert.AreEqual(oldSubscribed, board.IsSubscribed);
				Assert.AreEqual(oldRedLabel, board.LabelNames.Red);
				Assert.AreEqual(oldOrangeLabel, board.LabelNames.Orange);
				Assert.AreEqual(oldYellowLabel, board.LabelNames.Yellow);
				Assert.AreEqual(oldGreenLabel, board.LabelNames.Green);
				Assert.AreEqual(oldBlueLabel, board.LabelNames.Blue);
				Assert.AreEqual(oldPurpleLabel, board.LabelNames.Purple);
				Assert.AreEqual(oldPrefSelfJoin, board.Preferences.AllowsSelfJoin);
				Assert.AreEqual(oldPrefComments, board.Preferences.Comments);
				Assert.AreEqual(oldPrefInvitations, board.Preferences.Invitations);
				Assert.AreEqual(oldPrefPermission, board.Preferences.PermissionLevel);
				Assert.AreEqual(oldPrefCovers, board.Preferences.ShowCardCovers);
				Assert.AreEqual(oldPrefVoting, board.Preferences.Voting);
				Assert.AreEqual(oldPrefListGuide, board.PersonalPreferences.ShowListGuide);
				Assert.AreEqual(oldPrefSideBar, board.PersonalPreferences.ShowSidebar);
				Assert.AreEqual(oldPrefActivity, board.PersonalPreferences.ShowSidebarActivity);
				Assert.AreEqual(oldPrefActions, board.PersonalPreferences.ShowSidebarBoardActions);
				Assert.AreEqual(oldPrefMembers, board.PersonalPreferences.ShowSidebarMembers);
			}
		}
	}
}
