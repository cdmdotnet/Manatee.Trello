using System.Threading;
using System.Threading.Tasks;

namespace Manatee.Trello
{
	public interface ICustomFieldDefinitionCollection : IReadOnlyCollection<ICustomFieldDefinition>
	{
		Task<ICustomFieldDefinition> Add(string name, CustomFieldType type,
		                                 CancellationToken cancellationToken = default(CancellationToken),
		                                 params IDropDownOption[] options);
	}
}