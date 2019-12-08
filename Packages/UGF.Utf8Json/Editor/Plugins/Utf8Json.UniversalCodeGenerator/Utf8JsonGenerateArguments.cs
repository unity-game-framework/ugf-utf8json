// ReSharper disable all

namespace Utf8Json.UniversalCodeGenerator
{
    /// <summary>
    /// Represents arguments used to generate Utf8Json formatters.
    /// </summary>
    public struct Utf8JsonGenerateArguments
    {
        /// <summary>
        /// The value that determines whether to ignore read-only fields and properties.
        /// </summary>
        public bool IgnoreReadOnly;

        /// <summary>
        /// The value that determines whether to ingore types wihtout any serializable members.
        /// </summary>
        public bool IgnoreEmpty;

        /// <summary>
        /// The value that determines whether target type must contains specific attribute.
        /// </summary>
        public bool IsTypeRequireAttribute;

        /// <summary>
        /// The short name of attribute that type must contains.
        /// </summary>
        public string TypeRequiredAttributeShortName;
    }
}
