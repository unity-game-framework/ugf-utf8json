using UGF.Utf8Json.Runtime;

namespace Code.Tests
{
    [Utf8JsonSerializable]
    public class TestTarget
    {
        public bool Bool { get; set; }
        public int Int { get; set; }
        public float Float { get; set; }
        public long Long { get; set; }
    }
}
