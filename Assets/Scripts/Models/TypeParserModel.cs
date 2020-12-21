using System;
using System.Collections.Generic;

namespace UnityDeveloperConsole.Models
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
            try
            {
                return Convert.ChangeType(text, targetType);
            }
            catch
            {
                if (customTypes.TryGetValue(targetType, out ParseHandler parser))
                    return parser.Invoke(text);

                throw new InvalidCastException(
                    $"No support for deserializing {targetType}. "
                    + "You can register a custom parser using TypeParser.RegisterCustomType"
                );
            }
        }
    }
}
