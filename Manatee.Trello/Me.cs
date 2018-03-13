﻿using Manatee.Trello.Internal;
using Manatee.Trello.Internal.DataAccess;
using Manatee.Trello.Json;

namespace Manatee.Trello
{
	/// <summary>
	/// Represents the current member.
	/// </summary>
	public interface IMe : IMember
	{
		/// <summary>
		/// Gets or sets the source type for the member's avatar.
		/// </summary>
		new AvatarSource? AvatarSource { get; set; }

		/// <summary>
		/// Gets or sets the member's bio.
		/// </summary>
		new string Bio { get; set; }

		/// <summary>
		/// Gets the collection of boards owned by the member.
		/// </summary>
		new IBoardCollection Boards { get; }

		/// <summary>
		/// Gets or sets the member's email.
		/// </summary>
		string Email { get; set; }

		/// <summary>
		/// Gets or sets the member's full name.
		/// </summary>
		new string FullName { get; set; }

		/// <summary>
		/// Gets or sets the member's initials.
		/// </summary>
		new string Initials { get; set; }

		/// <summary>
		/// Gets the collection of notificaitons for the member.
		/// </summary>
		IReadOnlyNotificationCollection Notifications { get; }

		/// <summary>
		/// Gets the collection of organizations to which the member belongs.
		/// </summary>
		new IOrganizationCollection Organizations { get; }

		/// <summary>
		/// Gets the set of preferences for the member.
		/// </summary>
		IMemberPreferences Preferences { get; }

		/// <summary>
		/// Gets or sets the member's username.
		/// </summary>
		new string UserName { get; set; }
	}

	/// <summary>
	/// Represents the current member.
	/// </summary>
	public class Me : Member, IMe
	{
		private static IJsonMember _myJson;

		private readonly Field<string> _email;

		/// <summary>
		/// Gets or sets the source type for the member's avatar.
		/// </summary>
		public new AvatarSource? AvatarSource
		{
			get { return base.AvatarSource; }
			set { base.AvatarSource = value; }
		}
		/// <summary>
		/// Gets or sets the member's bio.
		/// </summary>
		public new string Bio
		{
			get { return base.Bio; }
			set { base.Bio = value; }
		}
		/// <summary>
		/// Gets the collection of boards owned by the member.
		/// </summary>
		public new IBoardCollection Boards => base.Boards as BoardCollection;
		/// <summary>
		/// Gets or sets the member's email.
		/// </summary>
		public string Email
		{
			get { return _email.Value; }
			set { _email.Value = value; }
		}
		/// <summary>
		/// Gets or sets the member's full name.
		/// </summary>
		public new string FullName
		{
			get { return base.FullName; }
			set { base.FullName = value; }
		}
		/// <summary>
		/// Gets or sets the member's initials.
		/// </summary>
		public new string Initials
		{
			get { return base.Initials; }
			set { base.Initials = value; }
		}
		/// <summary>
		/// Gets the collection of notificaitons for the member.
		/// </summary>
		public IReadOnlyNotificationCollection Notifications { get; }
		/// <summary>
		/// Gets the collection of organizations to which the member belongs.
		/// </summary>
		public new IOrganizationCollection Organizations => (OrganizationCollection) base.Organizations;
		/// <summary>
		/// Gets the set of preferences for the member.
		/// </summary>
		public IMemberPreferences Preferences { get; }
		/// <summary>
		/// Gets or sets the member's username.
		/// </summary>
		public new string UserName
		{
			get { return base.UserName; }
			set { base.UserName = value; }
		}

		internal Me()
			: base(GetId(), true, TrelloAuthorization.Default)
		{
			_email = new Field<string>(_context, nameof(Email));
			Notifications = new ReadOnlyNotificationCollection(() => Id, TrelloAuthorization.Default);
			Preferences = new MemberPreferences(_context.MemberPreferencesContext);

			_context.Merge(_myJson);
		}

		private static string GetId()
		{
			var endpoint = EndpointFactory.Build(EntityRequestType.Service_Read_Me);
			_myJson = JsonRepository.Execute<IJsonMember>(TrelloAuthorization.Default, endpoint);

			// If this object exists in the cache already as a regular Member, it needs to be replaced.
			var meAsMember = TrelloConfiguration.Cache.Find<Member>(m => m.Id == _myJson.Id);
			if (meAsMember != null)
				TrelloConfiguration.Cache.Remove(meAsMember);

			return _myJson.Id;
		}
	}
}