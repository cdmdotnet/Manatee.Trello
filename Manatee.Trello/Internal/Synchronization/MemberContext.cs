using System.Collections.Generic;
using System.Linq;
using Manatee.Trello.Enumerations;
using Manatee.Trello.Internal.DataAccess;
using Manatee.Trello.Internal.Genesis;
using Manatee.Trello.Json;

namespace Manatee.Trello.Internal.Synchronization
{
	internal class MemberContext : SynchronizationContext<IJsonMember>
	{
		static MemberContext()
		{
			_properties = new Dictionary<string, Property<IJsonMember>>
				{
					{"AvatarSource", new Property<IJsonMember>(d => d.AvatarSource.ConvertEnum<AvatarSource>(), (d, o) => ((AvatarSource) o).ConvertEnum())},
					{"Bio", new Property<IJsonMember>(d => d.Bio, (d, o) => d.Bio = (string) o)},
					{"Email", new Property<IJsonMember>(d => d.Email, (d, o) => d.Email = (string) o)},
					{"FullName", new Property<IJsonMember>(d => d.FullName, (d, o) => d.FullName = (string) o)},
					{"Initials", new Property<IJsonMember>(d => d.Initials, (d, o) => d.Initials = (string) o)},
					{"IsConfirmed", new Property<IJsonMember>(d => d.Confirmed, (d, o) => d.Confirmed = (bool?) o)},
					{"Status", new Property<IJsonMember>(d => d.Status.ConvertEnum<MemberStatus>(), (d, o) => d.Status = ((MemberStatus) o).ConvertEnum())},
					{"Trophies", new Property<IJsonMember>(d => d.Trophies, (d, o) => d.Trophies = o == null ? null : ((IEnumerable<string>) o).ToList())},
					{"Url", new Property<IJsonMember>(d => d.Url, (d, o) => d.Url = (string) o)},
					{"UserName", new Property<IJsonMember>(d => d.Username, (d, o) => d.Username = (string) o)},
				};
		}
		public MemberContext(string id)
		{
			Data.Id = id;
		}

		protected override IJsonMember GetData()
		{
			var endpoint = EndpointFactory.Build(EntityRequestType.Member_Read_Refresh, new Dictionary<string, object> {{"_id", Data.Id}});
			var newData = JsonRepository.Execute<IJsonMember>(TrelloAuthorization.Default, endpoint);
			return newData;
		}
		protected override void SubmitData()
		{
			var endpoint = EndpointFactory.Build(EntityRequestType.Member_Write_Update, new Dictionary<string, object> {{"_id", Data.Id}});
			JsonRepository.Execute(TrelloAuthorization.Default, endpoint, Data);
		}
	}
}