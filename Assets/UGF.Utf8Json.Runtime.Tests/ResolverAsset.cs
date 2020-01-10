// THIS IS A GENERATED CODE. DO NOT EDIT.
// ReSharper disable all

namespace Generated.Asset
{
    [global::UnityEngine.CreateAssetMenuAttribute(menuName = ("UGF/Utf8Json/Generated/Generated.Resolver88"), order = (2000))]
    public class Resolver88Asset : global::UGF.Utf8Json.Runtime.Resolver.Utf8JsonResolverAsset
    {
        public override global::Utf8Json.IJsonFormatterResolver GetResolver()
        {
            return Generated.Resolvers.Resolver88.Instance;
        }
    }
}

namespace Generated.Resolvers
{
    using System;
    using Utf8Json;

    public class Resolver88 : global::Utf8Json.IJsonFormatterResolver
    {
        public static readonly global::Utf8Json.IJsonFormatterResolver Instance = new Resolver88();

        private readonly System.Collections.Generic.Dictionary<global::System.Type, global::Utf8Json.IJsonFormatter> m_formatters = new System.Collections.Generic.Dictionary<global::System.Type, global::Utf8Json.IJsonFormatter>();

        Resolver88()
        {

        }
        public global::Utf8Json.IJsonFormatter GetFormatter(global::System.Type type)
        {
            if (!m_formatters.TryGetValue(type, out var formatter))
            {
                formatter = (global::Utf8Json.IJsonFormatter)Resolver88GetFormatterHelper.GetFormatter(type);

                m_formatters.Add(type, formatter);
            }

            return formatter;
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
                var f = Resolver88GetFormatterHelper.GetFormatter(typeof(T));
                if (f != null)
                {
                    formatter = (global::Utf8Json.IJsonFormatter<T>)f;
                }
            }
        }
    }

    internal static class Resolver88GetFormatterHelper
    {
        static readonly global::System.Collections.Generic.Dictionary<Type, int> lookup;

        static Resolver88GetFormatterHelper()
        {
            lookup = new global::System.Collections.Generic.Dictionary<Type, int>(7)
            {
                {typeof(global::System.Collections.Generic.List<int>), 0 },
                {typeof(global::UGF.Utf8Json.Runtime.Tests.TestAssembly.TestTarget2[]), 1 },
                {typeof(global::System.Collections.Generic.List<global::UGF.Utf8Json.Runtime.Tests.TestAssembly.TestTarget2>), 2 },
                {typeof(global::UnityEngine.Keyframe[]), 3 },
                {typeof(global::UnityEngine.HideFlags), 4 },
                {typeof(global::UGF.Utf8Json.Runtime.Tests.TestAssembly.TestTarget2), 5 },
                {typeof(global::UGF.Utf8Json.Runtime.Tests.TestAssembly.TestTarget), 6 },
            };
        }

        internal static object GetFormatter(Type t)
        {
            int key;
            if (!lookup.TryGetValue(t, out key)) return null;

            switch (key)
            {
                case 0: return new global::Utf8Json.Formatters.ListFormatter<int>();
                case 1: return new global::Utf8Json.Formatters.ArrayFormatter<global::UGF.Utf8Json.Runtime.Tests.TestAssembly.TestTarget2>();
                case 2: return new global::Utf8Json.Formatters.ListFormatter<global::UGF.Utf8Json.Runtime.Tests.TestAssembly.TestTarget2>();
                case 3: return new global::Utf8Json.Formatters.ArrayFormatter<global::UnityEngine.Keyframe>();
                case 4: return new Generated.Formatters.UnityEngine.HideFlagsFormatter();
                case 5: return new Generated.Formatters.UGF.Utf8Json.Runtime.Tests.TestAssembly.TestTarget2Formatter();
                case 6: return new Generated.Formatters.UGF.Utf8Json.Runtime.Tests.TestAssembly.TestTargetFormatter();
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

namespace Generated.Formatters.UnityEngine
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

namespace Generated.Formatters.UGF.Utf8Json.Runtime.Tests.TestAssembly
{
    using System;
    using Utf8Json;


    public sealed class TestTarget2Formatter : global::Utf8Json.JsonFormatterBase<global::UGF.Utf8Json.Runtime.Tests.TestAssembly.TestTarget2>
    {
        readonly global::Utf8Json.Internal.AutomataDictionary ____keyMapping;
        readonly byte[][] ____stringByteKeys;

        public TestTarget2Formatter()
        {
            this.____keyMapping = new global::Utf8Json.Internal.AutomataDictionary()
            {
                { global::Utf8Json.JsonWriter.GetEncodedPropertyNameWithoutQuotation("name"), 0},
                { global::Utf8Json.JsonWriter.GetEncodedPropertyNameWithoutQuotation("boolValue"), 1},
                { global::Utf8Json.JsonWriter.GetEncodedPropertyNameWithoutQuotation("floatValue"), 2},
                { global::Utf8Json.JsonWriter.GetEncodedPropertyNameWithoutQuotation("intValue"), 3},
                { global::Utf8Json.JsonWriter.GetEncodedPropertyNameWithoutQuotation("vector2"), 4},
                { global::Utf8Json.JsonWriter.GetEncodedPropertyNameWithoutQuotation("bounds"), 5},
                { global::Utf8Json.JsonWriter.GetEncodedPropertyNameWithoutQuotation("flags"), 6},
            };

            this.____stringByteKeys = new byte[][]
            {
                global::Utf8Json.JsonWriter.GetEncodedPropertyNameWithBeginObject("name"),
                global::Utf8Json.JsonWriter.GetEncodedPropertyNameWithPrefixValueSeparator("boolValue"),
                global::Utf8Json.JsonWriter.GetEncodedPropertyNameWithPrefixValueSeparator("floatValue"),
                global::Utf8Json.JsonWriter.GetEncodedPropertyNameWithPrefixValueSeparator("intValue"),
                global::Utf8Json.JsonWriter.GetEncodedPropertyNameWithPrefixValueSeparator("vector2"),
                global::Utf8Json.JsonWriter.GetEncodedPropertyNameWithPrefixValueSeparator("bounds"),
                global::Utf8Json.JsonWriter.GetEncodedPropertyNameWithPrefixValueSeparator("flags"),
                
            };
        }

        public override void Serialize(ref global::Utf8Json.JsonWriter writer, global::UGF.Utf8Json.Runtime.Tests.TestAssembly.TestTarget2 value, global::Utf8Json.IJsonFormatterResolver formatterResolver)
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

        public override global::UGF.Utf8Json.Runtime.Tests.TestAssembly.TestTarget2 Deserialize(ref global::Utf8Json.JsonReader reader, global::Utf8Json.IJsonFormatterResolver formatterResolver)
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

            var ____result = new global::UGF.Utf8Json.Runtime.Tests.TestAssembly.TestTarget2();
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


    public sealed class TestTargetFormatter : global::Utf8Json.JsonFormatterBase<global::UGF.Utf8Json.Runtime.Tests.TestAssembly.TestTarget>
    {
        readonly global::Utf8Json.Internal.AutomataDictionary ____keyMapping;
        readonly byte[][] ____stringByteKeys;

        public TestTargetFormatter()
        {
            this.____keyMapping = new global::Utf8Json.Internal.AutomataDictionary()
            {
                { global::Utf8Json.JsonWriter.GetEncodedPropertyNameWithoutQuotation("name"), 0},
                { global::Utf8Json.JsonWriter.GetEncodedPropertyNameWithoutQuotation("boolValue"), 1},
                { global::Utf8Json.JsonWriter.GetEncodedPropertyNameWithoutQuotation("floatValue"), 2},
                { global::Utf8Json.JsonWriter.GetEncodedPropertyNameWithoutQuotation("intValue"), 3},
                { global::Utf8Json.JsonWriter.GetEncodedPropertyNameWithoutQuotation("vector2"), 4},
                { global::Utf8Json.JsonWriter.GetEncodedPropertyNameWithoutQuotation("bounds"), 5},
                { global::Utf8Json.JsonWriter.GetEncodedPropertyNameWithoutQuotation("flags"), 6},
                { global::Utf8Json.JsonWriter.GetEncodedPropertyNameWithoutQuotation("arrayInt"), 7},
                { global::Utf8Json.JsonWriter.GetEncodedPropertyNameWithoutQuotation("listInt"), 8},
                { global::Utf8Json.JsonWriter.GetEncodedPropertyNameWithoutQuotation("arrayTarget"), 9},
                { global::Utf8Json.JsonWriter.GetEncodedPropertyNameWithoutQuotation("listTarget"), 10},
                { global::Utf8Json.JsonWriter.GetEncodedPropertyNameWithoutQuotation("arrayFrames"), 11},
            };

            this.____stringByteKeys = new byte[][]
            {
                global::Utf8Json.JsonWriter.GetEncodedPropertyNameWithBeginObject("name"),
                global::Utf8Json.JsonWriter.GetEncodedPropertyNameWithPrefixValueSeparator("boolValue"),
                global::Utf8Json.JsonWriter.GetEncodedPropertyNameWithPrefixValueSeparator("floatValue"),
                global::Utf8Json.JsonWriter.GetEncodedPropertyNameWithPrefixValueSeparator("intValue"),
                global::Utf8Json.JsonWriter.GetEncodedPropertyNameWithPrefixValueSeparator("vector2"),
                global::Utf8Json.JsonWriter.GetEncodedPropertyNameWithPrefixValueSeparator("bounds"),
                global::Utf8Json.JsonWriter.GetEncodedPropertyNameWithPrefixValueSeparator("flags"),
                global::Utf8Json.JsonWriter.GetEncodedPropertyNameWithPrefixValueSeparator("arrayInt"),
                global::Utf8Json.JsonWriter.GetEncodedPropertyNameWithPrefixValueSeparator("listInt"),
                global::Utf8Json.JsonWriter.GetEncodedPropertyNameWithPrefixValueSeparator("arrayTarget"),
                global::Utf8Json.JsonWriter.GetEncodedPropertyNameWithPrefixValueSeparator("listTarget"),
                global::Utf8Json.JsonWriter.GetEncodedPropertyNameWithPrefixValueSeparator("arrayFrames"),
                
            };
        }

        public override void Serialize(ref global::Utf8Json.JsonWriter writer, global::UGF.Utf8Json.Runtime.Tests.TestAssembly.TestTarget value, global::Utf8Json.IJsonFormatterResolver formatterResolver)
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
            writer.WriteRaw(this.____stringByteKeys[7]);
            global::Utf8Json.JsonFormatterResolverExtensions.GetFormatterWithVerify<int[]>(formatterResolver).Serialize(ref writer, value.ArrayInt, formatterResolver);
            writer.WriteRaw(this.____stringByteKeys[8]);
            global::Utf8Json.JsonFormatterResolverExtensions.GetFormatterWithVerify<global::System.Collections.Generic.List<int>>(formatterResolver).Serialize(ref writer, value.ListInt, formatterResolver);
            writer.WriteRaw(this.____stringByteKeys[9]);
            global::Utf8Json.JsonFormatterResolverExtensions.GetFormatterWithVerify<global::UGF.Utf8Json.Runtime.Tests.TestAssembly.TestTarget2[]>(formatterResolver).Serialize(ref writer, value.ArrayTarget, formatterResolver);
            writer.WriteRaw(this.____stringByteKeys[10]);
            global::Utf8Json.JsonFormatterResolverExtensions.GetFormatterWithVerify<global::System.Collections.Generic.List<global::UGF.Utf8Json.Runtime.Tests.TestAssembly.TestTarget2>>(formatterResolver).Serialize(ref writer, value.ListTarget, formatterResolver);
            writer.WriteRaw(this.____stringByteKeys[11]);
            global::Utf8Json.JsonFormatterResolverExtensions.GetFormatterWithVerify<global::UnityEngine.Keyframe[]>(formatterResolver).Serialize(ref writer, value.ArrayFrames, formatterResolver);
            
            writer.WriteEndObject();
        }

        public override global::UGF.Utf8Json.Runtime.Tests.TestAssembly.TestTarget Deserialize(ref global::Utf8Json.JsonReader reader, global::Utf8Json.IJsonFormatterResolver formatterResolver)
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
            var __ArrayInt__ = default(int[]);
            var __ArrayInt__b__ = false;
            var __ListInt__ = default(global::System.Collections.Generic.List<int>);
            var __ListInt__b__ = false;
            var __ArrayTarget__ = default(global::UGF.Utf8Json.Runtime.Tests.TestAssembly.TestTarget2[]);
            var __ArrayTarget__b__ = false;
            var __ListTarget__ = default(global::System.Collections.Generic.List<global::UGF.Utf8Json.Runtime.Tests.TestAssembly.TestTarget2>);
            var __ListTarget__b__ = false;
            var __ArrayFrames__ = default(global::UnityEngine.Keyframe[]);
            var __ArrayFrames__b__ = false;

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
                    case 7:
                        __ArrayInt__ = global::Utf8Json.JsonFormatterResolverExtensions.GetFormatterWithVerify<int[]>(formatterResolver).Deserialize(ref reader, formatterResolver);
                        __ArrayInt__b__ = true;
                        break;
                    case 8:
                        __ListInt__ = global::Utf8Json.JsonFormatterResolverExtensions.GetFormatterWithVerify<global::System.Collections.Generic.List<int>>(formatterResolver).Deserialize(ref reader, formatterResolver);
                        __ListInt__b__ = true;
                        break;
                    case 9:
                        __ArrayTarget__ = global::Utf8Json.JsonFormatterResolverExtensions.GetFormatterWithVerify<global::UGF.Utf8Json.Runtime.Tests.TestAssembly.TestTarget2[]>(formatterResolver).Deserialize(ref reader, formatterResolver);
                        __ArrayTarget__b__ = true;
                        break;
                    case 10:
                        __ListTarget__ = global::Utf8Json.JsonFormatterResolverExtensions.GetFormatterWithVerify<global::System.Collections.Generic.List<global::UGF.Utf8Json.Runtime.Tests.TestAssembly.TestTarget2>>(formatterResolver).Deserialize(ref reader, formatterResolver);
                        __ListTarget__b__ = true;
                        break;
                    case 11:
                        __ArrayFrames__ = global::Utf8Json.JsonFormatterResolverExtensions.GetFormatterWithVerify<global::UnityEngine.Keyframe[]>(formatterResolver).Deserialize(ref reader, formatterResolver);
                        __ArrayFrames__b__ = true;
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
            if(__ArrayInt__b__) ____result.ArrayInt = __ArrayInt__;
            if(__ListInt__b__) ____result.ListInt = __ListInt__;
            if(__ArrayTarget__b__) ____result.ArrayTarget = __ArrayTarget__;
            if(__ListTarget__b__) ____result.ListTarget = __ListTarget__;
            if(__ArrayFrames__b__) ____result.ArrayFrames = __ArrayFrames__;

            return ____result;
        }
    }

}

#pragma warning disable 168
#pragma warning restore 219
#pragma warning restore 414
#pragma warning restore 618
#pragma warning restore 612
