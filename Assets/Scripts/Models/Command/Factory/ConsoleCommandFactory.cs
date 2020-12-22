using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using UnityEngine;

namespace UnityDevConsole.Models.Command
{
    public class ConsoleCommandFactory : IConsoleCommandFactory
    {
        const BindingFlags flags = BindingFlags.Static | BindingFlags.Public | BindingFlags.NonPublic;

        public Dictionary<string, ICommand> CreateFromAssemblies (params string[] assemblies)
        {
            Dictionary<string, ICommand> commands = new Dictionary<string, ICommand>();

            IEnumerable<MethodInfo> methods = assemblies.AsParallel()
                .Select(Assembly.Load)
                .SelectMany(a => a.GetTypes())
                .SelectMany(t => t.GetMethods(flags));

            foreach (MethodInfo method in methods)
            {
                object[] taggedMethods = method.GetCustomAttributes(
                    typeof(ConsoleCommandAttribute),
                    false
                );

                foreach (object consoleCommand in taggedMethods)
                {
                    ConsoleCommandAttribute consoleAttr = (ConsoleCommandAttribute)consoleCommand;

                    if (commands.ContainsKey(consoleAttr.CommandName))
                    {
                        Debug.LogWarning(
                            "[UnityDevConsole] Duplicate command found. Command Name: "
                            + consoleAttr.CommandName
                        );
                    }
                    else
                        commands.Add(consoleAttr.CommandName, new Command(consoleAttr, method));
                }
            }
            return commands;
        }

        public ICommand Create (
            string commandName,
            string methodName,
            object context,
            bool developerOnly,
            bool hidden
        )
        {
            MethodInfo methodInfo = context.GetType().GetMethod(methodName, flags);
            return new Command(commandName, methodInfo, context, developerOnly, hidden);
        }
    }
}
