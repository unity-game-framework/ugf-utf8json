using System;
using UGF.Assemblies.Runtime;

namespace UGF.Utf8Json.Runtime
{
    /// <summary>
    /// Represents attribute used to mark type that require formatter generation.
    /// </summary>
    [AttributeUsage(AttributeTargets.Class)]
    public sealed class Utf8JsonSerializableAttribute : AssemblyBrowsableTypeAttribute
    {
    }
}
