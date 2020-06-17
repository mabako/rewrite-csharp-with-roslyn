using System;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.CSharp.Syntax;

namespace RewritingWithRoslyn
{
    public class SemanticVisitor : CSharpSyntaxWalker
    {
        private readonly SemanticModel _model;

        public SemanticVisitor(SemanticModel model)
        {
            _model = model;
        }

        public override void VisitInvocationExpression(InvocationExpressionSyntax node)
        {
            if (node.ToString().StartsWith("obj.SayHello("))
            {
                Console.WriteLine(node);
                Console.WriteLine(_model.GetSymbolInfo(node).Symbol);

                Console.WriteLine(node.ArgumentList.Arguments[0]);
                Console.WriteLine(_model.GetTypeInfo(node.ArgumentList.Arguments[0].Expression).Type);
            }

            base.VisitInvocationExpression(node);
        }
    }
}
