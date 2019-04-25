using System;
using JetBrains.Annotations;
using UGF.Assemblies.Runtime;

namespace UGF.Utf8Json.Runtime.ExternalType
{
    [BaseTypeRequired(typeof(IUtf8JsonExternalTypeDefine))]
    [AttributeUsage(AttributeTargets.Class)]
    public sealed class Utf8JsonExternalTypeDefineAttribute : AssemblyBrowsableTypeAttribute
    {
    }
}
