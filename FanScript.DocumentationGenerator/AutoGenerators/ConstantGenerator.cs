using FanScript.Compiler;

namespace FanScript.DocumentationGenerator.AutoGenerators
{
    public static class ConstantGenerator
    {
        public static void Generate(string docSrcPath, bool showSkipped)
        {
            docSrcPath = Path.Combine(docSrcPath, "Constants");
            Directory.CreateDirectory(docSrcPath);

            foreach (var group in Constants.Groups)
            {
                string path = Path.Combine(docSrcPath, group.Name + ".docsrc");

                if (File.Exists(path))
                {
                    if (showSkipped)
                        Console.WriteLine($"Skipped '{path}', because it already exists.");
                    continue;
                }

                using (FileStream stream = new FileStream(path, FileMode.Create, FileAccess.Write, FileShare.Read))
                using (StreamWriter writer = new StreamWriter(stream))
                    generateConstant(group, writer);

                Console.WriteLine($"Generated '{path}'.");
            }
        }

        private static void generateConstant(ConstantGroup group, TextWriter writer)
        {
            writer.WriteLine($"<arg name=\"name\">{group.Name}</>");
            writer.WriteLine("<template>constant</>");
        }
    }
}
