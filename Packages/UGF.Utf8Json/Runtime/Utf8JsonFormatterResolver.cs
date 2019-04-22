using System;
using System.Collections.Generic;
using Utf8Json;

namespace UGF.Utf8Json.Runtime
{
    public class Utf8JsonFormatterResolver : IUtf8JsonFormatterResolver
    {
        public Dictionary<Type, IJsonFormatter> Formatters { get; } = new Dictionary<Type, IJsonFormatter>();
        public List<IJsonFormatterResolver> Resolvers { get; } = new List<IJsonFormatterResolver>();

        IReadOnlyDictionary<Type, IJsonFormatter> IUtf8JsonFormatterResolver.Formatters { get { return Formatters; } }
        IReadOnlyList<IJsonFormatterResolver> IUtf8JsonFormatterResolver.Resolvers { get { return Resolvers; } }

        public void CacheFormatters()
        {
            foreach (KeyValuePair<Type, IJsonFormatter> pair in Formatters)
            {
                Utf8JsonUtility.SetFormatterCache(pair.Value, pair.Key);
            }
        }

        public IJsonFormatter<T> GetFormatter<T>()
        {
            IJsonFormatter<T> formatter = Utf8JsonFormatterCache<T>.Formatter;

            if (formatter == null)
            {
                if (Formatters.TryGetValue(typeof(T), out IJsonFormatter formatterBase) && formatterBase is IJsonFormatter<T> formatterGeneric)
                {
                    formatter = formatterGeneric;

                    Utf8JsonFormatterCache<T>.Formatter = formatter;
                }

                for (int i = 0; i < Resolvers.Count; i++)
                {
                    formatter = Resolvers[i].GetFormatter<T>();

                    if (formatter != null)
                    {
                        Utf8JsonFormatterCache<T>.Formatter = formatter;
                        break;
                    }
                }
            }

            return formatter;
        }
    }
}
