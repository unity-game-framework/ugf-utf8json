// ReSharper disable all

#pragma warning disable

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Utf8Json.UniversalCodeGenerator;

namespace Utf8Json.CodeGenerator.Generator
{
    internal partial class FormatterTemplate
    {
        public string Namespace;
        public ObjectSerializationInfo[] objectSerializationInfos;
    }

    internal partial class EnumTemplate
    {
        public string Namespace;
        public EnumSerializationInfo[] enumSerializationInfos;
    }

    internal partial class ResolverTemplate
    {
        public string Namespace;
        public string FormatterNamespace { get; set; }
        public string ResolverName = "GeneratedResolver";
        public IResolverRegisterInfo[] registerInfos;
    }
}
