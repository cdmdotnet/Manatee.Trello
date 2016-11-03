namespace Manatee.Trello.Json
{
	/// <summary>
	/// Defines the JSON structure for the AttachmentPreview object.
	/// </summary>
	public interface IJsonImagePreview : IJsonCacheable
	{
		///<summary>
		/// Gets or sets the width in pixels of the attachment preview.
		///</summary>
		[JsonDeserialize]
		int? Width { get; set; }
		///<summary>
		/// Gets or sets the height in pixels of the attachment preview.
		///</summary>
		[JsonDeserialize]
		int? Height { get; set; }
		///<summary>
		/// Gets or sets the attachment storage location.
		///</summary>
		[JsonDeserialize]
		string Url { get; set; }
		/// <summary>
		/// Gets or sets whether the attachment was scaled to produce the preview.
		/// </summary>
		[JsonDeserialize]
		bool? Scaled { get; set; }
	}
}