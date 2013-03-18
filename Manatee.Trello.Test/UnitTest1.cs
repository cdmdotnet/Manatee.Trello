/***************************************************************************************

	Copyright 2013 Little Crab Solutions

	   Licensed under the Apache License, Version 2.0 (the "License");
	   you may not use this file except in compliance with the License.
	   You may obtain a copy of the License at

		 http://www.apache.org/licenses/LICENSE-2.0

	   Unless required by applicable law or agreed to in writing, software
	   distributed under the License is distributed on an "AS IS" BASIS,
	   WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
	   See the License for the specific language governing permissions and
	   limitations under the License.
 
	File Name:		Member.cs
	Namespace:		Manatee.Trello.Test
	Class Name:		Member
	Purpose:		Provides a test method to be used during development of
					Manatee.Trello.

***************************************************************************************/
using System;
using System.Linq;
using Manatee.Trello.Implementation;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;	

namespace Manatee.Trello.Test
{
	// To re-authorize and receive new token (must have login access for the user to approve the token)
	// Other developers working on this should create their own sandbox boards and use their user accounts during the tests
	// https://trello.com/1/authorize?key=062109670e7f56b88783721892f8f66f&name=Manatee.Trello&expiration=1day&response_type=token&scope=read,write
	[TestClass]
	public class UnitTest1
	{
		private const string Key = "062109670e7f56b88783721892f8f66f";
		private const string Token = "";
		private const string UserName = "s_littlecrabsolutions";
		private const string BoardId = "5144051cbd0da6681200201e";
		private const string ListId = "5144051cbd0da6681200201f";
		private const string CardId = "5144066597027bec32001d78";
		private const string CheckListId = "514463bce0807abe320028a2";
		private const string OrganizationId = "50d4eb07a1b0902152003329";
		private const string ActionId = "51446f605061aeb832002655";

		[TestMethod]
		public void GetMember()
		{
			var service = new TrelloService(Key, Token);

			var member = service.Retrieve<Member>(UserName);
			var notifications = member.Notifications;
			var notification = notifications.FirstOrDefault();

			Assert.IsNotNull(notification);
		}
	}
}
