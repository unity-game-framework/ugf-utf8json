using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using Microsoft.CodeAnalysis.Editing;
using UGF.Assemblies.Editor;
using UGF.Code.Analysis.Editor;
using UGF.Code.Generate.Editor;
using UGF.Utf8Json.Runtime;
using UnityEditor;
using Utf8Json.UniversalCodeGenerator;
using Assembly = UnityEditor.Compilation.Assembly;

namespace UGF.Utf8Json.Editor
{
    public static class Utf8JsonEditorUtility
    {
        public static void GenerateAssetFromAssembly(string path, bool import = true)
        {
            if (path == null) throw new ArgumentNullException(nameof(path));

            string sourcePath = GetPathForGeneratedScript(path);
            string source = GenerateFromAssembly(path);

            File.WriteAllText(sourcePath, source);

            if (import)
            {
                AssetDatabase.ImportAsset(sourcePath);
            }
        }

        public static string GenerateFromAssembly(string path)
        {
            if (path == null) throw new ArgumentNullException(nameof(path));

            string assemblyName = Path.GetFileNameWithoutExtension(path);

            if (!AssemblyEditorUtility.TryFindCompilationAssemblyByName(assemblyName, out Assembly assembly))
            {
                throw new ArgumentException($"Assembly not found from the specified path: '{path}'.");
            }

            var sourcePaths = new List<string>();

            for (int i = 0; i < assembly.sourceFiles.Length; i++)
            {
                string sourcePath = assembly.sourceFiles[i];

                if (IsSerializableScript(sourcePath))
                {
                    sourcePaths.Add(sourcePath);
                }
            }

            string formatters = GenerateFormatters(sourcePaths, assembly.name);

            return formatters;
        }

        public static string GenerateFormatters(List<string> sourcePaths, string namespaceRoot)
        {
            if (sourcePaths == null) throw new ArgumentNullException(nameof(sourcePaths));
            if (namespaceRoot == null) throw new ArgumentNullException(nameof(namespaceRoot));

            CSharpCompilation compilation = CodeAnalysisEditorUtility.ProjectCompilation;
            SyntaxGenerator generator = CodeAnalysisEditorUtility.Generator;

            INamedTypeSymbol attributeTypeSymbol = compilation.GetTypeByMetadataName(typeof(Utf8JsonFormatterAttribute).FullName);
            SyntaxNode attributeNode = generator.Attribute(generator.TypeExpression(attributeTypeSymbol));

            var walkerCollectUsings = new CodeGenerateWalkerCollectUsingDirectives();
            var rewriterAddAttribute = new CodeGenerateRewriterAddAttributeToNode(generator, attributeNode, declaration => declaration.IsKind(SyntaxKind.ClassDeclaration));
            var rewriterFormatAttribute = new CodeGenerateRewriterFormatAttributeList();

            for (int i = 0; i < sourcePaths.Count; i++)
            {
                walkerCollectUsings.Visit(SyntaxFactory.ParseSyntaxTree(File.ReadAllText(sourcePaths[i])).GetRoot());
            }

            string formatters = Utf8JsonUniversalCodeGeneratorUtility.GenerateFormatters(sourcePaths, namespaceRoot);
            CompilationUnitSyntax unit = SyntaxFactory.ParseCompilationUnit(formatters);

            unit = unit.AddUsings(walkerCollectUsings.UsingDirectives.Select(x => x.WithoutLeadingTrivia()).ToArray());
            unit = (CompilationUnitSyntax)rewriterAddAttribute.Visit(unit);
            unit = (CompilationUnitSyntax)rewriterFormatAttribute.Visit(unit);
            unit = CodeGenerateEditorUtility.AddGeneratedCodeLeadingTrivia(unit);

            return unit.ToFullString();
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

        public static bool IsSerializableScript(string path, CSharpCompilation compilation = null)
        {
            if (path == null) throw new ArgumentNullException(nameof(path));
            if (compilation == null) compilation = CodeAnalysisEditorUtility.ProjectCompilation;

            return CodeGenerateEditorUtility.CheckAttributeFromScript(compilation, path, typeof(Utf8JsonSerializableAttribute));
        }

        public static bool IsAssemblyHasGeneratedScript(string path)
        {
            if (path == null) throw new ArgumentNullException(nameof(path));

            return File.Exists(GetPathForGeneratedScript(path));
        }
    }
}
