using UnityEditor;
using UnityEditorInternal;

namespace UGF.Utf8Json.Editor
{
    internal static class Utf8JsonEditorContextMenu
    {
        [MenuItem("CONTEXT/AssemblyDefinitionImporter/Utf8Json Generate Formatters", false, 1000)]
        private static void AssemblyUtf8JsonGenerateFormattersMenu(MenuCommand menuCommand)
        {
            var importer = (AssemblyDefinitionImporter)menuCommand.context;

            Utf8JsonEditorUtility.GenerateAssetFromAssembly(importer.assetPath);
        }
    }
}
