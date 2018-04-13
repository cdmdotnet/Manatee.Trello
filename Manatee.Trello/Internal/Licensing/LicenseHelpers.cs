#region License
// Copyright (c) Newtonsoft. All Rights Reserved.
// License: https://raw.github.com/JamesNK/Newtonsoft.Json.Schema/master/LICENSE.md
// Modified for use in Manatee.Trello
#endregion

using System;
using System.Globalization;
using System.IO;
using System.Reflection;
using System.Threading;
using Manatee.Json;
using Manatee.Json.Serialization;
using Manatee.Trello.Internal.Licensing;

[assembly:ReleaseDate("2018-04-13")]

namespace Manatee.Trello.Internal.Licensing
{
	internal static class LicenseHelpers
	{
		private static readonly object Lock;

		private static long _validationCount;
		private static long _generationCount;
		private static LicenseDetails _registeredLicense;
		private static Timer _resetTimer;

		static LicenseHelpers()
		{
			Lock = new object();
		}

		internal static void ResetCounts(object state)
		{
			_validationCount = 0;
			_generationCount = 0;
		}

		public static void IncrementAndCheckValidationCount()
		{
			if (_registeredLicense != null) return;

			EnsureResetTimer();

			const int maxOperationCount = 1000;
			Interlocked.Increment(ref _validationCount);

			if (_validationCount > maxOperationCount)
			{
				throw new LicenseException(
					$"The free-quota limit of {maxOperationCount} schema validations per hour has been reached. Please visit http://please.buy/my/library to upgrade to a commercial license.");
			}
		}

		public static void IncrementAndCheckGenerationCount()
		{
			if (_registeredLicense != null) return;

			EnsureResetTimer();

			const int maxOperationCount = 10;
			Interlocked.Increment(ref _generationCount);

			if (_generationCount > maxOperationCount)
			{
				throw new LicenseException(
					$"The free-quota limit of {maxOperationCount} schema generations per hour has been reached. Please visit http://please.buy/my/library to upgrade to a commercial license.");
			}
		}

		private static void EnsureResetTimer()
		{
			if (_resetTimer != null) return;

			lock (Lock)
			{
				if (_resetTimer != null) return;

				var timer = new Timer(ResetCounts, null, 0, Convert.ToInt32(TimeSpan.FromHours(1).TotalMilliseconds));

				Interlocked.MemoryBarrier();

				_resetTimer = timer;
			}
		}

		private static T[] SubArray<T>(this T[] data, int index, int length)
		{
			T[] result = new T[length];
			Array.Copy(data, index, result, 0, length);
			return result;
		}

		public static void RegisterLicense(string license)
		{
			var releaseDateAttribute = typeof(LicenseHelpers).GetTypeInfo()
			                                                 .Assembly
			                                                 .GetCustomAttribute<ReleaseDateAttribute>();

			RegisterLicense(license, releaseDateAttribute.ReleaseDate);
		}

		private static void RegisterLicense(string license, DateTime releaseDate)
		{
			if (string.IsNullOrEmpty(license))
			{
				throw new LicenseException("License text is empty.");
			}

			var licenseParts = license.Trim().Split('-');
			if (licenseParts.Length != 2 ||
			    !int.TryParse(licenseParts[0], NumberStyles.Integer, CultureInfo.InvariantCulture, out var licenseId))
				throw new LicenseException("Specified license text is invalid.");

			byte[] licenseData;

			try
			{
				licenseData = Convert.FromBase64String(licenseParts[1]);
			}
			catch
			{
				throw new LicenseException("Specified license text is invalid.");
			}

			if (licenseData.Length <= 128)
				throw new LicenseException("Specified license text is invalid.");

			JsonValue json;

			using (var ms = new MemoryStream(licenseData, 128, licenseData.Length - 128))
			using (var reader = new StreamReader(ms))
			{
				json = JsonValue.Parse(reader);
			}

			var deserializedLicense = new JsonSerializer().Deserialize<LicenseDetails>(json);

			var data = deserializedLicense.GetSignificateData();
			var signature = SubArray(licenseData, 0, 128);

			if (!CryptographyHelpers.ValidateData(data, signature))
				throw new LicenseException("License text does not match signature.");

			if (deserializedLicense.Id != licenseId)
				throw new LicenseException("License ID does not match signature license ID.");

			if (deserializedLicense.ExpiryDate < releaseDate)
			{
				var message = string.Format(
					"License is not valid for this version of Manatee.Trello. License free upgrade date expired on {0}. This version of Manatee.Trello was released on {1}.",
					deserializedLicense.ExpiryDate.ToString("yyyy-MM-dd", CultureInfo.InvariantCulture),
					releaseDate.ToString("yyyy-MM-dd", CultureInfo.InvariantCulture));

				throw new LicenseException(message);
			}

			if (deserializedLicense.Type == LicenseType.Test)
				throw new LicenseException("Specified license is for testing only.");

			SetRegisteredLicense(deserializedLicense);
		}

		private static void SetRegisteredLicense(LicenseDetails license)
		{
			_registeredLicense = license;
			if (_resetTimer == null) return;

			_resetTimer.Dispose();
			_resetTimer = null;
		}
	}
}