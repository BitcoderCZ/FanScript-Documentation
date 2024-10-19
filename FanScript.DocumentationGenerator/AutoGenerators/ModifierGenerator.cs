using FanScript.Compiler;

namespace FanScript.DocumentationGenerator.AutoGenerators
{
    public static class ModifierGenerator
    {
        public static void Generate(string docSrcPath, bool showSkipped)
        {
            docSrcPath = Path.Combine(docSrcPath, "Modifiers");
            Directory.CreateDirectory(docSrcPath);

            foreach (Modifiers mod in Enum.GetValues<Modifiers>())
            {
                string path = Path.Combine(docSrcPath, mod.ToString() + ".docsrc");

                if (File.Exists(path))
                {
                    if (showSkipped)
                        Console.WriteLine($"Skipped '{path}', because it already exists.");
                    continue;
                }

                using (FileStream stream = new FileStream(path, FileMode.Create, FileAccess.Write, FileShare.Read))
                using (StreamWriter writer = new StreamWriter(stream))
                    generateType(mod, writer);

                Console.WriteLine($"Generated '{path}'.");
            }
        }

        private static void generateType(Modifiers mod, TextWriter writer)
        {
            writer.WriteLine($"<arg name=\"name\">{Enum.GetName(mod)}</>");
            writer.WriteLine("<template>modifier</>");
        }
    }
}
