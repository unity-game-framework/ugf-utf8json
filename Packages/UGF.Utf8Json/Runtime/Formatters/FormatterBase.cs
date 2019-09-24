using Utf8Json;

namespace UGF.Utf8Json.Runtime.Formatters
{
    public abstract class FormatterBase : IJsonFormatter<object>
    {
        protected abstract void OnSerializeTypeless(ref JsonWriter writer, object value, IJsonFormatterResolver formatterResolver);
        protected abstract object OnDeserializeTypeless(ref JsonReader reader, IJsonFormatterResolver formatterResolver);

        void IJsonFormatter<object>.Serialize(ref JsonWriter writer, object value, IJsonFormatterResolver formatterResolver)
        {
            OnSerializeTypeless(ref writer, value, formatterResolver);
        }

        object IJsonFormatter<object>.Deserialize(ref JsonReader reader, IJsonFormatterResolver formatterResolver)
        {
            return OnDeserializeTypeless(ref reader, formatterResolver);
        }
    }
}
