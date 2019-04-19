using System;
using System.Reflection;
using Microsoft.CodeAnalysis.CSharp;
using UGF.Code.Analysis.Editor;
using UGF.Code.Generate.Editor.Container;
using Object = UnityEngine.Object;

namespace UGF.Utf8Json.Editor.ExternalType
{
    public static class Utf8JsonExternalTypeEditorUtility
    {
        public static bool IsValidExternalType(Type type)
        {
            bool isContainer = CodeGenerateContainerEditorUtility.IsValidType(type);
            bool hasDefaultConstructor = type.GetConstructor(Type.EmptyTypes) != null || type.IsValueType;
            bool notAttribute = !typeof(Attribute).IsAssignableFrom(type);
            bool notUnity = !typeof(Object).IsAssignableFrom(type);
            bool notObsolete = !type.IsDefined(typeof(ObsoleteAttribute));

            return isContainer && hasDefaultConstructor && notAttribute && notUnity && notObsolete;
        }

        public static CodeGenerateContainer CreateContainer(Type type)
        {
            CSharpCompilation compilation = CodeAnalysisEditorUtility.ProjectCompilation;
            CodeGenerateContainer container = CodeGenerateContainerEditorUtility.Create(compilation, type);

            return container;
        }
    }
}
