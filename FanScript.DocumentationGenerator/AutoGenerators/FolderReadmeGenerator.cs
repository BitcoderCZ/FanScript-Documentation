namespace FanScript.DocumentationGenerator.AutoGenerators
{
    public static class FolderReadmeGenerator
    {
        public static void Generate(string docSrcPath, bool onlyDisplayErrors)
        {
            foreach (string dir in Directory.EnumerateDirectories(docSrcPath))
                generate(dir, docSrcPath, onlyDisplayErrors);
        }

        private static void generate(string dir, string startDir, bool onlyDisplayErrors)
        {
            foreach (string subDir in Directory.EnumerateDirectories(dir))
                generate(subDir, startDir, onlyDisplayErrors);

            string path = Path.Combine(dir, "README.docsrc");

            if (File.Exists(path))
            {
                if (!onlyDisplayErrors)
                    Console.WriteLine($"Skipped '{path}', because it already exists.");

                return;
            }

            using (FileStream stream = new FileStream(path, FileMode.Create, FileAccess.Write, FileShare.Read))
            using (StreamWriter writer = new StreamWriter(stream))
            {
                writer.WriteLine("# " + Path.GetRelativePath(startDir, dir).Replace('\\', '/'));
                writer.WriteLine();
                writer.WriteLine("## Contents");
                writer.WriteLine();
                writer.WriteLine("$template contents");
                writer.Flush();

                if (!onlyDisplayErrors)
                    Console.WriteLine($"Generated '{path}'");
            }
        }
    }
}
