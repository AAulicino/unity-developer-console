using System;

public interface IConsoleCommandAttribute
{
    bool DeveloperOnly { get; set; }
    bool Hidden { get; set; }
}

[AttributeUsage(AttributeTargets.Method, Inherited = false, AllowMultiple = true)]
public sealed class ConsoleCommandAttribute : Attribute, IConsoleCommandAttribute
{
    public readonly string CommandName;

    public bool DeveloperOnly { get; set; }

    /// <summary>
    /// If the command should be displayed in the auto-complete.
    /// </summary>
    public bool Hidden { get; set; }

    /// <param name="commandName">Should not contain spaces!</param>
    public ConsoleCommandAttribute (string commandName)
    {
        /*
			With spaces it becomes very hard to determine if text after the space is a parameter or
         	commandName.
		*/
        if (commandName.Contains(" "))
            throw new FormatException(
                "[ConsoleCommandAttribute] No spaces allowed in a console command name!"
            );

        CommandName = commandName;
    }
}
