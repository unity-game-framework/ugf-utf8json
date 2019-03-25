using Utf8Json;

// ReSharper disable all

#pragma warning disable 618
#pragma warning disable 612
#pragma warning disable 414
#pragma warning disable 168

namespace TestEditorAssembly.Generated.Resolvers
{
    using System;
    using Utf8Json;

    public class TestEditorAssemblyGeneratedResolver : global::Utf8Json.IJsonFormatterResolver
    {
        public static readonly global::Utf8Json.IJsonFormatterResolver Instance = new TestEditorAssemblyGeneratedResolver();

        TestEditorAssemblyGeneratedResolver()
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
                var f = TestEditorAssemblyGeneratedResolverGetFormatterHelper.GetFormatter(typeof(T));
                if (f != null)
                {
                    formatter = (global::Utf8Json.IJsonFormatter<T>)f;
                }
            }
        }
    }

    internal static class TestEditorAssemblyGeneratedResolverGetFormatterHelper
    {
        static readonly global::System.Collections.Generic.Dictionary<Type, int> lookup;

        static TestEditorAssemblyGeneratedResolverGetFormatterHelper()
        {
            lookup = new global::System.Collections.Generic.Dictionary<Type, int>(1)
            {
                {typeof(global::UGF.Utf8Json.Editor.Tests.TestEditorAssembly.TestEditorTarget), 0 },
            };
        }

        internal static object GetFormatter(Type t)
        {
            int key;
            if (!lookup.TryGetValue(t, out key)) return null;

            switch (key)
            {
                case 0: return new TestEditorAssembly.Generated.Formatters.UGF.Utf8Json.Editor.Tests.TestEditorAssembly.TestEditorTargetFormatter();
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

namespace TestEditorAssembly.Generated.Formatters.UGF.Utf8Json.Editor.Tests.TestEditorAssembly
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
            };

            this.____stringByteKeys = new byte[][]
            {
                JsonWriter.GetEncodedPropertyNameWithBeginObject("BoolValue"),
                
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
                    default:
                        reader.ReadNextBlock();
                        break;
                }

                NEXT_LOOP:
                continue;
            }

            var ____result = new global::UGF.Utf8Json.Editor.Tests.TestEditorAssembly.TestEditorTarget();
            if(__BoolValue__b__) ____result.BoolValue = __BoolValue__;

            return ____result;
        }
    }

}

#pragma warning disable 168
#pragma warning restore 219
#pragma warning restore 414
#pragma warning restore 618
#pragma warning restore 612
