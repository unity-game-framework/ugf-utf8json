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
using UnityEditor;
using UnityEngine;

namespace UGF.Utf8Json.Editor.ExternalType
{
    public static class Utf8JsonExternalTypeEditorUtility
    {
        public const string EXTERNAL_TYPE_ASSET_EXTENSION_NAME = "utf8json-external";
        public const string EXTERNAL_TYPE_ASSET_EXTENSION = "." + EXTERNAL_TYPE_ASSET_EXTENSION_NAME;

        public static string GenerateExternalSources(IReadOnlyList<string> externalPaths, ICollection<string> sourcePaths, Type attributeType = null, ICodeGenerateContainerValidation validation = null, Compilation compilation = null, SyntaxGenerator generator = null)
        {
            if (externalPaths == null) throw new ArgumentNullException(nameof(externalPaths));
            if (sourcePaths == null) throw new ArgumentNullException(nameof(sourcePaths));
            if (validation == null) validation = CodeGenerateContainerExternalEditorUtility.DefaultValidation;
            if (compilation == null) compilation = CodeAnalysisEditorUtility.ProjectCompilation;
            if (generator == null) generator = CodeAnalysisEditorUtility.Generator;

            string externalsTempPath = FileUtil.GetUniqueTempPathInProject();
            var types = new HashSet<Type>();
            CSharpSyntaxRewriter rewriterAddAttribute = null;

            if (attributeType != null && compilation.TryConstructTypeSymbol(attributeType, out ITypeSymbol typeSymbol))
            {
                rewriterAddAttribute = GetAttributeRewriter(compilation, generator, typeSymbol);
            }

            Directory.CreateDirectory(externalsTempPath);

            for (int i = 0; i < externalPaths.Count; i++)
            {
                string externalPath = externalPaths[i];

                if (CodeGenerateContainerExternalEditorUtility.TryGetInfoFromAssetPath(externalPath, out Utf8JsonExternalTypeAssetInfo info) && info.TryGetTargetType(out Type type))
                {
                    if (types.Add(type))
                    {
                        SyntaxNode unit = CodeGenerateContainerExternalEditorUtility.CreateUnit(info, validation, compilation, generator);

                        if (rewriterAddAttribute != null)
                        {
                            unit = rewriterAddAttribute.Visit(unit);
                        }

                        string sourcePath = $"{externalsTempPath}/{Guid.NewGuid():N}.cs";
                        string source = unit.NormalizeWhitespace().ToFullString();

                        File.WriteAllText(sourcePath, source);

                        sourcePaths.Add(sourcePath);
                    }
                    else
                    {
                        Debug.LogWarning($"The specified type already generated: '{type}'.");
                    }
                }
            }

            return externalsTempPath;
        }

        private static CSharpSyntaxRewriter GetAttributeRewriter(Compilation compilation, SyntaxGenerator generator, ITypeSymbol attributeTypeSymbol)
        {
            if (compilation == null) throw new ArgumentNullException(nameof(compilation));
            if (generator == null) throw new ArgumentNullException(nameof(generator));
            if (attributeTypeSymbol == null) throw new ArgumentNullException(nameof(attributeTypeSymbol));

            SyntaxNode attribute = generator.Attribute(generator.TypeExpression(attributeTypeSymbol));

            return new CodeGenerateRewriterAddAttributeToNode(generator, attribute, declaration =>
            {
                SyntaxKind kind = declaration.Kind();

                return kind == SyntaxKind.ClassDeclaration || kind == SyntaxKind.StructDeclaration;
            });
        }
    }
}
