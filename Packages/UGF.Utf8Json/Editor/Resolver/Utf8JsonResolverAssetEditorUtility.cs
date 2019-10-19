using System;
using System.Collections.Generic;
using System.IO;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.Editing;
using UGF.Code.Analysis.Editor;
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

        public static string GenerateResolver(Utf8JsonResolverAssetData data, ICodeGenerateContainerValidation validation = null, Compilation compilation = null, SyntaxGenerator generator = null)
        {
            if (data == null) throw new ArgumentNullException(nameof(data));
            if (string.IsNullOrEmpty(data.name)) throw new ArgumentException("Value cannot be null or empty.", nameof(data.name));
            if (validation == null) validation = CodeGenerateContainerExternalEditorUtility.DefaultValidation;
            if (compilation == null) compilation = CodeAnalysisEditorUtility.ProjectCompilation;
            if (generator == null) generator = CodeAnalysisEditorUtility.Generator;

            var sourcePaths = new List<string>();
            string resolverName = Utf8JsonEditorUtility.FormatResolverName(data.name);
            string namespaceRoot = resolverName;
            string externalsTemp = string.Empty;

            var generateArguments = new Utf8JsonGenerateArguments
            {
                IgnoreReadOnly = data.IgnoreReadOnly,
                IsTypeRequireAttribute = data.AttributeRequired,
                TypeRequiredAttributeShortName = data.AttributeShortName
            };

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
                string attributeTypeName = null;

                for (int i = 0; i < data.Externals.Count; i++)
                {
                    string guid = data.Externals[i];
                    string path = AssetDatabase.GUIDToAssetPath(guid);

                    if (!string.IsNullOrEmpty(path))
                    {
                        if (AssetDatabase.IsValidFolder(path))
                        {
                            string[] files = Directory.GetFiles(path, $"*.{ResolverAssetExtensionName}");

                            externals.AddRange(files);
                        }
                        else
                        {
                            externals.Add(path);
                        }
                    }
                }

                if (data.AttributeRequired)
                {
                    attributeTypeName = data.AttributeShortName;
                }

                externalsTemp = Utf8JsonExternalTypeEditorUtility.GenerateExternalSources(externals, sourcePaths, attributeTypeName, validation, compilation, generator);
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
