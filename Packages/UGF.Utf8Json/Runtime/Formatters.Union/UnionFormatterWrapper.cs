using Utf8Json;

namespace UGF.Utf8Json.Runtime.Formatters.Union
{
    /// <summary>
    /// Represents formatter which wraps around base type of the target.
    /// </summary>
    public class UnionFormatterWrapper<TTarget, TBase> : UnionFormatterWrapperBase, IJsonFormatter<TBase> where TTarget : TBase
    {
        public void Serialize(ref JsonWriter writer, TBase value, IJsonFormatterResolver formatterResolver)
        {
            formatterResolver.GetFormatter<TTarget>().Serialize(ref writer, (TTarget)value, formatterResolver);
        }

        public new TBase Deserialize(ref JsonReader reader, IJsonFormatterResolver formatterResolver)
        {
            return formatterResolver.GetFormatter<TTarget>().Deserialize(ref reader, formatterResolver);
        }

        protected override void OnSerialize(ref JsonWriter writer, object value, IJsonFormatterResolver formatterResolver)
        {
            formatterResolver.GetFormatter<TTarget>().Serialize(ref writer, (TTarget)value, formatterResolver);
        }

        protected override object OnDeserialize(ref JsonReader reader, IJsonFormatterResolver formatterResolver)
        {
            return formatterResolver.GetFormatter<TTarget>().Deserialize(ref reader, formatterResolver);
        }
    }
}
