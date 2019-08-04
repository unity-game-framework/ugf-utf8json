using NUnit.Framework;
using TestAssembly.Formatters.UGF.Utf8Json.Runtime.Tests.TestAssembly;
using UGF.Utf8Json.Runtime.Tests.TestAssembly;
using Utf8Json;

namespace UGF.Utf8Json.Runtime.Tests
{
    public class TestUtf8JsonUtility
    {
        [Test]
        public void CreateDefaultResolver()
        {
            Utf8JsonFormatterResolver resolver = Utf8JsonUtility.CreateDefaultResolver();

            Assert.NotNull(resolver);
            Assert.AreEqual(3, resolver.Resolvers.Count);
        }

        [Test, Order(1)]
        public void SetFormatterCache()
        {
            var testTargetFormatter = new TestTargetFormatter();

            Utf8JsonUtility.SetFormatterCache(typeof(TestTarget), null);

            bool result0 = Utf8JsonUtility.TryGetFormatterCache(typeof(TestTarget), out IJsonFormatter formatter);

            Assert.False(result0);
            Assert.Null(formatter);

            Utf8JsonUtility.SetFormatterCache(typeof(TestTarget), testTargetFormatter);

            bool result1 = Utf8JsonUtility.TryGetFormatterCache(typeof(TestTarget), out formatter);

            Assert.True(result1);
            Assert.NotNull(formatter);
        }

        [Test, Order(2)]
        public void TryGetFormatterCache()
        {
            bool result0 = Utf8JsonUtility.TryGetFormatterCache(typeof(TestTarget), out IJsonFormatter formatter);

            Assert.True(result0);
            Assert.NotNull(formatter);
        }
    }
}
