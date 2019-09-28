using System;
using System.Collections.Generic;
using Utf8Json;

namespace UGF.Utf8Json.Runtime
{
    /// <summary>
    /// Represents resolver with the formatters and nested resolver.
    /// </summary>
    public interface IUtf8JsonFormatterResolver : IJsonFormatterResolver
    {
        /// <summary>
        /// Gets collection of the formatters stored by the target type.
        /// </summary>
        IReadOnlyDictionary<Type, IJsonFormatter> Formatters { get; }

        /// <summary>
        /// Gets collection of the nested resolvers.
        /// </summary>
        IReadOnlyList<IJsonFormatterResolver> Resolvers { get; }

        /// <summary>
        /// Adds specified formatter by the specified type.
        /// </summary>
        /// <param name="formatter">The formatter to add.</param>
        void AddFormatter<T>(IJsonFormatter<T> formatter);

        /// <summary>
        /// Adds the specified formatter by the specified type.
        /// </summary>
        /// <param name="type">The type of the formatter target.</param>
        /// <param name="formatter">The formatter to add.</param>
        void AddFormatter(Type type, IJsonFormatter formatter);

        /// <summary>
        /// Removes a formatter by the specified type.
        /// </summary>
        /// <param name="type">The type of the formatter target.</param>
        void RemoveFormatter(Type type);

        /// <summary>
        /// Adds the specified resolver.
        /// </summary>
        /// <param name="resolver">The resolver to add.</param>
        void AddResolver(IJsonFormatterResolver resolver);

        /// <summary>
        /// Removes the specified resolver.
        /// </summary>
        /// <param name="resolver">The resolver to remove.</param>
        void RemoveResolver(IJsonFormatterResolver resolver);

        /// <summary>
        /// Tries to get formatter by the specified type of the formatter target.
        /// </summary>
        /// <param name="formatter">The found formatter.</param>
        bool TryGetFormatter<T>(out IJsonFormatter<T> formatter);

        /// <summary>
        /// Tries to get formatter by the specified type of the formatter target.
        /// </summary>
        /// <param name="type">The type of the formatter target.</param>
        /// <param name="formatter">The found formatter.</param>
        bool TryGetFormatter(Type type, out IJsonFormatter formatter);
    }
}
