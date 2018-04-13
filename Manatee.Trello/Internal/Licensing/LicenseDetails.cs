#region License
// Copyright (c) Newtonsoft. All Rights Reserved.
// License: https://raw.github.com/JamesNK/Newtonsoft.Json.Schema/master/LICENSE.md
// Modified for use in Manatee.Trello
#endregion

using System;
using System.Globalization;
using System.Text;

namespace Manatee.Trello.Internal.Licensing
{
    internal class LicenseDetails
    {
        public int Id { get; set; }
        public DateTime ExpiryDate { get; set; }
        public string Type { get; set; }

        internal byte[] GetSignificateData()
        {
            string s = string.Join(":", new[]
            {
                Id.ToString(CultureInfo.InvariantCulture),
                ExpiryDate.ToString("yyyy-MM-dd", CultureInfo.InvariantCulture),
                Type
            });

            return Encoding.UTF8.GetBytes(s);
        }
    }
}