using System.Collections.Generic;
using System.Linq;
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
		/// Adds an existing label to the card.
		/// </summary>
		/// <param name="label">The label to add.</param>
		public void Add(ILabel label)
		{
			var error = NotNullRule<ILabel>.Instance.Validate(null, label);
			if (error != null)
				throw new ValidationException<ILabel>(label, new[] {error});

			var json = TrelloConfiguration.JsonFactory.Create<IJsonParameter>();
			json.String = label.Id;

			var endpoint = EndpointFactory.Build(EntityRequestType.Card_Write_AddLabel, new Dictionary<string, object> {{"_id", OwnerId}});
			JsonRepository.Execute(Auth, endpoint, json);

			Items.Add(label);
			_context.Expire();
		}
		/// <summary>
		/// Removes a label from the collection.
		/// </summary>
		/// <param name="label">The label to add.</param>
		public void Remove(ILabel label)
		{
			var error = NotNullRule<ILabel>.Instance.Validate(null, label);
			if (error != null)
				throw new ValidationException<ILabel>(label, new[] {error});

			var endpoint = EndpointFactory.Build(EntityRequestType.Card_Write_RemoveLabel, new Dictionary<string, object> {{"_id", OwnerId}, {"_labelId", label.Id}});
			JsonRepository.Execute(Auth, endpoint);

			Items.Remove(label);
			_context.Expire();
		}

		/// <summary>
		/// Implement to provide data to the collection.
		/// </summary>
		public sealed override async Task Refresh()
		{
			_context.Synchronize();
			if (_context.Data.Labels == null) return;

			Items.Clear();
			Items.AddRange(_context.Data.Labels.Select(jl =>
				{
					var label = jl.GetFromCache<Label>(Auth);
					label.Json = jl;
					return label;
				}));
		}
	}
}