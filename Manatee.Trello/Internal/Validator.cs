/***************************************************************************************

	Copyright 2013 Little Crab Solutions

	   Licensed under the Apache License, Version 2.0 (the "License");
	   you may not use this file except in compliance with the License.
	   You may obtain a copy of the License at

		 http://www.apache.org/licenses/LICENSE-2.0

	   Unless required by applicable law or agreed to in writing, software
	   distributed under the License is distributed on an "AS IS" BASIS,
	   WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
	   See the License for the specific language governing permissions and
	   limitations under the License.
 
	File Name:		Validator.cs
	Namespace:		Manatee.Trello.Internal
	Class Name:		Validator
	Purpose:		Exposes validation methods.

***************************************************************************************/
using System;
using System.Linq;
using System.Text.RegularExpressions;
using Manatee.Trello.Contracts;
using Manatee.Trello.Exceptions;

namespace Manatee.Trello.Internal
{
	internal class Validator : IValidator
	{
		private readonly ITrelloService _trelloService;

		public Validator(ITrelloService trelloService)
		{
			_trelloService = trelloService;
		}

		public void Writable()
		{
			if (_trelloService.UserToken.IsNullOrWhiteSpace())
				TrelloServiceConfiguration.Log.Error(new ReadOnlyAccessException());
		}
		public void Entity<T>(T entity, bool allowNulls = false)
			where T : ExpiringObject
		{
			if (entity == null)
			{
				if (allowNulls) return;
				TrelloServiceConfiguration.Log.Error(new ArgumentNullException("entity"));
			}
			if (entity.Id.IsNullOrWhiteSpace() || entity.IsStubbed)
				TrelloServiceConfiguration.Log.Error(new EntityNotOnTrelloException<T>(entity));
		}
		public void Nullable<T>(T? value)
			where T : struct
		{
			if (!value.HasValue)
				TrelloServiceConfiguration.Log.Error(new ArgumentNullException("value"));
		}
		public void NonEmptyString(string str)
		{
			if (str.IsNullOrWhiteSpace())
			TrelloServiceConfiguration.Log.Error(new ArgumentNullException("str"));
		}
		public void Position(Position pos)
		{
			if (pos == null)
				TrelloServiceConfiguration.Log.Error(new ArgumentNullException("pos"));
			if (!pos.IsValid)
				TrelloServiceConfiguration.Log.Error(new ArgumentException("Cannot set an invalid position."));
		}
		public string MinStringLength(string str, int minLength, string parameter)
		{
			NonEmptyString(str);
			str = str.Trim();
			if (str.Length < minLength)
				TrelloServiceConfiguration.Log.Error(new ArgumentException(string.Format("{0} must be at least {1} characters and cannot begin or end with whitespace.",
				                                                                         parameter, minLength)));
			return str;
		}
		public string StringLengthRange(string str, int low, int high, string parameter)
		{
			NonEmptyString(str);
			str = str.Trim();
			if (!str.Length.BetweenInclusive(low, high))
				TrelloServiceConfiguration.Log.Error(new ArgumentException(string.Format("{0} must be from {1} to {2} characters and cannot begin or end with whitespace.",
				                                                                         parameter, low, high)));
			return str;
		}
		public string UserName(string value)
		{
			var retVal = MinStringLength(value, 3, "Username");
			var regex = new Regex("^[a-z0-9_]+$");
			if (!regex.IsMatch(retVal))
				TrelloServiceConfiguration.Log.Error(new ArgumentException("Username may only contain lowercase characters, underscores, and numbers"));
			var response = _trelloService.SearchMembers(value);
			if (response.Any())
				TrelloServiceConfiguration.Log.Error(new ArgumentException(string.Format("Username '{0}' already exists.", value)));
			return retVal;
		}
		public string OrgName(string value)
		{
			var retVal = MinStringLength(value, 3, "Name");
			var regex = new Regex("^[a-z0-9_]+$");
			if (!regex.IsMatch(retVal))
				TrelloServiceConfiguration.Log.Error(new ArgumentException("Name may only contain lowercase characters, underscores, and numbers"));
			var response = _trelloService.Search(value, modelTypes: SearchModelType.Organizations);
			if ((response.Organizations != null) && response.Organizations.Any())
				TrelloServiceConfiguration.Log.Error(new ArgumentException(string.Format("Organization '{0}' already exists.", value)));
			return retVal;
		}
		public void Enumeration<T>(T value)
		{
			var validValues = Enum.GetValues(typeof (T)).Cast<T>();
			if (!validValues.Contains(value))
				TrelloServiceConfiguration.Log.Error(new ArgumentException(string.Format("Value '{0}' is not valid for type '{1}'", value, typeof (T))));
		}
		public void Url(string url)
		{
			if (url.IsNullOrWhiteSpace()) return;
			if (!(url.BeginsWith("http://") || url.BeginsWith("https://")) || !Uri.IsWellFormedUriString(url, UriKind.Absolute))
				TrelloServiceConfiguration.Log.Error(new ArgumentException("URL is not valid.  Must be well-formed and begin with 'http://' or 'https://'"));
		}
		public void ArgumentNotNull(object value, string name = "value")
		{
			if (value == null)
				TrelloServiceConfiguration.Log.Error(new ArgumentNullException(name));
		}
	}
}
