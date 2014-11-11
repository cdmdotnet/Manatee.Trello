﻿/***************************************************************************************

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
 
	File Name:		TokenContext.cs
	Namespace:		Manatee.Trello.Internal.Synchronization
	Class Name:		TokenContext
	Purpose:		Provides a data context for tokens.

***************************************************************************************/

using System;
using System.Collections.Generic;
using System.Linq;
using Manatee.Trello.Exceptions;
using Manatee.Trello.Internal.Caching;
using Manatee.Trello.Internal.DataAccess;
using Manatee.Trello.Internal.Validation;
using Manatee.Trello.Json;

namespace Manatee.Trello.Internal.Synchronization
{
	internal class TokenContext : SynchronizationContext<IJsonToken>
	{
		private bool _deleted;

		public TokenPermissionContext MemberPermissions { get; private set; }
		public TokenPermissionContext BoardPermissions { get; private set; }
		public TokenPermissionContext OrganizationPermissions { get; private set; }
		public virtual bool HasValidId { get { return IdRule.Instance.Validate(Data.Id, null) == null; } }

		static TokenContext()
		{
			_properties = new Dictionary<string, Property<IJsonToken>>
				{
					{"AppName", new Property<IJsonToken, string>(d => d.Identifier, (d, o) => d.Identifier = o)},
					{
						"Member", new Property<IJsonToken, Member>(d => d.Member == null ? null : d.Member.GetFromCache<Member>(),
						                                   (d, o) => d.Member = o != null ? o.Json : null)
					},
					{"DateCreated", new Property<IJsonToken, DateTime?>(d => d.DateCreated, (d, o) => d.DateCreated = o)},
					{"DateExpires", new Property<IJsonToken, DateTime?>(d => d.DateExpires, (d, o) => d.DateExpires = o)},
					{"Id", new Property<IJsonToken, string>(d => d.Id, (d, o) => d.Id = o)},
				};
		}
		public TokenContext(string id)
		{
			Data.Id = id;
			Data.Permissions = new List<IJsonTokenPermission>();
			MemberPermissions = new TokenPermissionContext();
			MemberPermissions.SynchronizeRequested += () => Synchronize();
			Data.Permissions.Add(MemberPermissions.Data);
			BoardPermissions = new TokenPermissionContext();
			BoardPermissions.SynchronizeRequested += () => Synchronize();
			Data.Permissions.Add(BoardPermissions.Data);
			OrganizationPermissions = new TokenPermissionContext();
			OrganizationPermissions.SynchronizeRequested += () => Synchronize();
			Data.Permissions.Add(OrganizationPermissions.Data);
		}

		public void Delete()
		{
			if (_deleted) return;
			CancelUpdate();

			var endpoint = EndpointFactory.Build(EntityRequestType.Token_Write_Delete, new Dictionary<string, object> { { "_id", Data.Id } });
			JsonRepository.Execute(TrelloAuthorization.Default, endpoint);

			_deleted = true;
		}

		protected override IJsonToken GetData()
		{
			try
			{
				var endpoint = EndpointFactory.Build(EntityRequestType.Token_Read_Refresh, new Dictionary<string, object> { { "_token", Data.Id } });
				var newData = JsonRepository.Execute<IJsonToken>(TrelloAuthorization.Default, endpoint);

				return newData;
			}
			catch (TrelloInteractionException e)
			{
				if (!e.IsNotFoundError()) throw;
				_deleted = true;
				return Data;
			}
		}
		protected override IEnumerable<string> MergeDependencies(IJsonToken json)
		{
			return MemberPermissions.Merge(json.Permissions.FirstOrDefault(p => p.ModelType == TokenModelType.Member))
			                        .Concat(BoardPermissions.Merge(json.Permissions.FirstOrDefault(p => p.ModelType == TokenModelType.Board)))
			                        .Concat(OrganizationPermissions.Merge(json.Permissions.FirstOrDefault(p => p.ModelType == TokenModelType.Organization)));
		}
		protected override bool CanUpdate()
		{
			return !_deleted;
		}
	}
}