using System;

namespace UnityDevConsole.Models.Command.Parser
{
    public interface ITypeParserModel
    {
        object Parse (string text, Type targetType);
        void RegisterCustomType (Type type, ParseHandler parser);
        void UnregisterCustomType (Type type);
    }
}
