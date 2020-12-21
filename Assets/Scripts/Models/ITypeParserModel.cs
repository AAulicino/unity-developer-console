using System;

namespace UnityDeveloperConsole.Models
{
    public interface ITypeParserModel
    {
        object Parse (string text, Type targetType);
        void RegisterCustomType (Type type, ParseHandler parser);
        void UnregisterCustomType (Type type);
    }
}
