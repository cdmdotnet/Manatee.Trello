#region License
// Copyright (c) Newtonsoft. All Rights Reserved.
// License: https://raw.github.com/JamesNK/Newtonsoft.Json.Schema/master/LICENSE.md
// Modified for use in Manatee.Trello
#endregion

using System;
using Manatee.Trello.Internal.Licensing;

namespace Manatee.Trello

{
	/// <summary>
	/// Manages the license used with Manatee.Trello. Please see https://gregsdennis.github.io/Manatee.Trello/usage/licensing.html for information on purchasing a license.
	/// </summary>
	public static class License
	{
		/// <summary>
		/// Register the specified license with Manatee.Trello. Please see https://gregsdennis.github.io/Manatee.Trello/usage/licensing.html for information on purchasing a license.
		/// </summary>
		/// <param name="license">The license text to register.</param>
		/// <remarks> 
		/// The recommended way to register the license key is to call <see cref="RegisterLicense"/> once during application start up. In ASP.NET web applications it can be placed in the <c>Startup.cs</c> or <c>Global.asax.cs</c>, in WPF applications it can be placed in the <c>Application.Startup</c> event, and in Console applications it can be placed in the <c>static void Main(string[] args)</c> method.
		/// </remarks>
		/// <example> 
		/// This sample shows how to register a Manatee.Trello license with the <see cref="RegisterLicense"/> method.
		/// <code>
		/// // replace with your license key
		/// string licenseKey = "manatee-json-license-key";
		/// License.RegisterLicense(licenseKey);
		/// </code>
		/// </example>
		public static void RegisterLicense(string license)
		{
			LicenseHelpers.RegisterLicense(license);
		}

		/// <summary>
		/// Gets the number of data fetches remaining in the current session.
		/// </summary>
		public static long RemainingRetrievals => LicenseHelpers.MaxOperationCount - LicenseHelpers.RetrievalCount;
		/// <summary>
		/// Gets the number of data submissions remaining in the current session.
		/// </summary>
		public static long RemainingSubmissions => LicenseHelpers.MaxOperationCount - LicenseHelpers.SubmissionCount;
		/// <summary>
		/// Gets the time of the start of the next session.
		/// </summary>
		public static DateTime SessionResetTime => LicenseHelpers.SessionExpiry;
	}
}