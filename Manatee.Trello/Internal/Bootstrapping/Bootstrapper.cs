/***************************************************************************************

	Copyright 2012 Greg Dennis

	   Licensed under the Apache License, Version 2.0 (the "License");
	   you may not use this file except in compliance with the License.
	   You may obtain a copy of the License at

		 http://www.apache.org/licenses/LICENSE-2.0

	   Unless required by applicable law or agreed to in writing, software
	   distributed under the License is distributed on an "AS IS" BASIS,
	   WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
	   See the License for the specific language governing permissions and
	   limitations under the License.
 
	File Name:		Bootstrapper.cs
	Namespace:		Manatee.Trello.Internal.Bootstrapping
	Class Name:		Bootstrapper
	Purpose:		Instantiates and initializes all dependencies for TrelloService.

***************************************************************************************/

using System.Collections.Generic;
using Manatee.Trello.Contracts;
using Manatee.Trello.Internal.DataAccess;
using Manatee.Trello.Internal.Genesis;
using Manatee.Trello.Internal.RequestProcessing;

namespace Manatee.Trello.Internal.Bootstrapping
{
	internal class Bootstrapper
	{
		public Queue<QueuableRestRequest> RequestQueue { get; private set; }
		public INetworkMonitor NetworkMonitor { get; private set; }
		public IRestRequestProcessor RequestProcessor { get; private set; }
		public IJsonRepository JsonRepository { get; private set; }
		public IValidator Validator { get; private set; }
		public IEntityFactory EntityFactory { get; private set; }
		public IEntityRepository EntityRepository { get; private set; }
		public IEndpointFactory EndpointFactory { get; private set; }

		public void Initialize(ITrelloService service, ITrelloServiceConfiguration config, TrelloAuthorization auth)
		{
			EndpointFactory = new EndpointFactory();
			RequestQueue = new Queue<QueuableRestRequest>();
			NetworkMonitor = RequestProcessing.NetworkMonitor.Default;
			RequestProcessor = new RestRequestProcessor(config.Log, RequestQueue, config.RestClientProvider, NetworkMonitor, auth.AppKey, auth.UserToken);
			JsonRepository = new JsonRepository(RequestProcessor, config.RestClientProvider.RequestProvider);
			Validator = new Validator(config.Log, service, RequestProcessor);
			EntityFactory = new EntityFactory(config.Log, Validator);
			EntityRepository = new EntityRepository(JsonRepository, EndpointFactory, EntityFactory, config.ItemDuration);
			if (config.Cache != null)
			{
				EntityRepository = new CachingEntityRepository(EntityRepository, config.Cache);
			}
		}
	}
}