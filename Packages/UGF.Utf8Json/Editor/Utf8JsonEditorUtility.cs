using System;
using System.Collections.Generic;
using System.IO;
using System.Reflection;
using UGF.Utf8Json.Runtime;
using UnityEditor;
using UnityEditor.Compilation;
using Utf8Json.UniversalCodeGenerator;
using Assembly = UnityEditor.Compilation.Assembly;
using Debug = UnityEngine.Debug;
using PackageInfo = UnityEditor.PackageManager.PackageInfo;

namespace UGF.Utf8Json.Editor
{
    [InitializeOnLoad]
    public static class Utf8JsonEditorUtility
    {
        private static readonly Func<string, PackageInfo> m_getForAssetPath;

        static Utf8JsonEditorUtility()
        {
            EditorUtility.ClearProgressBar();

            Type type = typeof(PackageInfo).Assembly.GetType("UnityEditor.PackageManager.Packages");
            MethodInfo method = type.GetMethod("GetForAssetPath", BindingFlags.DeclaredOnly | BindingFlags.Instance | BindingFlags.Public | BindingFlags.Static);

            if (method != null)
            {
                m_getForAssetPath = (Func<string, PackageInfo>)method.CreateDelegate(typeof(Func<string, PackageInfo>));
            }
        }

        public static void Generate(MonoScript monoScript)
        {
            Type type = monoScript.GetClass();
            string path = AssetDatabase.GetAssetPath(monoScript);
            string namespaceRoot = type.Namespace ?? string.Empty;
            
            string script = Generate(path, namespaceRoot);
            
            string directory = Path.GetDirectoryName(path);
            string name = Path.GetFileNameWithoutExtension(path);
            string pathGenerated = $"{directory}/{name}.Utf8Json.Generated.cs";

            File.WriteAllText(pathGenerated, script);
            AssetDatabase.ImportAsset(pathGenerated);
        }

        public static string Generate(string path, string namespaceRoot)
        {
            string result = Utf8JsonUniversalCodeGeneratorUtility.GenerateFormatters(new List<string> { path }, namespaceRoot);

            return result;
        }

        public static void Generate(List<string> filePaths, string outputPath, string resolverName, string rootNamespace, bool outputLog = true, bool throwException = false)
        {
            string result = Utf8JsonUniversalCodeGeneratorUtility.Generate(filePaths, resolverName, rootNamespace);

            File.WriteAllText(outputPath, result);
        }
    }
}