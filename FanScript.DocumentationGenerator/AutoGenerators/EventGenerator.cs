using FanScript.Compiler;
using FanScript.DocumentationGenerator.Utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FanScript.DocumentationGenerator.AutoGenerators
{
    public static class EventGenerator
    {
        public static void Generate(string docSrcPath, bool showSkipped)
        {
            docSrcPath = Path.Combine(docSrcPath, "Events");
            Directory.CreateDirectory(docSrcPath);

            foreach (SpecialBlockType eventType in Enum.GetValues<SpecialBlockType>())
            {
                string path = Path.Combine(docSrcPath, eventType.ToString() + ".docsrc");

                if (File.Exists(path))
                {
                    if (showSkipped)
                        Console.WriteLine($"Skipped '{path}', because it already exists.");
                    continue;
                }

                SpecialBlockTypeInfo info = eventType.GetInfo();


                using (FileStream stream = new FileStream(path, FileMode.Create, FileAccess.Write, FileShare.Read))
                using (StreamWriter writer = new StreamWriter(stream))
                    generateEvent(info, writer);
            }
        }

        private static void generateEvent(SpecialBlockTypeInfo info, TextWriter writer)
        {
            writer.Write("@name:");
            writer.WriteLine(info.Type.ToString());
            writer.Write("@info:");
            writer.WriteLine(addDotAtEnd(info.Description?.Replace("(", "\\(")?.Replace(")", "\\)")) ?? string.Empty);
            writer.WriteLine("@param_mods:" + string.Join(";;", info.Parameters.Select(param => param.Modifiers == 0 ? string.Empty : param.Modifiers.ToSyntaxString())));
            writer.WriteLine("@param_types:" + string.Join(";;", info.Parameters.Select(param => param.Type.Name)));
            writer.WriteLine("@param_names:" + string.Join(";;", info.Parameters.Select(param => param.Name)));
            writer.WriteLine("@param_is_constant:" + string.Join(";;", info.Parameters.Select(param => param.IsConstant)));
            writer.WriteLine("@param_infos:" + ";;".Repeat(Math.Max(0, info.Parameters.Length - 1)));
            writer.WriteLine("$template event");

            string? addDotAtEnd(string? str)
            {
                if (str is null || str.EndsWith('.'))
                    return str;
                else
                    return str + ".";
            }
        }
    }
}
