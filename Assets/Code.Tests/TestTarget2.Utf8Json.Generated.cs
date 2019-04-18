#pragma warning disable 618
#pragma warning disable 612
#pragma warning disable 414
#pragma warning disable 219
#pragma warning disable 168

using Utf8Json;

// ReSharper disable all

namespace Code.Tests
{
    public sealed class TestTarget2Formatter : global::Utf8Json.IJsonFormatter<global::Code.Tests.TestTarget2>
    {
        readonly global::Utf8Json.Internal.AutomataDictionary ____keyMapping;
        readonly byte[][] ____stringByteKeys;

        public TestTarget2Formatter()
        {
            this.____keyMapping = new global::Utf8Json.Internal.AutomataDictionary()
            {
                { JsonWriter.GetEncodedPropertyNameWithoutQuotation("Value"), 0},
            };

            this.____stringByteKeys = new byte[][]
            {
                JsonWriter.GetEncodedPropertyNameWithBeginObject("Value"),
                
            };
        }

        public void Serialize(ref JsonWriter writer, global::Code.Tests.TestTarget2 value, global::Utf8Json.IJsonFormatterResolver formatterResolver)
        {
            if (value == null)
            {
                writer.WriteNull();
                return;
            }
            

            writer.WriteRaw(this.____stringByteKeys[0]);
            writer.WriteBoolean(value.Value);
            
            writer.WriteEndObject();
        }

        public global::Code.Tests.TestTarget2 Deserialize(ref JsonReader reader, global::Utf8Json.IJsonFormatterResolver formatterResolver)
        {
            if (reader.ReadIsNull())
            {
                return null;
            }
            

            var __Value__ = default(bool);
            var __Value__b__ = false;

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
                        __Value__ = reader.ReadBoolean();
                        __Value__b__ = true;
                        break;
                    default:
                        reader.ReadNextBlock();
                        break;
                }

                NEXT_LOOP:
                continue;
            }

            var ____result = new global::Code.Tests.TestTarget2();
            if(__Value__b__) ____result.Value = __Value__;

            return ____result;
        }
    }

}

#pragma warning disable 168
#pragma warning restore 219
#pragma warning restore 414
#pragma warning restore 618
#pragma warning restore 612
