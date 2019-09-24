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

        void AddFormatter(Type type, IJsonFormatter formatter);
        void RemoveFormatter(Type type);
        void AddResolver(IJsonFormatterResolver resolver);
        void RemoveResolver(IJsonFormatterResolver resolver);
        bool TryGetFormatter<T>(out IJsonFormatter<T> formatter);
        bool TryGetFormatter(Type type, out IJsonFormatter formatter);
    }
}
