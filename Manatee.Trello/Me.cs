using Manatee.Trello.Internal;
using Manatee.Trello.Internal.DataAccess;
using Manatee.Trello.Json;

namespace Manatee.Trello
{
	public class Me : Member
	{
		private static IJsonMember _myJson;

		private readonly Field<string> _email;

		public new AvatarSource AvatarSource
		{
			get { return base.AvatarSource; }
			set { base.AvatarSource = value; }
		}
		public new string Bio
		{
			get { return base.Bio; }
			set { base.Bio = value; }
		}
		public new BoardCollection Boards { get { return base.Boards as BoardCollection; } }
		public string Email
		{
			get { return _email.Value; }
			set { _email.Value = value; }
		}
		public new string FullName
		{
			get { return base.FullName; }
			set { base.FullName = value; }
		}
		public new string Initials
		{
			get { return base.Initials; }
			set { base.Initials = value; }
		}
		public new OrganizationCollection Organizations { get { return base.Organizations as OrganizationCollection; } }
		public new string UserName
		{
			get { return base.UserName; }
			set { base.UserName = value; }
		}

		public Me()
			: base(GetId(), true)
		{
			_email = new Field<string>(_context, () => Email);

			_context.Merge(_myJson);
		}

		private static string GetId()
		{
			var endpoint = EndpointFactory.Build(EntityRequestType.Service_Read_Me);
			_myJson = JsonRepository.Execute<IJsonMember>(TrelloAuthorization.Default, endpoint);
			return _myJson.Id;
		}
	}
}