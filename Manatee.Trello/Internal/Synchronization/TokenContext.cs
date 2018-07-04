using System;
using System.Collections.Generic;
using System.Linq;
using Manatee.Trello.Internal.Caching;
using Manatee.Trello.Internal.DataAccess;
using Manatee.Trello.Internal.Validation;
using Manatee.Trello.Json;

namespace Manatee.Trello.Internal.Synchronization
{
	internal class TokenContext : DeletableSynchronizationContext<IJsonToken>
	{
		private static readonly Dictionary<string, object> Parameters;
		private static readonly Token.Fields MemberFields;

		public static Dictionary<string, object> CurrentParameters
		{
			get
			{
				lock (Parameters)
				{
					if (!Parameters.Any())
						GenerateParameters();

					return new Dictionary<string, object>(Parameters);
				}
			}
		}

		public TokenPermissionContext MemberPermissions { get; }
		public TokenPermissionContext BoardPermissions { get; }
		public TokenPermissionContext OrganizationPermissions { get; }
		public virtual bool HasValidId => IdRule.Instance.Validate(Data.Id, null) == null;

		static TokenContext()
		{
			Parameters = new Dictionary<string, object>();
			MemberFields = Token.Fields.Id |
			               Token.Fields.Member |
			               Token.Fields.DateCreated |
			               Token.Fields.DateExpires |
			               Token.Fields.Permissions;
			Properties = new Dictionary<string, Property<IJsonToken>>
				{
					{
						nameof(Token.AppName),
						new Property<IJsonToken, string>((d, a) => d.Identifier, (d, o) => d.Identifier = o)
					},
					{
						nameof(Token.Member),
						new Property<IJsonToken, Member>((d, a) => d.Member?.GetFromCache<Member, IJsonMember>(a),
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
			Data.Permissions.Add(MemberPermissions.Data);
			BoardPermissions = new TokenPermissionContext(Auth);
			Data.Permissions.Add(BoardPermissions.Data);
			OrganizationPermissions = new TokenPermissionContext(Auth);
			Data.Permissions.Add(OrganizationPermissions.Data);
		}

		public static void UpdateParameters()
		{
			lock (Parameters)
			{
				Parameters.Clear();
			}
		}

		private static void GenerateParameters()
		{
			lock (Parameters)
			{
				Parameters.Clear();
				var flags = Enum.GetValues(typeof(Token.Fields)).Cast<Token.Fields>().ToList();
				var availableFields = (Token.Fields)flags.Cast<int>().Sum();

				var memberFields = availableFields & MemberFields & Token.DownloadedFields;
				Parameters["fields"] = memberFields.GetDescription();

				var parameterFields = availableFields & Token.DownloadedFields & (~MemberFields);
				if (parameterFields.HasFlag(Token.Fields.Member))
				{
					Parameters["member"] = "true";
					Parameters["member_fields"] = MemberContext.CurrentParameters["fields"];
				}
			}
		}

		public override Endpoint GetRefreshEndpoint()
		{
			return EndpointFactory.Build(EntityRequestType.Token_Read_Refresh,
			                             new Dictionary<string, object> {{"_token", Data.Id}});
		}

		protected override Dictionary<string, object> GetParameters()
		{
			return CurrentParameters;
		}

		protected override Endpoint GetDeleteEndpoint()
		{
			return EndpointFactory.Build(EntityRequestType.Token_Write_Delete,
			                             new Dictionary<string, object> {{ "_id", Data.Id}});
		}

		protected override IEnumerable<string> MergeDependencies(IJsonToken json, bool overwrite)
		{
			return MemberPermissions.Merge(json.Permissions.FirstOrDefault(p => p.ModelType == TokenModelType.Member), overwrite)
			                        .Concat(BoardPermissions.Merge(json.Permissions.FirstOrDefault(p => p.ModelType == TokenModelType.Board), overwrite))
			                        .Concat(OrganizationPermissions.Merge(json.Permissions.FirstOrDefault(p => p.ModelType == TokenModelType.Organization), overwrite));
		}
	}
}