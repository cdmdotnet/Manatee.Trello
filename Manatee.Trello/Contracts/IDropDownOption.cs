using System.Threading;
using System.Threading.Tasks;

namespace Manatee.Trello
{
	public interface IDropDownOption : ICacheable
	{
		ICustomField<IDropDownOption> Field { get; }
		string Text { get; }
		LabelColor? Color { get; }
		Position Position { get; }

		Task Delete(CancellationToken ct = default(CancellationToken));
		Task Refresh(CancellationToken ct = default(CancellationToken));
	}
}