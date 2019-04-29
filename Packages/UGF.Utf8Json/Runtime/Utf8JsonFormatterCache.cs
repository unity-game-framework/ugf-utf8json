using JetBrains.Annotations;
using Utf8Json;

namespace UGF.Utf8Json.Runtime
{
    /// <summary>
    /// Represents static formatter cache.
    /// </summary>
    public static class Utf8JsonFormatterCache<T>
    {
        /// <summary>
        /// Cached formatter. (Can be Null)
        /// </summary>
        [CanBeNull] public static IJsonFormatter<T> Formatter;
    }
}
