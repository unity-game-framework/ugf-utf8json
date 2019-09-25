using System;
using System.Collections.Generic;
using Utf8Json;

namespace UGF.Utf8Json.Runtime.Formatters
{
    /// <summary>
    /// Represents union formatter.
    /// </summary>
    public class UnionFormatter<TTarget> : UnionFormatter, IJsonFormatter<TTarget>
    {
        public UnionFormatter(string typePropertyName = "type") : base(typePropertyName)
        {
        }

        public void AddFormatter<T>(string typeIdentifier) where T : TTarget
        {
            var formatter = new UnionFormatterWrapper<T, TTarget>();

            base.AddFormatter(typeIdentifier, typeof(T), formatter);
        }

        public override void AddFormatter(string typeIdentifier, Type targetType, IJsonFormatter formatter)
        {
            if (formatter == null) throw new ArgumentNullException(nameof(formatter));

            if (!(formatter is IJsonFormatter<TTarget>))
            {
                throw new ArgumentException($"The type of the formatter target is invalid: '{formatter}'.", nameof(formatter));
            }

            base.AddFormatter(typeIdentifier, targetType, formatter);
        }

        public void Serialize(ref JsonWriter writer, TTarget value, IJsonFormatterResolver formatterResolver)
        {
            if (formatterResolver == null) throw new ArgumentNullException(nameof(formatterResolver));

            if (!EqualityComparer<TTarget>.Default.Equals(value, default))
            {
                Type targetType = value.GetType();

                if (!TryGetFormatter(targetType, out IJsonFormatter<TTarget> formatter))
                {
                    throw new ArgumentException($"The formatter for the specified type not found: '{targetType}'.", nameof(targetType));
                }

                int identifier = GetFormatterIdentifier(targetType);
                JsonWriter typeWriter = WriteTypeIdentifierSpace(ref writer, identifier);

                formatter.Serialize(ref writer, value, formatterResolver);

                WriteTypeIdentifier(typeWriter, identifier);
            }
            else
            {
                writer.WriteNull();
            }
        }

        public new TTarget Deserialize(ref JsonReader reader, IJsonFormatterResolver formatterResolver)
        {
            if (formatterResolver == null) throw new ArgumentNullException(nameof(formatterResolver));

            if (!reader.ReadIsNull())
            {
                int identifier = ReadTypeIdentifier(reader);

                if (!TryGetFormatter(identifier, out IJsonFormatter<TTarget> formatter))
                {
                    throw new ArgumentException($"The formatter for the specified identifier not found: '{identifier}'.", nameof(identifier));
                }

                return formatter.Deserialize(ref reader, formatterResolver);
            }

            return default;
        }
    }
}
