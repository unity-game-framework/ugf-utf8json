using System;
using System.Text;
using NUnit.Framework;
using UGF.Utf8Json.Runtime.Formatters.Union;
using UGF.Utf8Json.Runtime.Tests.Formatters.UGF.Utf8Json.Runtime.Tests.Formatters.Union;
using Utf8Json;

namespace UGF.Utf8Json.Runtime.Tests.Formatters.Union
{
    public class TestUnionSerializer
    {
        private readonly string m_targetData = "{\"type\":\"target\",\"value\":10}";

        [Serializable]
        public class Target
        {
            public int Value { get; set; } = 10;
        }

        [Test]
        public void TypePropertyName()
        {
            Utf8JsonFormatterResolver resolver = Utf8JsonUtility.CreateDefaultResolver();
            var formatter = new TestUnionSerializer_TargetFormatter();

            var serializer0 = new UnionSerializer();
            var serializer1 = new UnionSerializer("type_id");
            var serializer2 = new UnionSerializer("type-id");

            serializer0.Add(typeof(Target), "target");
            serializer1.Add(typeof(Target), "target");
            serializer2.Add(typeof(Target), "target");

            var writer0 = new JsonWriter();
            var writer1 = new JsonWriter();
            var writer2 = new JsonWriter();

            serializer0.Serialize(ref writer0, new Target(), formatter, resolver);
            serializer1.Serialize(ref writer1, new Target(), formatter, resolver);
            serializer2.Serialize(ref writer2, new Target(), formatter, resolver);

            string result0 = writer0.ToString();
            string result1 = writer1.ToString();
            string result2 = writer2.ToString();

            string expected0 = "{\"type\":\"target\",\"value\":10}";
            string expected1 = "{\"type_id\":\"target\",\"value\":10}";
            string expected2 = "{\"type-id\":\"target\",\"value\":10}";

            Assert.AreEqual(expected0, result0);
            Assert.AreEqual(expected1, result1);
            Assert.AreEqual(expected2, result2);
        }

        [Test]
        public void Add()
        {
            var serializer = new UnionSerializer();

            int result0 = serializer.Add(typeof(int), "int");
            int result1 = serializer.Add(typeof(bool), "bool");

            Assert.AreNotEqual(result0, result1);
        }

        [Test]
        public void Remove()
        {
            var serializer = new UnionSerializer();

            int result0 = serializer.Add(typeof(int), "int");

            Assert.Greater(result0, int.MinValue);
            Assert.AreEqual(1, serializer.Types.Count);

            serializer.Remove(typeof(int));

            Assert.AreEqual(0, serializer.Types.Count);
        }

        [Test]
        public void Clear()
        {
            var serializer = new UnionSerializer();

            serializer.Add(typeof(int), "int");

            Assert.AreEqual(1, serializer.Types.Count);

            serializer.Clear();

            Assert.AreEqual(0, serializer.Types.Count);
        }

        [Test]
        public void GetIdentifier()
        {
            var serializer = new UnionSerializer();

            int result0 = serializer.Add(typeof(int), "int");
            int result1 = serializer.Add(typeof(bool), "bool");

            int result01 = serializer.GetIdentifier(typeof(int));
            int result11 = serializer.GetIdentifier(typeof(bool));

            Assert.AreEqual(result0, result01);
            Assert.AreEqual(result1, result11);
        }

        [Test]
        public void TryGetIdentifier()
        {
            var serializer = new UnionSerializer();

            int result0 = serializer.Add(typeof(int), "int");
            int result1 = serializer.Add(typeof(bool), "bool");

            bool result02 = serializer.TryGetIdentifier(typeof(int), out int result01);
            bool result12 = serializer.TryGetIdentifier(typeof(bool), out int result11);
            bool result22 = serializer.TryGetIdentifier(typeof(float), out _);

            Assert.True(result02);
            Assert.True(result12);
            Assert.False(result22);
            Assert.AreEqual(result0, result01);
            Assert.AreEqual(result1, result11);
        }

        [Test]
        public void Serialize()
        {
            Utf8JsonFormatterResolver resolver = Utf8JsonUtility.CreateDefaultResolver();
            var formatter = new TestUnionSerializer_TargetFormatter();
            var serializer = new UnionSerializer();
            var writer = new JsonWriter();
            var target = new Target();

            serializer.Add(typeof(Target), "target");
            serializer.Serialize(ref writer, target, formatter, resolver);

            string result0 = writer.ToString();

            Assert.AreEqual(m_targetData, result0);
        }

        [Test]
        public void Deserialize()
        {
            Utf8JsonFormatterResolver resolver = Utf8JsonUtility.CreateDefaultResolver();
            var formatter = new TestUnionSerializer_TargetFormatter();
            var serializer = new UnionSerializer();
            var reader = new JsonReader(Encoding.UTF8.GetBytes(m_targetData));

            serializer.Add(typeof(Target), "target");

            Target target = serializer.Deserialize(ref reader, formatter, resolver);

            Assert.NotNull(target);
            Assert.AreEqual(10, target.Value);
        }

        [Test]
        public void ReadTypeIdentifier()
        {
            var serializer = new UnionSerializer();

            int id0 = serializer.Add(typeof(int), "int");
            int id1 = serializer.Add(typeof(bool), "bool");

            string data0 = "{\"type\":\"int\"}";
            string data1 = "{\"type\":\"bool\"}";

            var reader0 = new JsonReader(Encoding.UTF8.GetBytes(data0));
            var reader1 = new JsonReader(Encoding.UTF8.GetBytes(data1));

            int result0 = serializer.ReadTypeIdentifier(reader0);
            int result1 = serializer.ReadTypeIdentifier(reader1);

            Assert.AreEqual(id0, result0);
            Assert.AreEqual(id1, result1);
        }
    }
}
