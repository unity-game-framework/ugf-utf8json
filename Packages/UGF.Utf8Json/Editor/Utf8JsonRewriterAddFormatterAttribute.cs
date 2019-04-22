using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using Microsoft.CodeAnalysis.Editing;

namespace UGF.Utf8Json.Editor
{
    internal sealed class Utf8JsonRewriterAddFormatterAttribute : CSharpSyntaxRewriter
    {
        public SyntaxGenerator Generator { get; }
        public TypeSyntax AttributeType { get; }

        public Utf8JsonRewriterAddFormatterAttribute(SyntaxGenerator generator, TypeSyntax attributeType, bool visitIntoStructuredTrivia = false) : base(visitIntoStructuredTrivia)
        {
            Generator = generator;
            AttributeType = attributeType;
        }

        public override SyntaxNode VisitClassDeclaration(ClassDeclarationSyntax node)
        {
            TypeSyntax targetType = null;

            if (node.BaseList != null)
            {
                for (int i = 0; i < node.BaseList.Types.Count; i++)
                {
                    TypeSyntax type = node.BaseList.Types[i].Type;

                    if (type is QualifiedNameSyntax qualifiedNameSyntax)
                    {
                        SimpleNameSyntax right = qualifiedNameSyntax.Right;

                        if (right.Identifier.Text == "IJsonFormatter" && right is GenericNameSyntax genericNameSyntax)
                        {
                            if (genericNameSyntax.TypeArgumentList.Arguments.Count == 1)
                            {
                                targetType = genericNameSyntax.TypeArgumentList.Arguments[0];
                            }
                        }
                    }
                }
            }

            if (targetType != null)
            {
                SyntaxNode attribute = Generator.Attribute(AttributeType, new[]
                {
                    Generator.TypeOfExpression(targetType)
                });

                return Generator.AddAttributes(node, attribute);
            }

            return base.VisitClassDeclaration(node);
        }
    }
}
