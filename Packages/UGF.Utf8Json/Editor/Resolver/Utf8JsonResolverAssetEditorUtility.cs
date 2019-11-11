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
using Object = UnityEngine.Object;

namespace UGF.Utf8Json.Editor.Resolver
{
    public static class Utf8JsonResolverAssetEditorUtility
    {
        public static string ResolverAssetExtensionName { get; } = "utf8json-resolver";

        public static void GenerateResolverAll(ICodeGenerateContainerValidation validation = null, Compilation compilation = null, SyntaxGenerator generator = null)
        {
        }

        public static void GenerateResolver(string assetPath, ICodeGenerateContainerValidation validation = null, Compilation compilation = null, SyntaxGenerator generator = null)
        {
            if (string.IsNullOrEmpty(assetPath)) throw new ArgumentException("Value cannot be null or empty.", nameof(assetPath));

            Utf8JsonResolverAssetData data = LoadResolverData(assetPath);
            string source = GenerateResolver(data, validation, compilation, generator);
            string path = data.GenerateSource || string.IsNullOrEmpty(data.Source)
                ? CodeGenerateEditorUtility.GetPathForGeneratedScript(assetPath, "Utf8Json")
                : AssetDatabase.GUIDToAssetPath(data.Source);

            File.WriteAllText(path, source);
            AssetDatabase.ImportAsset(path);

            if (data.GenerateSource && string.IsNullOrEmpty(data.Source))
            {
                data.Source = AssetDatabase.AssetPathToGUID(path);

                SaveResolverData(assetPath, data);
            }

            Object.DestroyImmediate(data);
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
                var externals = new List<string>();

                for (int i = 0; i < data.Sources.Count; i++)
                {
                    string guid = data.Sources[i];
                    string path = AssetDatabase.GUIDToAssetPath(guid);

                    if (!string.IsNullOrEmpty(path))
                    {
                        if (path.EndsWith(".cs", StringComparison.InvariantCultureIgnoreCase))
                        {
                            sourcePaths.Add(path);
                        }
                        else if (path.EndsWith($".{Utf8JsonExternalTypeEditorUtility.ExternalTypeAssetExtensionName}", StringComparison.InvariantCultureIgnoreCase))
                        {
                            externals.Add(path);
                        }
                    }
                }

                if (externals.Count > 0)
                {
                    externalsTemp = Utf8JsonExternalTypeEditorUtility.GenerateExternalSources(externals, sourcePaths, attributeType, validation, compilation, generator);
                }
            }

            string source = Utf8JsonEditorUtility.GenerateResolver(sourcePaths, resolverName, namespaceRoot, generateArguments);

            if (!string.IsNullOrEmpty(externalsTemp))
            {
                FileUtil.DeleteFileOrDirectory(externalsTemp);
            }

            return source;
        }

        public static void ClearResolver(string assetPath)
        {
            if (string.IsNullOrEmpty(assetPath)) throw new ArgumentException("Value cannot be null or empty.", nameof(assetPath));

            Utf8JsonResolverAssetData data = LoadResolverData(assetPath);
            string path = data.GenerateSource || string.IsNullOrEmpty(data.Source)
                ? CodeGenerateEditorUtility.GetPathForGeneratedScript(assetPath, "Utf8Json")
                : AssetDatabase.GUIDToAssetPath(data.Source);

            if (!string.IsNullOrEmpty(path))
            {
                AssetDatabase.MoveAssetToTrash(path);
            }

            Object.DestroyImmediate(data);
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
