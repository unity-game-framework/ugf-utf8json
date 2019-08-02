using System.IO;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using Microsoft.CodeAnalysis.Editing;
using NUnit.Framework;
using UGF.Code.Analysis.Editor;
using UGF.Utf8Json.Editor.Analysis;
using UGF.Utf8Json.Runtime;

namespace UGF.Utf8Json.Editor.Tests.Analysis
{
    public class TestUtf8JsonRewriterAddFormatterAttribute
    {
        private readonly string m_withoutAttributes = "Assets/UGF.Utf8Json.Editor.Tests/Analysis/TestFormatterWithoutAttributes.txt";
        private readonly string m_withAttributes = "Assets/UGF.Utf8Json.Editor.Tests/Analysis/TestFormatterWithAttributes.txt";

        [Test]
        public void Visit()
        {
            string sourceWithout = File.ReadAllText(m_withoutAttributes);
            string sourceWith = File.ReadAllText(m_withAttributes);

            CSharpCompilation compilation = CodeAnalysisEditorUtility.ProjectCompilation;
            SyntaxGenerator generator = CodeAnalysisEditorUtility.Generator;

            INamedTypeSymbol attributeTypeSymbol = compilation.GetTypeByMetadataName(typeof(Utf8JsonFormatterAttribute).FullName);
            var attributeType = (TypeSyntax)generator.TypeExpression(attributeTypeSymbol);

            var rewriter = new Utf8JsonRewriterAddFormatterAttribute(generator, attributeType);
            SyntaxTree tree = SyntaxFactory.ParseSyntaxTree(sourceWithout);

            string result = rewriter.Visit(tree.GetRoot()).ToFullString();

            Assert.AreEqual(sourceWith, result);
        }
    }
}
