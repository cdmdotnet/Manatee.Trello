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
 
	File Name:		Label.cs
	Namespace:		Manatee.Trello
	Class Name:		Label
	Purpose:		Represents a label as applied to a card on Trello.com.

***************************************************************************************/
using System;
using System.Linq;
using Manatee.Json;
using Manatee.Json.Enumerations;
using Manatee.Json.Extensions;
using Manatee.Trello.Contracts;
using Manatee.Trello.Implementation;

namespace Manatee.Trello
{
	//   "labels":[
	//      {
	//         "color":"green",
	//         "name":""
	//      },
	/// <summary>
	/// Represents a label as applied to a card.
	/// </summary>
	public class Label : JsonCompatibleExpiringObject, IEquatable<Label>
	{
		private static readonly OneToOneMap<LabelColor, string> _colorMap;

		private string _apicolor;
		private LabelColor _color = LabelColor.Unknown;
		private string _name;

		/// <summary>
		/// Gets the color of the label.
		/// </summary>
		public LabelColor Color
		{
			get
			{
				return _color;
			}
			internal set 
			{
				if (_color == value) return;
				_color = value;
				UpdateApiColor();
			}
		}
		/// <summary>
		/// Gets the name of the label.  Tied to the board which contains the card.
		/// </summary>
		public string Name
		{
			get
			{
				return _name;
			}
			internal set
			{
				if (_name == value) return;
				_name = value ?? string.Empty;
			}
		}

		internal override string Key { get { return "labels"; } }

		static Label()
		{
			_colorMap = new OneToOneMap<LabelColor, string>
			            	{
			            		{LabelColor.Green, "green"},
			            		{LabelColor.Yellow, "yellow"},
			            		{LabelColor.Orange, "orange"},
			            		{LabelColor.Red, "red"},
			            		{LabelColor.Purple, "purple"},
			            		{LabelColor.Blue, "blue"},
			            	};
		}
		/// <summary>
		/// Creates a new instance of the Label class.
		/// </summary>
		public Label()
			: this(LabelColor.Unknown) {}
		/// <summary>
		/// Creates a new instance of the Label class.
		/// </summary>
		public Label(LabelColor color)
		{
			_color = color;
			UpdateApiColor();
			_isInitialized = true;
		}
		internal Label(ITrelloRest svc, Card owner)
			: base(svc, owner)
		{
			_color = LabelColor.Unknown;
			UpdateApiColor();
			_isInitialized = true;
		}

		/// <summary>
		/// Builds an object from a JsonValue.
		/// </summary>
		/// <param name="json">The JsonValue representation of the object.</param>
		public override void FromJson(JsonValue json)
		{
			if (json == null) return;
			if (json.Type != JsonValueType.Object) return;
			var obj = json.Object;
			_apicolor = obj.TryGetString("color");
			_name = obj.TryGetString("name");
			UpdateColor();
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
			           		{"color", _apicolor},
			           		{"name", _name}
			           	};
			return json;
		}
		/// <summary>
		/// Indicates whether the current object is equal to another object of the same type.
		/// </summary>
		/// <returns>
		/// true if the current object is equal to the <paramref name="other"/> parameter; otherwise, false.
		/// </returns>
		/// <param name="other">An object to compare with this object.</param>
		public bool Equals(Label other)
		{
			return _apicolor == other._apicolor;
		}
		/// <summary>
		/// Determines whether the specified <see cref="T:System.Object"/> is equal to the current <see cref="T:System.Object"/>.
		/// </summary>
		/// <returns>
		/// true if the specified <see cref="T:System.Object"/> is equal to the current <see cref="T:System.Object"/>; otherwise, false.
		/// </returns>
		/// <param name="obj">The object to compare with the current object. </param><filterpriority>2</filterpriority>
		public override bool Equals(object obj)
		{
			if (!(obj is Label)) return false;
			return Equals((Label) obj);
		}
		/// <summary>
		/// Serves as a hash function for a particular type. 
		/// </summary>
		/// <returns>
		/// A hash code for the current <see cref="T:System.Object"/>.
		/// </returns>
		/// <filterpriority>2</filterpriority>
		public override int GetHashCode()
		{
			return base.GetHashCode();
		}
		/// <summary>
		/// Returns a string that represents the current object.
		/// </summary>
		/// <returns>
		/// A string that represents the current object.
		/// </returns>
		/// <filterpriority>2</filterpriority>
		public override string ToString()
		{
			return string.Format("{0} : {1}", Color, Name);
		}

		internal override void Refresh(ExpiringObject entity) {}

		/// <summary>
		/// Retrieves updated data from the service instance and refreshes the object.
		/// </summary>
		protected override void Get()
		{
			UpdateApiColor();
			_isInitialized = true;
		}
		/// <summary>
		/// Propigates the service instance to the object's owned objects.
		/// </summary>
		protected override void PropigateService() {}

		private void UpdateColor()
		{
			_color = _colorMap.Any(kvp => kvp.Value == _apicolor) ? _colorMap[_apicolor] : LabelColor.Unknown;
		}
		private void UpdateApiColor()
		{
			if (_colorMap.Any(kvp => kvp.Key == _color))
				_apicolor = _colorMap[_color];
		}
	}
}
