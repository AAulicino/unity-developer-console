using System.IO;
using UnityDevConsole.Settings;
using UnityEditor;
using UnityEngine;
using static UnityDevConsole.Settings.ConsoleSettings;

public class ConsoleSettingsEditor : Editor
{
    [MenuItem("Window/DeveloperConsole/Edit Settings")]
    public static void Edit ()
    {
        ConsoleSettings settings = ConsoleSettings.LoadInstance();
        if (settings == null)
        {
            settings = ScriptableObject.CreateInstance<ConsoleSettings>();
            string path = Path.Combine(Application.dataPath, SETTINGS_PATH);
            if (!Directory.Exists(path))
                Directory.CreateDirectory(path);
            string path2 = Path.Combine(Path.Combine("Assets", SETTINGS_PATH), SETTINGS_NAME);
            AssetDatabase.CreateAsset(settings, path2);
        }
        Debug.Log(settings);
        Selection.activeObject = settings;
    }
}
