using System;
using System.Collections.Generic;
using System.IO;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.Editing;
using UGF.Code.Analysis.Editor;
using UGF.Code.Generate.Editor;
using UGF.Code.Generate.Editor.Container;
using UGF.Code.Generate.Editor.Container.External;
using UGF.Utf8Json.Editor.ExternalType;
using UnityEditor;
using UnityEngine;
using Utf8Json.UniversalCodeGenerator;

namespace UGF.Utf8Json.Editor.Resolver
{
    public static class Utf8JsonResolverAssetEditorUtility
    {
        public static string ResolverAssetExtensionName { get; } = "utf8json-resolver";

        public static void GenerateResolver(string assetPath, ICodeGenerateContainerValidation validation = null, Compilation compilation = null, SyntaxGenerator generator = null)
        {
            if (string.IsNullOrEmpty(assetPath)) throw new ArgumentException("Value cannot be null or empty.", nameof(assetPath));
            if (!File.Exists(assetPath)) throw new ArgumentException("The file at specified asset path does not exists.", nameof(assetPath));

            string extension = Path.GetExtension(assetPath);
            string extensionTarget = $".{ResolverAssetExtensionName}";

            if (string.IsNullOrEmpty(extension) || !extension.Equals(extensionTarget, StringComparison.InvariantCultureIgnoreCase))
            {
                throw new ArgumentException($"The specified asset path contains invalid extension: '{assetPath}'.", nameof(assetPath));
            }

            Utf8JsonResolverAssetData data = LoadResolverData(assetPath);
            string source = GenerateResolver(data, validation, compilation, generator);
            string path = CodeGenerateEditorUtility.GetPathForGeneratedScript(assetPath, "Utf8Json");

            File.WriteAllText(path, source);
            AssetDatabase.ImportAsset(path);
        }

        public static string GenerateResolver(Utf8JsonResolverAssetData data, ICodeGenerateContainerValidation validation = null, Compilation compilation = null, SyntaxGenerator generator = null)
        {
            if (data == null) throw new ArgumentNullException(nameof(data));
            if (validation == null) validation = CodeGenerateContainerExternalEditorUtility.DefaultValidation;
            if (compilation == null) compilation = CodeAnalysisEditorUtility.ProjectCompilation;
            if (generator == null) generator = CodeAnalysisEditorUtility.Generator;

            var sourcePaths = new List<string>();
            string resolverName = Utf8JsonEditorUtility.FormatResolverName(data.Name);
            string namespaceRoot = data.NamespaceRoot;
            string externalsTemp = string.Empty;
            Type attributeType = null;

            var generateArguments = new Utf8JsonGenerateArguments
            {
                IgnoreReadOnly = data.IgnoreReadOnly,
                IsTypeRequireAttribute = data.AttributeRequired
            };

            if (data.AttributeRequired && data.TryGetAttributeType(out attributeType))
            {
                generateArguments.TypeRequiredAttributeShortName = attributeType.Name;
            }

            if (data.Sources.Count > 0)
            {
                for (int i = 0; i < data.Sources.Count; i++)
                {
                    string guid = data.Sources[i];
                    string path = AssetDatabase.GUIDToAssetPath(guid);

                    if (!string.IsNullOrEmpty(path))
                    {
                        if (AssetDatabase.IsValidFolder(path))
                        {
                            string[] files = Directory.GetFiles(path, "*.cs");

                            sourcePaths.AddRange(files);
                        }
                        else
                        {
                            sourcePaths.Add(path);
                        }
                    }
                }
            }

            if (data.Externals.Count > 0)
            {
                var externals = new List<string>();

                for (int i = 0; i < data.Externals.Count; i++)
                {
                    string guid = data.Externals[i];
                    string path = AssetDatabase.GUIDToAssetPath(guid);

                    if (!string.IsNullOrEmpty(path))
                    {
                        if (AssetDatabase.IsValidFolder(path))
                        {
                            string[] files = Directory.GetFiles(path, $"*.{Utf8JsonExternalTypeEditorUtility.ExternalTypeAssetExtensionName}");

                            externals.AddRange(files);
                        }
                        else
                        {
                            externals.Add(path);
                        }
                    }
                }

                externalsTemp = Utf8JsonExternalTypeEditorUtility.GenerateExternalSources(externals, sourcePaths, attributeType, validation, compilation, generator);
            }

            string source = Utf8JsonEditorUtility.GenerateResolver(sourcePaths, resolverName, namespaceRoot, generateArguments);

            if (!string.IsNullOrEmpty(externalsTemp))
            {
                FileUtil.DeleteFileOrDirectory(externalsTemp);
            }

            return source;
        }

        public static Utf8JsonResolverAssetData LoadResolverData(string assetPath)
        {
            if (string.IsNullOrEmpty(assetPath)) throw new ArgumentException("Value cannot be null or empty.", nameof(assetPath));

            string source = File.ReadAllText(assetPath);
            var data = ScriptableObject.CreateInstance<Utf8JsonResolverAssetData>();

            JsonUtility.FromJsonOverwrite(source, data);

            return data;
        }

        public static void SaveResolverData(string assetPath, Utf8JsonResolverAssetData data, bool importAsset = true)
        {
            if (string.IsNullOrEmpty(assetPath)) throw new ArgumentException("Value cannot be null or empty.", nameof(assetPath));
            if (data == null) throw new ArgumentNullException(nameof(data));

            string source = JsonUtility.ToJson(data, true);

            File.WriteAllText(assetPath, source);

            if (importAsset)
            {
                AssetDatabase.ImportAsset(assetPath);
            }
        }
    }
}