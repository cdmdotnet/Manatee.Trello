using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Manatee.Trello.Internal.DataAccess;
using Manatee.Trello.Internal.Validation;
using Manatee.Trello.Json;

namespace Manatee.Trello
{
	/// <summary>
	/// A collection of members.
	/// </summary>
	public class MemberCollection : ReadOnlyMemberCollection, IMemberCollection
	{
		internal MemberCollection(EntityRequestType requestType, Func<string> getOwnerId, TrelloAuthorization auth)
			: base(requestType, getOwnerId, auth) {}

		/// <summary>
		/// Adds a member to the collection.
		/// </summary>
		/// <param name="member">The member to add.</param>
		public async Task Add(IMember member)
		{
			var error = NotNullRule<IMember>.Instance.Validate(null, member);
			if (error != null)
				throw new ValidationException<IMember>(member, new[] {error});

			var json = TrelloConfiguration.JsonFactory.Create<IJsonParameter>();
			json.String = member.Id;

			var endpoint = EndpointFactory.Build(EntityRequestType.Card_Write_AssignMember, new Dictionary<string, object> {{"_id", OwnerId}});
			await JsonRepository.Execute(Auth, endpoint, json);

			Items.Add(member);
		}

		/// <summary>
		/// Removes a member from the collection.
		/// </summary>
		/// <param name="member">The member to remove.</param>
		public async Task Remove(IMember member)
		{
			var error = NotNullRule<IMember>.Instance.Validate(null, member);
			if (error != null)
				throw new ValidationException<IMember>(member, new[] {error});

			var endpoint = EndpointFactory.Build(EntityRequestType.Card_Write_RemoveMember, new Dictionary<string, object> {{"_id", OwnerId}, {"_memberId", member.Id}});
			await JsonRepository.Execute(Auth, endpoint);

			Items.Remove(member);
		}
	}
}