using System;
using System.Collections.Generic;
using System.IO;
using System.Reflection;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.Editing;
using UGF.Code.Analysis.Editor;
using UGF.Code.Generate.Editor.Container;
using UnityEditor;
using UnityEngine;
using Object = UnityEngine.Object;

namespace UGF.Utf8Json.Editor.ExternalType
{
    public static class Utf8JsonExternalTypeEditorUtility
    {
        public static string GenerateExternalContainers(IReadOnlyList<string> externals, ICollection<string> paths, SyntaxGenerator generator = null)
        {
            if (generator == null)
            {
                generator = CodeAnalysisEditorUtility.Generator;
            }

            string temp = FileUtil.GetUniqueTempPathInProject();

            for (int i = 0; i < externals.Count; i++)
            {
                string path = externals[i];
                var info = JsonUtility.FromJson<Utf8JsonExternalTypeAssetInfo>(File.ReadAllText(path));

                if (IsValidExternalTypeInfo(info))
                {
                    CodeGenerateContainer container = CreateContainer(info);

                    string sourcePath = $"{temp}/{container.Name}.cs";
                    string source = container.Generate(generator).NormalizeWhitespace().ToFullString();

                    File.WriteAllText(sourcePath, source);

                    paths.Add(sourcePath);
                }
            }

            return temp;
        }

        public static CodeGenerateContainer CreateContainer(Type type)
        {
            CSharpCompilation compilation = CodeAnalysisEditorUtility.ProjectCompilation;
            CodeGenerateContainer container = CodeGenerateContainerEditorUtility.Create(compilation, type);

            return container;
        }

        public static CodeGenerateContainer CreateContainer(Utf8JsonExternalTypeAssetInfo info)
        {
            Type type = Type.GetType(info.Type, true);
            CSharpCompilation compilation = CodeAnalysisEditorUtility.ProjectCompilation;
            CodeGenerateContainer container = CodeGenerateContainerEditorUtility.Create(compilation, type);

            var result = new CodeGenerateContainer(type.Name, type.IsValueType);

            for (int i = 0; i < container.Fields.Count; i++)
            {
                CodeGenerateContainerField field = container.Fields[i];

                if (IsInfoContainsMember(info, field.Name))
                {
                    result.Fields.Add(field);
                }
            }

            return result;
        }

        public static bool IsValidExternalTypeInfo(Utf8JsonExternalTypeAssetInfo info)
        {
            return Type.GetType(info.Type) != null;
        }

        public static bool IsValidExternalType(Type type)
        {
            bool isContainer = CodeGenerateContainerEditorUtility.IsValidType(type);
            bool hasDefaultConstructor = type.GetConstructor(Type.EmptyTypes) != null || type.IsValueType;
            bool notAttribute = !typeof(Attribute).IsAssignableFrom(type);
            bool notUnity = !typeof(Object).IsAssignableFrom(type);
            bool notObsolete = !type.IsDefined(typeof(ObsoleteAttribute));
            bool hasMembers = type.GetFields().Length > 0 || type.GetProperties().Length > 0;

            return isContainer && hasDefaultConstructor && notAttribute && notUnity && notObsolete && hasMembers;
        }

        private static bool IsInfoContainsMember(Utf8JsonExternalTypeAssetInfo info, string name)
        {
            for (int i = 0; i < info.Members.Count; i++)
            {
                if (info.Members[i].Name == name)
                {
                    return true;
                }
            }

            return false;
        }
    }
}
