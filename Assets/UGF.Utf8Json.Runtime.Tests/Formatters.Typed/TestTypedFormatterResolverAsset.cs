// THIS IS A GENERATED CODE. DO NOT EDIT.
// ReSharper disable all

namespace UGF.Utf8Json.Runtime.Tests.Formatter.Typed.Asset
{
    [global::UnityEngine.CreateAssetMenuAttribute(menuName = ("UGF/Utf8Json/Generated/UGF.Utf8Json.Runtime.Tests.Formatter.Typed.TestTypedFormatterResolver"), order = (2000))]
    public class TestTypedFormatterResolverAsset : global::UGF.Utf8Json.Runtime.Resolver.Utf8JsonResolverAsset
    {
        public override global::Utf8Json.IJsonFormatterResolver GetResolver()
        {
            return UGF.Utf8Json.Runtime.Tests.Formatter.Typed.Resolvers.TestTypedFormatterResolver.Instance;
        }
    }
}

namespace UGF.Utf8Json.Runtime.Tests.Formatter.Typed.Resolvers
{
    using System;
    using Utf8Json;

    public class TestTypedFormatterResolver : global::Utf8Json.IJsonFormatterResolver
    {
        public static readonly global::Utf8Json.IJsonFormatterResolver Instance = new TestTypedFormatterResolver();

        private readonly System.Collections.Generic.Dictionary<global::System.Type, global::Utf8Json.IJsonFormatter> m_formatters = new System.Collections.Generic.Dictionary<global::System.Type, global::Utf8Json.IJsonFormatter>();

        TestTypedFormatterResolver()
        {

        }
        public global::Utf8Json.IJsonFormatter GetFormatter(global::System.Type type)
        {
            if (!m_formatters.TryGetValue(type, out var formatter))
            {
                formatter = (global::Utf8Json.IJsonFormatter)TestTypedFormatterResolverGetFormatterHelper.GetFormatter(type);

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
                var f = TestTypedFormatterResolverGetFormatterHelper.GetFormatter(typeof(T));
                if (f != null)
                {
                    formatter = (global::Utf8Json.IJsonFormatter<T>)f;
                }
            }
        }
    }

    internal static class TestTypedFormatterResolverGetFormatterHelper
    {
        static readonly global::System.Collections.Generic.Dictionary<Type, int> lookup;

        static TestTypedFormatterResolverGetFormatterHelper()
        {
            lookup = new global::System.Collections.Generic.Dictionary<Type, int>(5)
            {
                {typeof(global::System.Collections.Generic.List<global::UGF.Utf8Json.Runtime.Tests.Formatters.Typed.TestTypedFormatter.ITarget>), 0 },
                {typeof(global::UGF.Utf8Json.Runtime.Tests.Formatters.Typed.TestTypedFormatter.Target1), 1 },
                {typeof(global::UGF.Utf8Json.Runtime.Tests.Formatters.Typed.TestTypedFormatter.Target2), 2 },
                {typeof(global::UGF.Utf8Json.Runtime.Tests.Formatters.Typed.TestTypedFormatter.Target3), 3 },
                {typeof(global::UGF.Utf8Json.Runtime.Tests.Formatters.Typed.TestTypedFormatter.TargetCollection), 4 },
            };
        }

        internal static object GetFormatter(Type t)
        {
            int key;
            if (!lookup.TryGetValue(t, out key)) return null;

            switch (key)
            {
                case 0: return new global::Utf8Json.Formatters.ListFormatter<global::UGF.Utf8Json.Runtime.Tests.Formatters.Typed.TestTypedFormatter.ITarget>();
                case 1: return new UGF.Utf8Json.Runtime.Tests.Formatter.Typed.Formatters.UGF.Utf8Json.Runtime.Tests.Formatters.Typed.TestTypedFormatter_Target1Formatter();
                case 2: return new UGF.Utf8Json.Runtime.Tests.Formatter.Typed.Formatters.UGF.Utf8Json.Runtime.Tests.Formatters.Typed.TestTypedFormatter_Target2Formatter();
                case 3: return new UGF.Utf8Json.Runtime.Tests.Formatter.Typed.Formatters.UGF.Utf8Json.Runtime.Tests.Formatters.Typed.TestTypedFormatter_Target3Formatter();
                case 4: return new UGF.Utf8Json.Runtime.Tests.Formatter.Typed.Formatters.UGF.Utf8Json.Runtime.Tests.Formatters.Typed.TestTypedFormatter_TargetCollectionFormatter();
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
#pragma warning disable 219
#pragma warning disable 168

namespace UGF.Utf8Json.Runtime.Tests.Formatter.Typed.Formatters.UGF.Utf8Json.Runtime.Tests.Formatters.Typed
{
    using System;
    using Utf8Json;


    public sealed class TestTypedFormatter_Target1Formatter : global::Utf8Json.JsonFormatterBase<global::UGF.Utf8Json.Runtime.Tests.Formatters.Typed.TestTypedFormatter.Target1>
    {
        readonly global::Utf8Json.Internal.AutomataDictionary ____keyMapping;
        readonly byte[][] ____stringByteKeys;

        public TestTypedFormatter_Target1Formatter()
        {
            this.____keyMapping = new global::Utf8Json.Internal.AutomataDictionary()
            {
                { global::Utf8Json.JsonWriter.GetEncodedPropertyNameWithoutQuotation("boolValue"), 0},
            };

            this.____stringByteKeys = new byte[][]
            {
                global::Utf8Json.JsonWriter.GetEncodedPropertyNameWithBeginObject("boolValue"),
                
            };
        }

        public override void Serialize(ref global::Utf8Json.JsonWriter writer, global::UGF.Utf8Json.Runtime.Tests.Formatters.Typed.TestTypedFormatter.Target1 value, global::Utf8Json.IJsonFormatterResolver formatterResolver)
        {
            if (value == null)
            {
                writer.WriteNull();
                return;
            }
            

            writer.WriteRaw(this.____stringByteKeys[0]);
            writer.WriteBoolean(value.BoolValue);
            
            writer.WriteEndObject();
        }

        public override global::UGF.Utf8Json.Runtime.Tests.Formatters.Typed.TestTypedFormatter.Target1 Deserialize(ref global::Utf8Json.JsonReader reader, global::Utf8Json.IJsonFormatterResolver formatterResolver)
        {
            if (reader.ReadIsNull())
            {
                return null;
            }
            

            var __BoolValue__ = default(bool);
            var __BoolValue__b__ = false;

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
                        __BoolValue__ = reader.ReadBoolean();
                        __BoolValue__b__ = true;
                        break;
                    default:
                        reader.ReadNextBlock();
                        break;
                }

                NEXT_LOOP:
                continue;
            }

            var ____result = new global::UGF.Utf8Json.Runtime.Tests.Formatters.Typed.TestTypedFormatter.Target1();
            if(__BoolValue__b__) ____result.BoolValue = __BoolValue__;

            return ____result;
        }
    }


    public sealed class TestTypedFormatter_Target2Formatter : global::Utf8Json.JsonFormatterBase<global::UGF.Utf8Json.Runtime.Tests.Formatters.Typed.TestTypedFormatter.Target2>
    {
        readonly global::Utf8Json.Internal.AutomataDictionary ____keyMapping;
        readonly byte[][] ____stringByteKeys;

        public TestTypedFormatter_Target2Formatter()
        {
            this.____keyMapping = new global::Utf8Json.Internal.AutomataDictionary()
            {
                { global::Utf8Json.JsonWriter.GetEncodedPropertyNameWithoutQuotation("intValue"), 0},
            };

            this.____stringByteKeys = new byte[][]
            {
                global::Utf8Json.JsonWriter.GetEncodedPropertyNameWithBeginObject("intValue"),
                
            };
        }

        public override void Serialize(ref global::Utf8Json.JsonWriter writer, global::UGF.Utf8Json.Runtime.Tests.Formatters.Typed.TestTypedFormatter.Target2 value, global::Utf8Json.IJsonFormatterResolver formatterResolver)
        {
            if (value == null)
            {
                writer.WriteNull();
                return;
            }
            

            writer.WriteRaw(this.____stringByteKeys[0]);
            writer.WriteInt32(value.IntValue);
            
            writer.WriteEndObject();
        }

        public override global::UGF.Utf8Json.Runtime.Tests.Formatters.Typed.TestTypedFormatter.Target2 Deserialize(ref global::Utf8Json.JsonReader reader, global::Utf8Json.IJsonFormatterResolver formatterResolver)
        {
            if (reader.ReadIsNull())
            {
                return null;
            }
            

            var __IntValue__ = default(int);
            var __IntValue__b__ = false;

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
                        __IntValue__ = reader.ReadInt32();
                        __IntValue__b__ = true;
                        break;
                    default:
                        reader.ReadNextBlock();
                        break;
                }

                NEXT_LOOP:
                continue;
            }

            var ____result = new global::UGF.Utf8Json.Runtime.Tests.Formatters.Typed.TestTypedFormatter.Target2();
            if(__IntValue__b__) ____result.IntValue = __IntValue__;

            return ____result;
        }
    }


    public sealed class TestTypedFormatter_Target3Formatter : global::Utf8Json.JsonFormatterBase<global::UGF.Utf8Json.Runtime.Tests.Formatters.Typed.TestTypedFormatter.Target3>
    {
        readonly global::Utf8Json.Internal.AutomataDictionary ____keyMapping;
        readonly byte[][] ____stringByteKeys;

        public TestTypedFormatter_Target3Formatter()
        {
            this.____keyMapping = new global::Utf8Json.Internal.AutomataDictionary()
            {
            };

            this.____stringByteKeys = new byte[][]
            {
                
            };
        }

        public override void Serialize(ref global::Utf8Json.JsonWriter writer, global::UGF.Utf8Json.Runtime.Tests.Formatters.Typed.TestTypedFormatter.Target3 value, global::Utf8Json.IJsonFormatterResolver formatterResolver)
        {
            if (value == null)
            {
                writer.WriteNull();
                return;
            }
            

           writer.WriteBeginObject();            
            writer.WriteEndObject();
        }

        public override global::UGF.Utf8Json.Runtime.Tests.Formatters.Typed.TestTypedFormatter.Target3 Deserialize(ref global::Utf8Json.JsonReader reader, global::Utf8Json.IJsonFormatterResolver formatterResolver)
        {
            if (reader.ReadIsNull())
            {
                return null;
            }
            


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
                    default:
                        reader.ReadNextBlock();
                        break;
                }

                NEXT_LOOP:
                continue;
            }

            var ____result = new global::UGF.Utf8Json.Runtime.Tests.Formatters.Typed.TestTypedFormatter.Target3();

            return ____result;
        }
    }


    public sealed class TestTypedFormatter_TargetCollectionFormatter : global::Utf8Json.JsonFormatterBase<global::UGF.Utf8Json.Runtime.Tests.Formatters.Typed.TestTypedFormatter.TargetCollection>
    {
        readonly global::Utf8Json.Internal.AutomataDictionary ____keyMapping;
        readonly byte[][] ____stringByteKeys;

        public TestTypedFormatter_TargetCollectionFormatter()
        {
            this.____keyMapping = new global::Utf8Json.Internal.AutomataDictionary()
            {
                { global::Utf8Json.JsonWriter.GetEncodedPropertyNameWithoutQuotation("targets"), 0},
            };

            this.____stringByteKeys = new byte[][]
            {
                global::Utf8Json.JsonWriter.GetEncodedPropertyNameWithBeginObject("targets"),
                
            };
        }

        public override void Serialize(ref global::Utf8Json.JsonWriter writer, global::UGF.Utf8Json.Runtime.Tests.Formatters.Typed.TestTypedFormatter.TargetCollection value, global::Utf8Json.IJsonFormatterResolver formatterResolver)
        {
            if (value == null)
            {
                writer.WriteNull();
                return;
            }
            

            writer.WriteRaw(this.____stringByteKeys[0]);
            global::Utf8Json.JsonFormatterResolverExtensions.GetFormatterWithVerify<global::System.Collections.Generic.List<global::UGF.Utf8Json.Runtime.Tests.Formatters.Typed.TestTypedFormatter.ITarget>>(formatterResolver).Serialize(ref writer, value.Targets, formatterResolver);
            
            writer.WriteEndObject();
        }

        public override global::UGF.Utf8Json.Runtime.Tests.Formatters.Typed.TestTypedFormatter.TargetCollection Deserialize(ref global::Utf8Json.JsonReader reader, global::Utf8Json.IJsonFormatterResolver formatterResolver)
        {
            if (reader.ReadIsNull())
            {
                return null;
            }
            

            var __Targets__ = default(global::System.Collections.Generic.List<global::UGF.Utf8Json.Runtime.Tests.Formatters.Typed.TestTypedFormatter.ITarget>);
            var __Targets__b__ = false;

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
                        __Targets__ = global::Utf8Json.JsonFormatterResolverExtensions.GetFormatterWithVerify<global::System.Collections.Generic.List<global::UGF.Utf8Json.Runtime.Tests.Formatters.Typed.TestTypedFormatter.ITarget>>(formatterResolver).Deserialize(ref reader, formatterResolver);
                        __Targets__b__ = true;
                        break;
                    default:
                        reader.ReadNextBlock();
                        break;
                }

                NEXT_LOOP:
                continue;
            }

            var ____result = new global::UGF.Utf8Json.Runtime.Tests.Formatters.Typed.TestTypedFormatter.TargetCollection();
            if(__Targets__b__) ____result.Targets = __Targets__;

            return ____result;
        }
    }

}

#pragma warning disable 168
#pragma warning restore 219
#pragma warning restore 414
#pragma warning restore 618
#pragma warning restore 612
