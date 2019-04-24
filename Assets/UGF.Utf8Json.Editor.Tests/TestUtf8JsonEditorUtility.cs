using NUnit.Framework;

namespace UGF.Utf8Json.Editor.Tests
{
    public class TestUtf8JsonEditorUtility
    {
        [Test]
        public void GenerateAssetFromAssembly()
        {
            Assert.Ignore();
        }

        [Test]
        public void GenerateFromAssembly()
        {
            Assert.Ignore();
        }

        [Test]
        public void GenerateFormatters()
        {
            Assert.Ignore();
        }

        [Test]
        public void GetPathForGeneratedScript()
        {
            string path = Utf8JsonEditorUtility.GetPathForGeneratedScript("Assets/Code/Script.cs");

            Assert.AreEqual("Assets/Code/Script.Utf8Json.Generated.cs", path);
        }

        [Test]
        public void IsSerializableScript()
        {
            bool result = Utf8JsonEditorUtility.IsSerializableScript("Assets/UGF.Utf8Json.Runtime.Tests/TestAssembly/TestTarget.cs");

            Assert.True(result);
        }

        [Test]
        public void IsAssemblyHasGeneratedScript()
        {
            bool result = Utf8JsonEditorUtility.IsAssemblyHasGeneratedScript("Assets/UGF.Utf8Json.Runtime.Tests/TestAssembly/TestAssembly.asmdef");

            Assert.True(result);
        }
    }
}
