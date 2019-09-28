using System;
using System.Collections.Generic;
using Utf8Json;

namespace UGF.Utf8Json.Runtime.Formatters.Union
{
    /// <summary>
    /// Represents union formatter implementation with the specified target type.
    /// </summary>
    public class UnionFormatter<TTarget> : UnionFormatter, IUnionFormatter<TTarget>, IJsonFormatter<TTarget>
    {
        /// <summary>
        /// Creates union formatter with the specified serializer.
        /// </summary>
        /// <param name="unionSerializer">The union serializer.</param>
        public UnionFormatter(IUnionSerializer unionSerializer) : base(unionSerializer)
        {
        }

        /// <summary>
        /// Adds automatically created formatter wrapper for the specified target type.
        /// </summary>
        /// <param name="typeIdentifier">The identifier of the type.</param>
        public void AddFormatter<T>(string typeIdentifier) where T : TTarget
        {
            if (typeIdentifier == null) throw new ArgumentNullException(nameof(typeIdentifier));

            var formatter = new UnionFormatterWrapper<T, TTarget>();

            AddFormatter(typeof(T), typeIdentifier, formatter);
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

                UnionSerializer.Serialize(ref writer, value, formatter, formatterResolver);
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
                int identifier = UnionSerializer.ReadTypeIdentifier(reader);

                if (!TryGetFormatter(identifier, out IJsonFormatter<TTarget> formatter))
                {
                    throw new ArgumentException($"The formatter for the specified identifier not found: '{identifier}'.", nameof(identifier));
                }

                return UnionSerializer.Deserialize(ref reader, formatter, formatterResolver);
            }

            return default;
        }
    }
}
