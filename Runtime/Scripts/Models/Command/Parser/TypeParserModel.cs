using System;
using System.Collections.Generic;

namespace UnityDevConsole.Models.Command.Parser
{
    public class TypeParserModel : ITypeParserModel
    {
        readonly Dictionary<Type, ParseHandler> customTypes = new Dictionary<Type, ParseHandler>();

        public void RegisterCustomType (Type type, ParseHandler parser)
        {
            customTypes.Add(type, parser);
        }

        public void UnregisterCustomType (Type type)
        {
            customTypes.Remove(type);
        }

        public object Parse (string text, Type targetType)
        {
            if (customTypes.TryGetValue(targetType, out ParseHandler parser))
                return parser.Invoke(text);

            try
            {
                return Convert.ChangeType(text, targetType);
            }
            catch (Exception inner)
            {
                throw new FormatException(
                    $"No support for parsing {targetType}. "
                    + "You can register a custom parser using TypeParser.RegisterCustomType",
                    inner
                );
            }
        }
    }
}
