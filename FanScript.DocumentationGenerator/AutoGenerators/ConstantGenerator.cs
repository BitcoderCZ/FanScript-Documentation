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

            foreach (var (name, cons) in Constants.GetGroups())
            {
                string path = Path.Combine(docSrcPath, name + ".docsrc");

                if (File.Exists(path))
                {
                    if (showSkipped)
                        Console.WriteLine($"Skipped '{path}', because it already exists.");
                    continue;
                }

                using (FileStream stream = new FileStream(path, FileMode.Create, FileAccess.Write, FileShare.Read))
                using (StreamWriter writer = new StreamWriter(stream))
                    generateConstant(name, cons.ToArray(), writer);

                Console.WriteLine($"Generated '{path}'.");
            }
        }

        private static void generateConstant(string name, Constant[] cons, TextWriter writer)
        {
            writer.WriteLine("@type:" + cons[0].Type);
            writer.WriteLine("@name:" + name);
            writer.WriteLine("@info:");
            writer.WriteLine("@names:" + string.Join(";;", cons.Select(con => con.Name)));
            writer.WriteLine("@values:" + string.Join(";;", cons.Select(con => toString(con.Value))));
            writer.WriteLine("@infos:" + ";;".Repeat(Math.Max(0, cons.Length - 1)));
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
