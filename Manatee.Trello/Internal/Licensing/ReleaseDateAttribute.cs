#region License
// Copyright (c) Newtonsoft. All Rights Reserved.
// License: https://raw.github.com/JamesNK/Newtonsoft.Json.Schema/master/LICENSE.md
// Modified for use in Manatee.Trello
#endregion

using System;
using System.Globalization;

namespace Manatee.Trello.Internal.Licensing
{
	internal class ReleaseDateAttribute : Attribute
	{
		public DateTime ReleaseDate { get; }

		public ReleaseDateAttribute(string releaseDate)
		{
			ReleaseDate = DateTime.ParseExact(releaseDate, "yyyy-MM-dd", CultureInfo.InvariantCulture);
		}
	}
}