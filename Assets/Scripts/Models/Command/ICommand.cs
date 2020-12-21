using System;
using System.Reflection;

namespace UnityDevConsole.Models.Command
{
    public interface ICommand : IComparable<Command>
    {
        string Name { get; }
        string FullName { get; }
        MethodInfo Method { get; }
        ParameterInfo[] Parameters { get; }
        object Context { get; }
        bool Hidden { get; }
        bool DeveloperOnly { get; }
    }
}
