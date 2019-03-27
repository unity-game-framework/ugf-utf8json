using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Reflection;
using System.Text;
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

        public static void GenerateForAssembly(string assemblyName)
        {
            if (TryFindAssembly(assemblyName, out Assembly assembly))
            {
                string path = CompilationPipeline.GetAssemblyDefinitionFilePathFromAssemblyName(assemblyName);
                string directory = Path.GetDirectoryName(Path.GetFullPath(path));

                path = $"{directory}/{assemblyName}.Utf8Json.Generated.cs";

                if (!string.IsNullOrEmpty(path))
                {
                    var sources = new List<string>();

                    for (int i = 0; i < assembly.sourceFiles.Length; i++)
                    {
                        string sourceFilePath = assembly.sourceFiles[i];
                        var script = AssetDatabase.LoadAssetAtPath<MonoScript>(sourceFilePath);
                        Type type = script != null ? script.GetClass() : null;

                        if (type != null && type.IsDefined(typeof(Utf8JsonSerializableAttribute)))
                        {
                            string fullPath = Path.GetFullPath(sourceFilePath);

                            sources.Add(fullPath);
                        }
                    }

                    string rootNamespace = $"{assemblyName}.Generated";
                    string resolverName = $"{assemblyName.Replace(".", string.Empty)}GeneratedResolver";

                    Generate(sources, path, resolverName, rootNamespace);
                }
                else
                {
                    Debug.LogWarning($"AssemblyDefinition path not found by specified assembly name: '{assemblyName}'.");
                }
            }
            else
            {
                Debug.LogWarning($"Assembly not found by specified assembly name: '{assemblyName}'.");
            }
        }

        public static void Generate(List<string> filePaths, string outputPath, string resolverName, string rootNamespace, bool outputLog = true, bool throwException = false)
        {
            string path = GetExecutePath();
            // string arguments = GetArguments(filePaths, outputPath, resolverName, rootNamespace);
            string arguments = GetArguments(filePaths);

            if (EditorUtility.DisplayCancelableProgressBar("Generate Resolver", "Processing...", 0.9F))
            {
                return;
            }

            // StartProcess(path, arguments, outputLog, throwException);

            var args = new Utf8JsonUniversalGeneratorUtility.RunArguments();
            
            args.InputFiles.AddRange(filePaths);
            args.OutputPath = outputPath;
            args.ResolverName = resolverName;
            args.NamespaceRoot = rootNamespace;
            
            Utf8JsonUniversalGeneratorUtility.Run(args);
            
            EditorUtility.ClearProgressBar();
        }

        private static string GetExecutePath()
        {
            PackageInfo packageInfo = GetPackageInfo("com.ugf.utf8json");

            return $"{packageInfo.resolvedPath}/Editor/.Compiler/win-x64/Utf8Json.UniversalCodeGenerator.exe";
        }

        private static string GetArguments(List<string> filePaths)
        {
            var builder = new StringBuilder();
            
            builder.Append(" -i \"");

            for (int i = 0; i < filePaths.Count; i++)
            {
                string filePath = filePaths[i];

                builder.Append(filePath);

                if (i != filePaths.Count - 1)
                {
                    builder.Append(", ");
                }
            }
            
            return builder.ToString();
        }

        private static string GetArguments(List<string> filePaths, string outputPath, string resolverName, string rootNamespace)
        {
            var builder = new StringBuilder();

            builder.Append(" -i \"");

            for (int i = 0; i < filePaths.Count; i++)
            {
                string filePath = filePaths[i];

                builder.Append(filePath);

                if (i != filePaths.Count - 1)
                {
                    builder.Append(", ");
                }
            }

            builder.Append('\"');
            builder.Append($" -o \"{outputPath}\"");
            builder.Append($" -r \"{resolverName}\"");
            builder.Append($" -n \"{rootNamespace}\"");

            return builder.ToString();
        }

        private static void StartProcess(string path, string arguments, bool outputLog, bool throwException)
        {
            var info = new ProcessStartInfo
            {
                UseShellExecute = false,
                CreateNoWindow = true,
                RedirectStandardOutput = outputLog,
                RedirectStandardError = outputLog,
                FileName = path,
                Arguments = arguments
            };

            var process = new Process
            {
                StartInfo = info,
                EnableRaisingEvents = true
            };

            process.Start();

            if (outputLog)
            {
                string output = process.StandardOutput.ReadToEnd();
                string error = process.StandardOutput.ReadToEnd();

                if (!string.IsNullOrEmpty(output))
                {
                    Debug.Log(output);
                }

                if (!string.IsNullOrEmpty(error))
                {
                    if (throwException)
                    {
                        throw new Exception(error);
                    }

                    Debug.LogError(error);
                }
            }

            process.WaitForExit();
            process.Dispose();
        }

        private static bool TryFindAssembly(string assemblyName, out Assembly assembly)
        {
            Assembly[] assemblies = CompilationPipeline.GetAssemblies();

            for (int i = 0; i < assemblies.Length; i++)
            {
                assembly = assemblies[i];

                if (assembly.name == assemblyName)
                {
                    return true;
                }
            }

            assembly = null;
            return false;
        }

        private static PackageInfo GetPackageInfo(string packageName)
        {
            return m_getForAssetPath.Invoke($"Packages/{packageName}");
        }
    }
}