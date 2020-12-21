using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text.RegularExpressions;
using UnityEngine;

namespace UnityDeveloperConsole.Models
{
    public class CommandsHandler
    {
        readonly Dictionary<string, Command> registeredCommands = new Dictionary<string, Command>();

        public Command[] LoadCompileTimeCommands ()
        {
            IEnumerable<MethodInfo> methods = AppDomain.CurrentDomain.GetAssemblies()
                .Where(a => a.FullName.Contains("Assembly-CSharp"))
                .SelectMany(a => a.GetTypes())
                .SelectMany(t => t.GetMethods(BindingFlags.Static | BindingFlags.Public | BindingFlags.NonPublic));

            foreach (MethodInfo method in methods)
            {
                foreach (object consoleCommand in method.GetCustomAttributes(typeof(ConsoleCommandAttribute), false))
                {
                    ConsoleCommandAttribute consoleAttr = (ConsoleCommandAttribute)consoleCommand;

                    if (registeredCommands.ContainsKey(consoleAttr.CommandName))
                        Debug.LogWarning("[UnityDeveloperConsole] Duplicate command found. Command Name:" + consoleAttr.CommandName);
                    else
                        registeredCommands.Add(consoleAttr.CommandName, new Command(consoleAttr, method));
                }
            }
            return registeredCommands.Values.ToArray();
        }

        public void RegisterRuntimeCommand (string commandName, string methodName, object context, bool developerOnly, bool indexed)
        {
            MethodInfo method = context.GetType().GetMethod(methodName, BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic);

            if (registeredCommands.ContainsKey(commandName))
                Debug.LogWarning("[UnityDeveloperConsole] Failed to insert runtime command. Reason: duplicate command name.");
            else
                registeredCommands.Add(commandName, new Command(commandName, method, context, developerOnly, indexed));
        }

        public void UnregisterRuntimeCommand (string commandName)
        {
            registeredCommands.Remove(commandName);
        }

        public object ExecuteCommand (string unparsedCommand)
        {
            if (string.IsNullOrEmpty(unparsedCommand))
                return null;

            //Splits by white spaces and preserve things between quotes. Example: Input=MyCommand arg1 "arg 2",
            //becomes string[] {"MyCommand", "arg1", "arg 2"}.
            string[] tokens = Regex.Matches(unparsedCommand, @"[\""].+?[\""]|[^ ]+")
              .Cast<Match>()
              .Select(m => m.Value.Trim('"'))
              .ToArray();

            string commandName = tokens.First();
            string[] args = tokens.Skip(1).ToArray();

            return ExecuteCommand(commandName, args);
        }

        public object ExecuteCommand (string commandName, string[] args)
        {
            if (registeredCommands.TryGetValue(commandName, out Command command))
            {
                List<object> invokeParams = new List<object>(command.Parameters.Length);

                for (int i = 0; i < command.Parameters.Length; i++)
                {
                    if (args.Length < i)
                    {
                        object parsedObject;

                        try
                        {
                            parsedObject = TypeParserModel.Parse(args[i], command.Parameters[i].ParameterType);
                        }
                        catch (Exception ex)
                        {
                            Debug.LogException(ex);
                            return string.Format("[DeveloperConsole] Failed to parse argument {0} of type: {1}. Expected: {2}",
                                    args[i], args[i].GetType(), command.Parameters[i].ParameterType);
                        }

                        invokeParams.Add(parsedObject);
                    }
                    else
                        invokeParams.Add(Type.Missing);
                }

                try
                {
                    return command.Method.Invoke(command.Context, invokeParams.ToArray());
                }
                catch (Exception ex)
                {
                    Debug.LogException(ex);
                    return "[DeveloperConsole] Command Failed. Ex: " + ex;
                }
            }

            return "Unknown command. " + commandName;
        }
    }
}
