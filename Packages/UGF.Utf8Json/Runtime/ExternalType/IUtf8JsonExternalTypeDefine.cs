using System;
using System.Collections.Generic;
using Utf8Json;

namespace UGF.Utf8Json.Runtime.ExternalType
{
    /// <summary>
    /// Represents define that stores formatters for the external types.
    /// </summary>
    public interface IUtf8JsonExternalTypeDefine
    {
        /// <summary>
        /// Gets collection of the formatters stored by the target type.
        /// </summary>
        /// <param name="formatters">The collection of the formatters to fill.</param>
        void GetFormatters(IDictionary<Type, IJsonFormatter> formatters);
    }
}
