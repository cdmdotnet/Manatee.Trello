using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Manatee.Trello.Internal.Caching;
using Manatee.Trello.Internal.DataAccess;
using Manatee.Trello.Internal.Synchronization;
using Manatee.Trello.Internal.Validation;
using Manatee.Trello.Json;

namespace Manatee.Trello
{
	/// <summary>
	/// A collection of labels for cards.
	/// </summary>
	public class CardLabelCollection : ReadOnlyCollection<ILabel>, ICardLabelCollection
	{
		private readonly CardContext _context;

		internal CardLabelCollection(CardContext context, TrelloAuthorization auth)
			: base(() => context.Data.Id, auth)
		{
			_context = context;
		}

		/// <summary>
		/// Adds a label to the collection.
		/// </summary>
		/// <param name="label">The label to add.</param>
		/// <param name="ct">(Optional) A cancellation token for async processing.</param>
		public async Task Add(ILabel label, CancellationToken ct = default(CancellationToken))
		{
			var error = NotNullRule<ILabel>.Instance.Validate(null, label);
			if (error != null)
				throw new ValidationException<ILabel>(label, new[] {error});

			var json = TrelloConfiguration.JsonFactory.Create<IJsonParameter>();
			json.String = label.Id;

			var endpoint = EndpointFactory.Build(EntityRequestType.Card_Write_AddLabel,
			                                     new Dictionary<string, object> {{"_id", OwnerId}});
			await JsonRepository.Execute(Auth, endpoint, json, ct);

			Items.Add(label);
			await _context.Synchronize(true, ct);
		}

		/// <summary>
		/// Removes a label from the collection.
		/// </summary>
		/// <param name="label">The label to add.</param>
		/// <param name="ct">(Optional) A cancellation token for async processing.</param>
		public async Task Remove(ILabel label, CancellationToken ct = default(CancellationToken))
		{
			var error = NotNullRule<ILabel>.Instance.Validate(null, label);
			if (error != null)
				throw new ValidationException<ILabel>(label, new[] {error});

			var endpoint = EndpointFactory.Build(EntityRequestType.Card_Write_RemoveLabel,
			                                     new Dictionary<string, object> {{"_id", OwnerId}, {"_labelId", label.Id}});
			await JsonRepository.Execute(Auth, endpoint, ct);

			Items.Remove(label);
			await _context.Synchronize(true, ct);
		}

		/// <summary>
		/// Manually updates the collection's data.
		/// </summary>
		/// <param name="force">Indicates that the refresh should ignore the value in <see cref="TrelloConfiguration.RefreshThrottle"/> and make the call to the API.</param>
		/// <param name="ct">(Optional) A cancellation token for async processing.</param>
		public sealed override async Task Refresh(bool force = false, CancellationToken ct = default(CancellationToken))
		{
			var endpoint = EndpointFactory.Build(EntityRequestType.Card_Read_Labels,
			                                     new Dictionary<string, object> {{"_id", OwnerId}});
			var newData = await JsonRepository.Execute<List<IJsonLabel>>(Auth, endpoint, ct, AdditionalParameters);

			Items.Clear();
			Items.AddRange(newData.Select(ja =>
				{
					var label = ja.GetFromCache<Label, IJsonLabel>(Auth);
					label.Json = ja;
					return label;
				}));
		}
	}
}