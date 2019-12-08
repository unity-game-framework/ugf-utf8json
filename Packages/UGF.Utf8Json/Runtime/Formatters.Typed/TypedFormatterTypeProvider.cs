using System;
using System.Collections.Generic;
using System.Text;
using Utf8Json;
using Utf8Json.Internal;

namespace UGF.Utf8Json.Runtime.Formatters.Typed
{
    public class TypedFormatterTypeProvider : ITypedFormatterTypeProvider
    {
        public Encoding Encoding { get; }
        public bool AutoAddUnknownTypes { get; set; } = true;

        private readonly Dictionary<Type, byte[]> m_typeToNameBytes = new Dictionary<Type, byte[]>();
        private readonly Dictionary<int, Type> m_idToType = new Dictionary<int, Type>();
        private readonly AutomataDictionary m_typeNameToId = new AutomataDictionary();

        public TypedFormatterTypeProvider(Encoding encoding = null)
        {
            Encoding = encoding ?? Encoding.UTF8;
        }

        public byte[] Add(Type type)
        {
            if (type == null) throw new ArgumentNullException(nameof(type));
            if (m_typeToNameBytes.ContainsKey(type)) throw new ArgumentException($"The specified type already exists: '{type}'.");

            int id = type.GetHashCode();
            byte[] typeName = GetTypeNameBytes(type);

            m_typeToNameBytes.Add(type, typeName);
            m_idToType.Add(id, type);
            m_typeNameToId.Add(typeName, id);

            return typeName;
        }

        public bool TryGetTypeName(Type type, out byte[] typeName)
        {
            if (type == null) throw new ArgumentNullException(nameof(type));

            if (!m_typeToNameBytes.TryGetValue(type, out typeName) && AutoAddUnknownTypes)
            {
                typeName = Add(type);
            }

            return typeName != null;
        }

        public bool TryGetType(ArraySegment<byte> typeName, out Type type)
        {
            if (!m_typeNameToId.TryGetValue(typeName, out int id))
            {
                if (AutoAddUnknownTypes && TryGetTypeFromName(typeName, out type))
                {
                    Add(type);
                    return true;
                }

                type = null;
                return false;
            }

            return m_idToType.TryGetValue(id, out type);
        }

        private bool TryGetTypeFromName(ArraySegment<byte> typeName, out Type type)
        {
            if (typeName.Array != null)
            {
                string name = Encoding.GetString(typeName.Array, typeName.Offset, typeName.Count);

                type = Type.GetType(name);

                return type != null;
            }

            type = null;
            return false;
        }

        private static byte[] GetTypeNameBytes(Type type)
        {
            string typeName = GetTypeName(type);

            return JsonWriter.GetEncodedPropertyNameWithoutQuotation(typeName);
        }

        private static string GetTypeName(Type type)
        {
            string assemblyName = type.Assembly.GetName().Name;
            string name = type.FullName;

            return !string.IsNullOrEmpty(name)
                ? $"{name}, {assemblyName}"
                : $"{type.Name}, {assemblyName}";
        }
    }
}
