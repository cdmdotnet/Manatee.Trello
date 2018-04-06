namespace Manatee.Trello.Contracts
{
	/// <summary>
	/// Definines properties and methods required to support webhooks.
	/// </summary>
	public interface ICanWebhook : ICacheable
	{
		/// <summary>
		/// Applies the changes an action represents.
		/// </summary>
		/// <param name="action">The action.</param>
		void ApplyAction(IAction action);
	}
}