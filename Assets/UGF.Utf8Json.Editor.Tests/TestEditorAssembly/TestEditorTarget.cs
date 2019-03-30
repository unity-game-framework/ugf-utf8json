using UGF.Utf8Json.Runtime;

namespace UGF.Utf8Json.Editor.Tests.TestEditorAssembly
{
    using UnityEngine;
    
    [Utf8JsonSerializable]
    public class TestEditorTarget
    {
        public bool BoolValue { get; set; } = true;
        public string StringValue { get; set; } = "Test";
        public Vector2 Vector2 { get; set; }
    }
}
