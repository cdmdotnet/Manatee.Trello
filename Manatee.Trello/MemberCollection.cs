using System;
using System.Collections.Generic;
using System.Linq;
using Manatee.Trello.Internal;
using Manatee.Trello.Internal.DataAccess;
using Manatee.Trello.Json;

namespace Manatee.Trello
{
	public class ReadOnlyMemberCollection : ReadOnlyCollection<Member>
	{
		private readonly EntityRequestType _updateRequestType;

		internal ReadOnlyMemberCollection(Type type, string ownerId)
			: base(ownerId)
		{
			if (type == typeof(Board))
				_updateRequestType = EntityRequestType.Board_Read_Members;
			else if (type == typeof(Organization))
				_updateRequestType = EntityRequestType.Organization_Read_Members;
			else
				_updateRequestType = EntityRequestType.Card_Read_Members;
		}

		protected override sealed void Update()
		{
			var endpoint = EndpointFactory.Build(_updateRequestType, new Dictionary<string, object> {{"_id", OwnerId}});
			var newData = JsonRepository.Execute<List<IJsonMember>>(TrelloAuthorization.Default, endpoint);

			Items.Clear();
			Items.AddRange(newData.Select(jc => jc.GetFromCache<Member>()));
		}
	}

	public class MemberCollection : ReadOnlyMemberCollection
	{
		internal MemberCollection(string ownerId)
			: base(typeof(Card), ownerId) { }

		public void Add(Member member)
		{
			var json = TrelloConfiguration.JsonFactory.Create<IJsonParameter>();
			json.Value = member.Id;

			var endpoint = EndpointFactory.Build(EntityRequestType.Card_Write_AssignMember);
			JsonRepository.Execute(TrelloAuthorization.Default, endpoint, json);
		}
		public void Remove(Member member)
		{
			var json = TrelloConfiguration.JsonFactory.Create<IJsonParameter>();
			json.Value = member.Id;

			var endpoint = EndpointFactory.Build(EntityRequestType.Card_Write_RemoveMember);
			JsonRepository.Execute(TrelloAuthorization.Default, endpoint, json);
		}
	}
}