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
using System.Threading.Tasks;
using Manatee.Json;
using Manatee.Json.Serialization;

namespace Manatee.Trello.Internal.Licensing
{
	internal static class LicenseHelpers
	{
		private class RunDetails
		{
			public long Retrievals { get; set; }
			public long Submissions { get; set; }
			public DateTime SessionExpiry { get; set; }
		}

		private const string BuyMeText = "Please see https://gregsdennis.github.io/Manatee.Trello/usage/licensing.html for information on purchasing a license.";
		private const string LicenseEnvironmentVariable = "TRELLO_LICENSE_TRACKING";

		private static readonly object TimerLock;
		private static readonly object CountsLock;
		private static readonly JsonSerializer Serializer;

		private static long _retrievalCount;
		private static long _submissionCount;
		private static DateTime _sessionExpiry;
		private static LicenseDetails _registeredLicense;
		private static Timer _resetTimer;
		private static bool _holdPersistence;

		public static TimeSpan SessionDuration { get; set; } = TimeSpan.FromHours(1);

		static LicenseHelpers()
		{
			TimerLock = new object();
			CountsLock = new object();
			Serializer = new JsonSerializer();

			LoadCurrentState();
		}

		private static void ResetCounts(object state)
		{
			_retrievalCount = 0;
			_submissionCount = 0;
			_sessionExpiry = DateTime.Now.Add(SessionDuration);
		}

		private static void LoadCurrentState()
		{
#if !NETSTANDARD1_3
			string text;
			lock (CountsLock)
			{
				try
				{
					text = Environment.GetEnvironmentVariable(LicenseEnvironmentVariable, EnvironmentVariableTarget.User);
				}
				catch (Exception e)
				{
					TrelloConfiguration.Log.Error(e);
					return;
				}
			}

			if (string.IsNullOrEmpty(text)) return;

			var json = JsonValue.Parse(text);
			var details = Serializer.Deserialize<RunDetails>(json);

			if (DateTime.Now >= details.SessionExpiry) return;

			_retrievalCount = details.Retrievals;
			_submissionCount = details.Submissions;
			_sessionExpiry = details.SessionExpiry;
#endif
		}

		public static void IncrementAndCheckRetrieveCount()
		{
			if (_registeredLicense != null) return;

			EnsureResetTimer();

			const int maxOperationCount = 300;
			Interlocked.Increment(ref _retrievalCount);
			SaveCurrentState();

			if (_retrievalCount > maxOperationCount)
				throw new LicenseException($"The free-quota limit of {maxOperationCount} data retrievals per hour has been reached. " +
				                           $"The current session will reset at {_sessionExpiry:g}. " + 
				                           BuyMeText);
		}

		public static void IncrementAndCheckSubmissionCount()
		{
			if (_registeredLicense != null) return;

			EnsureResetTimer();

			const int maxOperationCount = 300;
			Interlocked.Increment(ref _submissionCount);
			SaveCurrentState();

			if (_submissionCount > maxOperationCount)
				throw new LicenseException($"The free-quota limit of {maxOperationCount} data submissions per hour has been reached. " +
				                           $"The current session will reset at {_sessionExpiry:g}. " + 
				                           BuyMeText);
		}

		private static void SaveCurrentState()
		{
#if !NETSTANDARD1_3
			var newDetails = new RunDetails
				{
					Retrievals = _retrievalCount,
					Submissions = _submissionCount,
					SessionExpiry = _sessionExpiry
				};

			var json = Serializer.Serialize(newDetails);
			if (_holdPersistence) return;
			lock (CountsLock)
			{
				if (_holdPersistence) return;

				try
				{
					Environment.SetEnvironmentVariable(LicenseEnvironmentVariable, json.ToString(), EnvironmentVariableTarget.User);
				}
				catch (Exception e)
				{
					// nothing else we can really do here
					TrelloConfiguration.Log.Error(e);
				}
			}
#endif
		}

		private static void EnsureResetTimer()
		{
			if (_resetTimer != null) return;

			lock (TimerLock)
			{
				if (_resetTimer != null) return;

				var timer = new Timer(ResetCounts, null, TimeSpan.FromMilliseconds((_sessionExpiry - DateTime.Now).TotalMilliseconds), SessionDuration);

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

			var deserializedLicense = Serializer.Deserialize<LicenseDetails>(json);

			var data = deserializedLicense.GetSignificateData();
			var signature = SubArray(licenseData, 0, 128);

			if (!CryptographyHelpers.ValidateData(data, signature))
				throw new LicenseException("License text does not match signature.");

			if (deserializedLicense.Id != licenseId)
				throw new LicenseException("License ID does not match signature license ID.");

			if (deserializedLicense.ExpiryDate < releaseDate)
			{
				var message = $"License is not valid for this version of Manatee.Trello. License free upgrade date " +
				              $"expired on {deserializedLicense.ExpiryDate.ToString("yyyy-MM-dd", CultureInfo.InvariantCulture)}. " +
				              $"This version of Manatee.Trello was released on {releaseDate.ToString("yyyy-MM-dd", CultureInfo.InvariantCulture)}.";

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

		public static async Task Batch(Task batch)
		{
			_holdPersistence = true;
			await batch;
			_holdPersistence = false;
			SaveCurrentState();
		}
	}
}