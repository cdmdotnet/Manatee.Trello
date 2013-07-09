using System;
using System.Text;
using System.Collections.Generic;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Manatee.Trello.Test.FunctionalTests
{
	[TestClass]
	public class MemberTest
	{
		[TestMethod]
		public void RetrieveAndModify()
		{
			var service = new TrelloService(TrelloIds.AppKey, TrelloIds.UserToken);
			var member = service.Retrieve<Member>(TrelloIds.MemberId);
			Organization organization = null;
			//var oldUserName = member.Username;
			var oldBio = member.Bio;
			var oldFullName = member.FullName;
			var oldInitials = member.Initials;
			var oldMinutesBetweenSummaries = member.Preferences.MinutesBetweenSummaries;
			var oldColorBlind = member.Preferences.ColorBlind;
			try
			{
				//const string userName = "manateetrello_membertest_retrieveandmodify";
				const string bio = "This is a new bio.";
				const string fullName = "New Full Name";
				const string initials = "NFN";
				const MemberPreferenceSummaryPeriodType minutesBetweenSummaries = MemberPreferenceSummaryPeriodType.OneHour;
				const bool colorBlind = true;

				//member.Username = userName;
				member.Bio = bio;
				member.FullName = fullName;
				member.Initials = initials;

				member.MarkForUpdate();

				//Assert.AreEqual(userName, member.Username);
				Assert.AreEqual(bio, member.Bio);
				Assert.AreEqual(fullName, member.FullName);
				Assert.AreEqual(initials, member.Initials);

				const string organizationName = "Manatee.Trello CreateOrganization";
				organization = member.CreateOrganization(organizationName);

				Assert.IsNotNull(organization);
				Assert.AreEqual(organizationName, organization.DisplayName);
				Assert.IsTrue(member.Organizations.Contains(organization));

				member.Preferences.MinutesBetweenSummaries = minutesBetweenSummaries;
				member.Preferences.ColorBlind = colorBlind;

				Assert.AreEqual(minutesBetweenSummaries, member.Preferences.MinutesBetweenSummaries);
				Assert.AreEqual(colorBlind, member.Preferences.ColorBlind);
			}
			finally
			{
				//member.Username = oldUserName;
				member.Bio = oldBio;
				member.FullName = oldFullName;
				member.Initials = oldInitials;

				member.MarkForUpdate();

				//Assert.AreEqual(oldUserName, member.Username);
				Assert.AreEqual(oldBio, member.Bio);
				Assert.AreEqual(oldFullName, member.FullName);
				Assert.AreEqual(oldInitials, member.Initials);

				if (organization != null)
				{
					organization.Delete();
					Assert.IsFalse(member.Organizations.Contains(organization));
				}

				member.Preferences.MinutesBetweenSummaries = oldMinutesBetweenSummaries;
				member.Preferences.ColorBlind = oldColorBlind;

				Assert.AreEqual(oldMinutesBetweenSummaries, member.Preferences.MinutesBetweenSummaries);
				Assert.AreEqual(oldColorBlind, member.Preferences.ColorBlind);
			}
		}
	}
}
