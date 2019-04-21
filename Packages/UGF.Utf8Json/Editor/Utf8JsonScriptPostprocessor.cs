using System;
using System.Collections.Generic;
using System.IO;
using UnityEditor;
using UnityEditor.Compilation;

namespace UGF.Utf8Json.Editor
{
    internal sealed class Utf8JsonScriptPostprocessor : AssetPostprocessor
    {
        private static readonly HashSet<string> m_assemblies = new HashSet<string>();

        private static void OnPostprocessAllAssets(string[] importedAssets, string[] deletedAssets, string[] movedAssets, string[] movedFromAssetPaths)
        {
            for (int i = 0; i < importedAssets.Length; i++)
            {
                HandleImported(m_assemblies, importedAssets[i]);
            }

            for (int i = 0; i < deletedAssets.Length; i++)
            {
                HandleDeleted(m_assemblies, deletedAssets[i]);
            }

            for (int i = 0; i < movedAssets.Length; i++)
            {
                HandleMoved(m_assemblies, movedAssets[i], movedFromAssetPaths[i]);
            }

            if (m_assemblies.Count > 0)
            {
                foreach (string assembly in m_assemblies)
                {
                    Utf8JsonEditorUtility.GenerateAssetFromAssembly(assembly);
                }

                m_assemblies.Clear();
            }
        }

        private static void HandleImported(ICollection<string> assemblies, string path)
        {
            if (IsCSharpFile(path) && IsAssemblyHasGeneratedScript(path) && IsTargetScript(path))
            {
                GetAssembly(assemblies, path);
            }

            if (IsExternalFile(path) && IsAssemblyHasGeneratedScript(path))
            {
                GetAssembly(assemblies, path);
            }
        }

        private static void HandleDeleted(ICollection<string> assemblies, string path)
        {
            if (IsCSharpFile(path) && IsAssemblyHasGeneratedScript(path))
            {
                GetAssembly(assemblies, path);
            }

            if (IsExternalFile(path) && IsAssemblyHasGeneratedScript(path))
            {
                GetAssembly(assemblies, path);
            }
        }

        private static void HandleMoved(ICollection<string> assemblies, string to, string from)
        {
            HandleImported(assemblies, to);

            if (IsCSharpFile(from) && IsAssemblyHasGeneratedScript(from) && IsTargetScript(to))
            {
                GetAssembly(assemblies, from);
            }

            if (IsExternalFile(from) && IsAssemblyHasGeneratedScript(from))
            {
                GetAssembly(assemblies, from);
            }
        }

        private static void GetAssembly(ICollection<string> assemblies, string path)
        {
            string assemblyPath = CompilationPipeline.GetAssemblyDefinitionFilePathFromScriptPath(path);

            assemblies.Add(assemblyPath);
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

        private static bool IsTargetScript(string path)
        {
            return Utf8JsonEditorUtility.IsSerializableScript(path);
        }
    }
}
