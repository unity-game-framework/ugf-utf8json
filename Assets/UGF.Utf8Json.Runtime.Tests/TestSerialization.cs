using NUnit.Framework;
using UnityEngine;
using Utf8Json;
using Utf8Json.Resolvers;
using Utf8Json.Unity;

namespace UGF.Utf8Json.Runtime.Tests
{
    public class TestSerialization
    {
        private readonly string m_targetSerialized = "{\"Name\":\"Target\",\"BoolValue\":true,\"FloatValue\":50.5,\"IntValue\":50}";
        private readonly string m_target2Serialized = "{\"Vector2\":{\"x\":1,\"y\":1},\"Bounds\":{\"center\":{\"x\":1,\"y\":1,\"z\":1},\"size\":{\"x\":1,\"y\":1,\"z\":1}}}";
        
        static TestSerialization()
        {
            CompositeResolver.Register(UnityResolver.Instance, BuiltinResolver.Instance);
        }

        public class Target
        {
            public string Name { get; set; } = "Target";
            public bool BoolValue { get; set; } = true;
            public float FloatValue { get; set; } = 50.5F;
            public int IntValue { get; set; } = 50;
        }
        
        public class Target2
        {
            public Vector2 Vector2 { get; set; } = Vector2.one;
            public Bounds Bounds { get; set; } = new Bounds(Vector3.one, Vector3.one);
        }
        
        [Test]
        public void Serialize()
        {
            var target = new Target();

            string data = JsonSerializer.ToJsonString(target, CompositeResolver.Instance);
            
            Assert.AreEqual(m_targetSerialized, data);
        }

        [Test]
        public void Serialize2()
        {
            var target = new Target2();

            string data = JsonSerializer.ToJsonString(target, CompositeResolver.Instance);
            
            Assert.AreEqual(m_target2Serialized, data);
        }

        [Test]
        public void Deserialize()
        {
            var target = JsonSerializer.Deserialize<Target>(m_targetSerialized, CompositeResolver.Instance);
            
            Assert.NotNull(target);
            Assert.AreEqual("Target", target.Name);
            Assert.AreEqual(true, target.BoolValue);
            Assert.AreEqual(50.5F, target.FloatValue);
            Assert.AreEqual(50, target.IntValue);
        }

        [Test]
        public void Deserialize2()
        {
            var target = JsonSerializer.Deserialize<Target2>(m_target2Serialized, CompositeResolver.Instance);
            
            Assert.NotNull(target);
            Assert.AreEqual(Vector2.one, target.Vector2);
            Assert.AreEqual(new Bounds(Vector3.one, Vector3.one), target.Bounds);
        }
    }
}