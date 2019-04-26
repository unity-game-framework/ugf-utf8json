using System;
using System.Collections.Generic;
using Utf8Json;
using Utf8Json.Formatters;

namespace UGF.Utf8Json.Runtime
{
    /// <summary>
    /// Represents default resolver with the formatters and nested resolvers.
    /// </summary>
    public class Utf8JsonFormatterResolver : IUtf8JsonFormatterResolver
    {
        /// <summary>
        /// Gets collection of the formatters stored by the target type.
        /// </summary>
        public Dictionary<Type, IJsonFormatter> Formatters { get; } = new Dictionary<Type, IJsonFormatter>();

        /// <summary>
        /// Gets collection of the nested resolvers.
        /// </summary>
        public List<IJsonFormatterResolver> Resolvers { get; } = new List<IJsonFormatterResolver>();

        IReadOnlyDictionary<Type, IJsonFormatter> IUtf8JsonFormatterResolver.Formatters { get { return Formatters; } }
        IReadOnlyList<IJsonFormatterResolver> IUtf8JsonFormatterResolver.Resolvers { get { return Resolvers; } }

        /// <summary>
        /// Gets formatter for the specified type.
        /// </summary>
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
                else
                {
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
            }

            return formatter;
        }

        /// <summary>
        /// Forces to cache formatters.
        /// </summary>
        public void CacheFormatters()
        {
            foreach (KeyValuePair<Type, IJsonFormatter> pair in Formatters)
            {
                Utf8JsonUtility.SetFormatterCache(pair.Key, pair.Value);
            }
        }
    }
}
