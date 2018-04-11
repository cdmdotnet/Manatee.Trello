using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Reflection;

namespace Manatee.Trello
{
	// original source for concept: https://stackoverflow.com/a/38557486/878701
	// modified to suit needs

	///<summary>
	/// Enumerates known types of <see cref="Action"/>s.
	///</summary>
	public struct ActionType : IEquatable<ActionType>, IComparable<ActionType>, IComparable
	{
		#region "Enum" Members

		/// <summary>
		/// Not recognized.  May have been created since the current version of this API.
		/// </summary>
		/// <remarks>This value is not supported by Trello's API.</remarks>
		public static readonly ActionType Unknown;

		/// <summary>
		/// Indicates a <see cref="Member"/> was added to a <see cref="Board"/>.
		/// </summary>
		[Display(Description = "addAdminToBoard")]
		public static readonly ActionType AddAdminToBoard;

		/// <summary>
		/// Indicates a <see cref="Member"/> was added to a <see cref="Organization"/>.
		/// </summary>
		[Display(Description = "addAdminToOrganization")]
		public static readonly ActionType AddAdminToOrganization;

		/// <summary>
		/// Indicates an <see cref="Attachment"/> was added to a <see cref="Card"/>.
		/// </summary>
		[Display(Description = "addAttachmentToCard")]
		public static readonly ActionType AddAttachmentToCard;

		/// <summary>
		/// Indicates a <see cref="Member"/> pinned a <see cref="Board"/>.
		/// </summary>
		[Display(Description = "addBoardsPinnedToMember")]
		public static readonly ActionType AddBoardsPinnedToMember;

		/// <summary>
		/// Indicates a <see cref="CheckList"/> was added to a <see cref="Card"/>.
		/// </summary>
		[Display(Description = "addChecklistToCard")]
		public static readonly ActionType AddChecklistToCard;

		/// <summary>
		/// Indicates a <see cref="Label"/> was added to a <see cref="Card"/>.
		/// </summary>
		[Display(Description = "addLabelToCard")]
		public static readonly ActionType AddLabelToCard;

		/// <summary>
		/// Indicates a <see cref="Member"/> was added to a <see cref="Board"/>.
		/// </summary>
		[Display(Description = "addMemberToBoard")]
		public static readonly ActionType AddMemberToBoard;

		/// <summary>
		/// Indicates a <see cref="Member"/> was added to a <see cref="Card"/>.
		/// </summary>
		[Display(Description = "addMemberToCard")]
		public static readonly ActionType AddMemberToCard;

		/// <summary>
		/// Indicates a <see cref="Member"/> was added to an <see cref="Organization"/>.
		/// </summary>
		[Display(Description = "addMemberToOrganization")]
		public static readonly ActionType AddMemberToOrganization;

		/// <summary>
		/// Indicates a <see cref="Organization"/> was added to a <see cref="Board"/>.
		/// </summary>
		[Display(Description = "addToOrganizationBoard")]
		public static readonly ActionType AddToOrganizationBoard;

		/// <summary>
		/// Indicates a comment was added to a <see cref="Card"/>.
		/// </summary>
		[Display(Description = "commentCard")]
		public static readonly ActionType CommentCard;

		/// <summary>
		/// Indicates a <see cref="CheckList"/> item was converted to <see cref="Card"/>.
		/// </summary>
		[Display(Description = "convertToCardFromCheckItem")]
		public static readonly ActionType ConvertToCardFromCheckItem;

		/// <summary>
		/// Indicates a <see cref="Board"/> was copied.
		/// </summary>
		[Display(Description = "copyBoard")]
		public static readonly ActionType CopyBoard;

		/// <summary>
		/// Indicates a <see cref="Card"/> was copied.
		/// </summary>
		[Display(Description = "copyCard")]
		public static readonly ActionType CopyCard;

		/// <summary>
		/// Indicates a <see cref="CheckList"/> was copied.
		/// </summary>
		[Display(Description = "copyChecklist")]
		public static readonly ActionType CopyChecklist;

		/// <summary>
		/// Indicates a comment was copied from one <see cref="Card"/> to another.
		/// </summary>
		[Display(Description = "copyCommentCard")]
		public static readonly ActionType CopyCommentCard;

		/// <summary>
		/// Indicates a <see cref="Board"/> was created.
		/// </summary>
		[Display(Description = "createBoard")]
		public static readonly ActionType CreateBoard;

		/// <summary>
		/// Indicates a <see cref="Member"/> was invided to a <see cref="Board"/>.
		/// </summary>
		[Display(Description = "createBoardInvitation")]
		public static readonly ActionType CreateBoardInvitation;

		/// <summary>
		/// Indicates a <see cref="Board"/> preference was created.
		/// </summary>
		[Display(Description = "createBoardPreference")]
		public static readonly ActionType CreateBoardPreference;

		/// <summary>
		/// Indicates a <see cref="Card"/> was created.
		/// </summary>
		[Display(Description = "createCard")]
		public static readonly ActionType CreateCard;

		/// <summary>
		/// Indicates a <see cref="CheckList"/> was created.
		/// </summary>
		[Display(Description = "createChecklist")]
		public static readonly ActionType CreateChecklist;

		/// <summary>
		/// Indicates a <see cref="Label"/> was created.
		/// </summary>
		[Display(Description = "createLabel")]
		public static readonly ActionType CreateLabel;

		/// <summary>
		/// Indicates a <see cref="List"/> was created.
		/// </summary>
		[Display(Description = "createList")]
		public static readonly ActionType CreateList;

		/// <summary>
		/// Indicates an <see cref="Organization"/> was created.
		/// </summary>
		[Display(Description = "createOrganization")]
		public static readonly ActionType CreateOrganization;

		/// <summary>
		/// Indicates a <see cref="Member"/> was invided to an <see cref="Organization"/>.
		/// </summary>
		[Display(Description = "createOrganizationInvitation")]
		public static readonly ActionType CreateOrganizationInvitation;

		/// <summary>
		/// Indicates an <see cref="Attachment"/> was deleted from a <see cref="Card"/>.
		/// </summary>
		[Display(Description = "deleteAttachmentFromCard")]
		public static readonly ActionType DeleteAttachmentFromCard;

		/// <summary>
		/// Indicates an invitation to a <see cref="Board"/> was rescinded.
		/// </summary>
		[Display(Description = "deleteBoardInvitation")]
		public static readonly ActionType DeleteBoardInvitation;

		/// <summary>
		/// Indicates a <see cref="Card"/> was deleted.
		/// </summary>
		[Display(Description = "deleteCard")]
		public static readonly ActionType DeleteCard;

		/// <summary>
		/// Indicates a <see cref="CheckItem"/> was deleted.
		/// </summary>
		[Display(Description = "deleteCheckItem")]
		public static readonly ActionType DeleteCheckItem;

		/// <summary>
		/// Indicates a <see cref="Label"/> was deleted.
		/// </summary>
		[Display(Description = "deleteLabel")]
		public static readonly ActionType DeleteLabel;

		/// <summary>
		/// Indicates an invitation to an <see cref="Organization"/> was rescinded.
		/// </summary>
		[Display(Description = "deleteOrganizationInvitation")]
		public static readonly ActionType DeleteOrganizationInvitation;

		/// <summary>
		/// Indicates a power-up was disabled.
		/// </summary>
		[Display(Description = "disablePlugin")]
		public static readonly ActionType DisablePlugin;

		/// <summary>
		/// Indicates a power-up was disabled.
		/// </summary>
		[Display(Description = "disablePowerUp")]
		public static readonly ActionType DisablePowerUp;

		/// <summary>
		/// Indicates a <see cref="Card"/> was created via email.
		/// </summary>
		[Display(Description = "emailCard")]
		public static readonly ActionType EmailCard;

		/// <summary>
		/// Indicates a power-up was enabled.
		/// </summary>
		[Display(Description = "enablePlugin")]
		public static readonly ActionType EnablePlugin;

		/// <summary>
		/// Indicates a power-up was enabled.
		/// </summary>
		[Display(Description = "enablePowerUp")]
		public static readonly ActionType EnablePowerUp;

		/// <summary>
		/// Indicates a <see cref="Member"/> was made an admin of a <see cref="Board"/>.
		/// </summary>
		[Display(Description = "makeAdminOfBoard")]
		public static readonly ActionType MakeAdminOfBoard;

		/// <summary>
		/// Indicates a <see cref="Member"/> was made an admin of an <see cref="Organization"/>.
		/// </summary>
		[Display(Description = "makeAdminOfOrganization")]
		public static readonly ActionType MakeAdminOfOrganization;

		/// <summary>
		/// Indicates a <see cref="Member"/> was made a normal <see cref="Member"/> of a <see cref="Board"/>.
		/// </summary>
		[Display(Description = "makeNormalMemberOfBoard")]
		public static readonly ActionType MakeNormalMemberOfBoard;

		/// <summary>
		/// Indicates a <see cref="Member"/> was made a normal <see cref="Member"/> of an <see cref="Organization"/>.
		/// </summary>
		[Display(Description = "makeNormalMemberOfOrganization")]
		public static readonly ActionType MakeNormalMemberOfOrganization;

		/// <summary>
		/// Indicates a <see cref="Member"/> was made an observer of a <see cref="Board"/>.
		/// </summary>
		[Display(Description = "makeObserverOfBoard")]
		public static readonly ActionType MakeObserverOfBoard;

		/// <summary>
		/// Indicates a <see cref="Member"/> joined Trello.
		/// </summary>
		[Display(Description = "memberJoinedTrello")]
		public static readonly ActionType MemberJoinedTrello;

		/// <summary>
		/// Indicates a <see cref="Card"/> was moved from one <see cref="Board"/> to another.
		/// </summary>
		[Display(Description = "moveCardFromBoard")]
		public static readonly ActionType MoveCardFromBoard;

		/// <summary>
		/// Indicates a <see cref="Card"/> was moved from one <see cref="Board"/> to another.
		/// </summary>
		[Display(Description = "moveCardToBoard")]
		public static readonly ActionType MoveCardToBoard;

		/// <summary>
		/// Indicates a <see cref="List"/> was moved from one <see cref="Board"/> to another.
		/// </summary>
		[Display(Description = "moveListFromBoard")]
		public static readonly ActionType MoveListFromBoard;

		/// <summary>
		/// Indicates a <see cref="List"/> was moved from one <see cref="Board"/> to another.
		/// </summary>
		[Display(Description = "moveListToBoard")]
		public static readonly ActionType MoveListToBoard;

		/// <summary>
		/// Indicates a <see cref="Member"/> was removed as an admin from a <see cref="Board"/>.
		/// </summary>
		[Display(Description = "removeAdminFromBoard")]
		public static readonly ActionType RemoveAdminFromBoard;

		/// <summary>
		/// Indicates a <see cref="Member"/> was removed as an admin from an <see cref="Organization"/>.
		/// </summary>
		[Display(Description = "removeAdminFromOrganization")]
		public static readonly ActionType RemoveAdminFromOrganization;

		/// <summary>
		/// Indicates an <see cref="Member"/> unpinnned a <see cref="Board"/>.
		/// </summary>
		[Display(Description = "removeBoardsPinnedFromMember")]
		public static readonly ActionType RemoveBoardsPinnedFromMember;

		/// <summary>
		/// Indicates a <see cref="CheckList"/> was removed from a <see cref="Card"/>.
		/// </summary>
		[Display(Description = "removeChecklistFromCard")]
		public static readonly ActionType RemoveChecklistFromCard;

		/// <summary>
		/// Indicates an <see cref="Organization"/> was removed from a <see cref="Board"/>.
		/// </summary>
		[Display(Description = "removeFromOrganizationBoard")]
		public static readonly ActionType RemoveFromOrganizationBoard;

		/// <summary>
		/// Indicates a <see cref="Label"/> was removed from a <see cref="Card"/>.
		/// </summary>
		[Display(Description = "removeLabelFromCard")]
		public static readonly ActionType RemoveLabelFromCard;

		/// <summary>
		/// Indicates a <see cref="Member"/> was removed from a <see cref="Board"/>.
		/// </summary>
		[Display(Description = "removeMemberFromBoard")]
		public static readonly ActionType RemoveMemberFromBoard;

		/// <summary>
		/// Indicates a <see cref="Member"/> was removed from a <see cref="Card"/>.
		/// </summary>
		[Display(Description = "removeMemberFromCard")]
		public static readonly ActionType RemoveMemberFromCard;

		/// <summary>
		/// Indicates a <see cref="Member"/> was removed from an <see cref="Organization"/>.
		/// </summary>
		[Display(Description = "removeMemberFromOrganization")]
		public static readonly ActionType RemoveMemberFromOrganization;

		/// <summary>
		/// Indicates an invitation to a <see cref="Board"/> was created.
		/// </summary>
		[Display(Description = "unconfirmedBoardInvitation")]
		public static readonly ActionType UnconfirmedBoardInvitation;

		/// <summary>
		/// Indicates an invitation to an <see cref="Organization"/> was created.
		/// </summary>
		[Display(Description = "unconfirmedOrganizationInvitation")]
		public static readonly ActionType UnconfirmedOrganizationInvitation;

		/// <summary>
		/// Indicates a <see cref="Board"/> was updated.
		/// </summary>
		[Display(Description = "updateBoard")]
		public static readonly ActionType UpdateBoard;

		/// <summary>
		/// Indicates a <see cref="Card"/> was updated.
		/// </summary>
		[Display(Description = "updateCard")]
		public static readonly ActionType UpdateCard;

		/// <summary>
		/// Indicates a <see cref="CheckItem"/> was updated.
		/// </summary>
		[Display(Description = "updateCheckItem")]
		public static readonly ActionType UpdateCheckItem;

		/// <summary>
		/// Indicates a <see cref="CheckList"/> was updated.
		/// </summary>
		[Display(Description = "updateCheckItemStateOnCard")]
		public static readonly ActionType UpdateCheckItemStateOnCard;

		/// <summary>
		/// Indicates that a <see cref="CustomField"/> was updated.
		/// </summary>
		[Display(Description = "updateCustomField")]
		public static readonly ActionType UpdateCustomField;

		/// <summary>
		/// Indicates a <see cref="CheckList"/> was updated.
		/// </summary>
		[Display(Description = "updateChecklist")]
		public static readonly ActionType UpdateChecklist;

		/// <summary>
		/// Indicates a <see cref="Label"/> was updated.
		/// </summary>
		[Display(Description = "updateLabel")]
		public static readonly ActionType UpdateLabel;

		/// <summary>
		/// Indicates a <see cref="List"/> was updated.
		/// </summary>
		[Display(Description = "updateList")]
		public static readonly ActionType UpdateList;

		/// <summary>
		/// Indicates a <see cref="Member"/> was updated.
		/// </summary>
		[Display(Description = "updateMember")]
		public static readonly ActionType UpdateMember;

		/// <summary>
		/// Indicates an <see cref="Organization"/> was updated.
		/// </summary>
		[Display(Description = "updateOrganization")]
		public static readonly ActionType UpdateOrganization;

		/// <summary>
		/// Indicates a <see cref="Member"/> voted for a <see cref="Card"/>.
		/// </summary>
		[Display(Description = "voteOnCard")]
		public static readonly ActionType VoteOnCard;

		/// <summary>
		/// Indictes the default set of values returned by <see cref="Card.Actions"/>.
		/// </summary>
		public static ActionType DefaultForCardActions { get; } = CommentCard | UpdateCard;
		/// <summary>
		/// Indicates all action types
		/// </summary>
		[Display(Description = "all")]
		public static ActionType All { get; }

		#endregion

		#region State...

		private static readonly int FieldCount;
		private static readonly List<ActionType> FieldValues;
		private BitArray _array;
		private string _description;

		/// <summary>
		/// Lazy-initialized BitArray.
		/// </summary>
		private BitArray Bits => _array ?? (_array = new BitArray(FieldCount));

		#endregion ...State

		#region Construction...

		/// <summary>
		/// Static constructor. Sets the static public fields.
		/// </summary>
		static ActionType()
		{
			var fields = typeof(ActionType).GetTypeInfo().DeclaredFields.Where(f => f.IsStatic && f.IsPublic).ToList();
			FieldCount = fields.Count;
			FieldValues = new List<ActionType>();
			for (int i = 0; i < fields.Count; i++)
			{
				var field = fields[i];
				var fieldVal = new ActionType {_description = field.GetCustomAttribute<DisplayAttribute>()?.Description};
				fieldVal.Bits.Set(i, true);
				field.SetValue(null, fieldVal);
				FieldValues.Add(fieldVal);
			}

			var all = FieldValues.Aggregate(Unknown, (c, a) => a | c);
			all._description = "all";
			All = all;
		}
		#endregion ...Construction

		#region Operators...

		/// <summary>
		/// OR operator. Or together ActionType instances.
		/// </summary>
		/// <param name="lhs"></param>
		/// <param name="rhs"></param>
		/// <returns></returns>
		public static ActionType operator |(ActionType lhs, ActionType rhs)
		{
			var result = new ActionType();
			// BitArray is modified in place - always copy!
			result._array = new BitArray(lhs.Bits).Or(rhs.Bits);

			return result;
		}

		/// <summary>
		/// AND operator. And together ActionType instances.
		/// </summary>
		/// <param name="lhs"></param>
		/// <param name="rhs"></param>
		/// <returns></returns>
		public static ActionType operator &(ActionType lhs, ActionType rhs)
		{
			var result = new ActionType();
			// BitArray is modified in place - always copy!
			result._array = new BitArray(lhs.Bits).And(rhs.Bits);

			return result;
		}

		/// <summary>
		/// XOR operator. Xor together ActionType instances.
		/// </summary>
		/// <param name="lhs"></param>
		/// <param name="rhs"></param>
		/// <returns></returns>
		public static ActionType operator ^(ActionType lhs, ActionType rhs)
		{
			var result = new ActionType();
			// BitArray is modified in place - always copy!
			result._array = new BitArray(lhs.Bits).Xor(rhs.Bits);

			return result;
		}

		/// <summary>
		/// Equality operator.
		/// </summary>
		/// <param name="lhs"></param>
		/// <param name="rhs"></param>
		/// <returns></returns>
		public static bool operator ==(ActionType lhs, ActionType rhs)
		{
			return lhs.Equals(rhs);
		}

		/// <summary>
		/// Inequality operator.
		/// </summary>
		/// <param name="lhs"></param>
		/// <param name="rhs"></param>
		/// <returns></returns>
		public static bool operator !=(ActionType lhs, ActionType rhs)
		{
			return !(lhs == rhs);
		}
		#endregion ...Operators

		#region System.Object Overrides...

		/// <summary>
		/// Overridden. Returns a comma-separated string.
		/// </summary>
		/// <returns></returns>
		public override string ToString()
		{
			if (_description != null) return _description;

			var names = new List<string>();
			for (int i = 0; i < FieldCount; i++)
			{
				if (Bits[i])
					names.Add(FieldValues[i]._description);
			}

			return string.Join(",", names);
		}

		/// <summary>
		/// Overridden. Compares equality with another object.
		/// </summary>
		/// <param name="obj"></param>
		/// <returns></returns>
		public override bool Equals(object obj)
		{
			return obj is ActionType flags && Equals(flags);
		}

		/// <summary>
		/// Overridden. Gets the hash code of the internal BitArray.
		/// </summary>
		/// <returns></returns>
		public override int GetHashCode()
		{
			int hash = 17;
			for (int i = 0; i < Bits.Length; i++)
			{
				if (Bits[i])
					hash ^= i;
			}

			return hash;
		}
		#endregion ...System.Object Overrides

		#region IEquatable<ActionType> Members...

		/// <summary>
		/// Strongly-typed equality method.
		/// </summary>
		/// <param name="other"></param>
		/// <returns></returns>
		public bool Equals(ActionType other)
		{
			for (int i = 0; i < Bits.Length; i++)
			{
				if (Bits[i] != other.Bits[i])
					return false;
			}

			return true;
		}

		#endregion ...IEquatable<ActionType> Members

		#region IComparable<ActionType> Members...

		/// <summary>
		/// Compares based on highest bit set. Instance with higher
		/// bit set is bigger.
		/// </summary>
		/// <param name="other"></param>
		/// <returns></returns>
		public int CompareTo(ActionType other)
		{
			for (int i = Bits.Length - 1; i >= 0; i--)
			{
				bool thisVal = Bits[i];
				bool otherVal = other.Bits[i];
				if (thisVal && !otherVal)
					return 1;
				else if (!thisVal && otherVal)
					return -1;
			}

			return 0;
		}
		#endregion ...IComparable<ActionType> Members

		#region IComparable Members...

		int IComparable.CompareTo(object obj)
		{
			if (obj is ActionType)
			{
				return CompareTo((ActionType)obj);
			}

			return -1;
		}
		#endregion ...IComparable Members

		#region IConvertible Members...

		/// <summary>
		/// Returns TypeCode.Object.
		/// </summary>
		/// <returns></returns>
		public TypeCode GetTypeCode()
		{
			return TypeCode.Object;
		}
		#endregion ...IConvertible Members

		#region Public Interface...

		/// <summary>
		/// Checks <paramref name="flags"/> to see if all the bits set in that flags are also set in this flags.
		/// </summary>
		/// <param name="flags"></param>
		/// <returns></returns>
		public bool HasFlag(ActionType flags)
		{
			return (this & flags) == flags;
		}

		/// <summary>
		/// Gets all of the flags that are active in this instance.
		/// </summary>
		/// <returns></returns>
		public IEnumerable<ActionType> GetFlags()
		{
			return FieldValues.Where(HasFlag).ToList();
		}

		/// <summary>
		/// Gets the names of this ActionType enumerated type.
		/// </summary>
		/// <returns></returns>
		public static string[] GetNames()
		{
			return FieldValues.Select(x => x._description).ToArray();
		}

		/// <summary>
		/// Gets all the values of this ActionType enumerated type.
		/// </summary>
		/// <returns></returns>
		public static ActionType[] GetValues()
		{
			return FieldValues.ToArray();
		}

		/// <summary>
		/// Standard TryParse pattern. Parses a ActionType result from a string.
		/// </summary>
		/// <param name="s"></param>
		/// <param name="result"></param>
		/// <returns></returns>
		public static bool TryParse(string s, out ActionType result)
		{
			result = new ActionType();
			if (string.IsNullOrEmpty(s))
				return true;

			var fieldNames = s.Split(',');
			foreach (var f in fieldNames)
			{
				var field = FieldValues.FirstOrDefault(x => string.Equals(x._description, f.Trim(), StringComparison.OrdinalIgnoreCase));
				if (field._description == null)
				{
					result = Unknown;
					return false;
				}
				result.Bits.Set(FieldValues.IndexOf(field), true);
			}

			return true;
		}

		#endregion ...Public Interface
	}
}