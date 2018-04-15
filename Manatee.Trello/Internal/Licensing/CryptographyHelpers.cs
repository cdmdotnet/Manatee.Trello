#region License
// Copyright (c) Newtonsoft. All Rights Reserved.
// License: https://raw.github.com/JamesNK/Newtonsoft.Json.Schema/master/LICENSE.md
// Modified for use in Manatee.Trello
#endregion

using System;
using System.IO;
using System.Security.Cryptography;

namespace Manatee.Trello.Internal.Licensing
{
	internal static class CryptographyHelpers
	{
		private const string PublicKey = "<RSAKeyValue><Modulus>wNE8tiipWCy2LmB3cZYW8nj5Nm/fn3X2GYsoSx6XE1yfvW96Ul/vRBw6/jAAwk9aZIdix9+gleh5x7XE8snzZlNMDDCmIFz2SWY9f7SdYYD5gif2rIpeeIDS/5J731d6XX/BKISwtM+MRWakY6ihNU1SUIGsKH6HxUXPm80Q66s=</Modulus><Exponent>AQAB</Exponent></RSAKeyValue>";
		private const string PublicKeyCsp = "BgIAAACkAABSU0ExAAQAAAEAAQCr6xDNm89FxYd+KKyBUFJNNaGoY6RmRYzPtLCEKMF/XXpX33uS/9KAeF6KrPYngvmAYZ20fz1mSfZcIKYwDExTZvPJ8sS1x3nolaDfx2KHZFpPwgAw/jocRO9fUnpvvZ9cE5ceSyiLGfZ1n99vNvl48haWcXdgLrYsWKkotjzRwA==";

		internal static bool ValidateData(byte[] data, byte[] signature)
		{
			bool valid;

			using (var rsa = RSA.Create())
			{
				rsa.ImportParameters(ToRSAParameters(Convert.FromBase64String(PublicKeyCsp), false));

				valid = rsa.VerifyData(data, signature, HashAlgorithmName.SHA1, RSASignaturePadding.Pkcs1);
			}

			return valid;
		}

		private const int ALG_TYPE_RSA = (2 << 9);
		private const int ALG_CLASS_KEY_EXCHANGE = (5 << 13);
		private const int CALG_RSA_KEYX = (ALG_CLASS_KEY_EXCHANGE | ALG_TYPE_RSA | 0);

		// ReSharper disable once InconsistentNaming
		private static RSAParameters ToRSAParameters(byte[] cspBlob, bool includePrivateParameters)
		{
			var br = new BinaryReader(new MemoryStream(cspBlob));

			var bType = br.ReadByte(); // BLOBHEADER.bType: Expected to be 0x6 (PUBLICKEYBLOB) or 0x7 (PRIVATEKEYBLOB), though there's no check for backward compat reasons. 
			var bVersion = br.ReadByte(); // BLOBHEADER.bVersion: Expected to be 0x2, though there's no check for backward compat reasons.
			br.ReadUInt16(); // BLOBHEADER.wReserved
			var algId = br.ReadInt32(); // BLOBHEADER.aiKeyAlg
			if (algId != CALG_RSA_KEYX)
				throw new PlatformNotSupportedException(); // The FCall this code was ported from supports other algid's but we're only porting what we use.

			var magic = br.ReadInt32(); // RSAPubKey.magic: Expected to be 0x31415352 ('RSA1') or 0x32415352 ('RSA2') 
			var bitLen = br.ReadInt32(); // RSAPubKey.bitLen

			var modulusLength = bitLen/8;
			var halfModulusLength = (modulusLength + 1)/2;

			var expAsDword = br.ReadUInt32();

			var rsaParameters = new RSAParameters
				{
					Exponent = ExponentAsBytes(expAsDword),
					Modulus = br.ReadReversed(modulusLength)
				};
			if (includePrivateParameters)
			{
				rsaParameters.P = br.ReadReversed(halfModulusLength);
				rsaParameters.Q = br.ReadReversed(halfModulusLength);
				rsaParameters.DP = br.ReadReversed(halfModulusLength);
				rsaParameters.DQ = br.ReadReversed(halfModulusLength);
				rsaParameters.InverseQ = br.ReadReversed(halfModulusLength);
				rsaParameters.D = br.ReadReversed(modulusLength);
			}

			return rsaParameters;
		}

		/// <summary>
		/// Helper for converting a UInt32 exponent to bytes.
		/// </summary>
		private static byte[] ExponentAsBytes(uint exponent)
		{
			if (exponent <= 0xFF)
			{
				return new[] {(byte) exponent};
			}
			if (exponent <= 0xFFFF)
			{
				return new[]
				{
					(byte) (exponent >> 8),
					(byte) (exponent)
				};
			}
			if (exponent <= 0xFFFFFF)
			{
				return new[]
				{
					(byte) (exponent >> 16),
					(byte) (exponent >> 8),
					(byte) (exponent)
				};
			}

			return new[]
			{
				(byte) (exponent >> 24),
				(byte) (exponent >> 16),
				(byte) (exponent >> 8),
				(byte) (exponent)
			};
		}

		/// <summary>
		/// Read in a byte array in reverse order.
		/// </summary>
		private static byte[] ReadReversed(this BinaryReader br, int count)
		{
			var data = br.ReadBytes(count);
			Array.Reverse(data);
			return data;
		}
	}
}