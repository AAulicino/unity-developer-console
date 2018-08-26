using System;

[AttributeUsage(AttributeTargets.Method, Inherited = false, AllowMultiple = true)]
public sealed class ConsoleCommandAttribute : Attribute
{
	public readonly string CommandName;

	public bool DeveloperOnly { get; set; }
	/// <summary>
	/// Wether the command should be displayed in the suggestions dropdown while typing in the GUI.
	/// </summary>
	public bool Indexed { get; set; }

	/// <summary>
	/// 
	/// </summary>
	/// <param name="commandName">Should not contain spaces!</param>
	public ConsoleCommandAttribute (string commandName)
	{
		//With spaces it becomes impossible to determine if text after the space is a parameter or commandName.
		if (commandName.Contains(" "))
			throw new FormatException("[ConsoleCommandAttribute] No spaces allowed in a console command name!");

		CommandName = commandName;
	}
}