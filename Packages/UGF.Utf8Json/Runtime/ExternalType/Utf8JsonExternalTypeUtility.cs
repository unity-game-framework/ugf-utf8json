using System;
using System.Collections.Generic;
using System.Reflection;
using UGF.Assemblies.Runtime;
using UGF.Types.Runtime;
using Utf8Json;

namespace UGF.Utf8Json.Runtime.ExternalType
{
    /// <summary>
    /// Provides utilities to work with external types.
    /// </summary>
    public static class Utf8JsonExternalTypeUtility
    {
        /// <summary>
        /// Gets collection of the formatters stored by the target type from the all found defines.
        /// </summary>
        /// <param name="formatters">The collection of the formatters to fill.</param>
        /// <param name="assembly">The assembly to search.</param>
        public static void GetFormatters(IDictionary<Type, IJsonFormatter> formatters, Assembly assembly = null)
        {
            if (formatters == null) throw new ArgumentNullException(nameof(formatters));

            foreach (Type type in AssemblyUtility.GetBrowsableTypes<Utf8JsonExternalTypeDefineAttribute>(assembly))
            {
                if (TypesUtility.TryCreateType(type, out IUtf8JsonExternalTypeDefine define))
                {
                    define.GetFormatters(formatters);
                }
            }
        }

        /// <summary>
        /// Gets collection of the external type defines.
        /// </summary>
        /// <param name="defines">The collection of the external type defines to fill.</param>
        /// <param name="assembly">The assembly to search.</param>
        public static void GetDefines(ICollection<IUtf8JsonExternalTypeDefine> defines, Assembly assembly = null)
        {
            if (defines == null) throw new ArgumentNullException(nameof(defines));

            foreach (Type type in AssemblyUtility.GetBrowsableTypes<Utf8JsonExternalTypeDefineAttribute>(assembly))
            {
                if (TypesUtility.TryCreateType(type, out IUtf8JsonExternalTypeDefine define))
                {
                    defines.Add(define);
                }
            }
        }
    }
}
