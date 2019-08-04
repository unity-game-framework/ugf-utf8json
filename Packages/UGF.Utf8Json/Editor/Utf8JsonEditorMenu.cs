using UnityEditor;
using UnityEditorInternal;

namespace UGF.Utf8Json.Editor
{
    internal static class Utf8JsonEditorMenu
    {
        [MenuItem("CONTEXT/AssemblyDefinitionImporter/Utf8Json Generate", false, 1000)]
        private static void AssemblyGenerateMenu(MenuCommand menuCommand)
        {
            var importer = (AssemblyDefinitionImporter)menuCommand.context;

            Utf8JsonEditorUtility.GenerateAssetFromAssembly(importer.assetPath);
        }

        [MenuItem("CONTEXT/AssemblyDefinitionImporter/Utf8Json Generate All", false, 1000)]
        private static void AssemblyGenerateAllMenu(MenuCommand menuCommand)
        {
            Utf8JsonEditorUtility.GenerateAssetFromAssemblyAll();
        }
    }
}
