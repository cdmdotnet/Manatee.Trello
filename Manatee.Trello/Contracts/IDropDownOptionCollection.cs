using System.Threading;
using System.Threading.Tasks;

namespace Manatee.Trello
{
	public interface IDropDownOptionCollection
	{
		Task<IDropDownOption> Add(string text, Position position, LabelColor? color = null,
		                          CancellationToken ct = default(CancellationToken));
	}
}