using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.CSharp.Syntax;
// ReSharper disable SuggestVarOrType_SimpleTypes
// ReSharper disable SuggestVarOrType_BuiltInTypes

const string path = @"";
const string outPath = @"";
string[] files = Directory.GetFiles(path, "*.cs", SearchOption.AllDirectories);

foreach (string fileName in files)
{
    string input = File.ReadAllText(fileName);

    SyntaxTree tree = CSharpSyntaxTree.ParseText(input);
    SyntaxNode root = tree.GetRoot();
    SyntaxNode? newRoot = root.RemoveNodes(root.DescendantNodes().Where(c => c is not AttributeSyntax), SyntaxRemoveOptions.KeepNoTrivia);
    string modifiedScript = newRoot!.ToFullString();
    string outputPath = fileName.Replace(path, outPath);
    File.WriteAllText(outputPath, modifiedScript);
}