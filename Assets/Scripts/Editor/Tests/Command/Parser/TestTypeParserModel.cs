using System;
using UnityDevConsole;
using UnityDevConsole.Models.Command.Parser;

namespace UnityDeveloperConsole.Tests.Command
{
    public class TestTypeParserModel : ITypeParserModel
    {
        public object ParseReturnValue { get; set; }

        public object Parse (string text, Type targetType)
        {
            return ParseReturnValue;
        }

        public void RegisterCustomType (Type type, ParseHandler parser)
        {
            throw new NotImplementedException();
        }

        public void UnregisterCustomType (Type type)
        {
            throw new NotImplementedException();
        }
    }
}
