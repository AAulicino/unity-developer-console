using System;
using System.Reflection;

namespace UnityDevConsole.Models.Command
{
    public interface ICommand : IComparable<Command>
    {
        string Name { get; }
        string FullName { get; }
        ParameterInfo[] Parameters { get; }
        bool Hidden { get; }
        bool DeveloperOnly { get; }

        object Invoke (object[] parameters);
    }
}
