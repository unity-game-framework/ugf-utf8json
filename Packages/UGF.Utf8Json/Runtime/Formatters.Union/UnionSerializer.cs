using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;
using UnityEngine;
using Utf8Json;
using Utf8Json.Internal;

namespace UGF.Utf8Json.Runtime.Formatters.Union
{
    public class UnionSerializer : IUnionSerializer, IEnumerable<KeyValuePair<Type, int>>
    {
        public string TypePropertyName { get; }
        public IReadOnlyDictionary<Type, int> Types { get; }

        private readonly byte[] m_typePropertyNameBytes;
        private readonly ArraySegment<byte> m_typePropertyNameValue;
        private readonly Dictionary<Type, int> m_typeToId = new Dictionary<Type, int>();
        private readonly Dictionary<int, byte[]> m_typeNameBytes = new Dictionary<int, byte[]>();
        private AutomataDictionary m_typeNameToId = new AutomataDictionary();
        private int m_identifierCounter = int.MinValue + 1;

        public UnionSerializer(string typePropertyName = "type")
        {
            TypePropertyName = typePropertyName ?? throw new ArgumentNullException(nameof(typePropertyName));
            Types = new ReadOnlyDictionary<Type, int>(m_typeToId);

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
            m_typeNameBytes.Add(identifier, typeName);
            m_typeNameToId.Add(typeName, identifier);

            return identifier;
        }

        public bool Remove(Type targetType)
        {
            if (targetType == null) throw new ArgumentNullException(nameof(targetType));

            if (m_typeToId.TryGetValue(targetType, out int identifier))
            {
                m_typeNameToId.Add(m_typeNameBytes[identifier], int.MinValue);
                m_typeToId.Remove(targetType);
                m_typeNameBytes.Remove(identifier);

                return true;
            }

            return false;
        }

        public void Clear()
        {
            m_typeToId.Clear();
            m_typeNameBytes.Clear();
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
            int position = WriteTypeIdentifierSpace(ref writer, identifier);

            formatter.Serialize(ref writer, value, formatterResolver);

            WriteTypeIdentifier(ref writer, identifier, position);
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

        public Dictionary<Type, int>.Enumerator GetEnumerator()
        {
            return m_typeToId.GetEnumerator();
        }

        private int WriteTypeIdentifierSpace(ref JsonWriter writer, int identifier)
        {
            byte[] typeName = m_typeNameBytes[identifier];
            int length = m_typePropertyNameBytes.Length + typeName.Length + 3;
            int position = writer.CurrentOffset;

            writer.EnsureCapacity(length + 1);
            writer.AdvanceOffset(length);

            return position;
        }

        private void WriteTypeIdentifier(ref JsonWriter writer, int identifier, int position)
        {
            int current = writer.CurrentOffset;

            writer.CurrentOffset = position;
            writer.WriteBeginObject();
            writer.WriteRaw(m_typePropertyNameBytes);
            writer.WriteQuotation();
            writer.WriteRaw(m_typeNameBytes[identifier]);
            writer.WriteQuotation();
            writer.WriteValueSeparator();
            writer.CurrentOffset = current;
        }

        IEnumerator<KeyValuePair<Type, int>> IEnumerable<KeyValuePair<Type, int>>.GetEnumerator()
        {
            return ((IEnumerable<KeyValuePair<Type, int>>)m_typeToId).GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return ((IEnumerable)m_typeToId).GetEnumerator();
        }
    }
}
