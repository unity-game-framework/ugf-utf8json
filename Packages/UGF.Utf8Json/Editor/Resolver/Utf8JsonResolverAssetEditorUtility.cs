using System;
using System.Collections.Generic;
using System.IO;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.Editing;
using UGF.Code.Analysis.Editor;
using UGF.Code.Generate.Editor;
using UGF.Code.Generate.Editor.Container;
using UGF.Code.Generate.Editor.Container.External;
using UGF.Utf8Json.Editor.ExternalType;
using UGF.Utf8Json.Runtime.Resolver;
using UnityEditor;
using UnityEngine;
using Utf8Json;
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
            string path = GetDestinationSourcePath(assetPath, data.ResolverName, data.DestinationSource);

            File.WriteAllText(path, source);
            AssetDatabase.ImportAsset(path);

            if (string.IsNullOrEmpty(data.DestinationSource))
            {
                data.DestinationSource = AssetDatabase.AssetPathToGUID(path);

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
            string resolverName = Utf8JsonEditorUtility.FormatResolverName(data.ResolverName);
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

            if (data.ResolverAsset)
            {
                source = AppendResolverAsset(source, resolverName, namespaceRoot, compilation, generator);
            }

            return source;
        }

        public static void ClearResolver(string assetPath)
        {
            if (string.IsNullOrEmpty(assetPath)) throw new ArgumentException("Value cannot be null or empty.", nameof(assetPath));

            Utf8JsonResolverAssetData data = LoadResolverData(assetPath);
            string path = GetDestinationSourcePath(assetPath, data.ResolverName, data.DestinationSource);

            if (!string.IsNullOrEmpty(path))
            {
                AssetDatabase.MoveAssetToTrash(path);
            }

            Object.DestroyImmediate(data);
        }

        public static string GetDestinationSourcePath(string assetPath, string resolverName, string sourceGuid)
        {
            if (string.IsNullOrEmpty(assetPath)) throw new ArgumentException("Value cannot be null or empty.", nameof(assetPath));
            if (string.IsNullOrEmpty(resolverName)) throw new ArgumentException("Value cannot be null or empty.", nameof(resolverName));

            string path = AssetDatabase.GUIDToAssetPath(sourceGuid);

            if (string.IsNullOrEmpty(path))
            {
                string directory = Path.GetDirectoryName(assetPath);

                resolverName = Utf8JsonEditorUtility.FormatResolverName(resolverName);
                path = $"{directory}/{resolverName}Asset.cs";
            }

            return path;
        }

        public static string AppendResolverAsset(string source, string resolverName, string namespaceRoot, Compilation compilation = null, SyntaxGenerator generator = null)
        {
            if (compilation == null) compilation = CodeAnalysisEditorUtility.ProjectCompilation;
            if (generator == null) generator = CodeAnalysisEditorUtility.Generator;

            SyntaxNode unit = SyntaxFactory.ParseCompilationUnit(source);
            SyntaxNode assetDeclaration = GenerateResolverAsset(resolverName, namespaceRoot, compilation, generator);

            unit = generator.InsertMembers(unit, 0, assetDeclaration);

            return unit.ToFullString();
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

        private static SyntaxNode GenerateResolverAsset(string resolverName, string namespaceRoot, Compilation compilation = null, SyntaxGenerator generator = null)
        {
            if (string.IsNullOrEmpty(resolverName)) throw new ArgumentException("Value cannot be null or empty.", nameof(resolverName));
            if (string.IsNullOrEmpty(namespaceRoot)) throw new ArgumentException("Value cannot be null or empty.", nameof(namespaceRoot));
            if (compilation == null) compilation = CodeAnalysisEditorUtility.ProjectCompilation;
            if (generator == null) generator = CodeAnalysisEditorUtility.Generator;

            if (!compilation.TryConstructTypeSymbol(typeof(Utf8JsonResolverAsset), out ITypeSymbol baseTypeSymbol))
            {
                throw new ArgumentException("Can't construct asset base type from the specified compilation.");
            }

            if (!compilation.TryConstructTypeSymbol(typeof(IJsonFormatterResolver), out ITypeSymbol resolverTypeSymbol))
            {
                throw new ArgumentException("Can't construct resolver type from the specified compilation.");
            }

            if (!compilation.TryConstructTypeSymbol(typeof(CreateAssetMenuAttribute), out ITypeSymbol createAttributeTypeSymbol))
            {
                throw new ArgumentException("Can't construct create attribute type from the specified compilation.");
            }

            string namespaceName = $"{namespaceRoot}.Asset";
            string menuName = $"{namespaceRoot.Replace('.', '/')}/{resolverName}";
            string className = $"{resolverName}Asset";

            SyntaxNode baseType = generator.TypeExpression(baseTypeSymbol);
            SyntaxNode resolverType = generator.TypeExpression(resolverTypeSymbol);
            SyntaxNode attribute = generator.Attribute(generator.TypeExpression(createAttributeTypeSymbol), new[]
            {
                generator.AssignmentStatement(generator.IdentifierName("menuName"), generator.LiteralExpression(menuName))
            });

            var addAttributeToNode = new CodeGenerateRewriterAddAttributeToNode(generator, attribute, node => node.IsKind(SyntaxKind.ClassDeclaration));

            SyntaxNode declaration = generator.NamespaceDeclaration(namespaceName, new[]
            {
                generator.ClassDeclaration(className, null, Accessibility.Public, DeclarationModifiers.None, baseType, null, new[]
                {
                    generator.MethodDeclaration("GetResolver", null, null, resolverType, Accessibility.Public, DeclarationModifiers.Override, new[]
                    {
                        generator.ReturnStatement(generator.IdentifierName($"{namespaceRoot}.Resolvers.{resolverName}.Instance"))
                    })
                })
            });

            declaration = addAttributeToNode.Visit(declaration);
            declaration = declaration.NormalizeWhitespace();
            declaration = declaration.WithTrailingTrivia(SyntaxFactory.CarriageReturnLineFeed, SyntaxFactory.CarriageReturnLineFeed);

            return declaration;
        }
    }
}
