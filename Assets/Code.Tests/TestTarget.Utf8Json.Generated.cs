#pragma warning disable 618
#pragma warning disable 612
#pragma warning disable 414
#pragma warning disable 219
#pragma warning disable 168

using Utf8Json;

// ReSharper disable all

namespace Code.Tests.Formatters.Code.Tests
{
    using System;
    using Utf8Json;


    public sealed class TestTargetFormatter : global::Utf8Json.IJsonFormatter<global::Code.Tests.TestTarget>
    {
        readonly global::Utf8Json.Internal.AutomataDictionary ____keyMapping;
        readonly byte[][] ____stringByteKeys;

        public TestTargetFormatter()
        {
            this.____keyMapping = new global::Utf8Json.Internal.AutomataDictionary()
            {
                { JsonWriter.GetEncodedPropertyNameWithoutQuotation("Bool"), 0},
                { JsonWriter.GetEncodedPropertyNameWithoutQuotation("Int"), 1},
                { JsonWriter.GetEncodedPropertyNameWithoutQuotation("Float"), 2},
                { JsonWriter.GetEncodedPropertyNameWithoutQuotation("Long"), 3},
            };

            this.____stringByteKeys = new byte[][]
            {
                JsonWriter.GetEncodedPropertyNameWithBeginObject("Bool"),
                JsonWriter.GetEncodedPropertyNameWithPrefixValueSeparator("Int"),
                JsonWriter.GetEncodedPropertyNameWithPrefixValueSeparator("Float"),
                JsonWriter.GetEncodedPropertyNameWithPrefixValueSeparator("Long"),
                
            };
        }

        public void Serialize(ref JsonWriter writer, global::Code.Tests.TestTarget value, global::Utf8Json.IJsonFormatterResolver formatterResolver)
        {
            if (value == null)
            {
                writer.WriteNull();
                return;
            }
            

            writer.WriteRaw(this.____stringByteKeys[0]);
            writer.WriteBoolean(value.Bool);
            writer.WriteRaw(this.____stringByteKeys[1]);
            writer.WriteInt32(value.Int);
            writer.WriteRaw(this.____stringByteKeys[2]);
            writer.WriteSingle(value.Float);
            writer.WriteRaw(this.____stringByteKeys[3]);
            writer.WriteInt64(value.Long);
            
            writer.WriteEndObject();
        }

        public global::Code.Tests.TestTarget Deserialize(ref JsonReader reader, global::Utf8Json.IJsonFormatterResolver formatterResolver)
        {
            if (reader.ReadIsNull())
            {
                return null;
            }
            

            var __Bool__ = default(bool);
            var __Bool__b__ = false;
            var __Int__ = default(int);
            var __Int__b__ = false;
            var __Float__ = default(float);
            var __Float__b__ = false;
            var __Long__ = default(long);
            var __Long__b__ = false;

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
                        __Bool__ = reader.ReadBoolean();
                        __Bool__b__ = true;
                        break;
                    case 1:
                        __Int__ = reader.ReadInt32();
                        __Int__b__ = true;
                        break;
                    case 2:
                        __Float__ = reader.ReadSingle();
                        __Float__b__ = true;
                        break;
                    case 3:
                        __Long__ = reader.ReadInt64();
                        __Long__b__ = true;
                        break;
                    default:
                        reader.ReadNextBlock();
                        break;
                }

                NEXT_LOOP:
                continue;
            }

            var ____result = new global::Code.Tests.TestTarget();
            if(__Bool__b__) ____result.Bool = __Bool__;
            if(__Int__b__) ____result.Int = __Int__;
            if(__Float__b__) ____result.Float = __Float__;
            if(__Long__b__) ____result.Long = __Long__;

            return ____result;
        }
    }

}

#pragma warning disable 168
#pragma warning restore 219
#pragma warning restore 414
#pragma warning restore 618
#pragma warning restore 612
