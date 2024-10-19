namespace FanScript.DocumentationGenerator.AutoGenerators
{
    public static class FolderReadmeGenerator
    {
        public static void Generate(string docSrcPath, bool showSkipped)
        {
            foreach (string dir in Directory.EnumerateDirectories(docSrcPath))
                generate(dir, docSrcPath, showSkipped);
        }

        private static void generate(string dir, string startDir, bool showSkipped)
        {
            foreach (string subDir in Directory.EnumerateDirectories(dir))
                generate(subDir, startDir, showSkipped);

            string path = Path.Combine(dir, "README.docsrc");

            if (File.Exists(path))
            {
                if (showSkipped)
                    Console.WriteLine($"Skipped '{path}', because it already exists.");

                return;
            }

            using (FileStream stream = new FileStream(path, FileMode.Create, FileAccess.Write, FileShare.Read))
            using (StreamWriter writer = new StreamWriter(stream))
            {
                writer.WriteLine("<header level=\"1\">" + Path.GetRelativePath(startDir, dir).Replace('\\', '/') + "</>");
                writer.WriteLine();
                writer.WriteLine("<header level=\"2\">Contents</>");
                writer.WriteLine();
                writer.WriteLine("<template>contents</>");
                writer.Flush();

                Console.WriteLine($"Generated '{path}'");
            }
        }
    }
}
