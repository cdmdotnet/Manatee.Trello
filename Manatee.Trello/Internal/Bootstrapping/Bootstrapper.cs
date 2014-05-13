/***************************************************************************************

	Copyright 2013 Greg Dennis

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

using Manatee.Trello.Contracts;
using Manatee.Trello.Exceptions;
using Manatee.Trello.Internal.DataAccess;

namespace Manatee.Trello.Internal.Bootstrapping
{
	internal class Bootstrapper
	{
		public IValidator Validator { get; private set; }
		public IEntityRepository EntityRepository { get; private set; }

		public void Initialize(ITrelloService service, TrelloAuthorization auth)
		{
			if (TrelloServiceConfiguration.RestClientProvider == null)
				throw new MissingRestClientProviderException();
			if (TrelloServiceConfiguration.Serializer == null || TrelloServiceConfiguration.Deserializer == null)
				throw new MissingSerializerException();

			Validator = new Validator(service);

			EntityRepository = new EntityRepository(service.ItemDuration, Validator, auth);
		}
	}
}