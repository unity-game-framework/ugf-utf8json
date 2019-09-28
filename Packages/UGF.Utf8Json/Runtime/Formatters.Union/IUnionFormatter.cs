using System;
using Utf8Json;

namespace UGF.Utf8Json.Runtime.Formatters.Union
{
    /// <summary>
    /// Represents a formatter with nested formatters of the same target type.
    /// </summary>
    /// <remarks>
    /// This formatter stores another formatter with the same target type,
    /// union formatter determines which formatter to use for serializing a target of the specified type.
    /// </remarks>
    public interface IUnionFormatter
    {
        /// <summary>
        /// Adds formatter by the specified target type and with specified type identifier.
        /// </summary>
        /// <remarks>
        /// The type identifier will be used in serialized data to determine type during deserialization.
        /// </remarks>
        /// <param name="targetType">The type of the formatter target.</param>
        /// <param name="typeIdentifier">The type identifier to use in serialized data.</param>
        /// <param name="formatter">The formatter to add.</param>
        void AddFormatter(Type targetType, string typeIdentifier, IJsonFormatter formatter);

        /// <summary>
        /// Removes formatter by the specified target type.
        /// </summary>
        /// <param name="targetType">The type of the formatter target to remove.</param>
        void RemoveFormatter(Type targetType);

        /// <summary>
        /// Clears all formatters.
        /// </summary>
        void Clear();

        /// <summary>
        /// Gets formatter by the specified target type.
        /// </summary>
        /// <param name="targetType">The type of the formatter target.</param>
        T GetFormatter<T>(Type targetType) where T : IJsonFormatter;

        /// <summary>
        /// Tries to get formatter by the specified target type.
        /// </summary>
        /// <param name="targetType">The type of the formatter target.</param>
        /// <param name="formatter">The found formatter.</param>
        bool TryGetFormatter<T>(Type targetType, out T formatter) where T : IJsonFormatter;
    }
}
