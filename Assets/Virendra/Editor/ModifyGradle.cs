using System.IO;
using System.Linq;
using UnityEngine;

#if UNITY_EDITOR
using UnityEditor;
#endif

public class ModifyGradle : MonoBehaviour
{
    #if UNITY_2020_1_OR_NEWER
    [MenuItem("JioMixedReality/Manifest/Modify Gradle", priority = -100)]
    public static void Modify()
    {
        string path = Path.Combine(Application.dataPath, "Plugins/Android/mainTemplate.gradle");
        string[] lines = File.ReadAllLines(path);
        bool found = false;
        for (int i = 0; i < lines.Length; i++)
        {
            if (lines[i].Contains("aaptOptions"))
            {
                if (lines.Where(t => t.Contains("noCompress")).ToList().Count > 0)
                {
                    Debug.Log("noCompress property is already setup in mainTemplate");
                    return;
                }
                found = true;
                lines[i] = "aaptOptions {\n\t\t    noCompress = ['.ress', '.resource', '.obb'] + unityStreamingAssets.tokenize(', ')";
            }
        }
        if (!found)
        {
            Debug.LogError("Could not find android block in build.gradle");
            return;
        }
        File.WriteAllLines(path, lines);
        Debug.Log("Successfully added \"noCompress '.ress', '.resource', '.obb'\" to aaptOptions");
    }
    #endif
}