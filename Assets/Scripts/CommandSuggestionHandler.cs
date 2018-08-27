using System.Collections.Generic;

namespace UnityDeveloperConsole
{
	public static class CommandSuggestionsHandler
	{
		public static int InputHistorySize = 10;

		static List<Command> commands = new List<Command>();
		static Queue<string> InputHistory = new Queue<string>();

		static List<string> resultsBuffer = new List<string>(5);

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
				InputHistory.Dequeue();

			InputHistory.Enqueue(input);
		}

		public static string[] GetSuggestions (string input, int maxResults = 5)
		{
			resultsBuffer.Clear();
			int resultCount = 0;

			for (int i = 0, iMax = commands.Count; i < iMax; i++)
			{
				Command command = commands[i];

				if (command.Name.ToUpperInvariant().StartsWith(input.ToUpperInvariant()))
					resultsBuffer[resultCount] = command.FullName;
			}

			return resultsBuffer.ToArray();
		}

		public static string[] GetInputHistory ()
		{
			return InputHistory.ToArray();
		}
	}
}