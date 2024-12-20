﻿using FanScript.Compiler.Symbols;
using FanScript.Utils;

namespace FanScript.DocumentationGenerator.AutoGenerators
{
    public static class TypeGenerator
    {
        public static void Generate(string docSrcPath, bool showSkipped)
        {
            docSrcPath = Path.Combine(docSrcPath, "Types");
            Directory.CreateDirectory(docSrcPath);

            foreach (TypeSymbol type in TypeSymbol.BuiltInTypes.Concat([TypeSymbol.String, TypeSymbol.ArraySegment, TypeSymbol.Null]))
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
            writer.WriteLine($"<arg name=\"name\">{type.Name}</>");
            writer.WriteLine("<template>type</>");
        }
    }
}
