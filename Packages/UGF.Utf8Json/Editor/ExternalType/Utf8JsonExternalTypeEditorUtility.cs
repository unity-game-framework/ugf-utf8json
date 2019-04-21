using System;
using System.Collections.Generic;
using System.IO;
using System.Reflection;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.Editing;
using UGF.Code.Analysis.Editor;
using UGF.Code.Generate.Editor.Container;
using Object = UnityEngine.Object;

namespace UGF.Utf8Json.Editor.ExternalType
{
    public static class Utf8JsonExternalTypeEditorUtility
    {
        public static void GenerateExternalContainers(string path, IReadOnlyList<Type> types, ICollection<string> paths, CSharpCompilation compilation = null, SyntaxGenerator generator = null)
        {
            if (string.IsNullOrEmpty(path)) throw new ArgumentNullException(nameof(path));
            if (types == null) throw new ArgumentNullException(nameof(types));
            if (paths == null) throw new ArgumentNullException(nameof(paths));
            if (compilation == null) compilation = CodeAnalysisEditorUtility.ProjectCompilation;
            if (generator == null) generator = CodeAnalysisEditorUtility.Generator;

            for (int i = 0; i < types.Count; i++)
            {
                Type type = types[i];

                if (IsValidExternalType(type))
                {
                    SyntaxNode unit = CodeGenerateContainerEditorUtility.CreateUnit(compilation, generator, type);

                    string sourcePath = $"{path}/{Guid.NewGuid():N}.cs";
                    string source = unit.NormalizeWhitespace().ToFullString();

                    File.WriteAllText(sourcePath, source);

                    paths.Add(sourcePath);
                }
            }
        }

        public static CodeGenerateContainer CreateContainer(Type type, CSharpCompilation compilation = null)
        {
            if (type == null) throw new ArgumentNullException(nameof(type));
            if (compilation == null) compilation = CodeAnalysisEditorUtility.ProjectCompilation;

            return CodeGenerateContainerEditorUtility.Create(compilation, type);
        }

        public static CodeGenerateContainer CreateContainer(Utf8JsonExternalTypeAssetInfo info, CSharpCompilation compilation = null)
        {
            if (info.IsValid()) throw new ArgumentException("The specified info not valid.", nameof(info));
            if (compilation == null) compilation = CodeAnalysisEditorUtility.ProjectCompilation;

            Type type = Type.GetType(info.Type, true);
            CodeGenerateContainer container = CodeGenerateContainerEditorUtility.Create(compilation, type);

            var result = new CodeGenerateContainer(type.Name, type.IsValueType);

            for (int i = 0; i < container.Fields.Count; i++)
            {
                CodeGenerateContainerField field = container.Fields[i];

                if (info.Contains(field.Name))
                {
                    result.Fields.Add(field);
                }
            }

            return result;
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
    }
}
