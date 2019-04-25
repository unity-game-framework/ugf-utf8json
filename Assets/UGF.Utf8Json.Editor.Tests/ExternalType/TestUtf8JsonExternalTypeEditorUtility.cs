using System.IO;
using NUnit.Framework;
using UGF.Code.Generate.Editor.Container;
using UGF.Utf8Json.Editor.ExternalType;
using UGF.Utf8Json.Editor.Tests.TestEditorAssembly;
using UnityEngine;

namespace UGF.Utf8Json.Editor.Tests.ExternalType
{
    public class TestUtf8JsonExternalTypeEditorUtility
    {
        [Test]
        public void GenerateExternalContainers()
        {
            Assert.Ignore();
        }

        [Test]
        public void CreateContainerFromType()
        {
            CodeGenerateContainer container = Utf8JsonExternalTypeEditorUtility.CreateContainer(typeof(TestEditorTargetExternal));

            Assert.NotNull(container);
            Assert.AreEqual(5, container.Fields.Count);
        }

        [Test]
        public void CreateContainerFromInfo()
        {
            string path = "Assets/UGF.Utf8Json.Editor.Tests/ExternalType/TestExternalTypeInfo2.txt";
            var info = JsonUtility.FromJson<Utf8JsonExternalTypeAssetInfo>(File.ReadAllText(path));

            CodeGenerateContainer container = Utf8JsonExternalTypeEditorUtility.CreateContainer(info);

            Assert.NotNull(container);
            Assert.AreEqual(1, container.Fields.Count);
            Assert.True(container.Fields.Exists(x => x.Name == "BoolValue"));
            Assert.False(container.Fields.Exists(x => x.Name == "Bounds"));
        }

        [Test]
        public void GetExternalTypeAssetInfoFromPath()
        {
            string path = "Assets/UGF.Utf8Json.Editor.Tests/ExternalType/TestExternalTypeInfo.txt";

            Utf8JsonExternalTypeAssetInfo info = Utf8JsonExternalTypeEditorUtility.GetExternalTypeAssetInfoFromPath(path);

            Assert.NotNull(info);
        }

        [Test]
        public void IsValidExternalType()
        {
            bool result0 = Utf8JsonExternalTypeEditorUtility.IsValidExternalType(typeof(TestEditorTarget));

            Assert.True(result0);
        }
    }
}
