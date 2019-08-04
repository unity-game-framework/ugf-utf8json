using NUnit.Framework;

namespace UGF.Utf8Json.Runtime.Tests
{
    public class TestUtf8JsonUtility
    {
        [Test]
        public void CreateDefaultResolver()
        {
            Utf8JsonFormatterResolver resolver = Utf8JsonUtility.CreateDefaultResolver();

            Assert.NotNull(resolver);
            Assert.AreEqual(2, resolver.Resolvers.Count);
        }
    }
}
