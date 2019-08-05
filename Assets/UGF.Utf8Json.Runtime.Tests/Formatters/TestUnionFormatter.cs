using UGF.Utf8Json.Runtime.Formatters;
using Utf8Json;

namespace UGF.Utf8Json.Runtime.Tests.Formatters
{
    public class TestUnionFormatter
    {
        private class Formatter : UnionFormatter<ITarget>
        {
            public Formatter()
            {
                AddFormatter<Target1>("one");
                AddFormatter<Target2>("two");
            }
        }

        private interface ITarget
        {
        }

        private class Target1 : ITarget
        {
        }

        private class Target2 : ITarget
        {
        }

        private class Target1Formatter : IJsonFormatter<Target1>
        {
            public void Serialize(ref JsonWriter writer, Target1 value, IJsonFormatterResolver formatterResolver)
            {
            }

            public Target1 Deserialize(ref JsonReader reader, IJsonFormatterResolver formatterResolver)
            {
                return default;
            }
        }

        private class Target2Formatter : IJsonFormatter<Target2>
        {
            public void Serialize(ref JsonWriter writer, Target2 value, IJsonFormatterResolver formatterResolver)
            {
            }

            public Target2 Deserialize(ref JsonReader reader, IJsonFormatterResolver formatterResolver)
            {
                return default;
            }
        }
    }
}
