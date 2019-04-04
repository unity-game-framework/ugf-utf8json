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

            List<string> sourcePaths = GetSerializableScriptPathsFromAssembly(assembly);

            return GenerateFormatters(sourcePaths, assembly.name);
        }

        public static string GenerateFormatters(List<string> sourcePaths, string namespaceRoot)
        {
            if (sourcePaths == null) throw new ArgumentNullException(nameof(sourcePaths));
            if (namespaceRoot == null) throw new ArgumentNullException(nameof(namespaceRoot));

            CSharpCompilation compilation = CodeAnalysisEditorUtility.ProjectCompilation;
            SyntaxGenerator generator = CodeAnalysisEditorUtility.Generator;
            SyntaxNode formatterAttribute = generator.Attribute(compilation, typeof(Utf8JsonFormatterAttribute));

            var rewriterAddAttribute = new CodeGenerateRewriterAddAttributeToKind(generator, formatterAttribute, SyntaxKind.ClassDeclaration);

            HashSet<string> usings = UsingDirectivesCollectUniqueNames(sourcePaths);
            string formatters = Utf8JsonUniversalCodeGeneratorUtility.GenerateFormatters(sourcePaths, namespaceRoot);
            CompilationUnitSyntax unit = SyntaxFactory.ParseCompilationUnit(formatters);

            unit = unit.AddUsings(usings.Select(x => SyntaxFactory.UsingDirective(SyntaxFactory.ParseName(x))
                .NormalizeWhitespace()
                .WithTrailingTrivia(SyntaxFactory.CarriageReturnLineFeed)).ToArray());

            unit = (CompilationUnitSyntax)rewriterAddAttribute.Visit(unit);
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

        public static bool IsSerializableScript(string path)
        {
            if (path == null) throw new ArgumentNullException(nameof(path));

            CSharpCompilation compilation = CodeAnalysisEditorUtility.ProjectCompilation;
            SyntaxTree tree = SyntaxFactory.ParseSyntaxTree(File.ReadAllText(path));
            SemanticModel model = compilation.AddSyntaxTrees(tree).GetSemanticModel(tree);
            ITypeSymbol typeSymbol = compilation.GetTypeByMetadataName(typeof(Utf8JsonSerializableAttribute).FullName);

            var walker = new CodeGenerateWalkerCheckAttribute(model, typeSymbol);

            walker.Visit(tree.GetRoot());

            return walker.Result;
        }

        public static bool IsAssemblyHasGeneratedScript(string path)
        {
            if (path == null) throw new ArgumentNullException(nameof(path));

            return File.Exists(GetPathForGeneratedScript(path));
        }

        private static List<string> GetSerializableScriptPathsFromAssembly(Assembly assembly)
        {
            if (assembly == null) throw new ArgumentNullException(nameof(assembly));

            var paths = new List<string>();
            string[] sourceFiles = assembly.sourceFiles;

            for (int i = 0; i < sourceFiles.Length; i++)
            {
                string path = sourceFiles[i];

                if (IsSerializableScript(path))
                {
                    paths.Add(path);
                }
            }

            return paths;
        }

        private static HashSet<string> UsingDirectivesCollectUniqueNames(List<string> sourcePaths)
        {
            var walker = new CodeGenerateWalkerCollectUsingDirectives();

            for (int i = 0; i < sourcePaths.Count; i++)
            {
                string path = sourcePaths[i];
                SyntaxTree tree = SyntaxFactory.ParseSyntaxTree(File.ReadAllText(path));

                walker.Visit(tree.GetRoot());
            }

            return CodeGenerateEditorUtility.UsingDirectivesCollectUniqueNames(walker.UsingDirectives);
        }
    }
}
