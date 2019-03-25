using UnityEditor;
using UnityEditorInternal;

namespace UGF.Utf8Json.Editor
{
    internal static class Utf8JsonAssemblyDefinitionEditorMenu
    {
        [MenuItem("CONTEXT/AssemblyDefinitionImporter/Utf8Json Generate Resolver", false, 1000)]
        private static void Menu(MenuCommand menuCommand)
        {
            var importer = (AssemblyDefinitionImporter)menuCommand.context;
            var assemblyDefinition = AssetDatabase.LoadAssetAtPath<AssemblyDefinitionAsset>(importer.assetPath);
            
            Utf8JsonEditorUtility.GenerateForAssembly(assemblyDefinition.name);
        }
    }
}