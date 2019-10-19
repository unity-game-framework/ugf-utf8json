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
    /// <summary>
    /// Provides utilities to work with external types in editor.
    /// </summary>
    public static class Utf8JsonExternalTypeEditorUtility
    {
        /// <summary>
        /// Gets the extension name of the external type info file. ("utf8json-external")
        /// </summary>
        public static string ExternalTypeAssetExtensionName { get; } = "utf8json-external";

        public static string GenerateExternalSources(IReadOnlyList<string> externalPaths, ICollection<string> sourcePaths, string attributeTypeName = null, ICodeGenerateContainerValidation validation = null, Compilation compilation = null, SyntaxGenerator generator = null)
        {
            if (externalPaths == null) throw new ArgumentNullException(nameof(externalPaths));
            if (sourcePaths == null) throw new ArgumentNullException(nameof(sourcePaths));
            if (validation == null) validation = CodeGenerateContainerExternalEditorUtility.DefaultValidation;
            if (compilation == null) compilation = CodeAnalysisEditorUtility.ProjectCompilation;
            if (generator == null) generator = CodeAnalysisEditorUtility.Generator;

            string externalsTempPath = FileUtil.GetUniqueTempPathInProject();
            var types = new HashSet<Type>();
            CSharpSyntaxRewriter rewriterAddAttribute = null;

            if (!string.IsNullOrEmpty(attributeTypeName))
            {
                rewriterAddAttribute = GetAttributeRewriter(compilation, generator, attributeTypeName);
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

        private static CSharpSyntaxRewriter GetAttributeRewriter(Compilation compilation, SyntaxGenerator generator, string attributeTypeName)
        {
            INamedTypeSymbol attributeTypeSymbol = compilation.GetTypeByMetadataName(attributeTypeName);
            SyntaxNode attribute = generator.Attribute(generator.TypeExpression(attributeTypeSymbol));

            return new CodeGenerateRewriterAddAttributeToNode(generator, attribute, declaration =>
            {
                SyntaxKind kind = declaration.Kind();

                return kind == SyntaxKind.ClassDeclaration || kind == SyntaxKind.StructDeclaration;
            });
        }
    }
}
