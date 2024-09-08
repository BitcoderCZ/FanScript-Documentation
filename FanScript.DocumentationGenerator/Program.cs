using FanScript.DocumentationGenerator.AutoGenerators;
using FanScript.DocumentationGenerator.Builders;
using FanScript.DocumentationGenerator.Parsing;
using System.Collections.Immutable;

namespace FanScript.DocumentationGenerator
{
    internal class Program
    {
        static void Main(string[] args)
        {
            const bool onlyDisplayErrors = true;

            string srcDir = Path.GetFullPath("DocSrc");
            string outDir = Path.GetFullPath("MdDocs");

            if (!Directory.Exists(srcDir))
            {
                Console.WriteLine($"SrcDir '{srcDir}' doesn't exist");
                Console.ReadKey(true);
                return;
            }

            FunctionGenerator.Generate(srcDir, onlyDisplayErrors);
            ConstantGenerator.Generate(srcDir, onlyDisplayErrors);

            Console.WriteLine("Generated");

            foreach (string dir in Directory.EnumerateDirectories(srcDir, "*", SearchOption.AllDirectories))
            {
                string newDir = Path.Combine(outDir, Path.GetRelativePath(srcDir, dir));

                Directory.CreateDirectory(newDir);
            }

            Console.WriteLine("Created dirs");

            foreach (string path in Directory.EnumerateFiles(srcDir, "*.docsrc", SearchOption.AllDirectories))
            {
                string text = File.ReadAllText(path);

                Parser parser = new Parser(text);

                ImmutableArray<Token> tokens;
                try
                {
                    tokens = parser.Parse();
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Failed to parse file '{path}': {ex}");
                    continue;
                }

                try
                {
                    Builder builder = new MdBuilder(tokens);

                    string outPath = Path.Combine(outDir, Path.GetRelativePath(srcDir, path));

                    outPath = Path.ChangeExtension(outPath, ".md");

                    File.WriteAllText(outPath, builder.Build());
                    if (!onlyDisplayErrors)
                        Console.WriteLine($"Generated file '{outPath}'");
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Failed to build file '{path}': {ex}");
                    continue;
                }
            }

            Console.WriteLine("Built");

            Console.ReadKey(true);
        }
    }
}
