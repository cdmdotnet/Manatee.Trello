namespace Manatee.Trello.Internal.Searching
{
	internal class LabelSearchParameter : ISearchParameter
	{
		public string Query { get; }

		public LabelSearchParameter(Label label)
		{
			Query = $"#{(label.Name.IsNullOrWhiteSpace() ? label.Color.ToLowerString() : label.Name)}";
		}
		public LabelSearchParameter(LabelColor color)
		{
			Query = $"#{color.ToLowerString()}";
		}
	}
}