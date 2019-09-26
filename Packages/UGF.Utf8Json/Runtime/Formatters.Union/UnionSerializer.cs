using System;
using System.Collections.Generic;
using System.Text;
using UnityEngine;
using Utf8Json;
using Utf8Json.Internal;

namespace UGF.Utf8Json.Runtime.Formatters.Union
{
    public class UnionSerializer : IUnionSerializer
    {
        public string TypePropertyName { get; }

        private readonly byte[] m_typePropertyNameBytes;
        private readonly ArraySegment<byte> m_typePropertyNameValue;
        private readonly Dictionary<Type, int> m_typeToId = new Dictionary<Type, int>();
        private readonly Dictionary<int, byte[]> m_typeNames = new Dictionary<int, byte[]>();
        private AutomataDictionary m_typeNameToId = new AutomataDictionary();
        private int m_identifierCounter = int.MinValue + 1;

        public UnionSerializer(string typePropertyName = "type")
        {
            TypePropertyName = typePropertyName ?? throw new ArgumentNullException(nameof(typePropertyName));

            m_typePropertyNameBytes = JsonWriter.GetEncodedPropertyName(typePropertyName);
            m_typePropertyNameValue = new ArraySegment<byte>(m_typePropertyNameBytes, 1, m_typePropertyNameBytes.Length - 3);
        }

        public int Add(Type targetType, string typeIdentifier)
        {
            if (targetType == null) throw new ArgumentNullException(nameof(targetType));
            if (typeIdentifier == null) throw new ArgumentNullException(nameof(typeIdentifier));
            if (m_identifierCounter == int.MaxValue) throw new Exception("The identifier counter exceeded.");
            if (m_typeToId.ContainsKey(targetType)) throw new ArgumentException($"The formatter of the specified target type already exists: '{targetType}'.");

            int identifier = m_identifierCounter++;
            byte[] typeName = JsonWriter.GetEncodedPropertyNameWithoutQuotation(typeIdentifier);

            m_typeToId.Add(targetType, identifier);
            m_typeNames.Add(identifier, typeName);
            m_typeNameToId.Add(typeName, identifier);

            return identifier;
        }

        public bool Remove(Type targetType)
        {
            if (targetType == null) throw new ArgumentNullException(nameof(targetType));

            if (m_typeToId.TryGetValue(targetType, out int identifier))
            {
                m_typeNameToId.Add(m_typeNames[identifier], int.MinValue);
                m_typeToId.Remove(targetType);
                m_typeNames.Remove(identifier);

                return true;
            }

            return false;
        }

        public void Clear()
        {
            m_typeToId.Clear();
            m_typeNames.Clear();
            m_typeNameToId = new AutomataDictionary();
        }

        public int GetIdentifier(Type targetType)
        {
            if (targetType == null) throw new ArgumentNullException(nameof(targetType));

            return m_typeToId[targetType];
        }

        public bool TryGetIdentifier(Type targetType, out int identifier)
        {
            if (targetType == null) throw new ArgumentNullException(nameof(targetType));

            return m_typeToId.TryGetValue(targetType, out identifier);
        }

        public void Serialize<T>(ref JsonWriter writer, T value, IJsonFormatter<T> formatter, IJsonFormatterResolver formatterResolver)
        {
            if (formatter == null) throw new ArgumentNullException(nameof(formatter));
            if (formatterResolver == null) throw new ArgumentNullException(nameof(formatterResolver));

            Type targetType = value.GetType();
            int identifier = m_typeToId[targetType];
            JsonWriter typeWriter = WriteTypeIdentifierSpace(ref writer, identifier);

            formatter.Serialize(ref writer, value, formatterResolver);

            WriteTypeIdentifier(typeWriter, identifier);
        }

        public T Deserialize<T>(ref JsonReader reader, IJsonFormatter<T> formatter, IJsonFormatterResolver formatterResolver)
        {
            if (formatter == null) throw new ArgumentNullException(nameof(formatter));
            if (formatterResolver == null) throw new ArgumentNullException(nameof(formatterResolver));

            return formatter.Deserialize(ref reader, formatterResolver);
        }

        public int ReadTypeIdentifier(JsonReader reader)
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

        private JsonWriter WriteTypeIdentifierSpace(ref JsonWriter writer, int identifier)
        {
            byte[] typeName = m_typeNames[identifier];
            int length = m_typePropertyNameBytes.Length + typeName.Length + 3;

            writer.EnsureCapacity(length + 1);

            JsonWriter typeWriter = writer;

            writer.AdvanceOffset(length);

            return typeWriter;
        }

        private void WriteTypeIdentifier(JsonWriter writer, int identifier)
        {
            writer.WriteBeginObject();
            writer.WriteRaw(m_typePropertyNameBytes);
            writer.WriteQuotation();
            writer.WriteRaw(m_typeNames[identifier]);
            writer.WriteQuotation();
            writer.WriteValueSeparator();
        }
    }
}
