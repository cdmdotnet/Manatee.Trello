using System;
using System.Collections.Generic;
using System.Linq;
using Manatee.Trello.Exceptions;
using Manatee.Trello.Internal;
using Manatee.Trello.Internal.Caching;
using Manatee.Trello.Internal.DataAccess;
using Manatee.Trello.Internal.Validation;
using Manatee.Trello.Json;

namespace Manatee.Trello
{
	/// <summary>
	/// A read-only collection of members.
	/// </summary>
	public interface IReadOnlyMemberCollection : IReadOnlyCollection<IMember>
	{
		/// <summary>
		/// Retrieves a member which matches the supplied key.
		/// </summary>
		/// <param name="key">The key to match.</param>
		/// <returns>The matching member, or null if none found.</returns>
		/// <remarks>
		/// Matches on Member.Id, Member.FullName, and Member.Username.  Comparison is case-sensitive.
		/// </remarks>
		IMember this[string key] { get; }

		/// <summary>
		/// Adds a filter to the collection.
		/// </summary>
		/// <param name="filter">The filter value.</param>
		void Filter(MemberFilter filter);
		/// <summary>
		/// Adds a set of filters to the collection.
		/// </summary>
		/// <param name="filters">The filter values.</param>
		void Filter(IEnumerable<MemberFilter> filters);
	}

	/// <summary>
	/// A read-only collection of members.
	/// </summary>
	public class ReadOnlyMemberCollection : ReadOnlyCollection<IMember>, IReadOnlyMemberCollection
	{
		private readonly EntityRequestType _updateRequestType;
		private Dictionary<string, object> _additionalParameters;

		/// <summary>
		/// Retrieves a member which matches the supplied key.
		/// </summary>
		/// <param name="key">The key to match.</param>
		/// <returns>The matching member, or null if none found.</returns>
		/// <remarks>
		/// Matches on <see cref="Member.Id"/>, <see cref="Member.FullName"/>, and <see cref="Member.UserName"/>.  Comparison is case-sensitive.
		/// </remarks>
		public IMember this[string key] => GetByKey(key);

		internal ReadOnlyMemberCollection(EntityRequestType requestType, Func<string> getOwnerId, TrelloAuthorization auth)
			: base(getOwnerId, auth)
		{
			_updateRequestType = requestType;
			_additionalParameters = new Dictionary<string, object> {{"fields", "all"}};
		}
		internal ReadOnlyMemberCollection(ReadOnlyMemberCollection source, TrelloAuthorization auth)
			: base(() => source.OwnerId, auth)
		{
			_updateRequestType = source._updateRequestType;
			if (source._additionalParameters != null)
				_additionalParameters = new Dictionary<string, object>(source._additionalParameters);
		}

		/// <summary>
		/// Adds a filter to the collection.
		/// </summary>
		/// <param name="filter">The filter value.</param>
		public void Filter(MemberFilter filter)
		{
			var filters = filter.GetFlags().Cast<MemberFilter>();
			Filter(filters);
		}

		/// <summary>
		/// Adds a set of filters to the collection.
		/// </summary>
		/// <param name="filters">The filter values.</param>
		public void Filter(IEnumerable<MemberFilter> filters)
		{
			if (_additionalParameters == null)
				_additionalParameters = new Dictionary<string, object> {{"filter", string.Empty}};
			var filter = _additionalParameters.ContainsKey("filter") ? (string)_additionalParameters["filter"] : string.Empty;
			if (!filter.IsNullOrWhiteSpace())
				filter += ",";
			filter += filters.Select(a => a.GetDescription()).Join(",");
			_additionalParameters["filter"] = filter;
		}

		/// <summary>
		/// Implement to provide data to the collection.
		/// </summary>
		protected sealed override void Update()
		{
			var endpoint = EndpointFactory.Build(_updateRequestType, new Dictionary<string, object> {{"_id", OwnerId}});
			var newData = JsonRepository.Execute<List<IJsonMember>>(Auth, endpoint, _additionalParameters);

			Items.Clear();
			Items.AddRange(newData.Select(jm =>
				{
					var member = jm.GetFromCache<Member>(Auth);
					member.Json = jm;
					return member;
				}));
		}

		private IMember GetByKey(string key)
		{
			return this.FirstOrDefault(m => key.In(m.Id, m.FullName, m.UserName));
		}
	}

	/// <summary>
	/// A collection of members.
	/// </summary>
	public interface IMemberCollection : IReadOnlyMemberCollection
	{
		/// <summary>
		/// Adds a member to the collection.
		/// </summary>
		/// <param name="member">The member to add.</param>
		void Add(IMember member);

		/// <summary>
		/// Removes a member from the collection.
		/// </summary>
		/// <param name="member">The member to remove.</param>
		void Remove(IMember member);
	}

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
		public void Add(IMember member)
		{
			var error = NotNullRule<IMember>.Instance.Validate(null, member);
			if (error != null)
				throw new ValidationException<IMember>(member, new[] {error});

			var json = TrelloConfiguration.JsonFactory.Create<IJsonParameter>();
			json.String = member.Id;

			var endpoint = EndpointFactory.Build(EntityRequestType.Card_Write_AssignMember, new Dictionary<string, object> {{"_id", OwnerId}});
			JsonRepository.Execute(Auth, endpoint, json);

			Items.Add(member);
		}
		/// <summary>
		/// Removes a member from the collection.
		/// </summary>
		/// <param name="member">The member to remove.</param>
		public void Remove(IMember member)
		{
			var error = NotNullRule<IMember>.Instance.Validate(null, member);
			if (error != null)
				throw new ValidationException<IMember>(member, new[] {error});

			var endpoint = EndpointFactory.Build(EntityRequestType.Card_Write_RemoveMember, new Dictionary<string, object> {{"_id", OwnerId}, {"_memberId", member.Id}});
			JsonRepository.Execute(Auth, endpoint);

			Items.Remove(member);
		}
	}
}