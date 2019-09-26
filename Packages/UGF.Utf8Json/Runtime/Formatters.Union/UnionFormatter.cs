using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using Utf8Json;

namespace UGF.Utf8Json.Runtime.Formatters.Union
{
    public class UnionFormatter : IUnionFormatter, IJsonFormatter<object>, IEnumerable<KeyValuePair<int, IJsonFormatter>>
    {
        public IUnionSerializer UnionSerializer { get; }
        public IReadOnlyDictionary<int, IJsonFormatter> Formatters { get; }

        private readonly Dictionary<int, IJsonFormatter> m_formatters = new Dictionary<int, IJsonFormatter>();

        public UnionFormatter(IUnionSerializer unionSerializer = null)
        {
            UnionSerializer = unionSerializer ?? new UnionSerializer();
            Formatters = new ReadOnlyDictionary<int, IJsonFormatter>(m_formatters);
        }

        public void Serialize(ref JsonWriter writer, object value, IJsonFormatterResolver formatterResolver)
        {
            if (formatterResolver == null) throw new ArgumentNullException(nameof(formatterResolver));

            if (value != null)
            {
                Type targetType = value.GetType();

                if (!TryGetFormatter(targetType, out IJsonFormatter<object> formatter))
                {
                    throw new ArgumentException($"The formatter for the specified type not found: '{targetType}'.", nameof(targetType));
                }

                UnionSerializer.Serialize(ref writer, value, formatter, formatterResolver);
            }
            else
            {
                writer.WriteNull();
            }
        }

        public object Deserialize(ref JsonReader reader, IJsonFormatterResolver formatterResolver)
        {
            if (formatterResolver == null) throw new ArgumentNullException(nameof(formatterResolver));

            if (!reader.ReadIsNull())
            {
                int identifier = UnionSerializer.ReadTypeIdentifier(reader);

                if (!TryGetFormatter(identifier, out IJsonFormatter<object> formatter))
                {
                    throw new ArgumentException($"The formatter for the specified identifier not found: '{identifier}'.", nameof(identifier));
                }

                return UnionSerializer.Deserialize(ref reader, formatter, formatterResolver);
            }

            return null;
        }

        public void AddFormatter(Type targetType, string typeIdentifier, IJsonFormatter formatter)
        {
            if (targetType == null) throw new ArgumentNullException(nameof(targetType));
            if (typeIdentifier == null) throw new ArgumentNullException(nameof(typeIdentifier));
            if (formatter == null) throw new ArgumentNullException(nameof(formatter));

            int identifier = UnionSerializer.Add(targetType, typeIdentifier);

            m_formatters.Add(identifier, formatter);
        }

        public void RemoveFormatter(Type targetType)
        {
            if (targetType == null) throw new ArgumentNullException(nameof(targetType));

            if (UnionSerializer.TryGetIdentifier(targetType, out int identifier))
            {
                UnionSerializer.Remove(targetType);
                m_formatters.Remove(identifier);
            }
        }

        public void Clear()
        {
            UnionSerializer.Clear();
            m_formatters.Clear();
        }

        public IJsonFormatter<T> GetFormatter<T>(Type targetType)
        {
            if (targetType == null) throw new ArgumentNullException(nameof(targetType));

            return (IJsonFormatter<T>)m_formatters[UnionSerializer.GetIdentifier(targetType)];
        }

        public bool TryGetFormatter<T>(Type targetType, out IJsonFormatter<T> formatter)
        {
            if (targetType == null) throw new ArgumentNullException(nameof(targetType));

            if (UnionSerializer.TryGetIdentifier(targetType, out int identifier))
            {
                if (m_formatters.TryGetValue(identifier, out IJsonFormatter value) && value is IJsonFormatter<T> cast)
                {
                    formatter = cast;
                    return true;
                }
            }

            formatter = null;
            return false;
        }

        public bool TryGetFormatter<T>(int identifier, out IJsonFormatter<T> formatter)
        {
            if (m_formatters.TryGetValue(identifier, out IJsonFormatter value) && value is IJsonFormatter<T> cast)
            {
                formatter = cast;
                return true;
            }

            formatter = null;
            return false;
        }

        public Dictionary<int, IJsonFormatter>.Enumerator GetEnumerator()
        {
            return m_formatters.GetEnumerator();
        }

        IEnumerator<KeyValuePair<int, IJsonFormatter>> IEnumerable<KeyValuePair<int, IJsonFormatter>>.GetEnumerator()
        {
            return ((IEnumerable<KeyValuePair<int, IJsonFormatter>>)m_formatters).GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return ((IEnumerable)m_formatters).GetEnumerator();
        }
    }
}
