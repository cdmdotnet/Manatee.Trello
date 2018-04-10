using Manatee.Trello.Json;

namespace Manatee.Trello
{
	/// <summary>
	/// Defines properties which are required to cache an object.
	/// </summary>
	public interface ICacheable
	{
		/// <summary>
		/// Gets an ID on which matching can be performed.
		/// </summary>
		string Id { get; }
	}

	internal interface IMergeJson<in T>
	{
		void Merge(T json);
	}
}