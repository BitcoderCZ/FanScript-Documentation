using FanScript.Compiler.Symbols;
using FanScript.Documentation.DocElements;
using FanScript.Documentation.Exceptions;
using FanScript.DocumentationGenerator.Elements;
using FanScript.Utils;
using System.Text;

namespace FanScript.DocumentationGenerator.Utils
{
    internal static class U
    {
        public static string FuncToFile(FunctionSymbol func)
        {
            StringBuilder builder = new StringBuilder();

            builder.Append(func.Name);
            foreach (var param in func.Parameters)
            {
                builder.Append('.');
                builder.Append(param.Type.Name);
            }

            return builder
                .ToString()
                .ToUpperFirst();
        }

        public static DocElementParser CreateParser(FunctionSymbol? currentFunction)
        {
            DocElementParser parser = new DocElementParser(currentFunction);
            parser.ElementTypes.Add("arg", (args, value) =>
            {
                DocArg? _name = args.FirstOrDefault(arg => arg.Name == "name");

                if (_name is not DocArg name)
                    throw new ElementArgMissingException("arg", "name");
                else if (string.IsNullOrEmpty(name.Value))
                    throw new ElementArgValueMissingException("arg", "name");

                return new DocFileArg(args, value, name.Value);
            });
            parser.ElementTypes.Add("template", (args, value) =>
            {
                if (value is null)
                    throw new ElementParseException("template", "Value cannot be null.");
                if (value is not DocString valueStr)
                    throw new ElementParseException("template", "Value must be a string.");

                return new DocFileTemplate(args, valueStr);
            });

            return parser;
        }

        public static TAttrib GetAttribute<TEnum, TAttrib>(TEnum value)
            where TEnum : Enum
            where TAttrib : Attribute
        {
            Type enumType = typeof(TEnum);
            string name = Enum.GetName(enumType, value)!;
            return enumType
                .GetField(name)!
                .GetCustomAttributes(false)
                .OfType<TAttrib>()
                .Single();
        }
    }
}
