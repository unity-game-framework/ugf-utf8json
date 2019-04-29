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
    /// <summary>
    /// Provides utilities to work with external types in editor.
    /// </summary>
    public static class Utf8JsonExternalTypeEditorUtility
    {
        /// <summary>
        /// Gets the extension of the external type info file. (".utf8json-external")
        /// </summary>
        public static string ExternalTypeAssetExtension { get; } = ".utf8json-external";

        /// <summary>
        /// Generates formatter sources in the specified folder from the specified external info files.
        /// </summary>
        /// <param name="path">The path of the folder where to generate source files.</param>
        /// <param name="externals">The collection of the external type info files.</param>
        /// <param name="paths">The collection to add result path for each generated source.</param>
        /// <param name="compilation">The project compilation used during generation.</param>
        /// <param name="generator">The syntax generator used during generation.</param>
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

                if (TryGetExternalTypeAssetInfoFromPath(externalPath, out Utf8JsonExternalTypeAssetInfo info) && info.IsTargetTypeValid())
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

        /// <summary>
        /// Creates container from the specified valid type.
        /// </summary>
        /// <param name="type">The valid type to generate container.</param>
        /// <param name="compilation">The project compilation used during generation.</param>
        public static CodeGenerateContainer CreateContainer(Type type, CSharpCompilation compilation = null)
        {
            if (type == null) throw new ArgumentNullException(nameof(type));
            if (compilation == null) compilation = CodeAnalysisEditorUtility.ProjectCompilation;

            return CodeGenerateContainerEditorUtility.Create(compilation, type);
        }

        /// <summary>
        /// Creates container from the valid external type info.
        /// </summary>
        /// <param name="info">The external type info used to generated container.</param>
        /// <param name="compilation">The project compilation used during generation.</param>
        public static CodeGenerateContainer CreateContainer(Utf8JsonExternalTypeAssetInfo info, CSharpCompilation compilation = null)
        {
            if (!info.IsTargetTypeValid()) throw new ArgumentException("The specified info not valid.", nameof(info));
            if (compilation == null) compilation = CodeAnalysisEditorUtility.ProjectCompilation;

            Type type = info.GetTargetType();
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

        /// <summary>
        /// Tries to get external type info from the specified path.
        /// </summary>
        /// <param name="path">The path of the external type info file.</param>
        /// <param name="info">The loaded info.</param>
        public static bool TryGetExternalTypeAssetInfoFromPath(string path, out Utf8JsonExternalTypeAssetInfo info)
        {
            if (File.Exists(path))
            {
                string source = File.ReadAllText(path);

                info = JsonUtility.FromJson<Utf8JsonExternalTypeAssetInfo>(source);

                return info != null;
            }

            info = null;
            return false;
        }

        /// <summary>
        /// Determines whether the specified type valid to generate formatter.
        /// <para>
        /// The type can be valid for generating container only if:
        /// <para>- Type is container.</para>
        /// <para>- Type has default constructor.</para>
        /// <para>- Type is not an attribute, unity object, marked as obsolete or special name type.</para>
        /// <para>- Type has any container valid fields or properties.</para>
        /// </para>
        /// </summary>
        /// <param name="type">The type to check.</param>
        public static bool IsValidExternalType(Type type)
        {
            if (type == null) throw new ArgumentNullException(nameof(type));

            bool isContainer = CodeGenerateContainerEditorUtility.IsValidType(type);

            if (!isContainer)
            {
                return false;
            }

            bool hasDefaultConstructor = type.IsValueType || type.GetConstructor(Type.EmptyTypes) != null;

            if (!hasDefaultConstructor)
            {
                return false;
            }

            bool isAttribute = typeof(Attribute).IsAssignableFrom(type);
            bool isUnity = typeof(Object).IsAssignableFrom(type);
            bool isObsolete = type.IsDefined(typeof(ObsoleteAttribute));
            bool isSpecial = type.IsSpecialName;

            if (isAttribute || isUnity || isObsolete || isSpecial)
            {
                return false;
            }

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

            if (!hasMembers)
            {
                return false;
            }

            return true;
        }
    }
}
