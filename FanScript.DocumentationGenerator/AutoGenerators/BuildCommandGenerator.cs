using FanScript.Compiler;

namespace FanScript.DocumentationGenerator.AutoGenerators
{
    public static class BuildCommandGenerator
    {
        public static void Generate(string docSrcPath, bool showSkipped)
        {
            docSrcPath = Path.Combine(docSrcPath, "BuildCommands");
            Directory.CreateDirectory(docSrcPath);

            foreach (BuildCommand command in Enum.GetValues<BuildCommand>())
            {
                string path = Path.Combine(docSrcPath, command + ".docsrc");

                if (File.Exists(path))
                {
                    if (showSkipped)
                        Console.WriteLine($"Skipped '{path}', because it already exists.");
                    continue;
                }

                using (FileStream stream = new FileStream(path, FileMode.Create, FileAccess.Write, FileShare.Read))
                using (StreamWriter writer = new StreamWriter(stream))
                    generateCommand(command, writer);

                Console.WriteLine($"Generated '{path}'.");
            }
        }

        private static void generateCommand(BuildCommand command, TextWriter writer)
        {
            writer.WriteLine($"<arg name=\"name\">{Enum.GetName(command)}</>");
            writer.WriteLine("<template>build_command</>");
        }
    }
}
