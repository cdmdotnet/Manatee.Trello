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
	internal class CollaboratorContext : SynchronizationContext<IJsonCollaborator>
	{
		private static readonly Dictionary<string, object> Parameters;
		private static readonly Collaborator.Fields CollaboratorFields;

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

		public virtual bool HasValidId => IdRule.Instance.Validate(Data.Id, null) == null;

		static CollaboratorContext()
		{
			Parameters = new Dictionary<string, object>();
			CollaboratorFields = Collaborator.Fields.AvatarUrl |
						   Collaborator.Fields.Bio |
						   Collaborator.Fields.IsConfirmed |
						   Collaborator.Fields.Email |
						   Collaborator.Fields.FullName |
						   Collaborator.Fields.Initials |
						   Collaborator.Fields.MemberType |
						   Collaborator.Fields.Status |
						   Collaborator.Fields.Url |
						   Collaborator.Fields.Username;
			Properties = new Dictionary<string, Property<IJsonCollaborator>>
				{
					{
						nameof(Collaborator.Bio),
						new Property<IJsonCollaborator, string>((d, a) => d.Bio, (d, o) => d.Bio = o)
					},
					{
						nameof(Collaborator.FullName),
						new Property<IJsonCollaborator, string>((d, a) => d.FullName, (d, o) => d.FullName = o)
					},
					{
						nameof(Collaborator.Id),
						new Property<IJsonCollaborator, string>((d, a) => d.Id, (d, o) => d.Id = o)
					},
					{
						nameof(Collaborator.Initials),
						new Property<IJsonCollaborator, string>((d, a) => d.Initials, (d, o) => d.Initials = o)
					},
					{
						nameof(Collaborator.Status),
						new Property<IJsonCollaborator, MemberStatus?>((d, a) => d.Status, (d, o) => d.Status = o)
					},
					{
						nameof(Collaborator.Url),
						new Property<IJsonCollaborator, string>((d, a) => d.Url, (d, o) => d.Url = o)
					},
					{
						nameof(Collaborator.UserName),
						new Property<IJsonCollaborator, string>((d, a) => d.Username, (d, o) => d.Username = o)
					},
				};
		}
		public CollaboratorContext(string id, bool isMe, TrelloAuthorization auth)
			: base(auth)
		{
			Data.Id = id;
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
				var flags = Enum.GetValues(typeof(Collaborator.Fields)).Cast<Collaborator.Fields>().ToList();
				var availableFields = (Collaborator.Fields)flags.Cast<int>().Sum();

				var memberFields = availableFields & CollaboratorFields & Collaborator.DownloadedFields;
				Parameters["fields"] = memberFields.GetDescription();

				var parameterFields = availableFields & Collaborator.DownloadedFields & (~CollaboratorFields);
				if (parameterFields.HasFlag(Member.Fields.Actions))
				{
					Parameters["actions"] = "all";
					Parameters["actions_format"] = "list";
				}

				if (parameterFields.HasFlag(Member.Fields.Boards))
				{
					Parameters["boards"] = "all";
					Parameters["board_fields"] = BoardContext.CurrentParameters["fields"];
				}
				if (parameterFields.HasFlag(Member.Fields.Cards))
				{
					Parameters["cards"] = "all";
					Parameters["card_fields"] = CardContext.CurrentParameters["fields"]; ;
				}
				if (parameterFields.HasFlag(Member.Fields.Notifications))
				{
					Parameters["notifications"] = "all";
					Parameters["notification_fields"] = NotificationContext.CurrentParameters["fields"];
				}
				if (parameterFields.HasFlag(Member.Fields.Organizations))
				{
					Parameters["organizations"] = "all";
					Parameters["organization_fields"] = OrganizationContext.CurrentParameters["fields"];
				}
				if (parameterFields.HasFlag(Member.Fields.StarredBoards))
					Parameters["boardStars"] = "true";
				if (parameterFields.HasFlag(Member.Fields.BoardBackgrounds))
					Parameters["boardBackgrounds"] = "custom";
			}
		}

		public override Endpoint GetRefreshEndpoint()
		{
			return EndpointFactory.Build(EntityRequestType.Member_Read_Refresh,
			                             new Dictionary<string, object> {{"_id", Data.Id}});
		}

		protected override Dictionary<string, object> GetParameters()
		{
			return CurrentParameters;
		}
	}
}
