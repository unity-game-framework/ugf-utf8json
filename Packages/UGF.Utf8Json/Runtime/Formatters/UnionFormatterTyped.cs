using System;
using Utf8Json;

namespace UGF.Utf8Json.Runtime.Formatters
{
    public class UnionFormatterTyped<TTarget, TBase> : IJsonFormatter<TBase>, IUnionFormatterTyped where TTarget : TBase
    {
        public Type TargetType { get; } = typeof(TTarget);
        public Type BaseType { get; } = typeof(TBase);

        public void Serialize(ref JsonWriter writer, TBase value, IJsonFormatterResolver formatterResolver)
        {
            formatterResolver.GetFormatter<TTarget>().Serialize(ref writer, (TTarget)value, formatterResolver);
        }

        public TBase Deserialize(ref JsonReader reader, IJsonFormatterResolver formatterResolver)
        {
            return formatterResolver.GetFormatter<TTarget>().Deserialize(ref reader, formatterResolver);
        }
    }
}
