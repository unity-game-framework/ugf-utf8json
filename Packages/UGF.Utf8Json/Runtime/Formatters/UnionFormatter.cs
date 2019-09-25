using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;
using UnityEngine;
using Utf8Json;
using Utf8Json.Internal;

namespace UGF.Utf8Json.Runtime.Formatters
{
    public class UnionFormatter : IJsonFormatter<object>
    {
        public IReadOnlyList<byte> TypePropertyName { get; }
        public IReadOnlyList<byte> TypePropertyNameValue { get; }

        private readonly Dictionary<Type, int> m_typeToId = new Dictionary<Type, int>();
        private readonly Dictionary<int, byte[]> m_typeNames = new Dictionary<int, byte[]>();
        private readonly Dictionary<int, IJsonFormatter> m_formatters = new Dictionary<int, IJsonFormatter>();
        private readonly byte[] m_typePropertyName;
        private readonly ArraySegment<byte> m_typePropertyNameValue;
        private AutomataDictionary m_typeNameToId = new AutomataDictionary();
        private int m_identifierCounter = int.MinValue + 1;

        public UnionFormatter(string typePropertyName = "type")
        {
            if (typePropertyName == null) throw new ArgumentNullException(nameof(typePropertyName));

            m_typePropertyName = JsonWriter.GetEncodedPropertyName(typePropertyName);
            m_typePropertyNameValue = new ArraySegment<byte>(m_typePropertyName, 1, m_typePropertyName.Length - 3);

            TypePropertyName = new ReadOnlyCollection<byte>(m_typePropertyName);
            TypePropertyNameValue = new ReadOnlyCollection<byte>(m_typePropertyNameValue);
        }

        public virtual void AddFormatter(string typeIdentifier, Type targetType, IJsonFormatter formatter)
        {
            if (typeIdentifier == null) throw new ArgumentNullException(nameof(typeIdentifier));
            if (targetType == null) throw new ArgumentNullException(nameof(targetType));
            if (formatter == null) throw new ArgumentNullException(nameof(formatter));
            if (m_identifierCounter == int.MaxValue) throw new Exception("The identifier counter exceeded.");
            if (m_typeToId.ContainsKey(targetType)) throw new ArgumentException($"The formatter of the specified target type already exists: '{targetType}'.");

            int identifier = m_identifierCounter++;
            byte[] typeName = JsonWriter.GetEncodedPropertyNameWithoutQuotation(typeIdentifier);

            m_typeToId.Add(targetType, identifier);
            m_typeNames.Add(identifier, typeName);
            m_formatters.Add(identifier, formatter);
            m_typeNameToId.Add(typeName, identifier);
        }

        public virtual void RemoveFormatter(Type targetType)
        {
            if (targetType == null) throw new ArgumentNullException(nameof(targetType));

            if (m_typeToId.TryGetValue(targetType, out int identifier))
            {
                m_typeNameToId.Add(m_typeNames[identifier], int.MinValue);
                m_typeToId.Remove(targetType);
                m_typeNames.Remove(identifier);
                m_formatters.Remove(identifier);
            }
        }

        public virtual void Clear()
        {
            m_typeToId.Clear();
            m_typeNames.Clear();
            m_formatters.Clear();
            m_typeNameToId = new AutomataDictionary();
        }

        public int GetFormatterIdentifier(Type targetType)
        {
            if (targetType == null) throw new ArgumentNullException(nameof(targetType));

            return m_typeToId[targetType];
        }

        public bool TryGetFormatterIdentifier(Type targetType, out int identifier)
        {
            if (targetType == null) throw new ArgumentNullException(nameof(targetType));

            return m_typeToId.TryGetValue(targetType, out identifier);
        }

        public IJsonFormatter<T> GetFormatter<T>(Type targetType)
        {
            if (targetType == null) throw new ArgumentNullException(nameof(targetType));

            return (IJsonFormatter<T>)m_formatters[m_typeToId[targetType]];
        }

        public bool TryGetFormatter<T>(Type targetType, out IJsonFormatter<T> formatter)
        {
            if (targetType == null) throw new ArgumentNullException(nameof(targetType));

            if (m_typeToId.TryGetValue(targetType, out int identifier))
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

                int identifier = m_typeToId[targetType];
                JsonWriter typeWriter = WriteTypeIdentifierSpace(ref writer, identifier);

                formatter.Serialize(ref writer, value, formatterResolver);

                WriteTypeIdentifier(typeWriter, identifier);
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
                int identifier = ReadTypeIdentifier(reader);

                if (!TryGetFormatter(identifier, out IJsonFormatter<object> formatter))
                {
                    throw new ArgumentException($"The formatter for the specified identifier not found: '{identifier}'.", nameof(identifier));
                }

                return formatter.Deserialize(ref reader, formatterResolver);
            }

            return null;
        }

        protected JsonWriter WriteTypeIdentifierSpace(ref JsonWriter writer, int identifier)
        {
            byte[] typeName = m_typeNames[identifier];
            int length = m_typePropertyName.Length + typeName.Length + 3;

            writer.EnsureCapacity(length + 1);

            JsonWriter typeWriter = writer;

            writer.AdvanceOffset(length);

            return typeWriter;
        }

        protected void WriteTypeIdentifier(JsonWriter writer, int identifier)
        {
            writer.WriteBeginObject();
            writer.WriteRaw(m_typePropertyName);
            writer.WriteQuotation();
            writer.WriteRaw(m_typeNames[identifier]);
            writer.WriteQuotation();
            writer.WriteValueSeparator();
        }

        protected int ReadTypeIdentifier(JsonReader reader)
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
