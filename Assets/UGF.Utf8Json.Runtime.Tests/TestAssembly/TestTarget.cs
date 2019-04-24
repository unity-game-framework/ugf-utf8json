using UnityEngine;

namespace UGF.Utf8Json.Runtime.Tests.TestAssembly
{
    [Utf8JsonSerializable]
    public class TestTarget
    {
        public string Name { get; set; } = "TestTarget";
        public bool BoolValue { get; set; } = true;
        public float FloatValue { get; set; } = 50.5F;
        public int IntValue { get; set; } = 50;
        public Vector2 Vector2 { get; set; } = Vector2.one;
        public Bounds Bounds { get; set; } = new Bounds(Vector3.one, Vector3.one);
    }
}
