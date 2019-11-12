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

            Utf8JsonResolverAssetInfo info = LoadResolverInfo(assetPath);
            string source = GenerateResolver(info, validation, compilation, generator);
            string path = GetDestinationSourcePath(assetPath, info.ResolverName, info.DestinationSource);

            File.WriteAllText(path, source);
            AssetDatabase.ImportAsset(path);

            if (string.IsNullOrEmpty(info.DestinationSource))
            {
                info.DestinationSource = AssetDatabase.AssetPathToGUID(path);

                SaveResolverInfo(assetPath, info);
            }
        }

        public static string GenerateResolver(Utf8JsonResolverAssetInfo info, ICodeGenerateContainerValidation validation = null, Compilation compilation = null, SyntaxGenerator generator = null)
        {
            if (info == null) throw new ArgumentNullException(nameof(info));
            if (validation == null) validation = CodeGenerateContainerExternalEditorUtility.DefaultValidation;
            if (compilation == null) compilation = CodeAnalysisEditorUtility.ProjectCompilation;
            if (generator == null) generator = CodeAnalysisEditorUtility.Generator;

            var sourcePaths = new List<string>();
            string resolverName = Utf8JsonEditorUtility.FormatResolverName(info.ResolverName);
            string namespaceRoot = info.NamespaceRoot;
            string externalsTemp = string.Empty;
            Type attributeType = null;

            var generateArguments = new Utf8JsonGenerateArguments
            {
                IgnoreReadOnly = info.IgnoreReadOnly,
                IsTypeRequireAttribute = info.AttributeRequired
            };

            if (info.AttributeRequired && info.TryGetAttributeType(out attributeType))
            {
                generateArguments.TypeRequiredAttributeShortName = attributeType.Name;
            }

            if (info.Sources.Count > 0)
            {
                var externals = new List<string>();

                for (int i = 0; i < info.Sources.Count; i++)
                {
                    string guid = info.Sources[i];
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

            if (info.ResolverAsset)
            {
                source = AppendResolverAsset(source, resolverName, namespaceRoot, compilation, generator);
            }

            return source;
        }

        public static void ClearResolver(string assetPath)
        {
            if (string.IsNullOrEmpty(assetPath)) throw new ArgumentException("Value cannot be null or empty.", nameof(assetPath));

            Utf8JsonResolverAssetInfo info = LoadResolverInfo(assetPath);
            string path = GetDestinationSourcePath(assetPath, info.ResolverName, info.DestinationSource);

            if (!string.IsNullOrEmpty(path))
            {
                AssetDatabase.MoveAssetToTrash(path);
            }
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

        public static Utf8JsonResolverAssetInfo LoadResolverInfo(string assetPath)
        {
            if (string.IsNullOrEmpty(assetPath)) throw new ArgumentException("Value cannot be null or empty.", nameof(assetPath));

            string source = File.ReadAllText(assetPath);
            var data = new Utf8JsonResolverAssetInfo();

            JsonUtility.FromJsonOverwrite(source, data);

            return data;
        }

        public static void SaveResolverInfo(string assetPath, Utf8JsonResolverAssetInfo info, bool importAsset = true)
        {
            if (string.IsNullOrEmpty(assetPath)) throw new ArgumentException("Value cannot be null or empty.", nameof(assetPath));
            if (info == null) throw new ArgumentNullException(nameof(info));

            string source = EditorJsonUtility.ToJson(info, true);

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
