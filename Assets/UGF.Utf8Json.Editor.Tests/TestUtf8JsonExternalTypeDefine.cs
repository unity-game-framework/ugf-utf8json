using UGF.Utf8Json.Editor.Tests.TestEditorAssembly;
using UGF.Utf8Json.Runtime.ExternalType;

namespace UGF.Utf8Json.Editor.Tests
{
    [Utf8JsonExternalTypeDefine]
    public class TestUtf8JsonExternalTypeDefine : Utf8JsonExternalTypeDefine
    {
        public TestUtf8JsonExternalTypeDefine()
        {
            Types.Add(typeof(TestEditorTargetExternal));
        }
    }
}
