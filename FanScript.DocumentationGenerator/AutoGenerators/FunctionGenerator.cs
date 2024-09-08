using FanScript.Compiler;
using FanScript.Compiler.Symbols;
using FanScript.DocumentationGenerator.Utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace FanScript.DocumentationGenerator.AutoGenerators
{
    public static class FunctionGenerator
    {
        public static void Generate(string docSrcPath, bool onlyDisplayErrors)
        {
            docSrcPath = Path.Combine(docSrcPath, "Functions");
            Directory.CreateDirectory(docSrcPath);

            Dictionary<string, int> funcDuplicates = new();

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

                string name = func.Name;

                if (funcDuplicates.TryGetValue(name, out int numb))
                {
                    name += numb;
                    funcDuplicates[func.Name] = numb + 1;
                }
                else
                    funcDuplicates.Add(name, 2);

                name = name.ToUpperFirst();

                string path = Path.Combine(docSrcPath, name + ".docsrc");

                if (File.Exists(path))
                {
                    if (!onlyDisplayErrors)
                        Console.WriteLine($"Skipped '{path}', because it already exists.");
                    continue;
                }

                using (FileStream stream = new FileStream(path, FileMode.Create, FileAccess.Write, FileShare.Read))
                using (StreamWriter writer = new StreamWriter(stream))
                    processFunctions(func, writer);

                if (!onlyDisplayErrors)
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

                    string name = func.Name;

                    if (funcDuplicates.TryGetValue(name, out int numb))
                    {
                        name += numb;
                        funcDuplicates[func.Name] = numb + 1;
                    }
                    else
                        funcDuplicates.Add(name, 2);

                    name = name.ToUpperFirst();

                    string path = Path.Combine(basePath, name + ".docsrc");

                    if (File.Exists(path))
                    {
                        if (!onlyDisplayErrors)
                            Console.WriteLine($"Skipped '{path}', because it already exists.");
                        continue;
                    }

                    using (FileStream stream = new FileStream(path, FileMode.Create, FileAccess.Write, FileShare.Read))
                    using (StreamWriter writer = new StreamWriter(stream))
                        processFunctions(func, writer);

                    if (!onlyDisplayErrors)
                        Console.WriteLine($"Generated '{path}'.");
                }
            }
        }

        private static void processFunctions(BuiltinFunctionSymbol func, StreamWriter writer)
        {
            writer.WriteLine("@type:" + func.Type.Name);
            writer.WriteLine("@return_info:");
            writer.WriteLine("@is_generic:" + func.IsGeneric.ToString().ToLowerInvariant());
            writer.WriteLine("@is_method:" + func.IsMethod.ToString().ToLowerInvariant());
            writer.WriteLine("@name:" + func.Name);
            writer.WriteLine("@info:");
            writer.WriteLine("@param_mods:" + string.Join(";;", func.Parameters.Select(param => param.Modifiers == 0 ? string.Empty : param.Modifiers.ToSyntaxString())));
            writer.WriteLine("@param_types:" + string.Join(";;", func.Parameters.Select(param => param.Type.Name)));
            writer.WriteLine("@param_names:" + string.Join(";;", func.Parameters.Select(param => param.Name)));
            writer.WriteLine("@param_infos:" + ";;".Repeat(Math.Max(0, func.Parameters.Length - 1)));
            writer.WriteLine("$template function");
        }
    }
}
