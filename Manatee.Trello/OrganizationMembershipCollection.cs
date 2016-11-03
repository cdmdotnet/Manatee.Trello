using System;
using System.Collections.Generic;
using System.Linq;
using Manatee.Trello.Exceptions;
using Manatee.Trello.Internal;
using Manatee.Trello.Internal.DataAccess;
using Manatee.Trello.Internal.Validation;
using Manatee.Trello.Json;

namespace Manatee.Trello
{
	/// <summary>
	/// A read-only collection of organization memberships.
	/// </summary>
	public class ReadOnlyOrganizationMembershipCollection : ReadOnlyCollection<OrganizationMembership>
	{
		private Dictionary<string, object> _additionalParameters;

		internal ReadOnlyOrganizationMembershipCollection(Func<string> getOwnerId, TrelloAuthorization auth)
			: base(getOwnerId, auth)
		{
			_additionalParameters = new Dictionary<string, object> {{"fields", "all"}};
		}
		internal ReadOnlyOrganizationMembershipCollection(ReadOnlyOrganizationMembershipCollection source, TrelloAuthorization auth)
			: this(() => source.OwnerId, auth)
		{
			if (source._additionalParameters != null)
				_additionalParameters = new Dictionary<string, object>(source._additionalParameters);
		}

		/// <summary>
		/// Retrieves a membership which matches the supplied key.
		/// </summary>
		/// <param name="key">The key to match.</param>
		/// <returns>The matching list, or null if none found.</returns>
		/// <remarks>
		/// Matches on OrganizationMembership.Id, OrganizationMembership.Member.Id,
		/// OrganizationMembership.Member.FullName, and OrganizationMembership.Member.Username.
		/// Comparison is case-sensitive.
		/// </remarks>
		public OrganizationMembership this[string key] => GetByKey(key);

		/// <summary>
		/// Implement to provide data to the collection.
		/// </summary>
		protected sealed override void Update()
		{
			var endpoint = EndpointFactory.Build(EntityRequestType.Organization_Read_Memberships, new Dictionary<string, object> {{"_id", OwnerId}});
			var newData = JsonRepository.Execute<List<IJsonOrganizationMembership>>(Auth, endpoint, _additionalParameters);

			Items.Clear();
			Items.AddRange(newData.Select(jom =>
				{
					var membership = TrelloConfiguration.Cache.Find<OrganizationMembership>(c => c.Id == jom.Id) ?? new OrganizationMembership(jom, OwnerId, Auth);
					membership.Json = jom;
					return membership;
				}));
		}

		internal void AddFilter(IEnumerable<MembershipFilter> actionTypes)
		{
			if (_additionalParameters == null)
				_additionalParameters = new Dictionary<string, object> { { "filter", string.Empty } };
			var filter = ((string)_additionalParameters["filter"]);
			if (!filter.IsNullOrWhiteSpace())
				filter += ",";
			filter += actionTypes.Select(a => a.GetDescription()).Join(",");
			_additionalParameters["filter"] = filter;
		}

		private OrganizationMembership GetByKey(string key)
		{
			return this.FirstOrDefault(bm => key.In(bm.Id, bm.Member.Id, bm.Member.FullName, bm.Member.UserName));
		}
	}

	/// <summary>
	/// A collection of organization memberships.
	/// </summary>
	public class OrganizationMembershipCollection : ReadOnlyOrganizationMembershipCollection
	{
		internal OrganizationMembershipCollection(Func<string> getOwnerId, TrelloAuthorization auth)
			: base(getOwnerId, auth) {}

		/// <summary>
		/// Adds a member to an organization with specified privileges.
		/// </summary>
		/// <param name="member">The member to add.</param>
		/// <param name="membership">The membership type.</param>
		public void Add(Member member, OrganizationMembershipType membership)
		{
			var error = NotNullRule<Member>.Instance.Validate(null, member);
			if (error != null)
				throw new ValidationException<Member>(member, new[] {error});

			var json = TrelloConfiguration.JsonFactory.Create<IJsonOrganizationMembership>();
			json.Member = member.Json;
			json.MemberType = membership;

			var endpoint = EndpointFactory.Build(EntityRequestType.Organization_Write_AddOrUpdateMember, new Dictionary<string, object> {{"_id", OwnerId}, {"_memberId", member.Id}});
			JsonRepository.Execute(Auth, endpoint, json);
		}
		/// <summary>
		/// Removes a member from an organization.
		/// </summary>
		/// <param name="member">The member to remove.</param>
		public void Remove(Member member)
		{
			var error = NotNullRule<Member>.Instance.Validate(null, member);
			if (error != null)
				throw new ValidationException<Member>(member, new[] {error});

			var json = TrelloConfiguration.JsonFactory.Create<IJsonParameter>();
			json.String = member.Id;

			var endpoint = EndpointFactory.Build(EntityRequestType.Organization_Write_RemoveMember, new Dictionary<string, object> {{"_id", OwnerId}, {"_memberId", member.Id}});
			JsonRepository.Execute(Auth, endpoint, json);
		}
	}
}