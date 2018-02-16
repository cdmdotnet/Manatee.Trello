using System;
using Manatee.Trello.Internal;

namespace Manatee.Trello
{
	/// <summary>
	/// Contains authorization tokens needed to connect to Trello's API.
	/// </summary>
	public class TrelloAuthorization
	{
		private string _appKey;
		/// <summary>
		/// Gets the default authorization.
		/// </summary>
		public static TrelloAuthorization Default { get; }

		/// <summary>
		/// The token which identifies the application attempting to connect.
		/// </summary>
		public string AppKey
		{
			get { return _appKey; }
			set
			{
				if (value.IsNullOrWhiteSpace())
					throw new ArgumentNullException(nameof(value));
				_appKey = value;
			}
		}
		/// <summary>
		/// The token which identifies special permissions as granted by a specific user.
		/// </summary>
		public string UserToken { get; set; }

		static TrelloAuthorization()
		{
			Default = new TrelloAuthorization();
		}
	}
}