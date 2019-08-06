// THIS IS A GENERATED CODE. DO NOT EDIT.
// ReSharper disable all

namespace TestAssembly.Resolvers
{
    using System;
    using Utf8Json;

    public class TestAssemblyResolver : global::Utf8Json.IJsonFormatterResolver
    {
        public static readonly global::Utf8Json.IJsonFormatterResolver Instance = new TestAssemblyResolver();

        TestAssemblyResolver()
        {

        }

        public global::Utf8Json.IJsonFormatter<T> GetFormatter<T>()
        {
            return FormatterCache<T>.formatter;
        }

        static class FormatterCache<T>
        {
            public static readonly global::Utf8Json.IJsonFormatter<T> formatter;

            static FormatterCache()
            {
                var f = TestAssemblyResolverGetFormatterHelper.GetFormatter(typeof(T));
                if (f != null)
                {
                    formatter = (global::Utf8Json.IJsonFormatter<T>)f;
                }
            }
        }
    }

    internal static class TestAssemblyResolverGetFormatterHelper
    {
        static readonly global::System.Collections.Generic.Dictionary<Type, int> lookup;

        static TestAssemblyResolverGetFormatterHelper()
        {
            lookup = new global::System.Collections.Generic.Dictionary<Type, int>(2)
            {
                {typeof(global::UnityEngine.HideFlags), 0 },
                {typeof(global::UGF.Utf8Json.Runtime.Tests.TestAssembly.TestTarget), 1 },
            };
        }

        internal static object GetFormatter(Type t)
        {
            int key;
            if (!lookup.TryGetValue(t, out key)) return null;

            switch (key)
            {
                case 0: return new TestAssembly.Formatters.UnityEngine.HideFlagsFormatter();
                case 1: return new TestAssembly.Formatters.UGF.Utf8Json.Runtime.Tests.TestAssembly.TestTargetFormatter();
                default: return null;
            }
        }
    }
}

#pragma warning disable 168
#pragma warning restore 414
#pragma warning restore 618
#pragma warning restore 612

#pragma warning disable 618
#pragma warning disable 612
#pragma warning disable 414
#pragma warning disable 168

namespace TestAssembly.Formatters.UnityEngine
{
    using System;
    using Utf8Json;

    public sealed class HideFlagsFormatter : global::Utf8Json.IJsonFormatter<global::UnityEngine.HideFlags>
    {
        public void Serialize(ref global::Utf8Json.JsonWriter writer, global::UnityEngine.HideFlags value, global::Utf8Json.IJsonFormatterResolver formatterResolver)
        {
            writer.WriteInt32((Int32)value);
        }
        
        public global::UnityEngine.HideFlags Deserialize(ref global::Utf8Json.JsonReader reader, global::Utf8Json.IJsonFormatterResolver formatterResolver)
        {
            return (global::UnityEngine.HideFlags)reader.ReadInt32();
        }
    }


}

#pragma warning restore 168
#pragma warning restore 414
#pragma warning restore 618
#pragma warning restore 612

#pragma warning disable 618
#pragma warning disable 612
#pragma warning disable 414
#pragma warning disable 219
#pragma warning disable 168

namespace TestAssembly.Formatters.UGF.Utf8Json.Runtime.Tests.TestAssembly
{
    using System;
    using Utf8Json;


    public sealed class TestTargetFormatter : global::Utf8Json.IJsonFormatter<global::UGF.Utf8Json.Runtime.Tests.TestAssembly.TestTarget>
    {
        readonly global::Utf8Json.Internal.AutomataDictionary ____keyMapping;
        readonly byte[][] ____stringByteKeys;

        public TestTargetFormatter()
        {
            this.____keyMapping = new global::Utf8Json.Internal.AutomataDictionary()
            {
                { global::Utf8Json.JsonWriter.GetEncodedPropertyNameWithoutQuotation("Name"), 0},
                { global::Utf8Json.JsonWriter.GetEncodedPropertyNameWithoutQuotation("BoolValue"), 1},
                { global::Utf8Json.JsonWriter.GetEncodedPropertyNameWithoutQuotation("FloatValue"), 2},
                { global::Utf8Json.JsonWriter.GetEncodedPropertyNameWithoutQuotation("IntValue"), 3},
                { global::Utf8Json.JsonWriter.GetEncodedPropertyNameWithoutQuotation("Vector2"), 4},
                { global::Utf8Json.JsonWriter.GetEncodedPropertyNameWithoutQuotation("Bounds"), 5},
                { global::Utf8Json.JsonWriter.GetEncodedPropertyNameWithoutQuotation("Flags"), 6},
            };

            this.____stringByteKeys = new byte[][]
            {
                global::Utf8Json.JsonWriter.GetEncodedPropertyNameWithBeginObject("Name"),
                global::Utf8Json.JsonWriter.GetEncodedPropertyNameWithPrefixValueSeparator("BoolValue"),
                global::Utf8Json.JsonWriter.GetEncodedPropertyNameWithPrefixValueSeparator("FloatValue"),
                global::Utf8Json.JsonWriter.GetEncodedPropertyNameWithPrefixValueSeparator("IntValue"),
                global::Utf8Json.JsonWriter.GetEncodedPropertyNameWithPrefixValueSeparator("Vector2"),
                global::Utf8Json.JsonWriter.GetEncodedPropertyNameWithPrefixValueSeparator("Bounds"),
                global::Utf8Json.JsonWriter.GetEncodedPropertyNameWithPrefixValueSeparator("Flags"),
                
            };
        }

        public void Serialize(ref global::Utf8Json.JsonWriter writer, global::UGF.Utf8Json.Runtime.Tests.TestAssembly.TestTarget value, global::Utf8Json.IJsonFormatterResolver formatterResolver)
        {
            if (value == null)
            {
                writer.WriteNull();
                return;
            }
            

            writer.WriteRaw(this.____stringByteKeys[0]);
            writer.WriteString(value.Name);
            writer.WriteRaw(this.____stringByteKeys[1]);
            writer.WriteBoolean(value.BoolValue);
            writer.WriteRaw(this.____stringByteKeys[2]);
            writer.WriteSingle(value.FloatValue);
            writer.WriteRaw(this.____stringByteKeys[3]);
            writer.WriteInt32(value.IntValue);
            writer.WriteRaw(this.____stringByteKeys[4]);
            global::Utf8Json.JsonFormatterResolverExtensions.GetFormatterWithVerify<global::UnityEngine.Vector2>(formatterResolver).Serialize(ref writer, value.Vector2, formatterResolver);
            writer.WriteRaw(this.____stringByteKeys[5]);
            global::Utf8Json.JsonFormatterResolverExtensions.GetFormatterWithVerify<global::UnityEngine.Bounds>(formatterResolver).Serialize(ref writer, value.Bounds, formatterResolver);
            writer.WriteRaw(this.____stringByteKeys[6]);
            global::Utf8Json.JsonFormatterResolverExtensions.GetFormatterWithVerify<global::UnityEngine.HideFlags>(formatterResolver).Serialize(ref writer, value.Flags, formatterResolver);
            
            writer.WriteEndObject();
        }

        public global::UGF.Utf8Json.Runtime.Tests.TestAssembly.TestTarget Deserialize(ref global::Utf8Json.JsonReader reader, global::Utf8Json.IJsonFormatterResolver formatterResolver)
        {
            if (reader.ReadIsNull())
            {
                return null;
            }
            

            var __Name__ = default(string);
            var __Name__b__ = false;
            var __BoolValue__ = default(bool);
            var __BoolValue__b__ = false;
            var __FloatValue__ = default(float);
            var __FloatValue__b__ = false;
            var __IntValue__ = default(int);
            var __IntValue__b__ = false;
            var __Vector2__ = default(global::UnityEngine.Vector2);
            var __Vector2__b__ = false;
            var __Bounds__ = default(global::UnityEngine.Bounds);
            var __Bounds__b__ = false;
            var __Flags__ = default(global::UnityEngine.HideFlags);
            var __Flags__b__ = false;

            var ____count = 0;
            reader.ReadIsBeginObjectWithVerify();
            while (!reader.ReadIsEndObjectWithSkipValueSeparator(ref ____count))
            {
                var stringKey = reader.ReadPropertyNameSegmentRaw();
                int key;
                if (!____keyMapping.TryGetValue(stringKey, out key))
                {
                    reader.ReadNextBlock();
                    goto NEXT_LOOP;
                }

                switch (key)
                {
                    case 0:
                        __Name__ = reader.ReadString();
                        __Name__b__ = true;
                        break;
                    case 1:
                        __BoolValue__ = reader.ReadBoolean();
                        __BoolValue__b__ = true;
                        break;
                    case 2:
                        __FloatValue__ = reader.ReadSingle();
                        __FloatValue__b__ = true;
                        break;
                    case 3:
                        __IntValue__ = reader.ReadInt32();
                        __IntValue__b__ = true;
                        break;
                    case 4:
                        __Vector2__ = global::Utf8Json.JsonFormatterResolverExtensions.GetFormatterWithVerify<global::UnityEngine.Vector2>(formatterResolver).Deserialize(ref reader, formatterResolver);
                        __Vector2__b__ = true;
                        break;
                    case 5:
                        __Bounds__ = global::Utf8Json.JsonFormatterResolverExtensions.GetFormatterWithVerify<global::UnityEngine.Bounds>(formatterResolver).Deserialize(ref reader, formatterResolver);
                        __Bounds__b__ = true;
                        break;
                    case 6:
                        __Flags__ = global::Utf8Json.JsonFormatterResolverExtensions.GetFormatterWithVerify<global::UnityEngine.HideFlags>(formatterResolver).Deserialize(ref reader, formatterResolver);
                        __Flags__b__ = true;
                        break;
                    default:
                        reader.ReadNextBlock();
                        break;
                }

                NEXT_LOOP:
                continue;
            }

            var ____result = new global::UGF.Utf8Json.Runtime.Tests.TestAssembly.TestTarget();
            if(__Name__b__) ____result.Name = __Name__;
            if(__BoolValue__b__) ____result.BoolValue = __BoolValue__;
            if(__FloatValue__b__) ____result.FloatValue = __FloatValue__;
            if(__IntValue__b__) ____result.IntValue = __IntValue__;
            if(__Vector2__b__) ____result.Vector2 = __Vector2__;
            if(__Bounds__b__) ____result.Bounds = __Bounds__;
            if(__Flags__b__) ____result.Flags = __Flags__;

            return ____result;
        }
    }

}

#pragma warning disable 168
#pragma warning restore 219
#pragma warning restore 414
#pragma warning restore 618
#pragma warning restore 612
