#pragma warning disable 618
#pragma warning disable 612
#pragma warning disable 414
#pragma warning disable 219
#pragma warning disable 168

using Utf8Json;

// ReSharper disable all

namespace UGF.Utf8Json.Editor.Tests.TestEditorAssembly.Formatters.UGF.Utf8Json.Editor.Tests.TestEditorAssembly
{
    using System;
    using Utf8Json;


    public sealed class TestEditorTargetFormatter : global::Utf8Json.IJsonFormatter<global::UGF.Utf8Json.Editor.Tests.TestEditorAssembly.TestEditorTarget>
    {
        readonly global::Utf8Json.Internal.AutomataDictionary ____keyMapping;
        readonly byte[][] ____stringByteKeys;

        public TestEditorTargetFormatter()
        {
            this.____keyMapping = new global::Utf8Json.Internal.AutomataDictionary()
            {
                { JsonWriter.GetEncodedPropertyNameWithoutQuotation("BoolValue"), 0},
                { JsonWriter.GetEncodedPropertyNameWithoutQuotation("StringValue"), 1},
                { JsonWriter.GetEncodedPropertyNameWithoutQuotation("Vector2"), 2},
            };

            this.____stringByteKeys = new byte[][]
            {
                JsonWriter.GetEncodedPropertyNameWithBeginObject("BoolValue"),
                JsonWriter.GetEncodedPropertyNameWithPrefixValueSeparator("StringValue"),
                JsonWriter.GetEncodedPropertyNameWithPrefixValueSeparator("Vector2"),
                
            };
        }

        public void Serialize(ref JsonWriter writer, global::UGF.Utf8Json.Editor.Tests.TestEditorAssembly.TestEditorTarget value, global::Utf8Json.IJsonFormatterResolver formatterResolver)
        {
            if (value == null)
            {
                writer.WriteNull();
                return;
            }
            

            writer.WriteRaw(this.____stringByteKeys[0]);
            writer.WriteBoolean(value.BoolValue);
            writer.WriteRaw(this.____stringByteKeys[1]);
            writer.WriteString(value.StringValue);
            writer.WriteRaw(this.____stringByteKeys[2]);
            formatterResolver.GetFormatterWithVerify<UnityEngine.Vector2>().Serialize(ref writer, value.Vector2, formatterResolver);
            
            writer.WriteEndObject();
        }

        public global::UGF.Utf8Json.Editor.Tests.TestEditorAssembly.TestEditorTarget Deserialize(ref JsonReader reader, global::Utf8Json.IJsonFormatterResolver formatterResolver)
        {
            if (reader.ReadIsNull())
            {
                return null;
            }
            

            var __BoolValue__ = default(bool);
            var __BoolValue__b__ = false;
            var __StringValue__ = default(string);
            var __StringValue__b__ = false;
            var __Vector2__ = default(UnityEngine.Vector2);
            var __Vector2__b__ = false;

            var ____count = 0;
            reader.ReadIsBeginObjectWithVerify();
            while (!reader.ReadIsEndObjectWithSkipValueSeparator(ref ____count))
            {
                var stringKey = reader.ReadPropertyNameSegmentRaw();
                int key;
                if (!____keyMapping.TryGetValueSafe(stringKey, out key))
                {
                    reader.ReadNextBlock();
                    goto NEXT_LOOP;
                }

                switch (key)
                {
                    case 0:
                        __BoolValue__ = reader.ReadBoolean();
                        __BoolValue__b__ = true;
                        break;
                    case 1:
                        __StringValue__ = reader.ReadString();
                        __StringValue__b__ = true;
                        break;
                    case 2:
                        __Vector2__ = formatterResolver.GetFormatterWithVerify<UnityEngine.Vector2>().Deserialize(ref reader, formatterResolver);
                        __Vector2__b__ = true;
                        break;
                    default:
                        reader.ReadNextBlock();
                        break;
                }

                NEXT_LOOP:
                continue;
            }

            var ____result = new global::UGF.Utf8Json.Editor.Tests.TestEditorAssembly.TestEditorTarget();
            if(__BoolValue__b__) ____result.BoolValue = __BoolValue__;
            if(__StringValue__b__) ____result.StringValue = __StringValue__;
            if(__Vector2__b__) ____result.Vector2 = __Vector2__;

            return ____result;
        }
    }

}

#pragma warning disable 168
#pragma warning restore 219
#pragma warning restore 414
#pragma warning restore 618
#pragma warning restore 612