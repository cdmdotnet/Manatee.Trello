using System;
using System.Text;
using System.Collections.Generic;
using System.Linq;
using Manatee.Trello.Internal;
using Manatee.Trello.ManateeJson;
using Manatee.Trello.RestSharp;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Manatee.Trello.Test.Functional
{
	[TestClass]
	public class OrganizationTest
	{
		[TestMethod]
		public void CreateModifyAndDestroy()
		{
			Organization organization = null;
			var options = new TrelloServiceConfiguration();
			var serializer = new ManateeSerializer();
			options.Serializer = serializer;
			options.Deserializer = serializer;
			options.RestClientProvider = new RestSharpClientProvider(options);
			var auth = new TrelloAuthorization(TrelloIds.AppKey, TrelloIds.UserToken);
			var service = new TrelloService(options, auth);
			var me = service.Me;
			if (me == null)
				Assert.Inconclusive("Could not find member through 'Me' call.");
			try
			{
				const string organizationName = "Manatee.Trello CreateOrganization";
				const string newOrganizationName = "Manatee.Trello CreateOrganization updated";
				const string organizationDescription =
					"This is a test organization created by the Manatee.Trello functional test suite.";
				const string organizationWebsite = "http://www.manateeopensource.org";
				const string secondMember = "littlecrab@notarealdomain.com";
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
				//var member = organization.AddOrUpdateMember(secondMember, "Little Crab 2");

				//Assert.AreEqual(newOrganizationName, organization.DisplayName);
				//Assert.AreEqual(organizationDescription, organization.Description);
				//Assert.AreEqual(organizationWebsite, organization.Website);
				//Assert.IsTrue(organization.Memberships.Any(m => m.Member == member));

				//organization.RemoveMember(member);

				//Assert.IsFalse(organization.Memberships.Any(m => m.Member == member));
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
