using Manatee.Trello.Internal;
using Manatee.Trello.Internal.Synchronization;
using Manatee.Trello.Internal.Validation;

namespace Manatee.Trello
{
	/// <summary>
	/// Represents the display information for a custom field.
	/// </summary>
	public class CustomFieldDisplayInfo : ICustomFieldDisplayInfo
	{
		private readonly Field<bool?> _cardFront;
		private readonly CustomFieldDisplayInfoContext _context;

		/// <summary>
		/// Gets or sets whether the custom field will appear on the front of the card.
		/// </summary>
		public bool? CardFront
		{
			get { return _cardFront.Value; }
			set { _cardFront.Value = value; }
		}

		internal CustomFieldDisplayInfo(CustomFieldDisplayInfoContext context)
		{
			_context = context;

			_cardFront = new Field<bool?>(_context, nameof(CardFront));
			_cardFront.AddRule(NullableHasValueRule<bool>.Instance);
		}
	}
}
