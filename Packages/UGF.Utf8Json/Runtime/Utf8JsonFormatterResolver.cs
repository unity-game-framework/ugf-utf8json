using System;
using System.Collections.Generic;
using Utf8Json;

namespace UGF.Utf8Json.Runtime
{
    public class Utf8JsonFormatterResolver : IUtf8JsonFormatterResolver
    {
        public Dictionary<Type, IJsonFormatter> Formatters { get; } = new Dictionary<Type, IJsonFormatter>();

        IReadOnlyDictionary<Type, IJsonFormatter> IUtf8JsonFormatterResolver.Formatters { get { return Formatters; } }

        public IJsonFormatter<T> GetFormatter<T>()
        {
            IJsonFormatter<T> formatter = Utf8JsonFormatterCache<T>.Formatter;

            if (formatter == null && Formatters.TryGetValue(typeof(T), out IJsonFormatter formatterBase) && formatterBase is IJsonFormatter<T> formatterGeneric)
            {
                formatter = formatterGeneric;

                Utf8JsonFormatterCache<T>.Formatter = formatter;
            }

            return formatter;
        }
    }
}
