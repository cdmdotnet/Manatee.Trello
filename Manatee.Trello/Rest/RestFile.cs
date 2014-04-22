namespace Manatee.Trello.Rest
{
    public class RestFile
    {
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
