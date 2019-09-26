using System;
using NUnit.Framework;
using UGF.Utf8Json.Runtime.Formatters.Union;
using UGF.Utf8Json.Runtime.Tests.Resolvers;
using Utf8Json;

namespace UGF.Utf8Json.Runtime.Tests.Formatters.Union
{
    public class TestUnionFormatter
    {
        private readonly string m_target1Data = "{\"type\":\"one\",\"boolValue\":true}";
        private readonly string m_target1Data2 = "{\"type\":\"one\",\"boolValue\":false}";
        private readonly string m_target2Data = "{\"type\":\"two\",\"intValue\":10}";
        private readonly string m_target2Data2 = "{\"type\":\"two\",\"intValue\":100}";
        private Utf8JsonFormatterResolver m_resolver;

        private class Formatter : UnionFormatter
        {
            public Formatter()
            {
                AddFormatter(typeof(Target1), "one", new UnionFormatterWrapper<Target1, ITarget>());
                AddFormatter(typeof(Target2), "two", new UnionFormatterWrapper<Target2, ITarget>());
            }
        }

        private class FormatterContainer<TTarget> : IJsonFormatter<TTarget>
        {
            public UnionFormatter UnionFormatter { get; }

            public FormatterContainer(UnionFormatter unionFormatter)
            {
                UnionFormatter = unionFormatter;
            }

            public void Serialize(ref JsonWriter writer, TTarget value, IJsonFormatterResolver formatterResolver)
            {
                UnionFormatter.Serialize(ref writer, value, formatterResolver);
            }

            public TTarget Deserialize(ref JsonReader reader, IJsonFormatterResolver formatterResolver)
            {
                return (TTarget)UnionFormatter.Deserialize(ref reader, formatterResolver);
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

        [OneTimeSetUp]
        public void Setup()
        {
            m_resolver = Utf8JsonUtility.CreateDefaultResolver();
            m_resolver.AddFormatter(new FormatterContainer<ITarget>(new Formatter()));
            m_resolver.AddResolver(UGFUtf8JsonRuntimeTestsResolver.Instance);
        }

        public void AddFormatter()
        {
        }

        public void RemoveFormatter()
        {
        }

        public void Clear()
        {
        }

        public void GetFormatter()
        {
        }

        public void TryGetFormatterByTargetType()
        {
        }

        public void TryGetFormatterByIdentifier()
        {
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
