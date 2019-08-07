using System;
using System.Collections.Generic;
using System.Text;
using UnityEngine;
using Utf8Json;
using Utf8Json.Internal;

namespace UGF.Utf8Json.Runtime.Formatters
{
    public class UnionFormatter<TTarget> : IJsonFormatter<TTarget>
    {
        private readonly AutomataDictionary m_typeNameToId = new AutomataDictionary();
        private readonly Dictionary<Type, int> m_typeToId = new Dictionary<Type, int>();
        private readonly List<IJsonFormatter<TTarget>> m_formatters = new List<IJsonFormatter<TTarget>>();
        private readonly List<byte[]> m_typeNames = new List<byte[]>();
        private readonly byte[] m_typePropertyName;
        private readonly ArraySegment<byte> m_typePropertyNameValue;

        public UnionFormatter(string typePropertyName = "type")
        {
            m_typePropertyName = JsonWriter.GetEncodedPropertyName(typePropertyName);
            m_typePropertyNameValue = new ArraySegment<byte>(m_typePropertyName, 1, m_typePropertyName.Length - 3);
        }

        public void AddFormatter<T>(string typeIdentifier) where T : TTarget
        {
            AddFormatter<T>(typeIdentifier, new UnionFormatterTyped<T, TTarget>());
        }

        public void AddFormatter<T>(string typeIdentifier, IJsonFormatter<TTarget> formatter) where T : TTarget
        {
            int identifier = m_formatters.Count;
            byte[] typeName = JsonWriter.GetEncodedPropertyNameWithoutQuotation(typeIdentifier);

            m_typeNameToId.Add(typeName, identifier);
            m_typeToId.Add(typeof(T), identifier);
            m_formatters.Add(formatter);
            m_typeNames.Add(typeName);
        }

        public void Serialize(ref JsonWriter writer, TTarget value, IJsonFormatterResolver formatterResolver)
        {
            if (!EqualityComparer<TTarget>.Default.Equals(value, default))
            {
                Type targetType = value.GetType();
                int identifier = GetTypeIdentifier(targetType);

                JsonWriter typeWriter = WriteTypeIdentifierSpace(ref writer, identifier);

                m_formatters[identifier].Serialize(ref writer, value, formatterResolver);

                WriteTypeIdentifier(typeWriter, identifier);
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
                int identifier = ReadTypeIdentifier(reader);

                return m_formatters[identifier].Deserialize(ref reader, formatterResolver);
            }

            return default;
        }

        private int GetTypeIdentifier(Type type)
        {
            if (m_typeToId.TryGetValue(type, out int identifier))
            {
                return identifier;
            }

            throw new ArgumentException($"The identifier for specified type not found: '{type}'.", nameof(type));
        }

        private JsonWriter WriteTypeIdentifierSpace(ref JsonWriter writer, int identifier)
        {
            byte[] typeName = m_typeNames[identifier];
            int length = m_typePropertyName.Length + typeName.Length + 3;

            writer.EnsureCapacity(length + 1);

            JsonWriter typeWriter = writer;

            writer.AdvanceOffset(length);

            return typeWriter;
        }

        private void WriteTypeIdentifier(JsonWriter writer, int identifier)
        {
            writer.WriteBeginObject();
            writer.WriteRaw(m_typePropertyName);
            writer.WriteQuotation();
            writer.WriteRaw(m_typeNames[identifier]);
            writer.WriteQuotation();
            writer.WriteValueSeparator();
        }

        private int ReadTypeIdentifier(JsonReader reader)
        {
            int start = reader.GetCurrentOffsetUnsafe();
            int count = 0;

            while (reader.ReadIsInObject(ref count))
            {
                ArraySegment<byte> propertyName = reader.ReadPropertyNameSegmentRaw();

                if (ByteArrayComparer.Equals(propertyName, m_typePropertyNameValue))
                {
                    ArraySegment<byte> typeName = reader.ReadStringSegmentRaw();

                    if (m_typeNameToId.TryGetValue(typeName, out int identifier))
                    {
                        return identifier;
                    }

                    string name = typeName.Array != null ? Encoding.UTF8.GetString(typeName.Array, typeName.Offset, typeName.Count) : null;

                    throw new ArgumentException($"The type identifier not found: '{name}'.", nameof(typeName));
                }

                reader.ReadNextBlock();
            }

            int length = Mathf.Clamp(reader.GetCurrentOffsetUnsafe() - start, 0, 100);
            string value = Encoding.UTF8.GetString(reader.GetBufferUnsafe(), start, length);

            throw new ArgumentException($"The type property not found at: '{value}'.", nameof(reader));
        }
    }
}
