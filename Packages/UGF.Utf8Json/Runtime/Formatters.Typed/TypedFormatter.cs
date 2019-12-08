using System;
using System.Text;
using Utf8Json;
using Utf8Json.Internal;

namespace UGF.Utf8Json.Runtime.Formatters.Typed
{
    public class TypedFormatter<TTarget> : JsonFormatterBase<TTarget>
    {
        public IUtf8JsonFormatterResolver Resolver { get; }
        public ITypedFormatterTypeProvider TypeProvider { get; }
        public string TypePropertyName { get; }

        private readonly byte[] m_typePropertyNameBytes;
        private readonly ArraySegment<byte> m_typePropertyNameValue;

        public TypedFormatter(IUtf8JsonFormatterResolver resolver, ITypedFormatterTypeProvider typeProvider, string typePropertyName = "type")
        {
            Resolver = resolver;
            TypeProvider = typeProvider;
            TypePropertyName = typePropertyName;

            m_typePropertyNameBytes = JsonWriter.GetEncodedPropertyName(typePropertyName);
            m_typePropertyNameValue = new ArraySegment<byte>(m_typePropertyNameBytes, 1, m_typePropertyNameBytes.Length - 3);
        }

        public override void Serialize(ref JsonWriter writer, TTarget value, IJsonFormatterResolver formatterResolver)
        {
            Type type = value.GetType();

            if (!(Resolver.TryGetFormatter(type, out IJsonFormatter result) && result is IJsonFormatter<object> formatter))
            {
                throw new ArgumentException($"Formatter not found for specified type: '{type}'.");
            }

            if (!TypeProvider.TryGetTypeName(type, out byte[] typeName))
            {
                throw new ArgumentException($"Type name for specified type not found: '{type}'.");
            }

            int position = WriteTypeNameSpace(ref writer, typeName);

            formatter.Serialize(ref writer, value, formatterResolver);

            WriteTypeName(ref writer, typeName, position);
        }

        public override TTarget Deserialize(ref JsonReader reader, IJsonFormatterResolver formatterResolver)
        {
            if (!TryReadTypeName(reader, out ArraySegment<byte> typeName))
            {
                throw new ArgumentException("Type name property not found.");
            }

            if (!TypeProvider.TryGetType(typeName, out Type type))
            {
                string name = typeName.Array != null ? Encoding.UTF8.GetString(typeName.Array, typeName.Offset, typeName.Count) : null;

                throw new ArgumentException($"Type not found by the specified type name: '{name}'.", nameof(type));
            }

            if (!(Resolver.TryGetFormatter(type, out IJsonFormatter result) && result is IJsonFormatter<object> formatter))
            {
                throw new ArgumentException($"Formatter not found for specified type: '{type}'.");
            }

            return (TTarget)formatter.Deserialize(ref reader, formatterResolver);
        }

        private int WriteTypeNameSpace(ref JsonWriter writer, byte[] typeName)
        {
            int length = m_typePropertyNameBytes.Length + typeName.Length + 3;
            int position = writer.CurrentOffset;

            writer.EnsureCapacity(length + 1);
            writer.AdvanceOffset(length);

            return position;
        }

        private void WriteTypeName(ref JsonWriter writer, byte[] typeName, int position)
        {
            int current = writer.CurrentOffset;

            writer.CurrentOffset = position;
            writer.WriteBeginObject();
            writer.WriteRaw(m_typePropertyNameBytes);
            writer.WriteQuotation();
            writer.WriteRaw(typeName);
            writer.WriteQuotation();
            writer.WriteValueSeparator();
            writer.CurrentOffset = current;
        }

        private bool TryReadTypeName(JsonReader reader, out ArraySegment<byte> typeName)
        {
            int count = 0;

            while (reader.ReadIsInObject(ref count))
            {
                ArraySegment<byte> propertyName = reader.ReadPropertyNameSegmentRaw();

                if (ByteArrayComparer.Equals(propertyName, m_typePropertyNameValue))
                {
                    typeName = reader.ReadStringSegmentRaw();
                    return true;
                }

                reader.ReadNextBlock();
            }

            typeName = default;
            return false;
        }
    }
}
