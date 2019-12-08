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
            lookup = new global::System.Collections.Generic.Dictionary<Type, int>(12)
            {
                {typeof(global::System.Collections.Generic.List<int>), 0 },
                {typeof(global::UGF.Utf8Json.Runtime.Tests.TestAssembly.TestTarget2[]), 1 },
                {typeof(global::System.Collections.Generic.List<global::UGF.Utf8Json.Runtime.Tests.TestAssembly.TestTarget2>), 2 },
                {typeof(global::UnityEngine.Keyframe[]), 3 },
                {typeof(int[,]), 4 },
                {typeof(global::UnityEngine.HideFlags), 5 },
                {typeof(global::UGF.Utf8Json.Runtime.Tests.TestSerialization.Target), 6 },
                {typeof(global::UGF.Utf8Json.Runtime.Tests.TestSerialization.Target2), 7 },
                {typeof(global::UGF.Utf8Json.Runtime.Tests.TestEncoding.Target), 8 },
                {typeof(global::UGF.Utf8Json.Runtime.Tests.TestEncoding.Target2), 9 },
                {typeof(global::UGF.Utf8Json.Runtime.Tests.TestAssembly.TestTarget3), 10 },
                {typeof(global::UnityEngine.AI.NavMeshBuildSettings), 11 },
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
                case 4: return new global::Utf8Json.Formatters.TwoDimentionalArrayFormatter<int>();
                case 5: return new Generated.Formatters.UnityEngine.HideFlagsFormatter();
                case 6: return new Generated.Formatters.UGF.Utf8Json.Runtime.Tests.TestSerialization_TargetFormatter();
                case 7: return new Generated.Formatters.UGF.Utf8Json.Runtime.Tests.TestSerialization_Target2Formatter();
                case 8: return new Generated.Formatters.UGF.Utf8Json.Runtime.Tests.TestEncoding_TargetFormatter();
                case 9: return new Generated.Formatters.UGF.Utf8Json.Runtime.Tests.TestEncoding_Target2Formatter();
                case 10: return new Generated.Formatters.UGF.Utf8Json.Runtime.Tests.TestAssembly.TestTarget3Formatter();
                case 11: return new Generated.Formatters.UnityEngine.AI.NavMeshBuildSettingsFormatter();
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

namespace Generated.Formatters.UGF.Utf8Json.Runtime.Tests
{
    using System;
    using Utf8Json;


    public sealed class TestSerialization_TargetFormatter : global::Utf8Json.JsonFormatterBase<global::UGF.Utf8Json.Runtime.Tests.TestSerialization.Target>
    {
        readonly global::Utf8Json.Internal.AutomataDictionary ____keyMapping;
        readonly byte[][] ____stringByteKeys;

        public TestSerialization_TargetFormatter()
        {
            this.____keyMapping = new global::Utf8Json.Internal.AutomataDictionary()
            {
                { global::Utf8Json.JsonWriter.GetEncodedPropertyNameWithoutQuotation("name"), 0},
                { global::Utf8Json.JsonWriter.GetEncodedPropertyNameWithoutQuotation("boolValue"), 1},
                { global::Utf8Json.JsonWriter.GetEncodedPropertyNameWithoutQuotation("floatValue"), 2},
                { global::Utf8Json.JsonWriter.GetEncodedPropertyNameWithoutQuotation("intValue"), 3},
                { global::Utf8Json.JsonWriter.GetEncodedPropertyNameWithoutQuotation("flags"), 4},
            };

            this.____stringByteKeys = new byte[][]
            {
                global::Utf8Json.JsonWriter.GetEncodedPropertyNameWithBeginObject("name"),
                global::Utf8Json.JsonWriter.GetEncodedPropertyNameWithPrefixValueSeparator("boolValue"),
                global::Utf8Json.JsonWriter.GetEncodedPropertyNameWithPrefixValueSeparator("floatValue"),
                global::Utf8Json.JsonWriter.GetEncodedPropertyNameWithPrefixValueSeparator("intValue"),
                global::Utf8Json.JsonWriter.GetEncodedPropertyNameWithPrefixValueSeparator("flags"),
                
            };
        }

        public override void Serialize(ref global::Utf8Json.JsonWriter writer, global::UGF.Utf8Json.Runtime.Tests.TestSerialization.Target value, global::Utf8Json.IJsonFormatterResolver formatterResolver)
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
            
            writer.WriteEndObject();
        }

        public override global::UGF.Utf8Json.Runtime.Tests.TestSerialization.Target Deserialize(ref global::Utf8Json.JsonReader reader, global::Utf8Json.IJsonFormatterResolver formatterResolver)
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

            var ____result = new global::UGF.Utf8Json.Runtime.Tests.TestSerialization.Target();
            if(__Name__b__) ____result.Name = __Name__;
            if(__BoolValue__b__) ____result.BoolValue = __BoolValue__;
            if(__FloatValue__b__) ____result.FloatValue = __FloatValue__;
            if(__IntValue__b__) ____result.IntValue = __IntValue__;
            if(__Flags__b__) ____result.Flags = __Flags__;

            return ____result;
        }
    }


    public sealed class TestSerialization_Target2Formatter : global::Utf8Json.JsonFormatterBase<global::UGF.Utf8Json.Runtime.Tests.TestSerialization.Target2>
    {
        readonly global::Utf8Json.Internal.AutomataDictionary ____keyMapping;
        readonly byte[][] ____stringByteKeys;

        public TestSerialization_Target2Formatter()
        {
            this.____keyMapping = new global::Utf8Json.Internal.AutomataDictionary()
            {
                { global::Utf8Json.JsonWriter.GetEncodedPropertyNameWithoutQuotation("vector2"), 0},
                { global::Utf8Json.JsonWriter.GetEncodedPropertyNameWithoutQuotation("bounds"), 1},
            };

            this.____stringByteKeys = new byte[][]
            {
                global::Utf8Json.JsonWriter.GetEncodedPropertyNameWithBeginObject("vector2"),
                global::Utf8Json.JsonWriter.GetEncodedPropertyNameWithPrefixValueSeparator("bounds"),
                
            };
        }

        public override void Serialize(ref global::Utf8Json.JsonWriter writer, global::UGF.Utf8Json.Runtime.Tests.TestSerialization.Target2 value, global::Utf8Json.IJsonFormatterResolver formatterResolver)
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

        public override global::UGF.Utf8Json.Runtime.Tests.TestSerialization.Target2 Deserialize(ref global::Utf8Json.JsonReader reader, global::Utf8Json.IJsonFormatterResolver formatterResolver)
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
                if (!____keyMapping.TryGetValue(stringKey, out key))
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


    public sealed class TestEncoding_TargetFormatter : global::Utf8Json.JsonFormatterBase<global::UGF.Utf8Json.Runtime.Tests.TestEncoding.Target>
    {
        readonly global::Utf8Json.Internal.AutomataDictionary ____keyMapping;
        readonly byte[][] ____stringByteKeys;

        public TestEncoding_TargetFormatter()
        {
            this.____keyMapping = new global::Utf8Json.Internal.AutomataDictionary()
            {
                { global::Utf8Json.JsonWriter.GetEncodedPropertyNameWithoutQuotation("value"), 0},
            };

            this.____stringByteKeys = new byte[][]
            {
                global::Utf8Json.JsonWriter.GetEncodedPropertyNameWithBeginObject("value"),
                
            };
        }

        public override void Serialize(ref global::Utf8Json.JsonWriter writer, global::UGF.Utf8Json.Runtime.Tests.TestEncoding.Target value, global::Utf8Json.IJsonFormatterResolver formatterResolver)
        {
            if (value == null)
            {
                writer.WriteNull();
                return;
            }
            

            writer.WriteRaw(this.____stringByteKeys[0]);
            writer.WriteString(value.Value);
            
            writer.WriteEndObject();
        }

        public override global::UGF.Utf8Json.Runtime.Tests.TestEncoding.Target Deserialize(ref global::Utf8Json.JsonReader reader, global::Utf8Json.IJsonFormatterResolver formatterResolver)
        {
            if (reader.ReadIsNull())
            {
                return null;
            }
            

            var __Value__ = default(string);
            var __Value__b__ = false;

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
                        __Value__ = reader.ReadString();
                        __Value__b__ = true;
                        break;
                    default:
                        reader.ReadNextBlock();
                        break;
                }

                NEXT_LOOP:
                continue;
            }

            var ____result = new global::UGF.Utf8Json.Runtime.Tests.TestEncoding.Target();
            if(__Value__b__) ____result.Value = __Value__;

            return ____result;
        }
    }


    public sealed class TestEncoding_Target2Formatter : global::Utf8Json.JsonFormatterBase<global::UGF.Utf8Json.Runtime.Tests.TestEncoding.Target2>
    {
        readonly global::Utf8Json.Internal.AutomataDictionary ____keyMapping;
        readonly byte[][] ____stringByteKeys;

        public TestEncoding_Target2Formatter()
        {
            this.____keyMapping = new global::Utf8Json.Internal.AutomataDictionary()
            {
                { global::Utf8Json.JsonWriter.GetEncodedPropertyNameWithoutQuotation("value"), 0},
            };

            this.____stringByteKeys = new byte[][]
            {
                global::Utf8Json.JsonWriter.GetEncodedPropertyNameWithBeginObject("value"),
                
            };
        }

        public override void Serialize(ref global::Utf8Json.JsonWriter writer, global::UGF.Utf8Json.Runtime.Tests.TestEncoding.Target2 value, global::Utf8Json.IJsonFormatterResolver formatterResolver)
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

        public override global::UGF.Utf8Json.Runtime.Tests.TestEncoding.Target2 Deserialize(ref global::Utf8Json.JsonReader reader, global::Utf8Json.IJsonFormatterResolver formatterResolver)
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
                if (!____keyMapping.TryGetValue(stringKey, out key))
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

            var ____result = new global::UGF.Utf8Json.Runtime.Tests.TestEncoding.Target2();
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
#pragma warning disable 618
#pragma warning disable 612
#pragma warning disable 414
#pragma warning disable 219
#pragma warning disable 168

namespace Generated.Formatters.UGF.Utf8Json.Runtime.Tests.TestAssembly
{
    using System;
    using Utf8Json;


    public sealed class TestTarget3Formatter : global::Utf8Json.JsonFormatterBase<global::UGF.Utf8Json.Runtime.Tests.TestAssembly.TestTarget3>
    {
        readonly global::Utf8Json.Internal.AutomataDictionary ____keyMapping;
        readonly byte[][] ____stringByteKeys;

        public TestTarget3Formatter()
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
                { global::Utf8Json.JsonWriter.GetEncodedPropertyNameWithoutQuotation("arrayInt2"), 12},
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
                global::Utf8Json.JsonWriter.GetEncodedPropertyNameWithPrefixValueSeparator("arrayInt2"),
                
            };
        }

        public override void Serialize(ref global::Utf8Json.JsonWriter writer, global::UGF.Utf8Json.Runtime.Tests.TestAssembly.TestTarget3 value, global::Utf8Json.IJsonFormatterResolver formatterResolver)
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
            writer.WriteRaw(this.____stringByteKeys[12]);
            global::Utf8Json.JsonFormatterResolverExtensions.GetFormatterWithVerify<int[,]>(formatterResolver).Serialize(ref writer, value.ArrayInt2, formatterResolver);
            
            writer.WriteEndObject();
        }

        public override global::UGF.Utf8Json.Runtime.Tests.TestAssembly.TestTarget3 Deserialize(ref global::Utf8Json.JsonReader reader, global::Utf8Json.IJsonFormatterResolver formatterResolver)
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
            var __ArrayInt2__ = default(int[,]);
            var __ArrayInt2__b__ = false;

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
                    case 12:
                        __ArrayInt2__ = global::Utf8Json.JsonFormatterResolverExtensions.GetFormatterWithVerify<int[,]>(formatterResolver).Deserialize(ref reader, formatterResolver);
                        __ArrayInt2__b__ = true;
                        break;
                    default:
                        reader.ReadNextBlock();
                        break;
                }

                NEXT_LOOP:
                continue;
            }

            var ____result = new global::UGF.Utf8Json.Runtime.Tests.TestAssembly.TestTarget3();
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
            if(__ArrayInt2__b__) ____result.ArrayInt2 = __ArrayInt2__;

            return ____result;
        }
    }

}

#pragma warning disable 168
#pragma warning restore 219
#pragma warning restore 414
#pragma warning restore 618
#pragma warning restore 612
#pragma warning disable 618
#pragma warning disable 612
#pragma warning disable 414
#pragma warning disable 219
#pragma warning disable 168

namespace Generated.Formatters.UnityEngine.AI
{
    using System;
    using Utf8Json;


    public sealed class NavMeshBuildSettingsFormatter : global::Utf8Json.JsonFormatterBase<global::UnityEngine.AI.NavMeshBuildSettings>
    {
        readonly global::Utf8Json.Internal.AutomataDictionary ____keyMapping;
        readonly byte[][] ____stringByteKeys;

        public NavMeshBuildSettingsFormatter()
        {
            this.____keyMapping = new global::Utf8Json.Internal.AutomataDictionary()
            {
                { global::Utf8Json.JsonWriter.GetEncodedPropertyNameWithoutQuotation("agentTypeID"), 0},
                { global::Utf8Json.JsonWriter.GetEncodedPropertyNameWithoutQuotation("agentRadius"), 1},
                { global::Utf8Json.JsonWriter.GetEncodedPropertyNameWithoutQuotation("agentHeight"), 2},
                { global::Utf8Json.JsonWriter.GetEncodedPropertyNameWithoutQuotation("agentSlope"), 3},
                { global::Utf8Json.JsonWriter.GetEncodedPropertyNameWithoutQuotation("agentClimb"), 4},
                { global::Utf8Json.JsonWriter.GetEncodedPropertyNameWithoutQuotation("minRegionArea"), 5},
                { global::Utf8Json.JsonWriter.GetEncodedPropertyNameWithoutQuotation("overrideVoxelSize"), 6},
                { global::Utf8Json.JsonWriter.GetEncodedPropertyNameWithoutQuotation("voxelSize"), 7},
                { global::Utf8Json.JsonWriter.GetEncodedPropertyNameWithoutQuotation("overrideTileSize"), 8},
                { global::Utf8Json.JsonWriter.GetEncodedPropertyNameWithoutQuotation("tileSize"), 9},
            };

            this.____stringByteKeys = new byte[][]
            {
                global::Utf8Json.JsonWriter.GetEncodedPropertyNameWithBeginObject("agentTypeID"),
                global::Utf8Json.JsonWriter.GetEncodedPropertyNameWithPrefixValueSeparator("agentRadius"),
                global::Utf8Json.JsonWriter.GetEncodedPropertyNameWithPrefixValueSeparator("agentHeight"),
                global::Utf8Json.JsonWriter.GetEncodedPropertyNameWithPrefixValueSeparator("agentSlope"),
                global::Utf8Json.JsonWriter.GetEncodedPropertyNameWithPrefixValueSeparator("agentClimb"),
                global::Utf8Json.JsonWriter.GetEncodedPropertyNameWithPrefixValueSeparator("minRegionArea"),
                global::Utf8Json.JsonWriter.GetEncodedPropertyNameWithPrefixValueSeparator("overrideVoxelSize"),
                global::Utf8Json.JsonWriter.GetEncodedPropertyNameWithPrefixValueSeparator("voxelSize"),
                global::Utf8Json.JsonWriter.GetEncodedPropertyNameWithPrefixValueSeparator("overrideTileSize"),
                global::Utf8Json.JsonWriter.GetEncodedPropertyNameWithPrefixValueSeparator("tileSize"),
                
            };
        }

        public override void Serialize(ref global::Utf8Json.JsonWriter writer, global::UnityEngine.AI.NavMeshBuildSettings value, global::Utf8Json.IJsonFormatterResolver formatterResolver)
        {
            

            writer.WriteRaw(this.____stringByteKeys[0]);
            writer.WriteInt32(value.agentTypeID);
            writer.WriteRaw(this.____stringByteKeys[1]);
            writer.WriteSingle(value.agentRadius);
            writer.WriteRaw(this.____stringByteKeys[2]);
            writer.WriteSingle(value.agentHeight);
            writer.WriteRaw(this.____stringByteKeys[3]);
            writer.WriteSingle(value.agentSlope);
            writer.WriteRaw(this.____stringByteKeys[4]);
            writer.WriteSingle(value.agentClimb);
            writer.WriteRaw(this.____stringByteKeys[5]);
            writer.WriteSingle(value.minRegionArea);
            writer.WriteRaw(this.____stringByteKeys[6]);
            writer.WriteBoolean(value.overrideVoxelSize);
            writer.WriteRaw(this.____stringByteKeys[7]);
            writer.WriteSingle(value.voxelSize);
            writer.WriteRaw(this.____stringByteKeys[8]);
            writer.WriteBoolean(value.overrideTileSize);
            writer.WriteRaw(this.____stringByteKeys[9]);
            writer.WriteInt32(value.tileSize);
            
            writer.WriteEndObject();
        }

        public override global::UnityEngine.AI.NavMeshBuildSettings Deserialize(ref global::Utf8Json.JsonReader reader, global::Utf8Json.IJsonFormatterResolver formatterResolver)
        {
            if (reader.ReadIsNull())
            {
                throw new InvalidOperationException("typecode is null, struct not supported");
            }
            

            var __agentTypeID__ = default(int);
            var __agentTypeID__b__ = false;
            var __agentRadius__ = default(float);
            var __agentRadius__b__ = false;
            var __agentHeight__ = default(float);
            var __agentHeight__b__ = false;
            var __agentSlope__ = default(float);
            var __agentSlope__b__ = false;
            var __agentClimb__ = default(float);
            var __agentClimb__b__ = false;
            var __minRegionArea__ = default(float);
            var __minRegionArea__b__ = false;
            var __overrideVoxelSize__ = default(bool);
            var __overrideVoxelSize__b__ = false;
            var __voxelSize__ = default(float);
            var __voxelSize__b__ = false;
            var __overrideTileSize__ = default(bool);
            var __overrideTileSize__b__ = false;
            var __tileSize__ = default(int);
            var __tileSize__b__ = false;

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
                        __agentTypeID__ = reader.ReadInt32();
                        __agentTypeID__b__ = true;
                        break;
                    case 1:
                        __agentRadius__ = reader.ReadSingle();
                        __agentRadius__b__ = true;
                        break;
                    case 2:
                        __agentHeight__ = reader.ReadSingle();
                        __agentHeight__b__ = true;
                        break;
                    case 3:
                        __agentSlope__ = reader.ReadSingle();
                        __agentSlope__b__ = true;
                        break;
                    case 4:
                        __agentClimb__ = reader.ReadSingle();
                        __agentClimb__b__ = true;
                        break;
                    case 5:
                        __minRegionArea__ = reader.ReadSingle();
                        __minRegionArea__b__ = true;
                        break;
                    case 6:
                        __overrideVoxelSize__ = reader.ReadBoolean();
                        __overrideVoxelSize__b__ = true;
                        break;
                    case 7:
                        __voxelSize__ = reader.ReadSingle();
                        __voxelSize__b__ = true;
                        break;
                    case 8:
                        __overrideTileSize__ = reader.ReadBoolean();
                        __overrideTileSize__b__ = true;
                        break;
                    case 9:
                        __tileSize__ = reader.ReadInt32();
                        __tileSize__b__ = true;
                        break;
                    default:
                        reader.ReadNextBlock();
                        break;
                }

                NEXT_LOOP:
                continue;
            }

            var ____result = new global::UnityEngine.AI.NavMeshBuildSettings();
            if(__agentTypeID__b__) ____result.agentTypeID = __agentTypeID__;
            if(__agentRadius__b__) ____result.agentRadius = __agentRadius__;
            if(__agentHeight__b__) ____result.agentHeight = __agentHeight__;
            if(__agentSlope__b__) ____result.agentSlope = __agentSlope__;
            if(__agentClimb__b__) ____result.agentClimb = __agentClimb__;
            if(__minRegionArea__b__) ____result.minRegionArea = __minRegionArea__;
            if(__overrideVoxelSize__b__) ____result.overrideVoxelSize = __overrideVoxelSize__;
            if(__voxelSize__b__) ____result.voxelSize = __voxelSize__;
            if(__overrideTileSize__b__) ____result.overrideTileSize = __overrideTileSize__;
            if(__tileSize__b__) ____result.tileSize = __tileSize__;

            return ____result;
        }
    }

}

#pragma warning disable 168
#pragma warning restore 219
#pragma warning restore 414
#pragma warning restore 618
#pragma warning restore 612
