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
        public static string Generate(List<string> inputFiles, string resolverName, string namespaceRoot)
        {
            return Generate(inputFiles, null, null, false, resolverName, namespaceRoot);
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
        public static string Generate(List<string> inputFiles, List<string> inputDirectories, List<string> conditionalSymbols = null, bool allowInternal = false, string resolverName = "GeneratedResolver", string namespaceRoot = "Utf8Json")
        {
            return InternalGenerate(InternalGetArguments(inputFiles, inputDirectories, conditionalSymbols, allowInternal, resolverName, namespaceRoot));
        }

        /// <summary>
        /// Generates formatters without resolver from the specified files and other arguments.
        /// </summary>
        /// <param name="inputFiles">The collection of input .cs files.</param>
        /// <param name="namespaceRoot">The root namespace for generated resolver and formatters.</param>
        public static string GenerateFormatters(List<string> inputFiles, string namespaceRoot)
        {
            return GenerateFormatters(inputFiles, null, null, false, namespaceRoot);
        }
        
        /// <summary>
        /// Generates formatters without resolver from the specified files and other arguments.
        /// </summary>
        /// <param name="inputFiles">The collection of input .cs files.</param>
        /// <param name="inputDirectories">The collection of directories with input .cs files.</param>
        /// <param name="conditionalSymbols">The collection of conditional compile symbols.</param>
        /// <param name="allowInternal">The value that determines whether to allow generate of the internal.</param>
        /// <param name="namespaceRoot">The root namespace for generated resolver and formatters.</param>
        public static string GenerateFormatters(List<string> inputFiles, List<string> inputDirectories, List<string> conditionalSymbols = null, bool allowInternal = false, string namespaceRoot = "Utf8Json")
        {
            return InternalGenerateFormatters(InternalGetArguments(inputFiles, inputDirectories, conditionalSymbols, allowInternal, "GeneratedResolver", namespaceRoot));
        }

        internal static CommandlineArguments InternalGetArguments(List<string> inputFiles, List<string> inputDirectories = null, List<string> conditionalSymbols = null, bool allowInternal = false, string resolverName = "GeneratedResolver", string namespaceRoot = "Utf8Json")
        {
            if (inputFiles == null) throw new ArgumentNullException(nameof(inputFiles));

            if (inputFiles.Count == 0 && (inputDirectories == null || inputDirectories.Count == 0))
            {
                throw new ArgumentException("Input files nor directories not specified.", nameof(inputDirectories));
            }
            
            if (string.IsNullOrEmpty(resolverName)) throw new ArgumentException("Resolver name must be specified.", nameof(resolverName));
            
            return new CommandlineArguments
            {
                InputFiles = new List<string>(inputFiles),
                InputDirectories = inputDirectories != null ? new List<string>(inputDirectories) : new List<string>(),
                ConditionalSymbols = conditionalSymbols != null ? new List<string>(conditionalSymbols) : new List<string>(),
                AllowInternal = allowInternal,
                ResolverName = resolverName,
                NamespaceRoot = namespaceRoot
            };
        }

        internal static string InternalGenerate(CommandlineArguments arguments)
        {
            var collector = new TypeCollector(arguments.InputFiles, arguments.InputDirectories, arguments.ConditionalSymbols, !arguments.AllowInternal);

            (ObjectSerializationInfo[] objectInfo, GenericSerializationInfo[] genericInfo) = collector.Collect();

            FormatterTemplate[] objectFormatterTemplates = objectInfo
                .GroupBy(x => x.Namespace)
                .Select(x => new FormatterTemplate
                {
                    Namespace = arguments.GetNamespaceDot() + "Formatters" + ((x.Key == null) ? "" : "." + x.Key),
                    objectSerializationInfos = x.ToArray(),
                })
                .ToArray();

            var resolverTemplate = new ResolverTemplate()
            {
                Namespace = arguments.GetNamespaceDot() + "Resolvers",
                FormatterNamespace = arguments.GetNamespaceDot() + "Formatters",
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

        internal static string InternalGenerateFormatters(CommandlineArguments arguments)
        {
            var collector = new TypeCollector(arguments.InputFiles, arguments.InputDirectories, arguments.ConditionalSymbols, !arguments.AllowInternal);

            (ObjectSerializationInfo[] objectInfo, GenericSerializationInfo[] _) = collector.Collect();

            FormatterTemplate[] objectFormatterTemplates = objectInfo
                .GroupBy(x => x.Namespace)
                .Select(x => new FormatterTemplate
                {
                    Namespace = arguments.GetNamespaceDot() + "Formatters" + ((x.Key == null) ? "" : "." + x.Key),
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