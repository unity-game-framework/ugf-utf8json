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
    }
}
