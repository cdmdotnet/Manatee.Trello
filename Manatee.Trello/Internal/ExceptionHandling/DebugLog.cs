using System;
using System.Text;
using Manatee.Trello.Contracts;

namespace Manatee.Trello.Internal.ExceptionHandling
{
	internal class DebugLog : ILog
	{
		public void Debug(string message, params object[] parameters)
		{
			var output = $"Debug: {message}";
			Post(output);
		}
		public void Info(string message, params object[] parameters)
		{
			var output = string.Format($"Info: {message}", parameters);
			Post(output);
		}
		public void Error(Exception e, bool shouldThrow = true)
		{
			var output = BuildMessage($"Error: An exception of type {e.GetType().Name} occurred:",
									  e.Message,
									  e.StackTrace);
			Post(output);
			if (shouldThrow)
				throw e;
		}

		private static string BuildMessage(params string[] lines)
		{
			var sb = new StringBuilder();
			foreach (var line in lines)
			{
				sb.AppendLine(line);
			}
			return sb.ToString();
		}
		private static void Post(string output)
		{
			System.Diagnostics.Debug.WriteLine(output);
		}
	}
}