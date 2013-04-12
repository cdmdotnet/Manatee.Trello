using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Text.RegularExpressions;
using Manatee.Trello.Contracts;
using Manatee.Trello.Exceptions;

namespace Manatee.Trello.Implementation
{
	internal static class Validate
	{
		public static void Writable(ITrelloRest api)
		{
			if (api == null) return;
			if (api.AuthToken == null)
				throw new ReadOnlyAccessException();
		}
		public static void Entity<T>(T entity, bool allowNulls = false)
			where T : ExpiringObject
		{
			if (entity == null)
			{
				if (allowNulls) return;
				throw new ArgumentNullException("entity");
			}
			if (entity.Id == null)
				throw new EntityNotOnTrelloException<T>(entity);
		}
		public static void Nullable<T>(T? value)
			where T : struct
		{
			if (!value.HasValue)
				throw new ArgumentNullException("value");
		}
		public static void NonEmptyString(string str)
		{
			if (string.IsNullOrWhiteSpace(str))
				throw new ArgumentNullException("str");
		}
		public static void Position(Position pos)
		{
			if (pos == null)
				throw new ArgumentNullException("pos");
			if (!pos.IsValid)
				throw new ArgumentException("Cannot set an invalid position.");
		}
		public static string MinStringLength(string str, int minLength, string parameter)
		{
			if (str == null)
				throw new ArgumentNullException("str");
			str = str.Trim();
			if (str.Length < minLength)
				throw new ArgumentException(string.Format("{0} must be at least {1} characters and cannot begin or end with whitespace.", parameter, minLength));
			return str;
		}
		public static string StringLengthRange(string str, int low, int high, string parameter)
		{
			if (str == null)
				throw new ArgumentNullException("str");
			str = str.Trim();
			if (!str.Length.BetweenInclusive(low, high))
				throw new ArgumentException(string.Format("{0} must be from {1} to {2} characters and cannot begin or end with whitespace.", parameter, low, high));
			return str;
		}
		public static string UserName(ITrelloRest svc, string value)
		{
			var retVal = MinStringLength(value, 3, "Username");
			if ((svc != null) && (svc.Get(svc.RequestProvider.Create<Member>(retVal)) != null))
				throw new UsernameInUseException(value);
			return retVal;
		}
		public static string OrgName(ITrelloRest svc, string value)
		{
			var retVal = MinStringLength(value, 3, "Name");
			if ((svc != null) && (svc.Get(svc.RequestProvider.Create<Organization>(retVal)) != null))
				throw new OrgNameInUseException(value);
			return retVal;
		}
	}
}
