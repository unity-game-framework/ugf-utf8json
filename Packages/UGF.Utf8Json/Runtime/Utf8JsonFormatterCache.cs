using JetBrains.Annotations;
using Utf8Json;

namespace UGF.Utf8Json.Runtime
{
    public static class Utf8JsonFormatterCache<T>
    {
        [CanBeNull]
        public static IJsonFormatter<T> Formatter;
    }
}
