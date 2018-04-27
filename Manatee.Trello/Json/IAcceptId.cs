namespace Manatee.Trello.Json
{
	/// <summary>
	/// Implemented on types that accept both a string ID as well as a full object for deserialization.
	/// </summary>
	public interface IAcceptId
	{
		/// <summary>
		/// To be set to true when deserializing from an object.
		/// </summary>
		/// <remarks>
		/// This property is neither serialized nor deserialized.
		/// </remarks>
		bool ValidForMerge { get; set; }
	}
}