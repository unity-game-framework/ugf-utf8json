using Utf8Json;

namespace UGF.Utf8Json.Runtime.Formatters
{
    public abstract class UnionFormatterWrapperBase : IJsonFormatter<object>
    {
        public void Serialize(ref JsonWriter writer, object value, IJsonFormatterResolver formatterResolver)
        {
            OnSerialize(ref writer, value, formatterResolver);
        }

        public object Deserialize(ref JsonReader reader, IJsonFormatterResolver formatterResolver)
        {
            return OnDeserialize(ref reader, formatterResolver);
        }

        protected abstract void OnSerialize(ref JsonWriter writer, object value, IJsonFormatterResolver formatterResolver);
        protected abstract object OnDeserialize(ref JsonReader reader, IJsonFormatterResolver formatterResolver);
    }
}
