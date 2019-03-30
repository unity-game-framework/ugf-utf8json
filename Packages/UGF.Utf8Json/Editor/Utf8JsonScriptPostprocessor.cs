using System;
using System.IO;
using UnityEditor;

namespace UGF.Utf8Json.Editor
{
    internal sealed class Utf8JsonScriptPostprocessor : AssetPostprocessor
    {
        private static void OnPostprocessAllAssets(string[] importedAssets, string[] deletedAssets, string[] movedAssets, string[] movedFromAssetPaths)
        {
            // HandleImported(importedAssets);
            // HandleDeleted(deletedAssets);
            // HandleMoved(movedAssets, movedFromAssetPaths);
        }

        private static void HandleImported(string[] paths)
        {
            for (int i = 0; i < paths.Length; i++)
            {
                string path = paths[i];

                HandleImported(path);
            }
        }

        private static void HandleImported(string path)
        {
            if (IsCSharpFile(path))
            {
                var monoScript = AssetDatabase.LoadAssetAtPath<MonoScript>(path);

                if (monoScript != null)
                {
                    Type type = monoScript.GetClass();

                    // if (type != null && Utf8JsonEditorUtility.IsTypeValidForGenerate(type))
                    // {
                    //     Utf8JsonEditorUtility.GenerateAsset(monoScript, true);
                    // }
                }
            }
        }

        private static void HandleDeleted(string[] paths)
        {
            for (int i = 0; i < paths.Length; i++)
            {
                string path = paths[i];

                HandleDeleted(path);
            }
        }

        private static void HandleDeleted(string path)
        {
            if (IsCSharpFile(path))
            {
                string scriptPath = Utf8JsonEditorUtility.GetPathForGeneratedScript(path);

                if (File.Exists(scriptPath))
                {
                    AssetDatabase.MoveAssetToTrash(scriptPath);
                }
            }
        }

        private static void HandleMoved(string[] to, string[] from)
        {
            for (int i = 0; i < to.Length; i++)
            {
                string pathTo = to[i];
                string pathFrom = from[i];

                HandleMoved(pathTo, pathFrom);
            }
        }

        private static void HandleMoved(string to, string from)
        {
        }

        private static bool IsCSharpFile(string path)
        {
            return Path.GetExtension(path) == ".cs";
        }
    }
}
