using System;
using System.IO;
using UnityEditor;
using UnityEditor.Compilation;

namespace UGF.Utf8Json.Editor
{
    internal sealed class Utf8JsonScriptPostprocessor : AssetPostprocessor
    {
        private static void OnPostprocessAllAssets(string[] importedAssets, string[] deletedAssets, string[] movedAssets, string[] movedFromAssetPaths)
        {
            for (int i = 0; i < importedAssets.Length; i++)
            {
                HandleImported(importedAssets[i]);
            }

            for (int i = 0; i < deletedAssets.Length; i++)
            {
                HandleDeleted(deletedAssets[i]);
            }

            for (int i = 0; i < movedAssets.Length; i++)
            {
                HandleMoved(movedAssets[i], movedFromAssetPaths[i]);
            }
        }

        private static void HandleImported(string path)
        {
            if (IsCSharpFile(path) && IsAssemblyHasGeneratedScript(path) && Utf8JsonEditorUtility.IsSerializableScript(path))
            {
                GenerateAssembly(path);
            }
        }

        private static void HandleDeleted(string path)
        {
            if (IsCSharpFile(path) && IsAssemblyHasGeneratedScript(path))
            {
                GenerateAssembly(path);
            }
        }

        private static void HandleMoved(string to, string from)
        {
            HandleDeleted(to);

            if (IsCSharpFile(from) && IsAssemblyHasGeneratedScript(from) && Utf8JsonEditorUtility.IsSerializableScript(to))
            {
                GenerateAssembly(from);
            }
        }

        private static void GenerateAssembly(string path)
        {
            string assemblyPath = CompilationPipeline.GetAssemblyDefinitionFilePathFromScriptPath(path);

            Utf8JsonEditorUtility.GenerateAssetFromAssembly(assemblyPath);
        }

        private static bool IsCSharpFile(string path)
        {
            string extension = Path.GetExtension(path);

            return !string.IsNullOrEmpty(extension) && extension.Equals(".cs", StringComparison.InvariantCultureIgnoreCase);
        }

        private static bool IsExternalFile(string path)
        {
            string extension = Path.GetExtension(path);

            return !string.IsNullOrEmpty(extension) && extension.Equals(".utf8json-external", StringComparison.InvariantCultureIgnoreCase);
        }

        private static bool IsAssemblyHasGeneratedScript(string path)
        {
            string assemblyPath = CompilationPipeline.GetAssemblyDefinitionFilePathFromScriptPath(path);

            return Utf8JsonEditorUtility.IsAssemblyHasGeneratedScript(assemblyPath);
        }
    }
}
