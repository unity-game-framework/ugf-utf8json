using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using UGF.Code.Analysis.Editor;
using UGF.Utf8Json.Runtime;
using UnityEditor;
using UnityEditor.Compilation;
using Utf8Json.UniversalCodeGenerator;
using Assembly = UnityEditor.Compilation.Assembly;

namespace UGF.Utf8Json.Editor
{
    public static class Utf8JsonEditorUtility
    {
        public static void GenerateAssetFromAssembly(string path, bool import = true)
        {
            if (path == null) throw new ArgumentNullException(nameof(path));

            string sourcePath = GetPathForGeneratedScript(path);
            string source = GenerateFromAssembly(path);

            File.WriteAllText(sourcePath, source);

            if (import)
            {
                AssetDatabase.ImportAsset(sourcePath);
            }
        }

        public static string GenerateFromAssembly(string path)
        {
            if (path == null) throw new ArgumentNullException(nameof(path));

            if (!TryGetAssemblyByPath(path, out Assembly assembly))
            {
                throw new ArgumentException($"Assembly not found from the specified path: '{path}'.");
            }

            List<string> sourcePaths = GetSerializableScriptPathsFromAssembly(assembly);

            return GenerateFormatters(sourcePaths, assembly.name);
        }

        public static string GenerateFormatters(List<string> sourcePaths, string namespaceRoot)
        {
            if (sourcePaths == null) throw new ArgumentNullException(nameof(sourcePaths));
            if (namespaceRoot == null) throw new ArgumentNullException(nameof(namespaceRoot));

            HashSet<string> usings = CodeAnalysisEditorUtility.CollectUsingNamesFromPaths(sourcePaths);
            string formatters = Utf8JsonUniversalCodeGeneratorUtility.GenerateFormatters(sourcePaths, namespaceRoot);

            formatters = CodeAnalysisEditorUtility.AddUsings(formatters, usings);
            formatters = CodeAnalysisEditorUtility.AddAttributeToClassDeclaration(CodeAnalysisEditorUtility.ProjectCompilation, formatters, typeof(Utf8JsonFormatterAttribute), false);
            formatters = CodeAnalysisEditorUtility.AddLeadingTrivia(formatters, new[] { $"// ReSharper disable all{Environment.NewLine}" });

            return formatters;
        }

        public static string GetPathForGeneratedScript(string path)
        {
            if (string.IsNullOrEmpty(path)) throw new ArgumentNullException(nameof(path));

            var builder = new StringBuilder();
            string directory = Path.GetDirectoryName(path);
            string name = Path.GetFileNameWithoutExtension(path);

            if (!string.IsNullOrEmpty(directory))
            {
                directory = directory.Replace("\\", "/");

                builder.Append(directory);
                builder.Append("/");
            }

            builder.Append(name);
            builder.Append(".Utf8Json.Generated.cs");

            return builder.ToString();
        }

        public static bool IsSerializableScript(string path)
        {
            if (path == null) throw new ArgumentNullException(nameof(path));

            string source = File.ReadAllText(path);

            return CodeAnalysisEditorUtility.CheckAttribute(CodeAnalysisEditorUtility.ProjectCompilation, source, typeof(Utf8JsonSerializableAttribute));
        }

        public static bool IsAssemblyHasGeneratedScript(string path)
        {
            if (path == null) throw new ArgumentNullException(nameof(path));

            return File.Exists(GetPathForGeneratedScript(path));
        }
        
        private static List<string> GetSerializableScriptPathsFromAssembly(Assembly assembly)
        {
            if (assembly == null) throw new ArgumentNullException(nameof(assembly));

            return CodeAnalysisEditorUtility.CheckAttributeAllPaths(CodeAnalysisEditorUtility.ProjectCompilation, assembly.sourceFiles, typeof(Utf8JsonSerializableAttribute));
        }

        private static bool TryGetAssemblyByPath(string path, out Assembly assembly)
        {
            if (path == null) throw new ArgumentNullException(nameof(path));

            return TryGetAssemblyByName(Path.GetFileNameWithoutExtension(path), out assembly);
        }

        private static bool TryGetAssemblyByName(string name, out Assembly assembly)
        {
            if (name == null) throw new ArgumentNullException(nameof(name));

            Assembly[] assemblies = CompilationPipeline.GetAssemblies();

            for (int i = 0; i < assemblies.Length; i++)
            {
                assembly = assemblies[i];

                if (assembly.name == name)
                {
                    return true;
                }
            }

            assembly = null;
            return false;
        }
    }
}
