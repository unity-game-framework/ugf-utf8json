using System;
using System.Collections.Generic;
using System.IO;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using Microsoft.CodeAnalysis.Editing;
using UGF.Assemblies.Editor;
using UGF.Code.Analysis.Editor;
using UGF.Code.Generate.Editor;
using UGF.Code.Generate.Editor.Container;
using UGF.Code.Generate.Editor.Container.External;
using UGF.Utf8Json.Editor.ExternalType;
using UnityEditor;
using UnityEditor.Compilation;
using UnityEngine;
using Utf8Json.UniversalCodeGenerator;
using Assembly = UnityEditor.Compilation.Assembly;

namespace UGF.Utf8Json.Editor
{
    /// <summary>
    /// Provides utilities to work with Utf8 Json serialization in editor.
    /// </summary>
    public static class Utf8JsonEditorUtility
    {
        /// <summary>
        /// Generates assets with the generated code for all assemblies which have generated file.
        /// </summary>
        /// <param name="import">The value determines whether to force asset database import.</param>
        /// <param name="validation">The container type validation used to generate externals.</param>
        /// <param name="compilation">The project compilation used during generation.</param>
        /// <param name="generator">The syntax generator used during generation.</param>
        public static void GenerateAssetFromAssemblyAll(bool import = true, ICodeGenerateContainerValidation validation = null, Compilation compilation = null, SyntaxGenerator generator = null)
        {
            Assembly[] assemblies = CompilationPipeline.GetAssemblies();

            for (int i = 0; i < assemblies.Length; i++)
            {
                Assembly assembly = assemblies[i];
                string assemblyPath = CompilationPipeline.GetAssemblyDefinitionFilePathFromAssemblyName(assembly.name);

                if (!string.IsNullOrEmpty(assemblyPath))
                {
                    string path = CodeGenerateEditorUtility.GetPathForGeneratedScript(assemblyPath, "Utf8Json");

                    if (File.Exists(path))
                    {
                        GenerateAssetFromAssembly(assemblyPath, import, validation, compilation, generator);
                    }
                }
            }
        }

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

            string sourcePath = CodeGenerateEditorUtility.GetPathForGeneratedScript(path, "Utf8Json");
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
            var sourcePaths = new List<string>();

            if (AssemblyEditorUtility.TryFindCompilationAssemblyByName(assemblyName, out Assembly assembly))
            {
                for (int i = 0; i < assembly.sourceFiles.Length; i++)
                {
                    string sourcePath = assembly.sourceFiles[i];

                    if (CodeGenerateEditorUtility.CheckAttributeFromScript(compilation, sourcePath, typeof(SerializableAttribute)))
                    {
                        sourcePaths.Add(sourcePath);
                    }
                }
            }

            string externalsTempPath = InternalGenerateExternals(sourcePaths, path, validation, compilation, generator);

            string resolverName = GetResolverNameFromAssemblyName(assemblyName);
            string resolver = GenerateResolver(sourcePaths, resolverName, assemblyName);

            if (!string.IsNullOrEmpty(externalsTempPath))
            {
                FileUtil.DeleteFileOrDirectory(externalsTempPath);
            }

            return resolver;
        }

        /// <summary>
        /// Generates source of the resolver from the specified path of the sources.
        /// </summary>
        /// <param name="sourcePaths">The collection of the source paths.</param>
        /// <param name="resolverName">The name of the generated resolver.</param>
        /// <param name="namespaceRoot">The namespace root of the generated formatters.</param>
        public static string GenerateResolver(IReadOnlyList<string> sourcePaths, string resolverName, string namespaceRoot)
        {
            if (sourcePaths == null) throw new ArgumentNullException(nameof(sourcePaths));
            if (resolverName == null) throw new ArgumentNullException(nameof(resolverName));
            if (namespaceRoot == null) throw new ArgumentNullException(nameof(namespaceRoot));

            var arguments = new Utf8JsonGenerateArguments
            {
                IgnoreReadOnly = true,
                IsTypeRequireAttribute = true,
                TypeRequiredAttributeShortName = "Serializable"
            };

            string resolver = Utf8JsonUniversalCodeGeneratorUtility.Generate(sourcePaths, resolverName, namespaceRoot, arguments);
            CompilationUnitSyntax unit = SyntaxFactory.ParseCompilationUnit(resolver);

            unit = CodeGenerateEditorUtility.AddGeneratedCodeLeadingTrivia(unit);

            return unit.ToFullString();
        }

        public static string GetResolverNameFromAssemblyName(string assemblyName)
        {
            if (assemblyName == null) throw new ArgumentNullException(nameof(assemblyName));

            return $"{assemblyName.Replace(" ", string.Empty).Replace(".", string.Empty)}Resolver";
        }

        private static string InternalGenerateExternals(ICollection<string> sourcePaths, string path, ICodeGenerateContainerValidation validation, Compilation compilation, SyntaxGenerator generator)
        {
            var externals = new List<string>();
            var types = new HashSet<Type>();
            string externalsTempPath = string.Empty;

            AssemblyEditorUtility.GetAssetPathsUnderAssemblyDefinitionFile(externals, path, Utf8JsonExternalTypeEditorUtility.ExternalTypeAssetExtensionName);

            if (externals.Count > 0)
            {
                INamedTypeSymbol attributeTypeSymbol = compilation.GetTypeByMetadataName(typeof(SerializableAttribute).FullName);
                SyntaxNode attribute = generator.Attribute(generator.TypeExpression(attributeTypeSymbol));

                var rewriterAddAttribute = new CodeGenerateRewriterAddAttributeToNode(generator, attribute, declaration =>
                {
                    SyntaxKind kind = declaration.Kind();

                    return kind == SyntaxKind.ClassDeclaration || kind == SyntaxKind.StructDeclaration;
                });

                externalsTempPath = FileUtil.GetUniqueTempPathInProject();

                Directory.CreateDirectory(externalsTempPath);

                for (int i = 0; i < externals.Count; i++)
                {
                    string externalPath = externals[i];

                    if (CodeGenerateContainerExternalEditorUtility.TryGetInfoFromAssetPath(externalPath, out Utf8JsonExternalTypeAssetInfo info) && info.TryGetTargetType(out Type type))
                    {
                        if (types.Add(type))
                        {
                            SyntaxNode unit = CodeGenerateContainerExternalEditorUtility.CreateUnit(info, validation, compilation, generator);

                            unit = rewriterAddAttribute.Visit(unit);

                            string sourcePath = $"{externalsTempPath}/{Guid.NewGuid():N}.cs";
                            string source = unit.NormalizeWhitespace().ToFullString();

                            File.WriteAllText(sourcePath, source);

                            sourcePaths.Add(sourcePath);
                        }
                        else
                        {
                            Debug.LogWarning($"The specified type already included for assembly: '{type}', assembly:'{path}'.");
                        }
                    }
                }
            }

            return externalsTempPath;
        }
    }
}
