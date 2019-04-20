using System;
using System.Collections.Generic;
using System.IO;
using System.Reflection;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.Editing;
using UGF.Assemblies.Runtime;
using UGF.Code.Analysis.Editor;
using UGF.Code.Generate.Editor;
using UGF.Code.Generate.Editor.Container;
using UGF.Types.Runtime;
using UGF.Utf8Json.Runtime;
using UGF.Utf8Json.Runtime.ExternalType;
using UnityEditor;
using Object = UnityEngine.Object;

namespace UGF.Utf8Json.Editor.ExternalType
{
    public static class Utf8JsonExternalTypeEditorUtility
    {
        public static string GenerateExternalContainers(ICollection<string> paths, CSharpCompilation compilation, SyntaxGenerator generator, Assembly assembly = null)
        {
            if (paths == null) throw new ArgumentNullException(nameof(paths));
            if (compilation == null) throw new ArgumentNullException(nameof(compilation));
            if (generator == null) throw new ArgumentNullException(nameof(generator));

            string path = FileUtil.GetUniqueTempPathInProject();

            Directory.CreateDirectory(path);

            foreach (Type type in AssemblyUtility.GetBrowsableTypes<Utf8JsonExternalTypeDefineAttribute>(assembly))
            {
                if (TypesUtility.TryCreateType(type, out IUtf8JsonExternalTypeDefine define))
                {
                    GenerateExternalContainers(path, define.Types, paths, compilation, generator);
                }
            }

            return path;
        }

        public static void GenerateExternalContainers(string path, IReadOnlyList<Type> types, ICollection<string> paths, CSharpCompilation compilation, SyntaxGenerator generator)
        {
            if (string.IsNullOrEmpty(path)) throw new ArgumentNullException(nameof(path));
            if (types == null) throw new ArgumentNullException(nameof(types));
            if (paths == null) throw new ArgumentNullException(nameof(paths));
            if (compilation == null) throw new ArgumentNullException(nameof(compilation));
            if (generator == null) throw new ArgumentNullException(nameof(generator));

            for (int i = 0; i < types.Count; i++)
            {
                Type type = types[i];

                if (IsValidExternalType(type))
                {
                    SyntaxNode unit = CodeGenerateContainerEditorUtility.CreateUnit(compilation, generator, type);

                    string sourcePath = $"{path}/{Guid.NewGuid()}.cs";
                    string source = unit.NormalizeWhitespace().ToFullString();

                    File.WriteAllText(sourcePath, source);

                    paths.Add(sourcePath);
                }
            }
        }

        public static bool IsValidExternalType(Type type)
        {
            if (type == null) throw new ArgumentNullException(nameof(type));

            bool isContainer = CodeGenerateContainerEditorUtility.IsValidType(type);
            bool hasDefaultConstructor = type.GetConstructor(Type.EmptyTypes) != null || type.IsValueType;
            bool notAttribute = !typeof(Attribute).IsAssignableFrom(type);
            bool notUnity = !typeof(Object).IsAssignableFrom(type);
            bool notObsolete = !type.IsDefined(typeof(ObsoleteAttribute));
            bool hasMembers = type.GetFields().Length > 0 || type.GetProperties().Length > 0;
            bool isSpecial = type.IsSpecialName;

            return isContainer && hasDefaultConstructor && notAttribute && notUnity && notObsolete && hasMembers && !isSpecial;
        }

        public static bool IsExternalTypeDefineScript(string path)
        {
            if (path == null) throw new ArgumentNullException(nameof(path));

            CSharpCompilation compilation = CodeAnalysisEditorUtility.ProjectCompilation;

            SyntaxTree tree = SyntaxFactory.ParseSyntaxTree(File.ReadAllText(path));
            SemanticModel model = compilation.AddSyntaxTrees(tree).GetSemanticModel(tree);
            ITypeSymbol typeSymbol = compilation.GetTypeByMetadataName(typeof(Utf8JsonExternalTypeDefineAttribute).FullName);

            var walker = new CodeGenerateWalkerCheckAttribute(model, typeSymbol);

            walker.Visit(tree.GetRoot());

            return walker.Result;
        }
    }
}
