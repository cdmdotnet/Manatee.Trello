using System;
using System.Text;
using System.Collections.Generic;
using System.Linq;
using Manatee.Trello.Internal;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Manatee.Trello.Test.FunctionalTests
{
	[TestClass]
	public class OrganizationTest
	{
		[TestMethod]
		public void CreateModifyAndDestroy()
		{
			Organization organization = null;
			var service = new TrelloService(TrelloIds.AppKey, TrelloIds.UserToken);
			var me = service.Me;
			if (me == null)
				Assert.Inconclusive(string.Format("Could not find list with ID = {{{0}}}", TrelloIds.ListId));
			try
			{
				const string organizationName = "Manatee.Trello CreateOrganization";
				const string newOrganizationName = "Manatee.Trello CreateOrganization updated";
				const string organizationDescription =
					"This is a test organization created by the Manatee.Trello functional test suite.";
				const string organizationWebsite = "http://www.manateeopensource.org";
				const string secondMember = "gregsdennis";
				organization = me.CreateOrganization(organizationName);

				Assert.IsNotNull(organization);
				Assert.AreEqual(organizationName, organization.DisplayName);
				Assert.AreNotEqual(0, organization.Actions.Count());
				Assert.AreEqual(0, organization.Boards.Count());
				Assert.AreEqual(0, organization.InvitedMembers.Count());
				Assert.AreEqual(1, organization.Memberships.Count());
				Assert.AreEqual(me, organization.Memberships.Select(m => m.Member).First());
				Assert.IsTrue(organization.IsPaidAccount.In(false, (bool?) null));

				organization.DisplayName = newOrganizationName;
				organization.Description = organizationDescription;
				organization.Website = organizationWebsite;
				var member = service.Retrieve<Member>(secondMember);
				if (member == null)
					Assert.Inconclusive(string.Format("Could not find list with ID = {{{0}}}", secondMember));
				organization.AddOrUpdateMember(member);

				Assert.AreEqual(newOrganizationName, organization.DisplayName);
				Assert.AreEqual(organizationDescription, organization.Description);
				Assert.AreEqual(organizationWebsite, organization.Website);
				Assert.IsTrue(organization.Memberships.Any(m => m.Member == member));

				organization.RemoveMember(member);

				Assert.IsFalse(organization.Memberships.Any(m => m.Member == member));
			}
			finally
			{
				if (organization != null)
				{
					organization.Delete();
					Assert.IsFalse(me.Organizations.Contains(organization));
				}
			}
		}
	}
}
