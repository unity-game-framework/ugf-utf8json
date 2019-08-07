using System.IO;
using UGF.Code.Generate.Editor;
using UnityEditor;
using UnityEditorInternal;

namespace UGF.Utf8Json.Editor
{
    internal static class Utf8JsonEditorMenu
    {
        [MenuItem("CONTEXT/AssemblyDefinitionImporter/Utf8Json Generate Enabled", false, 1000)]
        private static void AssemblyEnableGenerateMenu(MenuCommand menuCommand)
        {
        }

        [MenuItem("CONTEXT/AssemblyDefinitionImporter/Utf8Json Generate Enabled", true, 1000)]
        private static bool AssemblyEnableGenerateValidate(MenuCommand menuCommand)
        {
            var importer = (AssemblyDefinitionImporter)menuCommand.context;
            string path = CodeGenerateEditorUtility.GetPathForGeneratedScript(importer.assetPath, "Utf8Json");
            bool isActive = File.Exists(path);

            Menu.SetChecked("CONTEXT/AssemblyDefinitionImporter/Utf8Json Generate Enabled", isActive);

            return true;
        }

        [MenuItem("CONTEXT/AssemblyDefinitionImporter/Utf8Json Generate", false, 1000)]
        private static void AssemblyGenerateMenu(MenuCommand menuCommand)
        {
            var importer = (AssemblyDefinitionImporter)menuCommand.context;

            Utf8JsonEditorUtility.GenerateAssetFromAssembly(importer.assetPath);
        }

        [MenuItem("CONTEXT/AssemblyDefinitionImporter/Utf8Json Generate", true, 1000)]
        private static bool AssemblyGenerateValidate(MenuCommand menuCommand)
        {
            var importer = (AssemblyDefinitionImporter)menuCommand.context;
            string path = CodeGenerateEditorUtility.GetPathForGeneratedScript(importer.assetPath, "Utf8Json");

            return !EditorApplication.isCompiling && File.Exists(path);
        }

        [MenuItem("CONTEXT/AssemblyDefinitionImporter/Utf8Json Generate All", false, 1000)]
        private static void AssemblyGenerateAllMenu(MenuCommand menuCommand)
        {
            Utf8JsonEditorUtility.GenerateAssetFromAssemblyAll();
        }

        [MenuItem("CONTEXT/AssemblyDefinitionImporter/Utf8Json Generate All", true, 1000)]
        private static bool AssemblyGenerateAllValidate(MenuCommand menuCommand)
        {
            return !EditorApplication.isCompiling;
        }
    }
}
