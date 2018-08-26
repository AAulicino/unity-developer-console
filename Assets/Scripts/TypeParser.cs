using System;
using System.Collections.Generic;

public class TypeParser
{
	public delegate object ParseHandler (string unparsedObject);

	static Dictionary<Type, ParseHandler> customTypes = new Dictionary<Type, ParseHandler>();

	public static void RegisterCustomType (Type type, ParseHandler parser)
	{
		customTypes.Add(type, parser);
	}

	public static void UnregisterCustomType (Type type)
	{
		customTypes.Remove(type);
	}

	public static object Parse (string text, Type targetType)
	{
		try
		{
			return Convert.ChangeType(text, targetType);
		}
		catch
		{
			ParseHandler parser;
			if(customTypes.TryGetValue(targetType, out parser))
			{
				return parser.Invoke(text);
			}
			throw;
		}
	}
}
