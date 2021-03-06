﻿// ReSharper disable all

using System;

namespace Utf8Json
{
    public delegate void JsonSerializeAction<T>(ref JsonWriter writer, T value, IJsonFormatterResolver resolver);

    public delegate T JsonDeserializeFunc<T>(ref JsonReader reader, IJsonFormatterResolver resolver);

    public interface IJsonFormatter
    {
    }

    public interface IJsonFormatter<T> : IJsonFormatter
    {
        void Serialize(ref JsonWriter writer, T value, IJsonFormatterResolver formatterResolver);
        T Deserialize(ref JsonReader reader, IJsonFormatterResolver formatterResolver);
    }

    public interface IObjectPropertyNameFormatter<T>
    {
        void SerializeToPropertyName(ref JsonWriter writer, T value, IJsonFormatterResolver formatterResolver);
        T DeserializeFromPropertyName(ref JsonReader reader, IJsonFormatterResolver formatterResolver);
    }

    public abstract class JsonFormatterBase<T> : JsonFormatterAbstract<T>, IJsonFormatter<T>
    {
    }

    public abstract class JsonFormatterAbstract<T> : IJsonFormatter<object>
    {
        public abstract void Serialize(ref JsonWriter writer, T value, IJsonFormatterResolver formatterResolver);
        public abstract T Deserialize(ref JsonReader reader, IJsonFormatterResolver formatterResolver);

        void IJsonFormatter<object>.Serialize(ref JsonWriter writer, object value, IJsonFormatterResolver formatterResolver)
        {
            Serialize(ref writer, (T)value, formatterResolver);
        }

        object IJsonFormatter<object>.Deserialize(ref JsonReader reader, IJsonFormatterResolver formatterResolver)
        {
            return Deserialize(ref reader, formatterResolver);
        }
    }

    public static class JsonFormatterExtensions
    {
        public static string ToJsonString<T>(this IJsonFormatter<T> formatter, T value, IJsonFormatterResolver formatterResolver)
        {
            var writer = new JsonWriter();
            formatter.Serialize(ref writer, value, formatterResolver);
            return writer.ToString();
        }
    }
}
