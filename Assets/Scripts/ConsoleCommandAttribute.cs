
[System.AttributeUsage(System.AttributeTargets.Method, Inherited = false, AllowMultiple = true)]
public sealed class ConsoleCommandAttribute : System.Attribute
{
	public readonly string CommandName;
	
	public bool DeveloperOnly { get; set; }
	/// <summary>
	/// Wether the command should be displayed in the suggestions dropdown while typing in the GUI.
	/// </summary>
	public bool Indexed { get; set; }

	public ConsoleCommandAttribute (string commandName)
	{
		CommandName = commandName;
	}
}