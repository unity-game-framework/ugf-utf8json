using System;
using Utf8Json;

namespace UGF.Utf8Json.Runtime.Formatters.Union
{
    /// <summary>
    /// Represents union serializer.
    /// </summary>
    /// <remarks>
    /// Stores information about types to serialize in specific field.
    /// </remarks>
    public interface IUnionSerializer
    {
        /// <summary>
        /// Gets the name of the property to store type information.
        /// </summary>
        string TypePropertyName { get; }

        /// <summary>
        /// Adds type identifier by the specified target type.
        /// <para>Returns internal id of the target type.</para>
        /// </summary>
        /// <param name="targetType">The type of the target.</param>
        /// <param name="typeIdentifier">The type identifier.</param>
        int Add(Type targetType, string typeIdentifier);

        /// <summary>
        /// Removes type information by the specified target type.
        /// </summary>
        /// <param name="targetType">The type of the target.</param>
        bool Remove(Type targetType);

        /// <summary>
        /// Clears all information.
        /// </summary>
        void Clear();

        /// <summary>
        /// Gets internal identifier for the specified target type.
        /// </summary>
        /// <param name="targetType">The type of the target.</param>
        int GetIdentifier(Type targetType);

        /// <summary>
        /// Tries to get internal identifier for the specified target type.
        /// </summary>
        /// <param name="targetType">The type of the target.</param>
        /// <param name="identifier">The found identifier.</param>
        bool TryGetIdentifier(Type targetType, out int identifier);

        /// <summary>
        /// Serialize the specified value with formatter and resolver, using type information.
        /// </summary>
        /// <remarks>
        /// This method will serialize the specified value with the specified formatter
        /// and additionally will add type information with the specified type property name.
        /// </remarks>
        /// <param name="writer">The writer to use.</param>
        /// <param name="value">The value to serialize.</param>
        /// <param name="formatter">The formatter used to serialize.</param>
        /// <param name="formatterResolver">The formatter resolver.</param>
        void Serialize<T>(ref JsonWriter writer, T value, IJsonFormatter<T> formatter, IJsonFormatterResolver formatterResolver);

        /// <summary>
        /// Deserialize data from the specified reader using the specified formatter.
        /// </summary>
        /// <param name="reader">The reader to use.</param>
        /// <param name="formatter">The formatter.</param>
        /// <param name="formatterResolver">The formatter resolver.</param>
        T Deserialize<T>(ref JsonReader reader, IJsonFormatter<T> formatter, IJsonFormatterResolver formatterResolver);

        /// <summary>
        /// Reads type information from the specified reader and returns internal identifier of the type.
        /// </summary>
        /// <param name="reader">The reader to use.</param>
        int ReadTypeIdentifier(JsonReader reader);
    }
}
