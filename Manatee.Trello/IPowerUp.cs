using Manatee.Trello.Contracts;

namespace Manatee.Trello
{
	public interface IPowerUp : ICacheable
	{
		string Name { get; }
		bool? Public { get; }
		string AdditionalInfo { get; }
	}
}
