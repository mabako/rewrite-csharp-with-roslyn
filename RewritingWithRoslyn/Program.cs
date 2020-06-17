using System;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Build.Locator;
using Microsoft.CodeAnalysis.MSBuild;

namespace RewritingWithRoslyn
{
    class Program
    {
        static async Task Main(string[] args)
        {
            MSBuildLocator.RegisterDefaults();
            using var workspace = MSBuildWorkspace.Create();
            workspace.WorkspaceFailed += (sender, e) =>
                Console.WriteLine($"[failed] {e.Diagnostic}");

            var solution = await workspace.OpenSolutionAsync(@"..\..\..\..\RewritingWithRoslyn.sln");

            TestRewriter rewriter = new TestRewriter("first", "unassigned");
            foreach (var document in solution.Projects.SelectMany(x => x.Documents))
            {
                var root = await document.GetSyntaxRootAsync()
                           ?? throw new Exception($"Could not get syntax root for {document.FilePath}");
                var result = rewriter.Visit(root);

                var model = await document.GetSemanticModelAsync()
                            ?? throw new Exception($"Could not get semantic model for {document.FilePath}");
                new SemanticVisitor(model).Visit(root);

                if (!result.IsEquivalentTo(root)) {
                    await File.WriteAllTextAsync(document.FilePath, result.ToFullString());
                }
            }
        }
    }
}
