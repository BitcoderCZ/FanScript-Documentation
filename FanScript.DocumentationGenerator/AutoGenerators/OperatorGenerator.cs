using FanScript.Compiler.Binding;
using FanScript.Compiler.Syntax;
using FanScript.DocumentationGenerator.Utils;
using Microsoft.VisualBasic;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Globalization;
using System.Linq;
using System.Reflection;
using System.Reflection.Metadata;
using System.Text;
using System.Threading.Tasks;

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
                    writer.WriteLine("@name:" + kind.ToString());
                    writer.WriteLine("@symbol:" + SyntaxFacts.GetText(ops[0].SyntaxKind));
                    writer.WriteLine("@info:");
                    writer.WriteLine("@type_combs:" + string.Join(';', ops.Select(op => $"{op.LeftType},{op.RightType},{op.Type}")));
                    writer.WriteLine("@comb_infos:" + ";;".Repeat(Math.Max(0, ops.Length - 1)));
                    writer.WriteLine("$template operator_binary");
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
                    writer.WriteLine("@name:" + kind.ToString());
                    writer.WriteLine("@symbol:" + SyntaxFacts.GetText(ops[0].SyntaxKind));
                    writer.WriteLine("@info:");
                    writer.WriteLine("@type_combs:" + string.Join(';', ops.Select(op => $"{op.OperandType},{op.Type}")));
                    writer.WriteLine("@comb_infos:" + ";;".Repeat(Math.Max(0, ops.Length - 1)));
                    writer.WriteLine("$template operator_unary");
                }

                Console.WriteLine($"Generated '{path}'.");
            }
        }
    }
}
