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
 
	File Name:		LabelNames.cs
	Namespace:		Manatee.Trello
	Class Name:		LabelNames
	Purpose:		Defines a set of labels for a board on Trello.com.

***************************************************************************************/
using System;
using System.Collections;
using System.Collections.Generic;
using Manatee.Json;
using Manatee.Json.Enumerations;
using Manatee.Json.Extensions;
using Manatee.Trello.Contracts;
using Manatee.Trello.Implementation;

namespace Manatee.Trello
{
	//   "labelNames":{
	//      "red":"",
	//      "orange":"",
	//      "yellow":"",
	//      "green":"",
	//      "blue":"",
	//      "purple":""
	//   }
	/// <summary>
	/// Defines a set of labels for a board.
	/// </summary>
	public class LabelNames : JsonCompatibleExpiringObject, IEnumerable<Label>
	{
		private string _red;
		private string _orange;
		private string _yellow;
		private string _green;
		private string _blue;
		private string _purple;

		/// <summary>
		/// Gets or sets the name of the red label.
		/// </summary>
		public string Red
		{
			get
			{
				VerifyNotExpired();
				return _red;
			}
			set
			{
				if (_red == value) return;
				_red = value ?? string.Empty;
				Parameters.Add("value", _red);
				Put("red");
			}
		}
		/// <summary>
		/// Gets or sets the name of the orange label.
		/// </summary>
		public string Orange
		{
			get
			{
				VerifyNotExpired();
				return _orange;
			}
			set
			{
				if (_orange == value) return;
				_orange = value ?? string.Empty;
				Parameters.Add("value", _orange);
				Put("orange");
			}
		}
		/// <summary>
		/// Gets or sets the name of the yellow label.
		/// </summary>
		public string Yellow
		{
			get
			{
				VerifyNotExpired();
				return _yellow;
			}
			set
			{
				if (_yellow == value) return;
				_yellow = value ?? string.Empty;
				Parameters.Add("value", _yellow);
				Put("yellow");
			}
		}
		/// <summary>
		/// Gets or sets the name of the green label.
		/// </summary>
		public string Green
		{
			get
			{
				VerifyNotExpired();
				return _green;
			}
			set
			{
				if (_green == value) return;
				_green = value ?? string.Empty;
				Parameters.Add("value", _green);
				Put("green");
			}
		}
		/// <summary>
		/// Gets or sets the name of the blue label.
		/// </summary>
		public string Blue
		{
			get
			{
				VerifyNotExpired();
				return _blue;
			}
			set
			{
				if (_blue == value) return;
				_blue = value ?? string.Empty;
				Parameters.Add("value", _blue);
				Put("blue");
			}
		}
		/// <summary>
		/// Gets or sets the name of the purple label.
		/// </summary>
		public string Purple
		{
			get
			{
				VerifyNotExpired();
				return _purple;
			}
			set
			{
				if (_purple == value) return;
				_purple = value ?? string.Empty;
				Parameters.Add("value", _purple);
				Put("purple");
			}
		}

		/// <summary>
		/// Creates a new instance of the LabelNames class.
		/// </summary>
		public LabelNames() {}
		internal LabelNames(TrelloService svc, Board owner)
			: base(svc, owner) {}

		/// <summary>
		/// Builds an object from a JsonValue.
		/// </summary>
		/// <param name="json">The JsonValue representation of the object.</param>
		public override void FromJson(JsonValue json)
		{
			if (json == null) return;
			if (json.Type != JsonValueType.Object) return;
			var obj = json.Object;
			_red = obj.TryGetString("red");
			_orange = obj.TryGetString("orange");
			_yellow = obj.TryGetString("yellow");
			_green = obj.TryGetString("green");
			_blue = obj.TryGetString("blue");
			_purple = obj.TryGetString("purple");
			_isInitialized = true;
		}
		/// <summary>
		/// Converts an object to a JsonValue.
		/// </summary>
		/// <returns>
		/// The JsonValue representation of the object.
		/// </returns>
		public override JsonValue ToJson()
		{
			if (!_isInitialized) VerifyNotExpired();
			var json = new JsonObject
			           	{
			           		{"red", _red},
			           		{"orange", _orange},
			           		{"yellow", _yellow},
			           		{"green", _green},
			           		{"blue", _blue},
			           		{"purple", _purple}
			           	};
			return json;
		}
		/// <summary>
		/// Returns an enumerator that iterates through the collection.
		/// </summary>
		/// <returns>
		/// A <see cref="T:System.Collections.Generic.IEnumerator`1"/> that can be used to iterate through the collection.
		/// </returns>
		/// <filterpriority>1</filterpriority>
		public IEnumerator<Label> GetEnumerator()
		{
			return new List<Label>
			       	{
			       		new Label {Color = LabelColor.Red, Name = _red},
			       		new Label {Color = LabelColor.Orange, Name = _orange},
			       		new Label {Color = LabelColor.Yellow, Name = _yellow},
			       		new Label {Color = LabelColor.Green, Name = _green},
			       		new Label {Color = LabelColor.Blue, Name = _blue},
			       		new Label {Color = LabelColor.Purple, Name = _purple},
			       	}.GetEnumerator();
		}
		IEnumerator IEnumerable.GetEnumerator()
		{
			return GetEnumerator();
		}

		internal override bool Match(string id)
		{
			return false;
		}
		internal override void Refresh(ExpiringObject entity)
		{
			var labels = entity as LabelNames;
			if (labels == null) return;
			_red = labels._red;
			_orange = labels._orange;
			_yellow = labels._yellow;
			_green = labels._green;
			_blue = labels._blue;
			_purple = labels._purple;
			_isInitialized = true;
		}

		/// <summary>
		/// Retrieves updated data from the service instance and refreshes the object.
		/// </summary>
		protected override void Get()
		{
			var entity = Svc.Api.Get(Svc.RequestProvider.Create<LabelNames>(new[] {Owner, this}));
			Refresh(entity);
		}
		/// <summary>
		/// Propigates the service instance to the object's owned objects.
		/// </summary>
		protected override void PropigateSerivce() {}

		private void Put(string extension)
		{
			if (Svc == null)
			{
				Parameters.Clear();
				return;
			}
			var request = Svc.RequestProvider.Create<LabelNames>(new[] { Owner, this }, this, extension);
			Svc.PutAndCache(request);
		}
	}
}
