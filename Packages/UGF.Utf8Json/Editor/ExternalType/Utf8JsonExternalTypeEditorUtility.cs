using System;
using Container;

namespace UGF.Utf8Json.Editor.ExternalType
{
    public static class Utf8JsonExternalTypeEditorUtility
    {
        public static bool IsValidExternalType(Type type)
        {
            bool isContainer = CodeGenerateContainerEditorUtility.IsValidType(type);
            bool hasCtor = type.GetConstructor(Type.EmptyTypes) != null;

            return isContainer && hasCtor;
        }
    }
}
