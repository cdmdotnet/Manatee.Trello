using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace Manatee.Trello
{
	public interface ICustomFieldDefinition : ICacheable
	{
		IBoard Board { get; }
		string FieldGroup { get; }
		string Name { get; }
		Position Position { get; }
		CustomFieldType? Type { get; }
		IDropDownOptionCollection Options { get; }

		Task Delete(CancellationToken ct = default(CancellationToken));
		Task Refresh(CancellationToken ct = default(CancellationToken));
	}
}