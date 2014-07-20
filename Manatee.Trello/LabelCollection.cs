using System.Linq;
using Manatee.Trello.Internal.Synchronization;
using Manatee.Trello.Json;

namespace Manatee.Trello
{
	public class ReadOnlyLabelCollection : ReadOnlyCollection<ReadOnlyLabel>
	{
		internal readonly CardContext _context;

		internal ReadOnlyLabelCollection(CardContext context)
			: base(context.Data.Id)
		{
			_context = context;
		}

		protected override sealed void Update()
		{
			_context.Synchronize();
			if (_context.Data.Labels == null) return;
			foreach (var jsonLabel in _context.Data.Labels)
			{
				var label = Items.SingleOrDefault(l => l.Color == jsonLabel.Color);
				if (label == null)
					Items.Add(new Label(jsonLabel.Color, jsonLabel.Name));
				else
					label.Name = jsonLabel.Name;
			}
			foreach (var label in Items.ToList())
			{
				if (_context.Data.Labels.All(jl => jl.Color != label.Color))
					Items.Remove(label);
			}
		}
	}

	public class LabelCollection : ReadOnlyLabelCollection
	{
		internal LabelCollection(CardContext context)
			: base(context) {}

		public ReadOnlyLabel Add(LabelColor color)
		{
			var label = Items.FirstOrDefault(l => l.Color == color);
			if (label == null)
			{
				label = new ReadOnlyLabel(color, null);
				Items.Add(label);
				var jsonLabel = TrelloConfiguration.JsonFactory.Create<IJsonLabel>();
				jsonLabel.Color = color;
				_context.Data.Labels.Add(jsonLabel);
				_context.ResetTimer();
			}
			return label;
		}
		public void Remove(LabelColor color)
		{
			var label = Items.FirstOrDefault(l => l.Color == color);
			if (label == null) return;
			Items.Remove(label);

			var jsonLabel = _context.Data.Labels.FirstOrDefault(l => l.Color == color);
			if (jsonLabel == null) return;
			_context.Data.Labels.Remove(jsonLabel);
			_context.ResetTimer();
		}
	}
}