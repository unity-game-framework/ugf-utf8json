using System;
using System.Collections.Generic;
using System.IO;
using System.Reflection;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.Editing;
using UGF.Code.Analysis.Editor;
using UGF.Code.Generate.Editor.Container;
using UnityEngine;
using Object = UnityEngine.Object;

namespace UGF.Utf8Json.Editor.ExternalType
{
    public static class Utf8JsonExternalTypeEditorUtility
    {
        public static string ExternalTypeAssetExtension { get; } = ".utf8json-external";

        public static void GenerateExternalContainers(string path, IReadOnlyList<string> externals, ICollection<string> paths, CSharpCompilation compilation = null, SyntaxGenerator generator = null)
        {
            if (string.IsNullOrEmpty(path)) throw new ArgumentNullException(nameof(path));
            if (externals == null) throw new ArgumentNullException(nameof(externals));
            if (paths == null) throw new ArgumentNullException(nameof(paths));
            if (compilation == null) compilation = CodeAnalysisEditorUtility.ProjectCompilation;
            if (generator == null) generator = CodeAnalysisEditorUtility.Generator;

            for (int i = 0; i < externals.Count; i++)
            {
                string externalPath = externals[i];
                Utf8JsonExternalTypeAssetInfo info = GetExternalTypeAssetInfoFromPath(externalPath);

                if (info.IsValid())
                {
                    Type type = info.GetTargetType();

                    if (IsValidExternalType(type))
                    {
                        CodeGenerateContainer container = CreateContainer(info, compilation);
                        SyntaxNode unit = CodeGenerateContainerEditorUtility.CreateUnit(generator, container, type.Namespace);

                        string sourcePath = $"{path}/{Guid.NewGuid():N}.cs";
                        string source = unit.NormalizeWhitespace().ToFullString();

                        File.WriteAllText(sourcePath, source);

                        paths.Add(sourcePath);
                    }
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
            if (!info.IsValid()) throw new ArgumentException("The specified info not valid.", nameof(info));
            if (compilation == null) compilation = CodeAnalysisEditorUtility.ProjectCompilation;

            Type type = Type.GetType(info.Type, true);
            CodeGenerateContainer container = CodeGenerateContainerEditorUtility.Create(compilation, type);

            var result = new CodeGenerateContainer(type.Name, type.IsValueType);

            for (int i = 0; i < container.Fields.Count; i++)
            {
                CodeGenerateContainerField field = container.Fields[i];

                if (info.TryGetMember(field.Name, out Utf8JsonExternalTypeAssetInfo.MemberInfo member) && member.Active)
                {
                    result.Fields.Add(field);
                }
            }

            return result;
        }

        public static Utf8JsonExternalTypeAssetInfo GetExternalTypeAssetInfoFromPath(string path)
        {
            if (path == null) throw new ArgumentNullException(nameof(path));

            string source = File.ReadAllText(path);

            return JsonUtility.FromJson<Utf8JsonExternalTypeAssetInfo>(source);
        }

        public static bool IsValidExternalType(Type type)
        {
            if (type == null) throw new ArgumentNullException(nameof(type));

            bool isContainer = CodeGenerateContainerEditorUtility.IsValidType(type);
            bool hasDefaultConstructor = type.GetConstructor(Type.EmptyTypes) != null || type.IsValueType;
            bool notAttribute = !typeof(Attribute).IsAssignableFrom(type);
            bool notUnity = !typeof(Object).IsAssignableFrom(type);
            bool notObsolete = !type.IsDefined(typeof(ObsoleteAttribute));
            bool isSpecial = type.IsSpecialName;

            bool hasValidFields = false;
            bool hasValidProperties = false;

            FieldInfo[] fields = type.GetFields(BindingFlags.Instance | BindingFlags.Public);
            PropertyInfo[] properties = type.GetProperties(BindingFlags.Instance | BindingFlags.Public);

            for (int i = 0; i < fields.Length; i++)
            {
                if (CodeGenerateContainerEditorUtility.IsValidField(fields[i]))
                {
                    hasValidFields = true;
                    break;
                }
            }

            for (int i = 0; i < properties.Length; i++)
            {
                if (CodeGenerateContainerEditorUtility.IsValidProperty(properties[i]))
                {
                    hasValidProperties = true;
                    break;
                }
            }

            bool hasMembers = hasValidFields || hasValidProperties;

            return isContainer && hasDefaultConstructor && notAttribute && notUnity && notObsolete && !isSpecial && hasMembers;
        }
    }
}
