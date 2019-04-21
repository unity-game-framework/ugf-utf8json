using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using Utf8Json;

namespace UGF.Utf8Json.Runtime
{
    public class Utf8JsonFormatterResolver : IUtf8JsonFormatterResolver, IJsonFormatterResolver, IEnumerable<KeyValuePair<Type, IJsonFormatter>>
    {
        public IReadOnlyDictionary<Type, IJsonFormatter> Formatters { get; }

        private readonly Dictionary<Type, IJsonFormatter> m_formatters = new Dictionary<Type, IJsonFormatter>();

        public Utf8JsonFormatterResolver()
        {
            Formatters = new ReadOnlyDictionary<Type, IJsonFormatter>(m_formatters);
        }

        public void Add(Type type, IJsonFormatter formatter)
        {
            m_formatters.Add(type, formatter);
        }

        public void Remove(Type type)
        {
            m_formatters.Remove(type);
        }

        public void Clear()
        {
            m_formatters.Clear();
        }

        public IJsonFormatter<T> GetFormatter<T>()
        {
            return Utf8JsonFormatterResolverCache<T>.GetFormatter(this);
        }

        public Dictionary<Type, IJsonFormatter>.Enumerator GetEnumerator()
        {
            return m_formatters.GetEnumerator();
        }

        IEnumerator<KeyValuePair<Type, IJsonFormatter>> IEnumerable<KeyValuePair<Type, IJsonFormatter>>.GetEnumerator()
        {
            return GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }
    }
}
