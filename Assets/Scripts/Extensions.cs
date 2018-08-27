using UnityEngine;

namespace UnityDeveloperConsole
{
	public static class Extensions
	{
		public static Transform FindChildRecursive (this Transform parent, string name)
		{
			foreach (Transform child in parent)
			{
				if (child.name == name)
					return child;
				else
				{
					Transform result = FindChildRecursive(child, name);
					if (result != null)
						return result;
				}
			}
			return null;
		}
	}
}