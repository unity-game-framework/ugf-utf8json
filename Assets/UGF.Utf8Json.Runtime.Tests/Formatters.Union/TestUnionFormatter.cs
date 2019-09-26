using System;
using NUnit.Framework;
using UGF.Utf8Json.Runtime.Formatters.Union;
using UGF.Utf8Json.Runtime.Tests.Resolvers;
using Utf8Json;
using Utf8Json.Formatters;

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
            public Formatter() : base(new UnionSerializer())
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

        [Test]
        public void AddFormatter()
        {
            var serializer = new UnionSerializer();
            var formatter = new UnionFormatter(serializer);

            Assert.AreEqual(0, serializer.Types.Count);
            Assert.AreEqual(0, formatter.Formatters.Count);

            formatter.AddFormatter(typeof(int), "int", new Int32Formatter());

            Assert.AreEqual(1, serializer.Types.Count);
            Assert.AreEqual(1, formatter.Formatters.Count);
        }

        [Test]
        public void RemoveFormatter()
        {
            var serializer = new UnionSerializer();
            var formatter = new UnionFormatter(serializer);

            formatter.AddFormatter(typeof(int), "int", new Int32Formatter());

            Assert.AreEqual(1, serializer.Types.Count);
            Assert.AreEqual(1, formatter.Formatters.Count);

            formatter.RemoveFormatter(typeof(int));

            Assert.AreEqual(0, serializer.Types.Count);
            Assert.AreEqual(0, formatter.Formatters.Count);
        }

        [Test]
        public void Clear()
        {
            var serializer = new UnionSerializer();
            var formatter = new UnionFormatter(serializer);

            formatter.AddFormatter(typeof(int), "int", new Int32Formatter());

            Assert.AreEqual(1, serializer.Types.Count);
            Assert.AreEqual(1, formatter.Formatters.Count);

            formatter.Clear();

            Assert.AreEqual(0, serializer.Types.Count);
            Assert.AreEqual(0, formatter.Formatters.Count);
        }

        [Test]
        public void GetFormatter()
        {
            var serializer = new UnionSerializer();
            var formatter = new UnionFormatter(serializer);
            var formatter0 = new Int32Formatter();

            formatter.AddFormatter(typeof(int), "int", formatter0);

            var result = formatter.GetFormatter<IJsonFormatter<int>>(typeof(int));

            Assert.NotNull(result);
            Assert.AreEqual(formatter0, result);
        }

        [Test]
        public void TryGetFormatterByTargetType()
        {
            var serializer = new UnionSerializer();
            var formatter = new UnionFormatter(serializer);
            var formatter0 = new Int32Formatter();
            var formatter1 = new SingleFormatter();

            formatter.AddFormatter(typeof(int), "int", formatter0);
            formatter.AddFormatter(typeof(float), "float", formatter1);

            bool result0 = formatter.TryGetFormatter(typeof(int), out IJsonFormatter<int> formatter01);
            bool result1 = formatter.TryGetFormatter(typeof(float), out IJsonFormatter<float> formatter11);
            bool result2 = formatter.TryGetFormatter(typeof(bool), out IJsonFormatter<bool> formatter21);

            Assert.True(result0);
            Assert.True(result1);
            Assert.False(result2);
            Assert.NotNull(formatter01);
            Assert.NotNull(formatter11);
            Assert.Null(formatter21);
            Assert.AreEqual(formatter0, formatter01);
            Assert.AreEqual(formatter1, formatter11);
        }

        [Test]
        public void TryGetFormatterByIdentifier()
        {
            var serializer = new UnionSerializer();
            var formatter = new UnionFormatter(serializer);
            var formatter0 = new Int32Formatter();
            var formatter1 = new SingleFormatter();

            formatter.AddFormatter(typeof(int), "int", formatter0);
            formatter.AddFormatter(typeof(float), "float", formatter1);

            int id0 = serializer.GetIdentifier(typeof(int));
            int id1 = serializer.GetIdentifier(typeof(float));

            bool result0 = formatter.TryGetFormatter(id0, out IJsonFormatter<int> formatter01);
            bool result1 = formatter.TryGetFormatter(id1, out IJsonFormatter<float> formatter11);
            bool result2 = formatter.TryGetFormatter(2, out IJsonFormatter<bool> formatter21);

            Assert.True(result0);
            Assert.True(result1);
            Assert.False(result2);
            Assert.NotNull(formatter01);
            Assert.NotNull(formatter11);
            Assert.Null(formatter21);
            Assert.AreEqual(formatter0, formatter01);
            Assert.AreEqual(formatter1, formatter11);
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
