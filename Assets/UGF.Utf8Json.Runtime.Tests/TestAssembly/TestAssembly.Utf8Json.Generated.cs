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
            lookup = new global::System.Collections.Generic.Dictionary<Type, int>(3)
            {
                {typeof(global::UnityEngine.HideFlags), 0 },
                {typeof(global::UGF.Utf8Json.Runtime.Tests.TestAssembly.TestTarget), 1 },
                {typeof(global::UnityEngine.AI.NavMeshBuildSettings), 2 },
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
                case 2: return new TestAssembly.Formatters.UnityEngine.AI.NavMeshBuildSettingsFormatter();
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
#pragma warning disable 618
#pragma warning disable 612
#pragma warning disable 414
#pragma warning disable 219
#pragma warning disable 168

namespace TestAssembly.Formatters.UnityEngine.AI
{
    using System;
    using Utf8Json;


    public sealed class NavMeshBuildSettingsFormatter : global::Utf8Json.IJsonFormatter<global::UnityEngine.AI.NavMeshBuildSettings>
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

        public void Serialize(ref global::Utf8Json.JsonWriter writer, global::UnityEngine.AI.NavMeshBuildSettings value, global::Utf8Json.IJsonFormatterResolver formatterResolver)
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

        public global::UnityEngine.AI.NavMeshBuildSettings Deserialize(ref global::Utf8Json.JsonReader reader, global::Utf8Json.IJsonFormatterResolver formatterResolver)
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
