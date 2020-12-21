using System;
using System.Reflection;

namespace UnityDeveloperConsole.Models
{
    public interface ICommand : IComparable<Command>
    {
        string Name { get; }
        string FullName { get; }
        MethodInfo Method { get; }
        ParameterInfo[] Parameters { get; }
        object Context { get; }
        bool Indexed { get; }
        bool DeveloperOnly { get; }
    }
}
