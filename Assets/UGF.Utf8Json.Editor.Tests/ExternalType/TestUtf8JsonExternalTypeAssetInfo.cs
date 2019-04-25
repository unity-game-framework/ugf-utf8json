using System;
using System.IO;
using NUnit.Framework;
using UGF.Utf8Json.Editor.ExternalType;
using UGF.Utf8Json.Editor.Tests.TestEditorAssembly;
using UnityEngine;

namespace UGF.Utf8Json.Editor.Tests.ExternalType
{
    public class TestUtf8JsonExternalTypeAssetInfo
    {
        private Utf8JsonExternalTypeAssetInfo m_info;

        [SetUp]
        public void Setup()
        {
            string text = File.ReadAllText("Assets/UGF.Utf8Json.Editor.Tests/ExternalType/TestExternalTypeInfo.txt");

            m_info = JsonUtility.FromJson<Utf8JsonExternalTypeAssetInfo>(text);
        }

        [Test]
        public void Type()
        {
            string expected = "UGF.Utf8Json.Editor.Tests.TestEditorAssembly.TestEditorTargetExternal, TestEditorAssembly, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null";

            Assert.AreEqual(expected, m_info.Type);
        }

        [Test]
        public void Members()
        {
            Assert.NotNull(m_info.Members);
            Assert.AreEqual(2, m_info.Members.Count);
        }

        [Test]
        public void GetTargetType()
        {
            Type type = m_info.GetTargetType();

            Assert.NotNull(type);
            Assert.AreEqual(typeof(TestEditorTargetExternal), type);
        }

        [Test]
        public void TryGetMember()
        {
            bool result0 = m_info.TryGetMember("member0", out Utf8JsonExternalTypeAssetInfo.MemberInfo member0);
            bool result1 = m_info.TryGetMember("member1", out Utf8JsonExternalTypeAssetInfo.MemberInfo member1);
            bool result2 = m_info.TryGetMember("member2", out Utf8JsonExternalTypeAssetInfo.MemberInfo member2);

            Assert.True(result0);
            Assert.True(result1);
            Assert.False(result2);
        }

        [Test]
        public void IsValid()
        {
            bool result0 = m_info.IsValid();
            bool result1 = JsonUtility.FromJson<Utf8JsonExternalTypeAssetInfo>("{}").IsValid();

            Assert.True(result0);
            Assert.False(result1);
        }
    }
}
