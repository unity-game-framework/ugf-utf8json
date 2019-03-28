using System;
using System.Collections.Generic;
using System.IO;
using System.Reflection;
using System.Text;
using UGF.Code.Analysis.Editor;
using UGF.Utf8Json.Runtime;
using UnityEditor;
using Utf8Json.UniversalCodeGenerator;

namespace UGF.Utf8Json.Editor
{
    public static class Utf8JsonEditorUtility
    {
        public static void GenerateAsset(MonoScript monoScript, bool import = false)
        {
            if (monoScript == null) throw new ArgumentNullException(nameof(monoScript));
            
            Type type = monoScript.GetClass();
            string path = AssetDatabase.GetAssetPath(monoScript);
            string namespaceRoot = type.Namespace ?? string.Empty;

            string script = GenerateFormatter(path, namespaceRoot);
            string scriptPath = GetPathForGeneratedScript(path);

            File.WriteAllText(scriptPath, script);

            if (import)
            {
                AssetDatabase.ImportAsset(scriptPath);   
            }
        }

        public static string GenerateFormatter(string path, string namespaceRoot)
        {
            string result = Utf8JsonUniversalCodeGeneratorUtility.GenerateFormatters(new List<string> { path }, namespaceRoot);

            result = AddDefaultLeadingTrivia(result);

            return result;
        }

        public static string Generate(List<string> inputFiles, string resolverName, string namespaceRoot)
        {
            string result = Utf8JsonUniversalCodeGeneratorUtility.Generate(inputFiles, resolverName, namespaceRoot);

            result = AddDefaultLeadingTrivia(result);

            return result;
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

        public static bool IsTypeValidForGenerate(Type type)
        {
            if (type == null) throw new ArgumentNullException(nameof(type));
            
            return type.IsDefined(typeof(Utf8JsonSerializableAttribute));
        }

        private static string AddDefaultLeadingTrivia(string text)
        {
            if (text == null) throw new ArgumentNullException(nameof(text));
            
            return CodeAnalysisEditorUtility.AddLeadingTrivia(text, new List<string> { "using Utf8Json;", string.Empty, "// ReSharper disable all" });
        }
    }
}
