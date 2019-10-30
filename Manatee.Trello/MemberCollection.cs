using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
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
		internal MemberCollection(Func<string> getOwnerId, TrelloAuthorization auth)
			: base(EntityRequestType.Card_Read_Members, getOwnerId, auth) {}

		/// <summary>
		/// Adds a member to the collection.
		/// </summary>
		/// <param name="ct">(Optional) A cancellation token for async processing.</param>
		/// <param name="member">The member to add.</param>
		public async Task Add(IMember member, CancellationToken ct = default)
		{
			var error = NotNullRule<IMember>.Instance.Validate(null, member);
			if (error != null)
				throw new ValidationException<IMember>(member, new[] {error});

			var json = TrelloConfiguration.JsonFactory.Create<IJsonParameter>();
			json.String = member.Id;

			var endpoint = EndpointFactory.Build(EntityRequestType.Card_Write_AssignMember, new Dictionary<string, object> {{"_id", OwnerId}});
			await JsonRepository.Execute(Auth, endpoint, json, ct);

			Items.Add(member);

			if (TrelloConfiguration.EnableConsistencyProcessing &&
			    member.Cards is ReadOnlyCollection<ICard> cardCollection)
			{
				var card = TrelloConfiguration.Cache.OfType<ICard>().FirstOrDefault(c => c.Id == OwnerId);
				if (card != null)
					cardCollection.Items.Add(card);
			}
		}

		/// <summary>
		/// Removes a member from the collection.
		/// </summary>
		/// <param name="ct">(Optional) A cancellation token for async processing.</param>
		/// <param name="member">The member to remove.</param>
		public async Task Remove(IMember member, CancellationToken ct = default)
		{
			var error = NotNullRule<IMember>.Instance.Validate(null, member);
			if (error != null)
				throw new ValidationException<IMember>(member, new[] {error});

			var endpoint = EndpointFactory.Build(EntityRequestType.Card_Write_RemoveMember, new Dictionary<string, object> {{"_id", OwnerId}, {"_memberId", member.Id}});
			await JsonRepository.Execute(Auth, endpoint, ct);

			Items.Remove(member);

			if (TrelloConfiguration.EnableConsistencyProcessing &&
			    member.Cards is ReadOnlyCollection<ICard> cardCollection)
			{
				var card = TrelloConfiguration.Cache.OfType<ICard>().FirstOrDefault(c => c.Id == OwnerId);
				if (card != null)
					cardCollection.Items.Remove(card);
			}
		}
	}
}