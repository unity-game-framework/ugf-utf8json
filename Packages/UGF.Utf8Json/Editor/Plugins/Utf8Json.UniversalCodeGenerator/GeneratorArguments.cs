// ReSharper disable all

using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using Utf8Json.CodeGenerator.Generator;

namespace Utf8Json.UniversalCodeGenerator
{
    internal class GeneratorArguments
    {
        public List<string> InputFiles { get; set; }
        public List<string> InputDirectories { get; set; }
        public string OutputPath { get; set; }
        public List<string> ConditionalSymbols { get; set; }
        public string ResolverName { get; set; }
        public string NamespaceRoot { get; set; }
        public bool AllowInternal { get; set; }

        public bool IsParsed { get; set; }

        public GeneratorArguments()
        {
        }

        public string GetNamespaceDot()
        {
            return string.IsNullOrWhiteSpace(NamespaceRoot) ? "" : NamespaceRoot + ".";
        }
    }
}
