using System.Collections.Generic;

namespace UnityDeveloperConsole
{
	public static class CommandSuggestionsHandler
	{
		static List<Command> commands = new List<Command>();
		static List<string> InputHistory = new List<string>();

		public static int InputHistorySize { get; set; }

		public static void RegisterCommands (Command[] command)
		{
			for (int i = 0; i < command.Length; i++)
				if(command[i].Indexed)
					commands.Add(command[i]);

			commands.Sort();
		}

		public static void RegisterCommand (Command command)
		{
			if (!command.Indexed)
				return;

			commands.Add(command);
			commands.Sort();
		}

		public static void RegisterInputToHistory (string input)
		{
			if (InputHistory.Count >= InputHistorySize)
				InputHistory.RemoveAt(0);

			InputHistory.Add(input);
		}

		public static string[] GetSuggestions (string input, int maxResults = 5)
		{
			List<string> results = new List<string>(maxResults);

			int resultCount = 0;

			for (int i = 0; i < commands.Count; i++)
			{
				Command command = commands[i];

				if (command.Name.ToUpperInvariant().StartsWith(input.ToUpperInvariant()))
					results[resultCount] = command.FullName;
			}

			return results.ToArray();
		}

		public static string[] GetLatestInputs ()
		{
			return InputHistory.ToArray();
		}
	}
}