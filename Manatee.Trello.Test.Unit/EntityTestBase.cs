/***************************************************************************************

	Copyright 2014 Greg Dennis

	   Licensed under the Apache License, Version 2.0 (the "License");
	   you may not use this file except in compliance with the License.
	   You may obtain a copy of the License at

		 http://www.apache.org/licenses/LICENSE-2.0

	   Unless required by applicable law or agreed to in writing, software
	   distributed under the License is distributed on an "AS IS" BASIS,
	   WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
	   See the License for the specific language governing permissions and
	   limitations under the License.
 
	File Name:		EntityTestBase.cs
	Namespace:		Manatee.Trello.Test.Unit.Entities
	Class Name:		EntityTestBase
	Purpose:		Provides base functionality for the entity classes.

***************************************************************************************/

using Manatee.Trello.Contracts;
using Manatee.Trello.Json;
using Manatee.Trello.Rest;
using Manatee.Trello.Test.Unit.Mocks;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;

namespace Manatee.Trello.Test.Unit
{
	public abstract class EntityTestBase<T> : TrelloTestBase
	{
		#region Setup

		protected class DependencyCollection : IDependencyCollection
		{
			public Mock<ISerializer> Serializer { get; private set; }
			public Mock<IDeserializer> Deserializer { get; private set; }
			public Mock<IJsonFactory> JsonFactory { get; private set; }
			public Mock<IRestClientProvider> RestClientProvider { get; private set; }
			public MockRestClient RestClient { get; private set; }
			public Mock<ICache> Cache { get; private set; }
			public Mock<ILog> Log { get; private set; }

			public DependencyCollection()
			{
				Serializer = new Mock<ISerializer>();
				Deserializer = new Mock<IDeserializer>();
				JsonFactory = new Mock<IJsonFactory>();
				RestClientProvider = new Mock<IRestClientProvider>();
				RestClient = new MockRestClient();
				Cache = new Mock<ICache>();
				Log = new Mock<ILog>();
			}

			public void ConfigureDefaultBehavior()
			{
				TrelloConfiguration.Serializer = Serializer.Object;
				TrelloConfiguration.Deserializer = Deserializer.Object;
				TrelloConfiguration.JsonFactory = JsonFactory.Object;
				TrelloConfiguration.RestClientProvider = RestClientProvider.Object;
				TrelloConfiguration.Cache = Cache.Object;
				TrelloConfiguration.Log = Log.Object;

				TrelloAuthorization.Default.AppKey = TrelloIds.AppKey;
				TrelloAuthorization.Default.UserToken = TrelloIds.UserToken;

				RestClientProvider.Setup(p => p.CreateRestClient(It.IsAny<string>()))
				                  .Returns(RestClient);
			}
		}

		#endregion

		#region Test methods

		[TestMethod]
		public void TestMethod1()
		{
			throw new NotImplementedException();
		}

		#endregion

		#region Given


		#endregion

		#region When


		#endregion

		#region Then


		#endregion
	}
}