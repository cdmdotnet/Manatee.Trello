using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
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
			Properties = new Dictionary<string, Property<IJsonToken>>
				{
					{
						nameof(Token.AppName),
						new Property<IJsonToken, string>((d, a) => d.Identifier, (d, o) => d.Identifier = o)
					},
					{
						nameof(Token.Member),
						new Property<IJsonToken, Member>((d, a) => d.Member?.GetFromCache<Member>(a),
						                                 (d, o) => d.Member = o?.Json)
					},
					{
						nameof(Token.DateCreated),
						new Property<IJsonToken, DateTime?>((d, a) => d.DateCreated, (d, o) => d.DateCreated = o)
					},
					{
						nameof(Token.DateExpires),
						new Property<IJsonToken, DateTime?>((d, a) => d.DateExpires, (d, o) => d.DateExpires = o)
					},
					{
						nameof(Token.Id),
						new Property<IJsonToken, string>((d, a) => d.Id, (d, o) => d.Id = o)
					},
				};
		}
		public TokenContext(string id, TrelloAuthorization auth)
			: base(auth)
		{
			Data.Id = id;
			Data.Permissions = new List<IJsonTokenPermission>();
			MemberPermissions = new TokenPermissionContext(Auth);
			MemberPermissions.SynchronizeRequested += ct => Synchronize(ct);
			Data.Permissions.Add(MemberPermissions.Data);
			BoardPermissions = new TokenPermissionContext(Auth);
			BoardPermissions.SynchronizeRequested += ct => Synchronize(ct);
			Data.Permissions.Add(BoardPermissions.Data);
			OrganizationPermissions = new TokenPermissionContext(Auth);
			OrganizationPermissions.SynchronizeRequested += ct => Synchronize(ct);
			Data.Permissions.Add(OrganizationPermissions.Data);
		}

		public async Task Delete(CancellationToken ct)
		{
			if (_deleted) return;
			CancelUpdate();

			var endpoint = EndpointFactory.Build(EntityRequestType.Token_Write_Delete, new Dictionary<string, object> {{"_id", Data.Id}});
			await JsonRepository.Execute(Auth, endpoint, ct);

			_deleted = true;
		}
		public override async Task Expire(CancellationToken ct)
		{
			await base.Expire(ct);
		}

		protected override async Task<IJsonToken> GetData(CancellationToken ct)
		{
			try
			{
				var endpoint = EndpointFactory.Build(EntityRequestType.Token_Read_Refresh, new Dictionary<string, object> {{"_token", Data.Id}});
				var newData = await JsonRepository.Execute<IJsonToken>(Auth, endpoint, ct);
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
	}
}