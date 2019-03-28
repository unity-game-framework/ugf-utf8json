using UnityEditor;

namespace UGF.Utf8Json.Editor
{
    internal static class Utf8JsonEditorContextMenu
    {
        [MenuItem("CONTEXT/MonoImporter/Utf8Json Generate Formatter", false, 1000)]
        private static void Menu2(MenuCommand menuCommand)
        {
            var importer = (MonoImporter)menuCommand.context;
            var script = AssetDatabase.LoadAssetAtPath<MonoScript>(importer.assetPath);

            Utf8JsonEditorUtility.Generate(script);
        }
    }
}