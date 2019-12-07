using System;
using Utf8Json;

namespace UGF.Utf8Json.Runtime.Formatters.Typed
{
    public class TypedFormatter<TTarget> : IJsonFormatter<TTarget>
    {
        public IUtf8JsonFormatterResolver Resolver { get; }
        public string TypePropertyName { get; set; } = "type";

        public TypedFormatter(IUtf8JsonFormatterResolver resolver)
        {
            Resolver = resolver;
        }

        public void Serialize(ref JsonWriter writer, TTarget value, IJsonFormatterResolver formatterResolver)
        {
            Type type = value.GetType();

            if (type != typeof(TTarget))
            {
                if (!(Resolver.TryGetFormatter(type, out IJsonFormatter result) && result is IJsonFormatter<object> formatter))
                {
                    throw new ArgumentException($"Formatter not found for specified type: '{type}'.");
                }

                string typeName = type.AssemblyQualifiedName;
                int position = WriteTypeNameSpace(ref writer, typeName);

                formatter.Serialize(ref writer, value, formatterResolver);

                WriteTypeName(ref writer, typeName, position);
            }
            else
            {
                Resolver.GetFormatter<TTarget>().Serialize(ref writer, value, formatterResolver);
            }
        }

        public TTarget Deserialize(ref JsonReader reader, IJsonFormatterResolver formatterResolver)
        {
            if (TryReadTypeName(reader, out string typeName))
            {
                Type type = !string.IsNullOrEmpty(typeName) ? Type.GetType(typeName) : null;

                if (type == null)
                {
                    throw new ArgumentException($"Type not found by the specified type name: '{typeName}'.", nameof(type));
                }

                if (!(Resolver.TryGetFormatter(type, out IJsonFormatter result) && result is IJsonFormatter<object> formatter))
                {
                    throw new ArgumentException($"Formatter not found for specified type: '{type}'.");
                }

                return (TTarget)formatter.Deserialize(ref reader, formatterResolver);
            }

            return Resolver.GetFormatter<TTarget>().Deserialize(ref reader, formatterResolver);
        }

        private int WriteTypeNameSpace(ref JsonWriter writer, string typeName)
        {
            int length = TypePropertyName.Length + typeName.Length + 3;
            int position = writer.CurrentOffset;

            writer.EnsureCapacity(length + 1);
            writer.AdvanceOffset(length);

            return position;
        }

        private void WriteTypeName(ref JsonWriter writer, string typeName, int position)
        {
            int current = writer.CurrentOffset;

            writer.CurrentOffset = position;
            writer.WriteBeginObject();
            writer.WriteString(TypePropertyName);
            writer.WriteQuotation();
            writer.WriteString(typeName);
            writer.WriteQuotation();
            writer.WriteValueSeparator();
            writer.CurrentOffset = current;
        }

        private bool TryReadTypeName(JsonReader reader, out string typeName)
        {
            int count = 0;

            while (reader.ReadIsInObject(ref count))
            {
                string propertyName = reader.ReadPropertyName();

                if (propertyName.Equals(TypePropertyName, StringComparison.Ordinal))
                {
                    typeName = reader.ReadString();
                    return true;
                }

                reader.ReadNextBlock();
            }

            typeName = null;
            return false;
        }
    }
}
