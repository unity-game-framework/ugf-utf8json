using System;
using Utf8Json;

namespace UGF.Utf8Json.Runtime.Formatters
{
    public interface IUnionSerializer
    {
        string TypePropertyName { get; }

        int Add(Type targetType, string typeIdentifier);
        bool Remove(Type targetType);
        void Clear();
        int GetIdentifier(Type targetType);
        bool TryGetIdentifier(Type targetType, out int identifier);
        void Serialize<T>(ref JsonWriter writer, T value, IJsonFormatter<T> formatter, IJsonFormatterResolver formatterResolver);
        T Deserialize<T>(ref JsonReader reader, IJsonFormatter<T> formatter, IJsonFormatterResolver formatterResolver);
        int ReadTypeIdentifier(JsonReader reader);
    }
}
