using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using Utf8Json;

namespace UGF.Utf8Json.Runtime.Formatters
{
    public abstract class UnionFormatter<TTarget> : IJsonFormatter<TTarget>
    {
        public IReadOnlyDictionary<string, IJsonFormatter<TTarget>> Formatters { get; }

        private readonly Dictionary<string, IJsonFormatter<TTarget>> m_formatters = new Dictionary<string, IJsonFormatter<TTarget>>();
        private readonly Dictionary<Type, string> m_types = new Dictionary<Type, string>();
        private readonly Dictionary<string, byte[]> m_typeValues = new Dictionary<string, byte[]>();
        private readonly byte[] m_typePropertyName;

        protected UnionFormatter(string typePropertyName = "type")
        {
            Formatters = new ReadOnlyDictionary<string, IJsonFormatter<TTarget>>(m_formatters);

            m_typePropertyName = JsonWriter.GetEncodedPropertyName(typePropertyName);
        }

        public void AddFormatter<T>(string typeIdentifier) where T : TTarget
        {
            m_formatters.Add(typeIdentifier, new UnionFormatterTyped<T, TTarget>());
            m_types.Add(typeof(T), typeIdentifier);
            m_typeValues.Add(typeIdentifier, JsonWriter.GetEncodedValue(typeIdentifier));
        }

        public bool RemoveFormatter(string typeIdentifier)
        {
            if (m_formatters.TryGetValue(typeIdentifier, out IJsonFormatter<TTarget> formatter) && formatter is IUnionFormatterTyped formatterTyped)
            {
                m_formatters.Remove(typeIdentifier);
                m_types.Remove(formatterTyped.TargetType);
                m_typeValues.Remove(typeIdentifier);
                return true;
            }

            return false;
        }

        public void Clear()
        {
            m_formatters.Clear();
            m_typeValues.Clear();
        }

        public void Serialize(ref JsonWriter writer, TTarget value, IJsonFormatterResolver formatterResolver)
        {
            if (!EqualityComparer<TTarget>.Default.Equals(value, default))
            {
                Type targetType = value.GetType();

                if (m_types.TryGetValue(targetType, out string typeIdentifier))
                {
                    m_formatters[""].Serialize(ref writer, value, formatterResolver);
                }
                else
                {
                    throw new ArgumentException($"The identifier for specified target not found: '{value}'.", nameof(value));
                }
            }
            else
            {
                writer.WriteNull();
            }
        }

        public TTarget Deserialize(ref JsonReader reader, IJsonFormatterResolver formatterResolver)
        {
            if (!reader.ReadIsNull())
            {
                return m_formatters[""].Deserialize(ref reader, formatterResolver);
            }

            return default;
        }

        public Dictionary<string, IJsonFormatter<TTarget>>.Enumerator GetEnumerator()
        {
            return m_formatters.GetEnumerator();
        }
    }
}
