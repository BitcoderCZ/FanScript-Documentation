using FanScript.Compiler;
using FanScript.Compiler.Symbols;
using FanScript.DocumentationGenerator.Utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FanScript.DocumentationGenerator.AutoGenerators
{
    public static class TypeGenerator
    {
        public static void Generate(string docSrcPath, bool showSkipped)
        {
            docSrcPath = Path.Combine(docSrcPath, "Types");
            Directory.CreateDirectory(docSrcPath);

            foreach (TypeSymbol type in TypeSymbol.BuiltInTypes.Concat([TypeSymbol.String, TypeSymbol.ArraySegment]))
            {
                string path = Path.Combine(docSrcPath, type.Name.ToUpperFirst() + ".docsrc");

                if (File.Exists(path))
                {
                    if (showSkipped)
                        Console.WriteLine($"Skipped '{path}', because it already exists.");
                    continue;
                }


                using (FileStream stream = new FileStream(path, FileMode.Create, FileAccess.Write, FileShare.Read))
                using (StreamWriter writer = new StreamWriter(stream))
                    generateType(type, writer);

                Console.WriteLine($"Generated '{path}'.");
            }
        }

        private static void generateType(TypeSymbol type, TextWriter writer)
        {
            writer.WriteLine("@is_generic:" + type.IsGenericDefinition);
            writer.WriteLine("@name:" + type.Name);
            writer.WriteLine("@info:");
            writer.WriteLine("@how_to_create:");
            writer.WriteLine("@remarks:");
            writer.WriteLine("$template type");
        }
    }
}
