using FanScript.Compiler;
using FanScript.DocumentationGenerator.Utils;
using System.Diagnostics;
using System.Globalization;

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
            writer.WriteLine("@type:" + group.Type);
            writer.WriteLine("@name:" + group.Name);
            writer.WriteLine("@info:");
            writer.WriteLine("@names:" + string.Join(";;", group.Values.Select(con => group.Name + "_" + con.Name)));
            writer.WriteLine("@values:" + string.Join(";;", group.Values.Select(con => toString(con.Value))));
            writer.WriteLine("@infos:" + ";;".Repeat(Math.Max(0, group.Values.Length - 1)));
            writer.WriteLine("$template constant");

            string toString(object o)
            {
                Debug.Assert(o is not null);

                switch (o)
                {
                    case float f:
                        return f.ToString(CultureInfo.InvariantCulture);
                    default:
                        return o.ToString()!;
                }
            }
        }
    }
}
