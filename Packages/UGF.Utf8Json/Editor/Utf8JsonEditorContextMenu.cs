using System;
using UnityEditor;

namespace UGF.Utf8Json.Editor
{
    internal static class Utf8JsonEditorContextMenu
    {
        [MenuItem("CONTEXT/MonoImporter/Utf8Json Generate Formatter", false, 1100)]
        private static void Menu(MenuCommand menuCommand)
        {
            var importer = (MonoImporter)menuCommand.context;
            var script = AssetDatabase.LoadAssetAtPath<MonoScript>(importer.assetPath);

            Utf8JsonEditorUtility.GenerateAsset(script, true);
        }

        [MenuItem("CONTEXT/MonoImporter/Utf8Json Generate Formatter", true, 1100)]
        private static bool Validate(MenuCommand menuCommand)
        {
            var importer = (MonoImporter)menuCommand.context;
            var script = AssetDatabase.LoadAssetAtPath<MonoScript>(importer.assetPath);
            Type type = script.GetClass();

            return type != null && Utf8JsonEditorUtility.IsTypeValidForGenerate(type);
        }
    }
}
