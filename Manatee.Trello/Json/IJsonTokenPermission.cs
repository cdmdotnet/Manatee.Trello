namespace Manatee.Trello.Json
{
	/// <summary>
	/// Defines the JSON structure for the TokenPermission object.
	/// </summary>
	public interface IJsonTokenPermission
	{
		/// <summary>
		/// Gets or sets the ID of the model to which a token grants permissions.
		/// </summary>
		[JsonDeserialize]
		string IdModel { get; set; }
		/// <summary>
		/// Gets or sets the type of the model.
		/// </summary>
		[JsonDeserialize]
		TokenModelType? ModelType { get; set; }
		/// <summary>
		/// Gets or sets whether a token grants read permissions to the model.
		/// </summary>
		[JsonDeserialize]
		bool? Read { get; set; }
		/// <summary>
		/// Gets or sets whether a token grants write permissions to the model.
		/// </summary>
		[JsonDeserialize]
		bool? Write { get; set; }
	}
}