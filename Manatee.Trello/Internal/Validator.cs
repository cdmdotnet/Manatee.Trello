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
 
	File Name:		Validate.cs
	Namespace:		Manatee.Trello
	Class Name:		Validate
	Purpose:		Exposes validation methods.

***************************************************************************************/
using System;
using System.Collections.Generic;
using System.Linq;
using Manatee.Trello.Contracts;
using Manatee.Trello.Exceptions;
using Manatee.Trello.Internal.DataAccess;
using Manatee.Trello.Json;

namespace Manatee.Trello.Internal
{
	internal class Validator : IValidator
	{
		private readonly IJsonRepository _entityRepository;
		private readonly ITrelloService _svc;

		public Validator(ITrelloService svc, IJsonRepository entityRepository)
		{
			_svc = svc;
			_entityRepository = entityRepository;
		}

		public void Writable()
		{
			if (_svc == null) return;
			if (string.IsNullOrWhiteSpace(_svc.UserToken))
				_svc.Configuration.Log.Error(new ReadOnlyAccessException());
		}
		public void Entity<T>(T entity, bool allowNulls = false)
			where T : ExpiringObject
		{
			if (entity == null)
			{
				if (allowNulls) return;
				_svc.Configuration.Log.Error(new ArgumentNullException("entity"));
			}
			if (string.IsNullOrWhiteSpace(entity.Id))
				_svc.Configuration.Log.Error(new EntityNotOnTrelloException<T>(entity));
		}
		public void Nullable<T>(T? value)
			where T : struct
		{
			if (!value.HasValue)
				_svc.Configuration.Log.Error(new ArgumentNullException("value"));
		}
		public void NonEmptyString(string str)
		{
			if (string.IsNullOrWhiteSpace(str))
				_svc.Configuration.Log.Error(new ArgumentNullException("str"));
		}
		public void Position(Position pos)
		{
			if (pos == null)
				_svc.Configuration.Log.Error(new ArgumentNullException("pos"));
			if (!pos.IsValid)
				_svc.Configuration.Log.Error(new ArgumentException("Cannot set an invalid position."));
		}
		public string MinStringLength(string str, int minLength, string parameter)
		{
			if (str == null)
				_svc.Configuration.Log.Error(new ArgumentNullException("str"));
			str = str.Trim();
			if (str.Length < minLength)
				_svc.Configuration.Log.Error(new ArgumentException(string.Format("{0} must be at least {1} characters and cannot begin or end with whitespace.",
																				  parameter,
																				  minLength)));
			return str;
		}
		public string StringLengthRange(string str, int low, int high, string parameter)
		{
			if (str == null)
				_svc.Configuration.Log.Error(new ArgumentNullException("str"));
			str = str.Trim();
			if (!str.Length.BetweenInclusive(low, high))
				_svc.Configuration.Log.Error(new ArgumentException(string.Format("{0} must be from {1} to {2} characters and cannot begin or end with whitespace.",
																	parameter,
																	low,
																	high)));
			return str;
		}
		public string UserName(string value)
		{
			var retVal = MinStringLength(value, 3, "Username");
			if (retVal != retVal.ToLower())
				_svc.Configuration.Log.Error(new ArgumentException("Username may only contain lowercase characters, underscores, and numbers"));
			if (_svc != null)
			{
				var endpoint = new Endpoint(new[] {"search", "members"});
				var parameters = new Dictionary<string, object>
					{
						{"query", retVal},
						{"fields", "id"},
					};
				if (_entityRepository.Get<List<IJsonMember>>(endpoint.ToString(), parameters).Count != 0)
					_svc.Configuration.Log.Error(new UsernameInUseException(value));
			}
			return retVal;
		}
		public string OrgName(string value)
		{
			var retVal = MinStringLength(value, 3, "Name");
			if (retVal != retVal.ToLower())
				_svc.Configuration.Log.Error(new ArgumentException("Name may only contain lowercase characters, underscores, and numbers"));
			if (_svc != null)
			{
				var endpoint = new Endpoint(new[] {"search", retVal});
				var parameters = new Dictionary<string, object>
					{
						{"modelTypes", "organizations"},
						{"fields", "id"},
					};
				var response = _entityRepository.Get<IJsonSearchResults>(endpoint.ToString(), parameters);
				if (response.OrganizationIds.Count != 0)
					_svc.Configuration.Log.Error(new OrgNameInUseException(value));
			}
			return retVal;
		}
		public void Enumeration<T>(T value)
		{
			var validValues = Enum.GetValues(typeof (T)).Cast<T>();
			if (!validValues.Contains(value))
				_svc.Configuration.Log.Error(new ArgumentException(string.Format("Value '{0}' is not valid for type '{1}'", value, typeof (T))));
		}
		public void Url(string url)
		{
			NonEmptyString(url);
			if ((url.Substring(0, 7) != "http://") && (url.Substring(0, 8) != "https://"))
				_svc.Configuration.Log.Error(new ArgumentException("URL is not valid.  Must begin with 'http://' or 'https://'"));
		}
		public void ArgumentNotNull(object value, string name = "value")
		{
			if (value == null)
				_svc.Configuration.Log.Error(new ArgumentNullException(name));
		}
	}
}
