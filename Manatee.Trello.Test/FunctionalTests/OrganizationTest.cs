using System;
using System.Text;
using System.Collections.Generic;
using System.Linq;
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
				Assert.AreEqual(1, organization.Members.Count());
				Assert.AreEqual(me, organization.Members.First());

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
				Assert.IsTrue(organization.Members.Any(m => m == member));

				organization.RemoveMember(member);

				Assert.IsFalse(organization.Members.Any(m => m == member));
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
