using System;
using System.Collections.Generic;
using System.IO;
using System.Reflection;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.Editing;
using UGF.Code.Generate.Editor.Container;
using UnityEditor;
using Object = UnityEngine.Object;

namespace UGF.Utf8Json.Editor.ExternalType
{
    public static class Utf8JsonExternalTypeEditorUtility
    {
        public static string GenerateExternalContainers(IReadOnlyList<Type> types, ICollection<string> paths, CSharpCompilation compilation, SyntaxGenerator generator)
        {
            string temp = FileUtil.GetUniqueTempPathInProject();

            Directory.CreateDirectory(temp);

            for (int i = 0; i < types.Count; i++)
            {
                Type type = types[i];

                if (IsValidExternalType(type))
                {
                    SyntaxNode unit = CodeGenerateContainerEditorUtility.CreateUnit(compilation, generator, type);

                    string sourcePath = $"{temp}/{Guid.NewGuid()}.cs";
                    string source = unit.NormalizeWhitespace().ToFullString();

                    File.WriteAllText(sourcePath, source);

                    paths.Add(sourcePath);
                }
            }

            return temp;
        }

        public static bool IsValidExternalType(Type type)
        {
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
