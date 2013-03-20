using Manatee.Trello.Implementation;
using RestSharp;

namespace Manatee.Trello.Rest
{
	internal class CollectionRequest<TOwner, TEntity> : RequestBase
		where TOwner : EntityBase
		where TEntity : ExpiringObject, new()
	{
		public TEntity Entity { get; private set; }

		public CollectionRequest(string ownerId, TEntity obj)
			: this(ownerId)
		{
			Entity = obj;
		}
		public CollectionRequest(string ownerId)
			: base(GetPath())
		{
			AddParameter("id", ownerId, ParameterType.UrlSegment);
		}

		public void AddParameters()
		{
			foreach (var parameter in Entity.Parameters)
			{
				AddParameter(parameter.Key, parameter.Value);
			}
			Entity.Parameters.Clear();
		}

		private static string GetPath()
		{
			string section = SectionStrings[typeof(TOwner)],
			       itemType = SectionStrings[typeof(TEntity)];
			return string.Format("{0}/{{id}}/{1}", section, itemType);
		}
	}
}