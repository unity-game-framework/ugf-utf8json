using System;
using JetBrains.Annotations;
using UGF.Assemblies.Runtime;

namespace UGF.Utf8Json.Runtime.ExternalType
{
    /// <summary>
    /// Represents attribute to mark defines that store formatters for external types as browsable.
    /// </summary>
    [BaseTypeRequired(typeof(IUtf8JsonExternalTypeDefine))]
    [AttributeUsage(AttributeTargets.Class)]
    public sealed class Utf8JsonExternalTypeDefineAttribute : AssemblyBrowsableTypeAttribute
    {
    }
}
