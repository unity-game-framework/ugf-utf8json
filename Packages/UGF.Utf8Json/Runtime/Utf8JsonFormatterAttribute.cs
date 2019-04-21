using System;
using JetBrains.Annotations;
using UGF.Assemblies.Runtime;
using Utf8Json;

namespace UGF.Utf8Json.Runtime
{
    [BaseTypeRequired(typeof(IJsonFormatter))]
    [AttributeUsage(AttributeTargets.Class)]
    public sealed class Utf8JsonFormatterAttribute : AssemblyBrowsableTypeAttribute
    {
        public Type TargetType { get; }

        public Utf8JsonFormatterAttribute(Type targetType)
        {
            TargetType = targetType;
        }
    }
}
