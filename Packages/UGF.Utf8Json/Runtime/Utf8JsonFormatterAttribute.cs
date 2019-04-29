using System;
using JetBrains.Annotations;
using UGF.Assemblies.Runtime;
using Utf8Json;

namespace UGF.Utf8Json.Runtime
{
    /// <summary>
    /// Represents attribute to mark formatters as browsable.
    /// </summary>
    [BaseTypeRequired(typeof(IJsonFormatter))]
    [AttributeUsage(AttributeTargets.Class)]
    public sealed class Utf8JsonFormatterAttribute : AssemblyBrowsableTypeAttribute
    {
        /// <summary>
        /// Gets the type of the formatter target.
        /// </summary>
        public Type TargetType { get; }

        /// <summary>
        /// Creates attribute with the specified type of the formatter target.
        /// </summary>
        /// <param name="targetType">The type of the formatter target.</param>
        public Utf8JsonFormatterAttribute(Type targetType)
        {
            TargetType = targetType ?? throw new ArgumentNullException(nameof(targetType));
        }
    }
}
