using FanScript.Documentation.DocElements;
using FanScript.Documentation.DocElements.Builders;
using FanScript.DocumentationGenerator.AutoGenerators;
using FanScript.DocumentationGenerator.Builders;
using FanScript.DocumentationGenerator.Utils;
using System.Diagnostics;

namespace FanScript.DocumentationGenerator
{
    internal class Program
    {
        static void Main(string[] args)
        {
#if DEBUG
            Debugger.Launch();
#endif

            string srcDir = Path.GetFullPath("DocSrc");
            string outDir = Path.GetFullPath("MdDocs");

            if (!Directory.Exists(srcDir))
            {
                Console.WriteLine($"SrcDir '{srcDir}' doesn't exist");
                Console.ReadKey(true);
                return;
            }

            FunctionGenerator.Generate(srcDir, false);
            ConstantGenerator.Generate(srcDir, false);
            EventGenerator.Generate(srcDir, false);
            TypeGenerator.Generate(srcDir, false);
            ModifierGenerator.Generate(srcDir, false);
            OperatorGenerator.Generate(srcDir, false);
            BuildCommandGenerator.Generate(srcDir, false);
            FolderReadmeGenerator.Generate(srcDir, false);

            Console.WriteLine("Generated all.");

            foreach (string dir in Directory.EnumerateDirectories(srcDir, "*", SearchOption.AllDirectories))
            {
                string newDir = Path.Combine(outDir, Path.GetRelativePath(srcDir, dir));

                Directory.CreateDirectory(newDir);
            }

            Console.WriteLine("Created dirs.");

            DocElementParser parser = U.CreateParser(null);

            foreach (string path in Directory.EnumerateFiles(srcDir, "*.docsrc", SearchOption.AllDirectories))
            {
                string text = File.ReadAllText(path);

                DocElement? element;

#if !DEBUG
                try
                {
#endif
                element = parser.Parse(text);
#if !DEBUG
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Failed to parse file '{path}': {ex}");
                    continue;
                }
#endif

#if !DEBUG
                try
                {
#endif
                DocElementBuilder builder = new MdBuilder(path);

                string outPath = Path.Combine(outDir, Path.GetRelativePath(srcDir, path));

                outPath = Path.ChangeExtension(outPath, ".md");

                File.WriteAllText(outPath, builder.Build(element));
#if !DEBUG
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Failed to build file '{path}': {ex}");
                    continue;
                }
#endif
            }

            Console.WriteLine("Built.");

            Console.ReadKey(true);
        }
    }
}
