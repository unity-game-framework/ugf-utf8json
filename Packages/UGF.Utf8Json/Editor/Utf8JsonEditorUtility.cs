using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using UGF.Code.Generate.Editor;
using Utf8Json.UniversalCodeGenerator;

namespace UGF.Utf8Json.Editor
{
    /// <summary>
    /// Provides utilities to work with Utf8 Json serialization in editor.
    /// </summary>
    public static class Utf8JsonEditorUtility
    {
        public static string GenerateResolver(IReadOnlyList<string> sourcePaths, string resolverName, string namespaceRoot, Utf8JsonGenerateArguments generateArguments)
        {
            if (sourcePaths == null) throw new ArgumentNullException(nameof(sourcePaths));
            if (string.IsNullOrEmpty(resolverName)) throw new ArgumentException("Value cannot be null or empty.", nameof(resolverName));
            if (string.IsNullOrEmpty(namespaceRoot)) throw new ArgumentException("Value cannot be null or empty.", nameof(namespaceRoot));

            string resolver = Utf8JsonUniversalCodeGeneratorUtility.Generate(sourcePaths, resolverName, namespaceRoot, generateArguments);
            CompilationUnitSyntax unit = SyntaxFactory.ParseCompilationUnit(resolver);

            unit = CodeGenerateEditorUtility.AddGeneratedCodeLeadingTrivia(unit);

            return unit.ToFullString();
        }

        public static string FormatResolverName(string resolverName)
        {
            if (string.IsNullOrEmpty(resolverName)) throw new ArgumentException("Value cannot be null or empty.", nameof(resolverName));

            var builder = new StringBuilder();
            int start = 0;

            while (!char.IsLetter(resolverName[start]) && start < resolverName.Length)
            {
                start++;
            }

            for (int i = start; i < resolverName.Length; i++)
            {
                char ch = resolverName[i];

                if (char.IsLetterOrDigit(ch))
                {
                    builder.Append(ch);
                }
            }

            if (builder.Length == 0)
            {
                throw new ArgumentException($"The specified resolver name is invalid: '{resolverName}'.", nameof(resolverName));
            }

            return builder.ToString();
        }
    }
}
