using System;
using System.Reflection;
using UnityDevConsole.Models.Command;

namespace UnityDeveloperConsole.Tests.Command
{
    public class TestCommandModel : ICommandModel
    {
        public event Action<object[]> OnInvokeCalled;

        public string Name { get; set; } = "";
        public string Description { get; set; }
        public ParameterInfo[] Parameters { get; set; }
        public bool Hidden { get; set; }
        public bool DeveloperOnly { get; set; }

        public object InvokeReturnValue { get; set; }

        public int CompareTo (CommandModel other)
        {
            throw new NotImplementedException();
        }

        public object Invoke (object[] parameters)
        {
            OnInvokeCalled?.Invoke(parameters);
            return InvokeReturnValue;
        }
    }
}
