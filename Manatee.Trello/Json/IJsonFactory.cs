namespace Manatee.Trello.Json
{
	/// <summary>
	/// Creates instances of JSON interfaces.
	/// </summary>
	public interface IJsonFactory
	{
		/// <summary>
		/// Creates an instance of the requested JSON interface.
		/// </summary>
		/// <typeparam name="T">The type to create.</typeparam>
		/// <returns>An instance of the requested type.</returns>
		T Create<T>() where T : class;
	}
}