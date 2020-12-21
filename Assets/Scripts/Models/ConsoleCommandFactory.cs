using System;

public class ConsoleCommandFactory
{
    public Command[] FindCompileTimeCommands (string[] assemblies)
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
}
