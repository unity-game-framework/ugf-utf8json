// THIS IS A GENERATED CODE. DO NOT EDIT.
// ReSharper disable all

namespace UGF.Utf8Json.Editor.Tests.Formatters.UGF.Utf8Json.Editor.Tests.TestEditorAssembly
{
    using System;
    using Utf8Json;


    [global::UGF.Utf8Json.Runtime.Utf8JsonFormatterAttribute]
    public sealed class TestEditorTargetExternalFormatter : global::Utf8Json.IJsonFormatter<global::UGF.Utf8Json.Editor.Tests.TestEditorAssembly.TestEditorTargetExternal>
    {
        readonly global::Utf8Json.Internal.AutomataDictionary ____keyMapping;
        readonly byte[][] ____stringByteKeys;

        public TestEditorTargetExternalFormatter()
        {
            this.____keyMapping = new global::Utf8Json.Internal.AutomataDictionary()
            {
                { global::Utf8Json.JsonWriter.GetEncodedPropertyNameWithoutQuotation("BoolValue"), 0},
                { global::Utf8Json.JsonWriter.GetEncodedPropertyNameWithoutQuotation("StringValue"), 1},
                { global::Utf8Json.JsonWriter.GetEncodedPropertyNameWithoutQuotation("Vector2"), 2},
                { global::Utf8Json.JsonWriter.GetEncodedPropertyNameWithoutQuotation("FloatValue"), 3},
                { global::Utf8Json.JsonWriter.GetEncodedPropertyNameWithoutQuotation("Bounds"), 4},
            };

            this.____stringByteKeys = new byte[][]
            {
                global::Utf8Json.JsonWriter.GetEncodedPropertyNameWithBeginObject("BoolValue"),
                global::Utf8Json.JsonWriter.GetEncodedPropertyNameWithPrefixValueSeparator("StringValue"),
                global::Utf8Json.JsonWriter.GetEncodedPropertyNameWithPrefixValueSeparator("Vector2"),
                global::Utf8Json.JsonWriter.GetEncodedPropertyNameWithPrefixValueSeparator("FloatValue"),
                global::Utf8Json.JsonWriter.GetEncodedPropertyNameWithPrefixValueSeparator("Bounds"),
                
            };
        }

        public void Serialize(ref global::Utf8Json.JsonWriter writer, global::UGF.Utf8Json.Editor.Tests.TestEditorAssembly.TestEditorTargetExternal value, global::Utf8Json.IJsonFormatterResolver formatterResolver)
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
            global::Utf8Json.JsonFormatterResolverExtensions.GetFormatterWithVerify<UnityEngine.Vector2>(formatterResolver).Serialize(ref writer, value.Vector2, formatterResolver);
            writer.WriteRaw(this.____stringByteKeys[3]);
            writer.WriteSingle(value.FloatValue);
            writer.WriteRaw(this.____stringByteKeys[4]);
            global::Utf8Json.JsonFormatterResolverExtensions.GetFormatterWithVerify<UnityEngine.Bounds>(formatterResolver).Serialize(ref writer, value.Bounds, formatterResolver);
            
            writer.WriteEndObject();
        }

        public global::UGF.Utf8Json.Editor.Tests.TestEditorAssembly.TestEditorTargetExternal Deserialize(ref global::Utf8Json.JsonReader reader, global::Utf8Json.IJsonFormatterResolver formatterResolver)
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
            var __FloatValue__ = default(float);
            var __FloatValue__b__ = false;
            var __Bounds__ = default(UnityEngine.Bounds);
            var __Bounds__b__ = false;

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
                        __Vector2__ = global::Utf8Json.JsonFormatterResolverExtensions.GetFormatterWithVerify<UnityEngine.Vector2>(formatterResolver).Deserialize(ref reader, formatterResolver);
                        __Vector2__b__ = true;
                        break;
                    case 3:
                        __FloatValue__ = reader.ReadSingle();
                        __FloatValue__b__ = true;
                        break;
                    case 4:
                        __Bounds__ = global::Utf8Json.JsonFormatterResolverExtensions.GetFormatterWithVerify<UnityEngine.Bounds>(formatterResolver).Deserialize(ref reader, formatterResolver);
                        __Bounds__b__ = true;
                        break;
                    default:
                        reader.ReadNextBlock();
                        break;
                }

                NEXT_LOOP:
                continue;
            }

            var ____result = new global::UGF.Utf8Json.Editor.Tests.TestEditorAssembly.TestEditorTargetExternal();
            if(__BoolValue__b__) ____result.BoolValue = __BoolValue__;
            if(__StringValue__b__) ____result.StringValue = __StringValue__;
            if(__Vector2__b__) ____result.Vector2 = __Vector2__;
            if(__FloatValue__b__) ____result.FloatValue = __FloatValue__;
            if(__Bounds__b__) ____result.Bounds = __Bounds__;

            return ____result;
        }
    }

}

#pragma warning disable 168
#pragma warning restore 219
#pragma warning restore 414
#pragma warning restore 618
#pragma warning restore 612
