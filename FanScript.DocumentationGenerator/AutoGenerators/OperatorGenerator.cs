using FanScript.Compiler.Binding;

namespace FanScript.DocumentationGenerator.AutoGenerators
{
    public static class OperatorGenerator
    {
        public static void Generate(string docSrcPath, bool showSkipped)
        {
            docSrcPath = Path.Combine(docSrcPath, "Operators");
            Directory.CreateDirectory(docSrcPath);

            string binaryPath = Path.Combine(docSrcPath, "Binary");
            Directory.CreateDirectory(binaryPath);

            foreach (var item in BoundBinaryOperator.Operators
                .GroupBy(op => op.Kind))
            {
                BoundBinaryOperatorKind kind = item.Key;

                BoundBinaryOperator[] ops = item.ToArray();

                string path = Path.Combine(binaryPath, kind + ".docsrc");

                if (File.Exists(path))
                {
                    if (showSkipped)
                        Console.WriteLine($"Skipped '{path}', because it already exists.");
                    continue;
                }

                using (FileStream stream = new FileStream(path, FileMode.Create, FileAccess.Write, FileShare.Read))
                using (StreamWriter writer = new StreamWriter(stream))
                {
                    writer.WriteLine($"<arg name=\"name\">{Enum.GetName(kind)}</>");
                    writer.WriteLine("<template>operator_binary</>");
                }

                Console.WriteLine($"Generated '{path}'.");
            }

            string unaryPath = Path.Combine(docSrcPath, "Unary");
            Directory.CreateDirectory(unaryPath);

            foreach (var item in BoundUnaryOperator.Operators
                .GroupBy(op => op.Kind))
            {
                BoundUnaryOperatorKind kind = item.Key;

                BoundUnaryOperator[] ops = item.ToArray();

                string path = Path.Combine(unaryPath, kind + ".docsrc");

                if (File.Exists(path))
                {
                    if (showSkipped)
                        Console.WriteLine($"Skipped '{path}', because it already exists.");
                    continue;
                }

                using (FileStream stream = new FileStream(path, FileMode.Create, FileAccess.Write, FileShare.Read))
                using (StreamWriter writer = new StreamWriter(stream))
                {
                    writer.WriteLine($"<arg name=\"name\">{Enum.GetName(kind)}</>");
                    writer.WriteLine("<template>operator_unary</>");
                }

                Console.WriteLine($"Generated '{path}'.");
            }
        }
    }
}
