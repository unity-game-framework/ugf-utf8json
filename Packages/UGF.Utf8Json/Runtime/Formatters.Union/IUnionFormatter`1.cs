namespace UGF.Utf8Json.Runtime.Formatters.Union
{
    public interface IUnionFormatter<in TTarget> : IUnionFormatter
    {
        void AddFormatter<T>(string typeIdentifier) where T : TTarget;
    }
}
