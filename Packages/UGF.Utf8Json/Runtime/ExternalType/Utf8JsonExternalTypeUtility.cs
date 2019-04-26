using System;
using System.Collections.Generic;
using System.Reflection;
using UGF.Assemblies.Runtime;
using UGF.Types.Runtime;
using Utf8Json;

namespace UGF.Utf8Json.Runtime.ExternalType
{
    public static class Utf8JsonExternalTypeUtility
    {
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
