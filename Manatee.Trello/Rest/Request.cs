using Manatee.Trello.Implementation;
using RestSharp;

namespace Manatee.Trello.Rest
{
	internal class Request<T> : RequestBase
		where T : EntityBase, new()
	{
		public T Entity { get; private set; }

		public Request(T obj)
			: this(obj.Id)
		{
			Entity = obj;
		}
		public Request(string id)
			: base(GetPath())
		{
			AddParameter("id", id, ParameterType.UrlSegment);
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
			var section = SectionStrings[typeof (T)];
			return string.Format("{0}/{{id}}", section);
		}
	}

	internal class Request<TOwner, TEntity> : RequestBase
		where TOwner : EntityBase
		where TEntity : OwnedEntityBase<TOwner>, new()
	{
		public TEntity Entity { get; private set; }

		public Request(string ownerId, TEntity obj)
			: this(ownerId)
		{
			Entity = obj;
		}
		public Request(string ownerId)
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