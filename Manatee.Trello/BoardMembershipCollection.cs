using System;
using System.Collections.Generic;
using Manatee.Trello.Internal.DataAccess;
using Manatee.Trello.Internal.Validation;
using Manatee.Trello.Json;

namespace Manatee.Trello
{
	/// <summary>
	/// A collection of board memberships.
	/// </summary>
	public class BoardMembershipCollection : ReadOnlyBoardMembershipCollection, IBoardMembershipCollection
	{
		internal BoardMembershipCollection(Func<string> getOwnerId, TrelloAuthorization auth)
			: base(getOwnerId, auth) { }

		/// <summary>
		/// Adds a member to a board with specified privileges.
		/// </summary>
		/// <param name="member">The member to add.</param>
		/// <param name="membership">The membership type.</param>
		public IBoardMembership Add(IMember member, BoardMembershipType membership)
		{
			var error = NotNullRule<IMember>.Instance.Validate(null, member);
			if (error != null)
				throw new ValidationException<IMember>(member, new[] { error });

			var json = TrelloConfiguration.JsonFactory.Create<IJsonBoardMembership>();
			json.Member = ((Member)member).Json;
			json.MemberType = membership;

			var endpoint = EndpointFactory.Build(EntityRequestType.Board_Write_AddOrUpdateMember, new Dictionary<string, object> {{"_id", OwnerId}, {"_memberId", member.Id}});
			var newData = JsonRepository.Execute(Auth, endpoint, json);

			return new BoardMembership(newData, OwnerId, Auth);
		}
		/// <summary>
		/// Removes a member from a board.
		/// </summary>
		/// <param name="member">The member to remove.</param>
		public void Remove(IMember member)
		{
			var error = NotNullRule<IMember>.Instance.Validate(null, member);
			if (error != null)
				throw new ValidationException<IMember>(member, new[] { error });

			var endpoint = EndpointFactory.Build(EntityRequestType.Board_Write_RemoveMember, new Dictionary<string, object> {{"_id", OwnerId}, {"_memberId", member.Id}});
			JsonRepository.Execute(Auth, endpoint);
		}
	}
}