using FanScript.Compiler.Symbols;
using FanScript.DocumentationGenerator.Utils;
using System.Reflection;

namespace FanScript.DocumentationGenerator.AutoGenerators
{
    public static class FunctionGenerator
    {
        public static void Generate(string docSrcPath, bool showSkipped)
        {
            docSrcPath = Path.Combine(docSrcPath, "Functions");
            Directory.CreateDirectory(docSrcPath);

            foreach (FunctionSymbol? _func in typeof(BuiltinFunctions)
                .GetFields(BindingFlags.Public | BindingFlags.Static)
                .Where(f => f.FieldType == typeof(FunctionSymbol))
                .Select(f => (FunctionSymbol)f.GetValue(null)!))
            {
                if (_func is not BuiltinFunctionSymbol func)
                {
                    Console.WriteLine($"Function '{_func.Name}' skipped, because it isn't {nameof(BuiltinFunctionSymbol)}");
                    continue;
                }

                string name = U.FuncToFile(func);

                string path = Path.Combine(docSrcPath, name + ".docsrc");

                if (File.Exists(path))
                {
                    if (showSkipped)
                        Console.WriteLine($"Skipped '{path}', because it already exists.");

                    continue;
                }

                using (FileStream stream = new FileStream(path, FileMode.Create, FileAccess.Write, FileShare.Read))
                using (StreamWriter writer = new StreamWriter(stream))
                    generateFunction(func, writer);

                Console.WriteLine($"Generated '{path}'.");
            }

            foreach (Type subType in typeof(BuiltinFunctions)
                .GetNestedTypes(BindingFlags.NonPublic | BindingFlags.Static))
            {
                FunctionSymbol[] funcs = subType
                    .GetFields(BindingFlags.Public | BindingFlags.Static)
                    .Where(f => f.FieldType == typeof(FunctionSymbol))
                    .Select(f => (FunctionSymbol)f.GetValue(null)!).ToArray();

                if (funcs.Length == 0)
                    continue;

                string basePath = Path.Combine(docSrcPath, subType.Name);

                Directory.CreateDirectory(basePath);

                foreach (var _func in funcs)
                {
                    if (_func is not BuiltinFunctionSymbol func)
                    {
                        Console.WriteLine($"Function '{_func.Name}' skipped, because it isn't {nameof(BuiltinFunctionSymbol)}.");
                        continue;
                    }

                    string name = U.FuncToFile(func);

                    string path = Path.Combine(basePath, name + ".docsrc");

                    if (File.Exists(path))
                    {
                        if (showSkipped)
                            Console.WriteLine($"Skipped '{path}', because it already exists.");

                        continue;
                    }

                    using (FileStream stream = new FileStream(path, FileMode.Create, FileAccess.Write, FileShare.Read))
                    using (StreamWriter writer = new StreamWriter(stream))
                        generateFunction(func, writer);

                    Console.WriteLine($"Generated '{path}'.");
                }
            }
        }

        private static void generateFunction(BuiltinFunctionSymbol func, TextWriter writer)
        {
            writer.WriteLine($"<arg name=\"name\">{func.Name}</>");
            writer.WriteLine($"<arg name=\"param_types\">{string.Join(";", func.Parameters.Select(param => param.Type.Name))}</>");
            writer.WriteLine("<template>builtin_function</>");
        }
    }
}
