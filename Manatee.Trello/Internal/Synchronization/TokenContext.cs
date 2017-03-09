﻿using System;
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

		public TokenPermissionContext MemberPermissions { get; }
		public TokenPermissionContext BoardPermissions { get; }
		public TokenPermissionContext OrganizationPermissions { get; }
		public virtual bool HasValidId => IdRule.Instance.Validate(Data.Id, null) == null;

		static TokenContext()
		{
			_properties = new Dictionary<string, Property<IJsonToken>>
				{
					{"AppName", new Property<IJsonToken, string>((d, a) => d.Identifier, (d, o) => d.Identifier = o)},
					{
						"Member", new Property<IJsonToken, Member>((d, a) => d.Member?.GetFromCache<Member>(a),
						                                           (d, o) => d.Member = o?.Json)
					},
					{"DateCreated", new Property<IJsonToken, DateTime?>((d, a) => d.DateCreated, (d, o) => d.DateCreated = o)},
					{"DateExpires", new Property<IJsonToken, DateTime?>((d, a) => d.DateExpires, (d, o) => d.DateExpires = o)},
					{"Id", new Property<IJsonToken, string>((d, a) => d.Id, (d, o) => d.Id = o)},
				};
		}
		public TokenContext(string id, TrelloAuthorization auth)
			: base(auth)
		{
			Data.Id = id;
			Data.Permissions = new List<IJsonTokenPermission>();
			MemberPermissions = new TokenPermissionContext(Auth);
			MemberPermissions.SynchronizeRequested += () => Synchronize();
			Data.Permissions.Add(MemberPermissions.Data);
			BoardPermissions = new TokenPermissionContext(Auth);
			BoardPermissions.SynchronizeRequested += () => Synchronize();
			Data.Permissions.Add(BoardPermissions.Data);
			OrganizationPermissions = new TokenPermissionContext(Auth);
			OrganizationPermissions.SynchronizeRequested += () => Synchronize();
			Data.Permissions.Add(OrganizationPermissions.Data);
		}

		public void Delete()
		{
			if (_deleted) return;
			CancelUpdate();

			var endpoint = EndpointFactory.Build(EntityRequestType.Token_Write_Delete, new Dictionary<string, object> {{"_id", Data.Id}});
			JsonRepository.Execute(Auth, endpoint);

			_deleted = true;
		}

		protected override IJsonToken GetData()
		{
			try
			{
				var endpoint = EndpointFactory.Build(EntityRequestType.Token_Read_Refresh, new Dictionary<string, object> {{"_token", Data.Id}});
				var newData = JsonRepository.Execute<IJsonToken>(Auth, endpoint);
				MarkInitialized();

				return newData;
			}
			catch (TrelloInteractionException e)
			{
				if (!e.IsNotFoundError() || !IsInitialized) throw;
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
		public override void Expire()
		{
			MemberPermissions.Expire();
			BoardPermissions.Expire();
			OrganizationPermissions.Expire();
			base.Expire();
		}
	}
}