using System.Linq;
using System.Text.RegularExpressions;
using UnityEngine;
using UnityEngine.UI;

namespace UnityDeveloperConsole
{
	public class ConsoleUI : MonoBehaviour
	{
		public static ConsoleUI Instance { get; private set; }
		//Selectable text must be inputfield
		InputField body;
		InputField input;

		public static void Initialize ()
		{
			if (Instance != null)
				Instantiate(Resources.Load<GameObject>("UnityDeveloperConsoleUI"));
		}

		void Awake ()
		{
			if (Instance != null)
			{
				Destroy(gameObject);
				return;
			}

			Instance = this;
			DontDestroyOnLoad(gameObject);
		}

		public void Submit (string input)
		{
			Log("> " + input);

			string[] tokens = Regex.Matches(input, @"[\""].+?[\""]|[^ ]+")
			  .Cast<Match>()
			  .Select(m => m.Value.Trim('"'))
			  .ToArray();

			string commandName = tokens.First();
			string[] args = tokens.Skip(1).ToArray();

			Log(ConsoleCommandsHandler.ExecuteCommand(commandName, args));
		}

		public void Log (object message)
		{
			if (message == null)
				return;

			string content = message.ToString();

			if (content == null)
				return;

			body.text = content + "\n";
		}

		public void Clear ()
		{
			body.text = "";
		}
	}
}