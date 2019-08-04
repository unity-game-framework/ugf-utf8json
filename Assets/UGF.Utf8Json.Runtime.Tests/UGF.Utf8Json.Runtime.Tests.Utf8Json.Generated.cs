// THIS IS A GENERATED CODE. DO NOT EDIT.
// ReSharper disable all

using System;
using System.Collections.Generic;
using NUnit.Framework;
using UnityEngine;
using Utf8Json;
#pragma warning disable 618
#pragma warning disable 612
#pragma warning disable 414
#pragma warning disable 168

namespace UGF.Utf8Json.Runtime.Tests.Resolvers
{
    using System;
    using Utf8Json;

    public class UGFUtf8JsonRuntimeTestsResolver : global::Utf8Json.IJsonFormatterResolver
    {
        public static readonly global::Utf8Json.IJsonFormatterResolver Instance = new UGFUtf8JsonRuntimeTestsResolver();

        UGFUtf8JsonRuntimeTestsResolver()
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
                var f = UGFUtf8JsonRuntimeTestsResolverGetFormatterHelper.GetFormatter(typeof(T));
                if (f != null)
                {
                    formatter = (global::Utf8Json.IJsonFormatter<T>)f;
                }
            }
        }
    }

    internal static class UGFUtf8JsonRuntimeTestsResolverGetFormatterHelper
    {
        static readonly global::System.Collections.Generic.Dictionary<Type, int> lookup;

        static UGFUtf8JsonRuntimeTestsResolverGetFormatterHelper()
        {
            lookup = new global::System.Collections.Generic.Dictionary<Type, int>(4)
            {
                {typeof(global::System.Collections.Generic.List<global::UGF.Utf8Json.Runtime.Tests.TestSerialization.Target2>), 0 },
                {typeof(global::UnityEngine.HideFlags), 1 },
                {typeof(global::UGF.Utf8Json.Runtime.Tests.TestSerialization.Target2), 2 },
                {typeof(global::UGF.Utf8Json.Runtime.Tests.TestSerialization.Target), 3 },
            };
        }

        internal static object GetFormatter(Type t)
        {
            int key;
            if (!lookup.TryGetValue(t, out key)) return null;

            switch (key)
            {
                case 0: return new global::Utf8Json.Formatters.ListFormatter<global::UGF.Utf8Json.Runtime.Tests.TestSerialization.Target2>();
                case 1: return new UGF.Utf8Json.Runtime.Tests.Formatters.UnityEngine.HideFlagsFormatter();
                case 2: return new UGF.Utf8Json.Runtime.Tests.Formatters.UGF.Utf8Json.Runtime.Tests.TestSerialization_Target2Formatter();
                case 3: return new UGF.Utf8Json.Runtime.Tests.Formatters.UGF.Utf8Json.Runtime.Tests.TestSerialization_TargetFormatter();
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

namespace UGF.Utf8Json.Runtime.Tests.Formatters.UnityEngine
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

namespace UGF.Utf8Json.Runtime.Tests.Formatters.UGF.Utf8Json.Runtime.Tests
{
    using System;
    using Utf8Json;


    public sealed class TestSerialization_Target2Formatter : global::Utf8Json.IJsonFormatter<global::UGF.Utf8Json.Runtime.Tests.TestSerialization.Target2>
    {
        readonly global::Utf8Json.Internal.AutomataDictionary ____keyMapping;
        readonly byte[][] ____stringByteKeys;

        public TestSerialization_Target2Formatter()
        {
            this.____keyMapping = new global::Utf8Json.Internal.AutomataDictionary()
            {
                { global::Utf8Json.JsonWriter.GetEncodedPropertyNameWithoutQuotation("Vector2"), 0},
                { global::Utf8Json.JsonWriter.GetEncodedPropertyNameWithoutQuotation("Bounds"), 1},
            };

            this.____stringByteKeys = new byte[][]
            {
                global::Utf8Json.JsonWriter.GetEncodedPropertyNameWithBeginObject("Vector2"),
                global::Utf8Json.JsonWriter.GetEncodedPropertyNameWithPrefixValueSeparator("Bounds"),
                
            };
        }

        public void Serialize(ref global::Utf8Json.JsonWriter writer, global::UGF.Utf8Json.Runtime.Tests.TestSerialization.Target2 value, global::Utf8Json.IJsonFormatterResolver formatterResolver)
        {
            if (value == null)
            {
                writer.WriteNull();
                return;
            }
            

            writer.WriteRaw(this.____stringByteKeys[0]);
            global::Utf8Json.JsonFormatterResolverExtensions.GetFormatterWithVerify<global::UnityEngine.Vector2>(formatterResolver).Serialize(ref writer, value.Vector2, formatterResolver);
            writer.WriteRaw(this.____stringByteKeys[1]);
            global::Utf8Json.JsonFormatterResolverExtensions.GetFormatterWithVerify<global::UnityEngine.Bounds>(formatterResolver).Serialize(ref writer, value.Bounds, formatterResolver);
            
            writer.WriteEndObject();
        }

        public global::UGF.Utf8Json.Runtime.Tests.TestSerialization.Target2 Deserialize(ref global::Utf8Json.JsonReader reader, global::Utf8Json.IJsonFormatterResolver formatterResolver)
        {
            if (reader.ReadIsNull())
            {
                return null;
            }
            

            var __Vector2__ = default(global::UnityEngine.Vector2);
            var __Vector2__b__ = false;
            var __Bounds__ = default(global::UnityEngine.Bounds);
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
                        __Vector2__ = global::Utf8Json.JsonFormatterResolverExtensions.GetFormatterWithVerify<global::UnityEngine.Vector2>(formatterResolver).Deserialize(ref reader, formatterResolver);
                        __Vector2__b__ = true;
                        break;
                    case 1:
                        __Bounds__ = global::Utf8Json.JsonFormatterResolverExtensions.GetFormatterWithVerify<global::UnityEngine.Bounds>(formatterResolver).Deserialize(ref reader, formatterResolver);
                        __Bounds__b__ = true;
                        break;
                    default:
                        reader.ReadNextBlock();
                        break;
                }

                NEXT_LOOP:
                continue;
            }

            var ____result = new global::UGF.Utf8Json.Runtime.Tests.TestSerialization.Target2();
            if(__Vector2__b__) ____result.Vector2 = __Vector2__;
            if(__Bounds__b__) ____result.Bounds = __Bounds__;

            return ____result;
        }
    }


    public sealed class TestSerialization_TargetFormatter : global::Utf8Json.IJsonFormatter<global::UGF.Utf8Json.Runtime.Tests.TestSerialization.Target>
    {
        readonly global::Utf8Json.Internal.AutomataDictionary ____keyMapping;
        readonly byte[][] ____stringByteKeys;

        public TestSerialization_TargetFormatter()
        {
            this.____keyMapping = new global::Utf8Json.Internal.AutomataDictionary()
            {
                { global::Utf8Json.JsonWriter.GetEncodedPropertyNameWithoutQuotation("Name"), 0},
                { global::Utf8Json.JsonWriter.GetEncodedPropertyNameWithoutQuotation("BoolValue"), 1},
                { global::Utf8Json.JsonWriter.GetEncodedPropertyNameWithoutQuotation("FloatValue"), 2},
                { global::Utf8Json.JsonWriter.GetEncodedPropertyNameWithoutQuotation("IntValue"), 3},
                { global::Utf8Json.JsonWriter.GetEncodedPropertyNameWithoutQuotation("Flags"), 4},
                { global::Utf8Json.JsonWriter.GetEncodedPropertyNameWithoutQuotation("ListValue"), 5},
            };

            this.____stringByteKeys = new byte[][]
            {
                global::Utf8Json.JsonWriter.GetEncodedPropertyNameWithBeginObject("Name"),
                global::Utf8Json.JsonWriter.GetEncodedPropertyNameWithPrefixValueSeparator("BoolValue"),
                global::Utf8Json.JsonWriter.GetEncodedPropertyNameWithPrefixValueSeparator("FloatValue"),
                global::Utf8Json.JsonWriter.GetEncodedPropertyNameWithPrefixValueSeparator("IntValue"),
                global::Utf8Json.JsonWriter.GetEncodedPropertyNameWithPrefixValueSeparator("Flags"),
                global::Utf8Json.JsonWriter.GetEncodedPropertyNameWithPrefixValueSeparator("ListValue"),
                
            };
        }

        public void Serialize(ref global::Utf8Json.JsonWriter writer, global::UGF.Utf8Json.Runtime.Tests.TestSerialization.Target value, global::Utf8Json.IJsonFormatterResolver formatterResolver)
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
            global::Utf8Json.JsonFormatterResolverExtensions.GetFormatterWithVerify<global::UnityEngine.HideFlags>(formatterResolver).Serialize(ref writer, value.Flags, formatterResolver);
            writer.WriteRaw(this.____stringByteKeys[5]);
            global::Utf8Json.JsonFormatterResolverExtensions.GetFormatterWithVerify<global::System.Collections.Generic.List<global::UGF.Utf8Json.Runtime.Tests.TestSerialization.Target2>>(formatterResolver).Serialize(ref writer, value.ListValue, formatterResolver);
            
            writer.WriteEndObject();
        }

        public global::UGF.Utf8Json.Runtime.Tests.TestSerialization.Target Deserialize(ref global::Utf8Json.JsonReader reader, global::Utf8Json.IJsonFormatterResolver formatterResolver)
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
            var __Flags__ = default(global::UnityEngine.HideFlags);
            var __Flags__b__ = false;
            var __ListValue__ = default(global::System.Collections.Generic.List<global::UGF.Utf8Json.Runtime.Tests.TestSerialization.Target2>);
            var __ListValue__b__ = false;

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
                        __Flags__ = global::Utf8Json.JsonFormatterResolverExtensions.GetFormatterWithVerify<global::UnityEngine.HideFlags>(formatterResolver).Deserialize(ref reader, formatterResolver);
                        __Flags__b__ = true;
                        break;
                    case 5:
                        __ListValue__ = global::Utf8Json.JsonFormatterResolverExtensions.GetFormatterWithVerify<global::System.Collections.Generic.List<global::UGF.Utf8Json.Runtime.Tests.TestSerialization.Target2>>(formatterResolver).Deserialize(ref reader, formatterResolver);
                        __ListValue__b__ = true;
                        break;
                    default:
                        reader.ReadNextBlock();
                        break;
                }

                NEXT_LOOP:
                continue;
            }

            var ____result = new global::UGF.Utf8Json.Runtime.Tests.TestSerialization.Target();
            if(__Name__b__) ____result.Name = __Name__;
            if(__BoolValue__b__) ____result.BoolValue = __BoolValue__;
            if(__FloatValue__b__) ____result.FloatValue = __FloatValue__;
            if(__IntValue__b__) ____result.IntValue = __IntValue__;
            if(__Flags__b__) ____result.Flags = __Flags__;
            if(__ListValue__b__) ____result.ListValue = __ListValue__;

            return ____result;
        }
    }

}

#pragma warning disable 168
#pragma warning restore 219
#pragma warning restore 414
#pragma warning restore 618
#pragma warning restore 612
