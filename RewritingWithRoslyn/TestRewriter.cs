using System.Text.RegularExpressions;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;

namespace RewritingWithRoslyn
{
    // TODO(?): Give this a better name. It matches the blog post though!
    public class TestRewriter : CSharpSyntaxRewriter
    {
        private readonly string _newAuthor;
        private readonly Regex _originalAuthor;

        public TestRewriter(string originalAuthor, string newAuthor)
        {
            _originalAuthor = new Regex($@"(TODO|FIXME)\({Regex.Escape(originalAuthor)}\)", RegexOptions.IgnoreCase);
            _newAuthor = newAuthor;
        }

        public override SyntaxTrivia VisitTrivia(SyntaxTrivia trivia)
        {
            if (trivia.IsKind(SyntaxKind.SingleLineCommentTrivia) || trivia.IsKind(SyntaxKind.MultiLineCommentTrivia))
            {
                string text = trivia.ToFullString();
                if (_originalAuthor.IsMatch(text))
                {
                    return SyntaxFactory.Comment(_originalAuthor.Replace(text, $"$1({_newAuthor})"));
                }
            }
            return base.VisitTrivia(trivia);
        }
    }
}
