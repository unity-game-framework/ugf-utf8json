using System;
using NUnit.Framework;
using UGF.Utf8Json.Runtime.Formatters;
using UGF.Utf8Json.Runtime.Tests.Resolvers;
using Unity.Profiling;
using Utf8Json;

namespace UGF.Utf8Json.Runtime.Tests.Formatters
{
    public class TestUnionFormatter
    {
        private readonly string m_target1Data = "{\"type\":\"one\",\"BoolValue\":true}";
        private readonly string m_target1Data2 = "{\"type\":\"one\",\"BoolValue\":false}";
        private readonly string m_target2Data = "{\"type\":\"two\",\"IntValue\":10}";
        private readonly string m_target2Data2 = "{\"type\":\"two\",\"IntValue\":100}";
        private Utf8JsonFormatterResolver m_resolver;
        private ProfilerMarker m_serializeMethodMarker = new ProfilerMarker("Test.Serialize");
        private ProfilerMarker m_serializeMethodStringMarker = new ProfilerMarker("Test.Serialize.String");

        private class Formatter : UnionFormatter<ITarget>
        {
            public Formatter()
            {
                AddFormatter<Target1>("one");
                AddFormatter<Target2>("two");
            }
        }

        public interface ITarget
        {
        }


        [Serializable]
        public class Target1 : ITarget
        {
            public bool BoolValue { get; set; } = true;
        }

        [Serializable]
        public class Target2 : ITarget
        {
            public int IntValue { get; set; } = 10;
        }

        [SetUp]
        public void Setup()
        {
            m_resolver = Utf8JsonUtility.CreateDefaultResolver();
            m_resolver.AddFormatter(new Formatter());
            m_resolver.Resolvers.Add(UGFUtf8JsonRuntimeTestsResolver.Instance);
        }

        [Test]
        public void Serialize()
        {
            var target1 = new Target1();
            var target2 = new Target2();

            string data1 = JsonSerializer.ToJsonString<ITarget>(target1, m_resolver);
            string data2 = JsonSerializer.ToJsonString<ITarget>(target2, m_resolver);

            Assert.AreEqual(m_target1Data, data1);
            Assert.AreEqual(m_target2Data, data2);
            Assert.Pass($"{data1}\n{data2}");
        }

        [Test]
        public void Deserialize()
        {
            var target1 = JsonSerializer.Deserialize<ITarget>(m_target1Data2, m_resolver);
            var target2 = JsonSerializer.Deserialize<ITarget>(m_target2Data2, m_resolver);

            Assert.AreEqual(typeof(Target1), target1.GetType());
            Assert.AreEqual(typeof(Target2), target2.GetType());

            var target12 = (Target1)target1;
            var target22 = (Target2)target2;

            Assert.AreEqual(false, target12.BoolValue);
            Assert.AreEqual(100, target22.IntValue);
        }
    }
}
