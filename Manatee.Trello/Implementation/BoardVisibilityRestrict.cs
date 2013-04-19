using System.Linq;
using Manatee.Json;
using Manatee.Json.Enumerations;
using Manatee.Json.Extensions;
using Manatee.Json.Serialization;

namespace Manatee.Trello.Implementation
{
	internal class BoardVisibilityRestrict : IJsonCompatible
	{
		private static readonly OneToOneMap<BoardPermissionLevelType, string> _permissionMap;

		private string _apiPrivate;
		private string _apiOrg;
		private string _apiPublic;
		private BoardPermissionLevelType _private = BoardPermissionLevelType.Unknown;
		private BoardPermissionLevelType _org = BoardPermissionLevelType.Unknown;
		private BoardPermissionLevelType _public = BoardPermissionLevelType.Unknown;

		public BoardPermissionLevelType Private
		{
			get { return _private; }
			set
			{
				_private = value;
				UpdateApiPrivate();
			}
		}
		public BoardPermissionLevelType Org
		{
			get { return _org; }
			set
			{
				_org = value;
				UpdateApiOrg();
			}
		}
		public BoardPermissionLevelType Public
		{
			get { return _public; }
			set
			{
				_public = value;
				UpdateApiPublic();
			}
		}

		static BoardVisibilityRestrict()
		{
			_permissionMap = new OneToOneMap<BoardPermissionLevelType, string>
			                 	{
			                 		{BoardPermissionLevelType.Private, "private"},
			                 		{BoardPermissionLevelType.Org, "org"},
			                 		{BoardPermissionLevelType.Public, "public"},
			                 	};
		}

		public void FromJson(JsonValue json)
		{
			if (json == null) return;
			if (json.Type != JsonValueType.Object) return;
			var obj = json.Object;
			_apiPrivate = obj.TryGetString("private");
			_apiOrg = obj.TryGetString("org");
			_apiPublic = obj.TryGetString("public");
			UpdatePrivate();
			UpdateOrg();
			UpdatePublic();
		}
		public JsonValue ToJson()
		{
			var json = new JsonObject
			           	{
			           		{"private", _apiPrivate},
			           		{"org", _apiOrg},
			           		{"public", _apiPublic}
			           	};
			return json;
		}

		private void UpdatePrivate()
		{
			_private = _permissionMap.Any(kvp => kvp.Value == _apiPrivate) ? _permissionMap[_apiPrivate] : BoardPermissionLevelType.Unknown;
		}
		private void UpdateApiPrivate()
		{
			if (_permissionMap.Any(kvp => kvp.Key == _private))
				_apiPrivate = _permissionMap[_private];
		}
		private void UpdateOrg()
		{
			_org = _permissionMap.Any(kvp => kvp.Value == _apiOrg) ? _permissionMap[_apiOrg] : BoardPermissionLevelType.Unknown;
		}
		private void UpdateApiOrg()
		{
			if (_permissionMap.Any(kvp => kvp.Key == _org))
				_apiOrg = _permissionMap[_org];
		}
		private void UpdatePublic()
		{
			_public = _permissionMap.Any(kvp => kvp.Value == _apiPublic) ? _permissionMap[_apiPublic] : BoardPermissionLevelType.Unknown;
		}
		private void UpdateApiPublic()
		{
			if (_permissionMap.Any(kvp => kvp.Key == _public))
				_apiPublic = _permissionMap[_public];
		}
	}
}
