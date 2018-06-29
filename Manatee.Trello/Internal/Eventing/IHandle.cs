namespace Manatee.Trello.Internal.Eventing
{
	/// <summary>
	///   A marker interface for classes that subscribe to messages.
	/// </summary>
	internal interface IHandle {}

	/// <summary>
	///   Denotes a class which can handle a particular type of message.
	/// </summary>
	/// <typeparam name = "TMessage">The type of message to handle.</typeparam>
	// ReSharper disable once TypeParameterCanBeVariant
	internal interface IHandle<TMessage> : IHandle
	{
		/// <summary>
		///   Handles the message.
		/// </summary>
		/// <param name = "message">The message.</param>
		void Handle(TMessage message);
	}
}
