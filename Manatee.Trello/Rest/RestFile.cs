namespace Manatee.Trello.Rest
{
	/// <summary>
	/// Defines a file to be included in a REST request.
	/// </summary>
	public class RestFile
	{
		/// <summary>
		/// Defines a key to be used when attaching a file to a REST request.
		/// </summary>
		public const string ParameterKey = "file";

		/// <summary>
		/// The file name to use for the uploaded file
		/// </summary>
		public string FileName { get; set; }
		/// <summary>
		/// The file data
		/// </summary>
		public byte[] ContentBytes { get; set; }
	}
}
