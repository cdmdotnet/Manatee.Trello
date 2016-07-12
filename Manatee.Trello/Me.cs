/***************************************************************************************

	Copyright 2015 Greg Dennis

	   Licensed under the Apache License, Version 2.0 (the "License");
	   you may not use this file except in compliance with the License.
	   You may obtain a copy of the License at

		 http://www.apache.org/licenses/LICENSE-2.0

	   Unless required by applicable law or agreed to in writing, software
	   distributed under the License is distributed on an "AS IS" BASIS,
	   WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
	   See the License for the specific language governing permissions and
	   limitations under the License.
 
	File Name:		Me.cs
	Namespace:		Manatee.Trello
	Class Name:		Me
	Purpose:		Represents the current member.

***************************************************************************************/
using Manatee.Trello.Internal;
using Manatee.Trello.Internal.DataAccess;
using Manatee.Trello.Json;

namespace Manatee.Trello
{
	/// <summary>
	/// Represents the current member.
	/// </summary>
	public class Me : Member
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
		public new BoardCollection Boards => base.Boards as BoardCollection;
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
		public ReadOnlyNotificationCollection Notifications { get; }
		/// <summary>
		/// Gets the collection of organizations to which the member belongs.
		/// </summary>
		public new OrganizationCollection Organizations => base.Organizations as OrganizationCollection;
		/// <summary>
		/// Gets the set of preferences for the member.
		/// </summary>
		public MemberPreferences Preferences { get; }
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