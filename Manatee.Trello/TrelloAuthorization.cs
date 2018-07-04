using System;
using Manatee.Trello.Internal;

namespace Manatee.Trello
{
	/// <summary>
	/// Contains authorization tokens needed to connect to Trello's API.
	/// </summary>
	public class TrelloAuthorization : IEquatable<TrelloAuthorization>
	{
		private string _appKey;
		/// <summary>
		/// Gets the default authorization.
		/// </summary>
		public static TrelloAuthorization Default { get; }

		internal static TrelloAuthorization Null { get; } = new TrelloAuthorization();

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

		/// <summary>Indicates whether the current object is equal to another object of the same type.</summary>
		/// <returns>true if the current object is equal to the <paramref name="other" /> parameter; otherwise, false.</returns>
		/// <param name="other">An object to compare with this object.</param>
		public bool Equals(TrelloAuthorization other)
		{
			if (ReferenceEquals(null, other)) return false;
			if (ReferenceEquals(this, other)) return true;
			return string.Equals(_appKey, other._appKey) && string.Equals(UserToken, other.UserToken);
		}

		/// <summary>Determines whether the specified object is equal to the current object.</summary>
		/// <returns>true if the specified object  is equal to the current object; otherwise, false.</returns>
		/// <param name="obj">The object to compare with the current object. </param>
		public override bool Equals(object obj)
		{
			return Equals(obj as TrelloAuthorization);
		}

		/// <summary>Serves as the default hash function. </summary>
		/// <returns>A hash code for the current object.</returns>
		public override int GetHashCode()
		{
			unchecked
			{
				return ((_appKey != null ? _appKey.GetHashCode() : 0) * 397) ^ (UserToken != null ? UserToken.GetHashCode() : 0);
			}
		}

		/// <summary>
		/// Compares two <see cref="TrelloAuthorization"/> instances for equality.
		/// </summary>
		/// <param name="left">An authorization.</param>
		/// <param name="right">An authorization.</param>
		/// <returns>true if equal; false otherwise.</returns>
		public static bool operator ==(TrelloAuthorization left, TrelloAuthorization right)
		{
			return Equals(left, right);
		}

		/// <summary>
		/// Compares two <see cref="TrelloAuthorization"/> instances for equality.
		/// </summary>
		/// <param name="left">An authorization.</param>
		/// <param name="right">An authorization.</param>
		/// <returns>false if equal; true otherwise.</returns>
		public static bool operator !=(TrelloAuthorization left, TrelloAuthorization right)
		{
			return !Equals(left, right);
		}
	}
}