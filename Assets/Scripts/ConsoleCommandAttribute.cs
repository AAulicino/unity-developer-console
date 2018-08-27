using System;

[AttributeUsage(AttributeTargets.Method, Inherited = false, AllowMultiple = true)]
public sealed class ConsoleCommandAttribute : Attribute
{
	public readonly string CommandName;
	bool _indexed = true;
	bool _developerOnly = true;

	public bool DeveloperOnly
	{
		get { return _developerOnly; }
		set { _developerOnly = value; }
	}

	/// <summary>
	/// Wether the command should be displayed in the completion hint dropdown while typing in the ConsoleUI.
	/// </summary>
	public bool Indexed
	{
		get { return _indexed; }
		set { _indexed = value; }
	}

	/// <param name="commandName">Should not contain spaces!</param>
	public ConsoleCommandAttribute (string commandName)
	{
		//With spaces it becomes impossible to determine if text after the space is a parameter or commandName.
		if (commandName.Contains(" "))
			throw new FormatException("[ConsoleCommandAttribute] No spaces allowed in a console command name!");

		CommandName = commandName;
	}
}