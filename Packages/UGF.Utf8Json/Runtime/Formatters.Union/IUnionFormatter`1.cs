namespace UGF.Utf8Json.Runtime.Formatters.Union
{
    /// <summary>
    /// Represents generic implementation of the union formatter.
    /// </summary>
    public interface IUnionFormatter<in TTarget> : IUnionFormatter
    {
        /// <summary>
        /// Adds the automatically created formatter for the specified target type.
        /// </summary>
        /// <param name="typeIdentifier">The type identifier.</param>
        void AddFormatter<T>(string typeIdentifier) where T : TTarget;
    }
}
