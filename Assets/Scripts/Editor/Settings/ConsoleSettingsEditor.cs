using System.IO;
using UnityDevConsole.Settings;
using UnityEditor;
using UnityEngine;
using static UnityDevConsole.Settings.ConsoleSettings;

public class ConsoleSettingsEditor : Editor
{
    [MenuItem("Assets/Developer Console/Edit Settings")]
    public static void Edit ()
    {
        ConsoleSettings settings = LoadInstance();

        if (settings == null)
        {
            settings = CreateInstance<ConsoleSettings>();
            string path = Path.Combine(Application.dataPath, SETTINGS_PATH);

            if (!Directory.Exists(path))
                Directory.CreateDirectory(path);

            string path2 = Path.Combine("Assets", SETTINGS_PATH, SETTINGS_NAME + ".asset");
            AssetDatabase.CreateAsset(settings, path2);
        }
        Selection.activeObject = settings;
    }
}
