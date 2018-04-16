#region License
// Copyright (c) Newtonsoft. All Rights Reserved.
// License: https://raw.github.com/JamesNK/Newtonsoft.Json.Schema/master/LICENSE.md
// Modified for use in Manatee.Trello
#endregion

using System;
using System.Security.Cryptography;
using Manatee.Json;
using Manatee.Json.Serialization;

namespace Manatee.Trello.Internal.Licensing
{
	internal static class CryptographyHelpers
	{
		private const string PublicKey = "{\"Exponent\":\"AQAB\",\"Modulus\":\"2m8UmVALYWjUB5+2muX2BUNk/VWMAbmuD6W1BHPqyRIqhZ+VT7kUHgQJgTTRUDokWfqR0u2hPdDlnBMPbbbdtWMjKxsdHy/5mdCxXzI1pOUfY/JyxP8B2d/RneLOxT7fDGvuhhejTJcAiRwQ1N81o1Z01IWVH4muZhZg/xLpSCk=\"}";

		internal static bool ValidateData(byte[] data, byte[] signature)
		{
			bool valid;

			var publicKeyJson = JsonValue.Parse(PublicKey);
			var serializer = new JsonSerializer
				{
					Options =
						{
							AutoSerializeFields = true,
							CaseSensitiveDeserialization = false
						}
				};
			serializer.CustomSerializations.RegisterType(ToBase64String, FromBase64String);
			var parameters = serializer.Deserialize<RSAParameters>(publicKeyJson);

			using (var rsa = RSA.Create())
			{
				rsa.ImportParameters(parameters);

				valid = rsa.VerifyData(data, signature, HashAlgorithmName.SHA1, RSASignaturePadding.Pkcs1);
			}

			return valid;
		}

		private static byte[] FromBase64String(JsonValue json, JsonSerializer serializer)
		{
			return Convert.FromBase64String(json.String);
		}

		private static JsonValue ToBase64String(byte[] bytes, JsonSerializer serializer)
		{
			return Convert.ToBase64String(bytes);
		}
	}
}