using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.Editing;
using UGF.Assemblies.Editor;
using UGF.Code.Analysis.Editor;
using UGF.Code.Generate.Editor;
using UGF.Code.Generate.Editor.Container;
using UGF.Code.Generate.Editor.Container.External;
using UGF.Utf8Json.Editor.ExternalType;
using UGF.Utf8Json.Runtime;
using UnityEditor;
// using Utf8Json.UniversalCodeGenerator;
using Assembly = UnityEditor.Compilation.Assembly;

namespace UGF.Utf8Json.Editor
{
    /// <summary>
    /// Provides utilities to work with Utf8 Json serialization in editor.
    /// </summary>
    public static class Utf8JsonEditorUtility
    {
        /// <summary>
        /// Generates asset with the generated code for assembly from the specified path.
        /// </summary>
        /// <param name="path">The path of the assembly definition file.</param>
        /// <param name="import">The value determines whether to force asset database import.</param>
        /// <param name="validation">The container type validation used to generate externals.</param>
        /// <param name="compilation">The project compilation used during generation.</param>
        /// <param name="generator">The syntax generator used during generation.</param>
        public static void GenerateAssetFromAssembly(string path, bool import = true, ICodeGenerateContainerValidation validation = null, Compilation compilation = null, SyntaxGenerator generator = null)
        {
            if (path == null) throw new ArgumentNullException(nameof(path));

            string sourcePath = GetPathForGeneratedScript(path);
            string source = GenerateFromAssembly(path, validation, compilation, generator);

            File.WriteAllText(sourcePath, source);

            if (import)
            {
                AssetDatabase.ImportAsset(sourcePath);
            }
        }

        /// <summary>
        /// Generates source of the generated code for assembly from the specified path.
        /// </summary>
        /// <param name="path">The path of the assembly definition file.</param>
        /// <param name="validation">The container type validation used to generate externals.</param>
        /// <param name="compilation">The project compilation used during generation.</param>
        /// <param name="generator">The syntax generator used during generation.</param>
        public static string GenerateFromAssembly(string path, ICodeGenerateContainerValidation validation = null, Compilation compilation = null, SyntaxGenerator generator = null)
        {
            if (path == null) throw new ArgumentNullException(nameof(path));
            if (validation == null) validation = CodeGenerateContainerExternalEditorUtility.DefaultValidation;
            if (compilation == null) compilation = CodeAnalysisEditorUtility.ProjectCompilation;
            if (generator == null) generator = CodeAnalysisEditorUtility.Generator;

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

            var externals = new List<string>();
            string externalsTempPath = string.Empty;

            AssemblyEditorUtility.GetAssetPathsUnderAssemblyDefinitionFile(externals, path, Utf8JsonExternalTypeEditorUtility.ExternalTypeAssetExtension);

            if (externals.Count > 0)
            {
                externalsTempPath = FileUtil.GetUniqueTempPathInProject();

                Directory.CreateDirectory(externalsTempPath);

                for (int i = 0; i < externals.Count; i++)
                {
                    string externalPath = externals[i];

                    if (CodeGenerateContainerExternalEditorUtility.TryGetInfoFromAssetPath(externalPath, out Utf8JsonExternalTypeAssetInfo info) && info.TryGetTargetType(out _))
                    {
                        SyntaxNode unit = CodeGenerateContainerExternalEditorUtility.CreateUnit(info, validation, compilation, generator);

                        string sourcePath = $"{externalsTempPath}/{Guid.NewGuid():N}.cs";
                        string source = unit.NormalizeWhitespace().ToFullString();

                        File.WriteAllText(sourcePath, source);

                        sourcePaths.Add(sourcePath);
                    }
                }
            }

            string formatters = GenerateFormatters(sourcePaths, assembly.name, compilation, generator);

            if (!string.IsNullOrEmpty(externalsTempPath))
            {
                FileUtil.DeleteFileOrDirectory(externalsTempPath);
            }

            return formatters;
        }

        /// <summary>
        /// Generates source of the formatters from the specified path of the sources.
        /// </summary>
        /// <param name="sourcePaths">The collection of the source paths.</param>
        /// <param name="namespaceRoot">The namespace root of the generated formatters.</param>
        /// <param name="compilation">The project compilation used during generation.</param>
        /// <param name="generator">The syntax generator used during generation.</param>
        public static string GenerateFormatters(IReadOnlyList<string> sourcePaths, string namespaceRoot, Compilation compilation = null, SyntaxGenerator generator = null)
        {
            if (sourcePaths == null) throw new ArgumentNullException(nameof(sourcePaths));
            if (namespaceRoot == null) throw new ArgumentNullException(nameof(namespaceRoot));
            if (compilation == null) compilation = CodeAnalysisEditorUtility.ProjectCompilation;
            if (generator == null) generator = CodeAnalysisEditorUtility.Generator;

            // var arguments = new Utf8JsonGenerateArguments
            // {
            //     IgnoreReadOnly = true,
            //     IsTypeRequireAttribute = true,
            //     TypeRequiredAttributeShortName = "Utf8JsonSerializable"
            // };
            //
            // INamedTypeSymbol attributeTypeSymbol = compilation.GetTypeByMetadataName(typeof(Utf8JsonFormatterAttribute).FullName);
            // var attributeType = (TypeSyntax)generator.TypeExpression(attributeTypeSymbol);
            //
            // var walkerCollectUsings = new CodeGenerateWalkerCollectUsingDirectives();
            // var rewriterAddAttribute = new Utf8JsonRewriterAddFormatterAttribute(generator, attributeType);
            // var rewriterFormatAttribute = new CodeGenerateRewriterFormatAttributeList();
            //
            // for (int i = 0; i < sourcePaths.Count; i++)
            // {
            //     walkerCollectUsings.Visit(SyntaxFactory.ParseSyntaxTree(File.ReadAllText(sourcePaths[i])).GetRoot());
            // }
            //
            // string formatters = Utf8JsonUniversalCodeGeneratorUtility.GenerateFormatters(sourcePaths, namespaceRoot, arguments);
            // CompilationUnitSyntax unit = SyntaxFactory.ParseCompilationUnit(formatters);
            //
            // unit = unit.AddUsings(walkerCollectUsings.UsingDirectives.Select(x => x.WithoutLeadingTrivia()).ToArray());
            // unit = (CompilationUnitSyntax)rewriterAddAttribute.Visit(unit);
            // unit = (CompilationUnitSyntax)rewriterFormatAttribute.Visit(unit);
            // unit = CodeGenerateEditorUtility.AddGeneratedCodeLeadingTrivia(unit);
            //
            // return unit.ToFullString();

            return null;
        }

        /// <summary>
        /// Gets path for generated source from the specified path.
        /// </summary>
        /// <param name="path">The path used to generated.</param>
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

        /// <summary>
        /// Determines whether source from the specified path contains any declaration with the <see cref="Utf8JsonSerializableAttribute"/> attribute.
        /// </summary>
        /// <param name="path">The path of the source.</param>
        /// <param name="compilation">The project compilation used during generation.</param>
        public static bool IsSerializableScript(string path, CSharpCompilation compilation = null)
        {
            if (path == null) throw new ArgumentNullException(nameof(path));
            if (compilation == null) compilation = CodeAnalysisEditorUtility.ProjectCompilation;

            return CodeGenerateEditorUtility.CheckAttributeFromScript(compilation, path, typeof(Utf8JsonSerializableAttribute));
        }
    }
}
