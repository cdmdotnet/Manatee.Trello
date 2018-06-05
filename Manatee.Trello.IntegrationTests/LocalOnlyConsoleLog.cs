using System;
using System.Text;

namespace Manatee.Trello.IntegrationTests
{
	internal class LocalOnlyConsoleLog : ILog
	{
		private static readonly bool ProvideOutput;

		static LocalOnlyConsoleLog()
		{
			bool.TryParse(Environment.GetEnvironmentVariable("TRELLO_TEST_OUTPUT"), out ProvideOutput);
		}

		public void Debug(string message, params object[] parameters)
		{
			if (!ProvideOutput) return;

			var output = $"Debug: {message}";
			Post(output);
		}
		public void Info(string message, params object[] parameters)
		{
			if (!ProvideOutput) return;

			var output = string.Format($"Info: {message}", parameters);
			Post(output);
		}
		public void Error(Exception e)
		{
			if (!ProvideOutput) return;

			var output = BuildMessage($"Error: An exception of type {e.GetType().Name} occurred:",
			                          e.Message,
			                          e.StackTrace);
			Post(output);
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
			Console.WriteLine(output);
		}
	}
}
