using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Utf8Json.CodeGenerator.Generator;

namespace Utf8Json.UniversalCodeGenerator
{
    /// <summary>
    /// Provides utility to work with Utf8Json UniversalCodeGenerator.
    /// </summary>
    public static class Utf8JsonUniversalCodeGeneratorUtility
    {
        /// <summary>
        /// Generates resolver and formatters from the specified files and other arguments.
        /// </summary>
        /// <param name="inputFiles">The collection of input .cs files.</param>
        /// <param name="resolverName">The generated resolver name.</param>
        /// <param name="namespaceRoot">The root namespace for generated resolver and formatters.</param>
        /// <param name="arguments">The generate arguments to control additional generation behaviour.</param>
        public static string Generate(IEnumerable<string> inputFiles, string resolverName, string namespaceRoot, Utf8JsonGenerateArguments arguments = default)
        {
            return Generate(inputFiles, null, null, false, resolverName, namespaceRoot, arguments);
        }

        /// <summary>
        /// Generates resolver and formatters from the specified files and other arguments.
        /// </summary>
        /// <param name="inputFiles">The collection of input .cs files.</param>
        /// <param name="inputDirectories">The collection of directories with input .cs files.</param>
        /// <param name="conditionalSymbols">The collection of conditional compile symbols.</param>
        /// <param name="allowInternal">The value that determines whether to allow generate of the internal.</param>
        /// <param name="resolverName">The generated resolver name.</param>
        /// <param name="namespaceRoot">The root namespace for generated resolver and formatters.</param>
        /// <param name="arguments">The generate arguments to control additional generation behaviour.</param>
        public static string Generate(IEnumerable<string> inputFiles, IEnumerable<string> inputDirectories, IEnumerable<string> conditionalSymbols = null, bool allowInternal = false, string resolverName = "GeneratedResolver", string namespaceRoot = "Utf8Json", Utf8JsonGenerateArguments arguments = default)
        {
            return InternalGenerate(InternalGetArguments(inputFiles, inputDirectories, conditionalSymbols, allowInternal, resolverName, namespaceRoot), arguments);
        }

        /// <summary>
        /// Generates formatters without resolver from the specified files and other arguments.
        /// </summary>
        /// <param name="inputFiles">The collection of input .cs files.</param>
        /// <param name="namespaceRoot">The root namespace for generated resolver and formatters.</param>
        /// <param name="arguments">The generate arguments to control additional generation behaviour.</param>
        public static string GenerateFormatters(IEnumerable<string> inputFiles, string namespaceRoot, Utf8JsonGenerateArguments arguments = default)
        {
            return GenerateFormatters(inputFiles, null, null, false, namespaceRoot, arguments);
        }

        /// <summary>
        /// Generates formatters without resolver from the specified files and other arguments.
        /// </summary>
        /// <param name="inputFiles">The collection of input .cs files.</param>
        /// <param name="inputDirectories">The collection of directories with input .cs files.</param>
        /// <param name="conditionalSymbols">The collection of conditional compile symbols.</param>
        /// <param name="allowInternal">The value that determines whether to allow generate of the internal.</param>
        /// <param name="namespaceRoot">The root namespace for generated resolver and formatters.</param>
        /// <param name="arguments">The generate arguments to control additional generation behaviour.</param>
        public static string GenerateFormatters(IEnumerable<string> inputFiles, IEnumerable<string> inputDirectories, IEnumerable<string> conditionalSymbols = null, bool allowInternal = false, string namespaceRoot = "Utf8Json", Utf8JsonGenerateArguments arguments = default)
        {
            return InternalGenerateFormatters(InternalGetArguments(inputFiles, inputDirectories, conditionalSymbols, allowInternal, "GeneratedResolver", namespaceRoot), arguments);
        }

        internal static GeneratorArguments InternalGetArguments(IEnumerable<string> inputFiles, IEnumerable<string> inputDirectories = null, IEnumerable<string> conditionalSymbols = null, bool allowInternal = false, string resolverName = "GeneratedResolver", string namespaceRoot = "Utf8Json")
        {
            if (inputFiles == null) throw new ArgumentNullException(nameof(inputFiles));
            if (string.IsNullOrEmpty(resolverName)) throw new ArgumentException("Resolver name must be specified.", nameof(resolverName));

            return new GeneratorArguments
            {
                InputFiles = new List<string>(inputFiles),
                InputDirectories = inputDirectories != null ? new List<string>(inputDirectories) : new List<string>(),
                ConditionalSymbols = conditionalSymbols != null ? new List<string>(conditionalSymbols) : new List<string>(),
                AllowInternal = allowInternal,
                ResolverName = resolverName,
                NamespaceRoot = namespaceRoot
            };
        }

        internal static string InternalGenerate(GeneratorArguments arguments, Utf8JsonGenerateArguments arguments2)
        {
            var collector = new TypeCollector(arguments.InputFiles, arguments.InputDirectories, arguments.ConditionalSymbols, !arguments.AllowInternal, arguments2);
            string namespaceDot = arguments.GetNamespaceDot();

            (ObjectSerializationInfo[] objectInfo, GenericSerializationInfo[] genericInfo) = collector.Collect();

            FormatterTemplate[] objectFormatterTemplates = objectInfo
                .GroupBy(x => x.Namespace)
                .Select(x => new FormatterTemplate
                {
                    Namespace = $"{namespaceDot}Formatters{((x.Key == null) ? "" : $".{x.Key}")}",
                    objectSerializationInfos = x.ToArray(),
                })
                .ToArray();

            var resolverTemplate = new ResolverTemplate()
            {
                Namespace = $"{namespaceDot}Resolvers",
                FormatterNamespace = $"{namespaceDot}Formatters",
                ResolverName = arguments.ResolverName,
                registerInfos = genericInfo.Cast<IResolverRegisterInfo>().Concat(objectInfo).ToArray()
            };

            var builder = new StringBuilder();

            builder.AppendLine(resolverTemplate.TransformText());
            builder.AppendLine();

            foreach (FormatterTemplate item in objectFormatterTemplates)
            {
                builder.AppendLine(item.TransformText());
            }

            return builder.ToString();
        }

        internal static string InternalGenerateFormatters(GeneratorArguments arguments, Utf8JsonGenerateArguments arguments2)
        {
            var collector = new TypeCollector(arguments.InputFiles, arguments.InputDirectories, arguments.ConditionalSymbols, !arguments.AllowInternal, arguments2);
            string namespaceDot = arguments.GetNamespaceDot();

            (ObjectSerializationInfo[] objectInfo, GenericSerializationInfo[] _) = collector.Collect();

            FormatterTemplate[] objectFormatterTemplates = objectInfo
                .GroupBy(x => x.Namespace)
                .Select(x => new FormatterTemplate
                {
                    Namespace = $"{namespaceDot}Formatters{((x.Key == null) ? "" : $".{x.Key}")}",
                    objectSerializationInfos = x.ToArray(),
                })
                .ToArray();

            var builder = new StringBuilder();

            foreach (FormatterTemplate item in objectFormatterTemplates)
            {
                builder.AppendLine(item.TransformText());
            }

            return builder.ToString();
        }
    }
}
