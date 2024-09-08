using FanScript.Compiler.Symbols;
using FanScript.DocumentationGenerator.Parsing;
using FanScript.DocumentationGenerator.Tokens;
using FanScript.DocumentationGenerator.Tokens.Links;
using FanScript.DocumentationGenerator.Utils;
using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FanScript.DocumentationGenerator.Builders
{
    public sealed class MdBuilder : Builder
    {
        private const string linkPrefix = "/MdDocs/";

        private string path;

        private StringBuilder builder = new();

        private readonly Dictionary<string, string> args = new();

        private Func<string, bool> plinkValidator = param => true;

        public MdBuilder(ImmutableArray<Token> tokens, string path) : base(tokens)
        {
            this.path = path;
        }

        public override string Build()
        {
            builder.Clear();
            args.Clear();

            build();

            return builder.ToString();
        }

        protected override void buildTab(TabToken token)
            => builder.Append("&nbsp;&nbsp;&nbsp;&nbsp;");

        protected override void buildNewLine(NewLineToken token)
            => builder.AppendLine();

        protected override void buildString(StringToken token)
            => builder.Append(token.Value);

        protected override void buildHeader(HeaderToken token)
            => builder.Append(new string('#', token.Level) + " " + token.Value);

        protected override void buildArg(ArgToken token)
            => args.Add(token.Name, token.Value);

        protected override void buildTemplate(TemplateToken token)
        {
            switch (token.Value)
            {
                case "function":
                    buildFuntionTemplate();
                    break;
                case "constant":
                    buildConstantTemplate();
                    break;
                case "contents":
                    buildContentsTemplate();
                    break;
                default:
                    throw new InvalidDataException($"Unknown template '{token.Value}'.");
            }
        }

        protected override void buildLink(LinkToken token)
            => builder.Append($"[{token.DisplayString}]({token.Value})");
        protected override void buildParamLink(ParamLinkToken token)
        {
            if (!plinkValidator(token.Value))
                throw new Exception($"Parameter '{token.Value}' doesn't exist.");

            builder.Append($"[{token.Value}]({StringUtils.ToHeaderRef(token.Value)})");
        }
        protected override void buildConstantLink(ConstantLinkToken token)
            => builder.Append($"[{token.Value}]({linkPrefix}Constants/{token.Value}.md)");
        protected override void buildFunctionLink(FunctionLinkToken token)
        {
            string[] split = token.Value.Split(',');
            string name = split[0];
            int numbParams = split.Length - 1;

            // remove distinguishing number
            if (char.IsDigit(name[^1]))
                name = name.Substring(0, name.Length - 1);

            TypeSymbol[] paramTypes = new TypeSymbol[numbParams];

            for (int i = 0; i < numbParams; i++)
                paramTypes[i] = TypeSymbol.GetTypeInternal(split[i + 1]);

            int sameNameCounter = 0;

            foreach (var (func, category) in BuiltinFunctions.GetAllWithCategory())
            {
                if (func.Name == name &&
                    func.Parameters.Length == numbParams &&
                    func.Parameters.Select(param => param.Type).SequenceEqual(paramTypes))
                {
                    builder.Append('[');

                    builder.Append(func.Name);
                    if (func.IsGeneric)
                        builder.Append("<>");

                    builder.Append('(');
                    builder.Append(string.Join(',', func.Parameters.Select(param => param.Type.ToString())));
                    builder.Append(')');

                    builder.Append($"]({linkPrefix}Functions/");
                    if (!string.IsNullOrEmpty(category))
                    {
                        builder.Append(category);
                        builder.Append('/');
                    }
                    builder.Append(name.ToUpperFirst());
                    if (sameNameCounter != 0)
                        builder.Append(sameNameCounter + 1);
                    builder.Append(".md)");

                    return;
                }

                if (func.Name == name)
                    sameNameCounter++;
            }

            throw new Exception($"Function '{name}' with {numbParams} parameters ({string.Join(", ", paramTypes.Select(type => type.ToString()))}) wasn't found.");
        }

        protected override void buildCodeBlock(CodeBlockToken token)
        {
            builder.AppendLine("``` " + token.Lang);
            builder.AppendLine(token.Value);
            builder.AppendLine("```");
        }

        #region Templates
        private void buildFuntionTemplate()
        {
            string type = getArg("type");
            string? return_info = getOptionalArg("return_info");
            bool isGeneric = getBoolArg("is_generic");
            bool isMethod = getBoolArg("is_method");
            string name = getArg("name");
            string? info = getOptionalArg("info");
            string[] param_mods = getArg("param_mods").Split(";;");
            string[] param_types = getArg("param_types").Split(";;");
            string[] param_names = getArg("param_names").Split(";;");
            string[]? param_infos = getOptionalArg("param_infos")?.Split(";;");

            string? examples = getOptionalArg("examples");
            string? remarks = getOptionalArg("remarks");
            string[]? related = getOptionalArg("related")?.Split(";;");

            if (param_mods.Length != param_names.Length)
                throw new InvalidDataException("param_mods length must match param_names length");

            if (param_types.Length != param_names.Length)
                throw new InvalidDataException("param_types length must match param_names length");

            if (param_infos is not null && param_infos.Length != param_names.Length)
                throw new InvalidDataException("param_infos length must match param_names length");

            int numbParams = param_names.Length;

            if (param_names.Length == 1 && string.IsNullOrEmpty(param_names[0]))
                numbParams = 0;

            builder.Append($"# {name.ToUpperFirst()}");
            if (isGeneric)
                builder.Append("<>");
            builder.AppendLine("(" + string.Join(", ", param_types) + ")");
            builder.AppendLine();

            if (!string.IsNullOrEmpty(info))
            {
                string builtInfo = parse(info);
                if (!builtInfo.EndsWith('.'))
                    Console.WriteLine("Infos should end with. - " + info);

                builder.AppendLine(builtInfo);
                builder.AppendLine();
            }

            builder.AppendLine("```");

            builder.Append(type);
            builder.Append(' ');
            builder.Append(name.ToLowerFirst());
            if (isGeneric)
                builder.Append("<>");
            builder.Append('(');

            for (int i = 0; i < numbParams; i++)
            {
                if (i != 0)
                    builder.Append(", ");
                else if (isMethod)
                    builder.Append("this ");

                if (!string.IsNullOrEmpty(param_mods[i]))
                {
                    builder.Append(param_mods[i]);
                    builder.Append(' ');
                }

                appendType(param_types[i]);
                builder.Append(' ');
                builder.Append(param_names[i]);
            }

            builder.AppendLine(")");

            builder.AppendLine("```");
            builder.AppendLine();

            if (numbParams != 0)
            {
                builder.AppendLine("## Parameters");
                builder.AppendLine();

                for (int i = 0; i < numbParams; i++)
                {
                    builder.Append("#### `");
                    builder.Append(param_names[i]);
                    builder.AppendLine("`");

                    builder.Append("Type: ");
                    appendType(param_types[i]);
                    builder.AppendLine();

                    if (param_infos is not null && !string.IsNullOrEmpty(param_infos[i]))
                    {
                        builder.AppendLine();
                        string builtInfo = parse(param_infos[i]);
                        if (!builtInfo.EndsWith('.'))
                            Console.WriteLine("Param infos should end with. - " + info);

                        builder.AppendLine(builtInfo);
                    }

                    builder.AppendLine();
                }
            }

            if (type != "void")
            {
                builder.AppendLine("## Returns");
                builder.AppendLine();
                appendType(type);
                builder.AppendLine();

                if (return_info is not null)
                {
                    builder.AppendLine();
                    builder.AppendLine(parse(return_info));
                    builder.AppendLine();
                }
            }

            if (!string.IsNullOrEmpty(examples))
            {
                builder.AppendLine("## Examples");
                builder.AppendLine();

                builder.AppendLine(parse(examples));
                builder.AppendLine();
            }

            if (!string.IsNullOrEmpty(remarks))
            {
                builder.AppendLine("## Remarks");
                builder.AppendLine();

                builder.AppendLine(parse(remarks));
                builder.AppendLine();
            }

            if (related is not null)
            {
                builder.AppendLine("## Related");
                builder.AppendLine();

                for (int i = 0; i < related.Length; i++)
                {
                    builder.Append(" - ");
                    builder.AppendLine(parse(related[i]));
                }
                builder.AppendLine();
            }

            string parse(string str)
            {
                return this.parse(str, paramName => param_names.Contains(paramName)).Trim();
            }
        }
        private void buildConstantTemplate()
        {
            string type = getArg("type");
            string name = getArg("name");
            string? info = getOptionalArg("info");
            string[] names = getArg("names").Split(";;");
            string[] values = getArg("values").Split(";;");
            string[]? infos = getOptionalArg("infos")?.Split(";;");

            if (values.Length != names.Length)
                throw new InvalidDataException("values length must match names length");

            if (infos is not null && infos.Length != names.Length)
                throw new InvalidDataException("infos length must match names length");

            int numbValues = names.Length;

            builder.AppendLine($"# {name}");

            builder.Append("### Type: ");
            appendType(type);
            builder.AppendLine();

            if (!string.IsNullOrEmpty(info))
                builder.AppendLine(parse(info));

            builder.AppendLine("```");

            for (int i = 0; i < numbValues; i++)
                builder.AppendLine(names[i]);

            builder.AppendLine("```");

            for (int i = 0; i < numbValues; i++)
            {
                builder.Append("#### ");
                builder.AppendLine(names[i]);

                builder.Append("Value: ");
                builder.AppendLine(values[i]);

                if (infos is not null && !string.IsNullOrEmpty(infos[i]))
                {
                    builder.AppendLine();
                    builder.AppendLine(parse(infos[i]));
                }
            }
        }
        private void buildContentsTemplate()
        {
            string startDir = Path.GetDirectoryName(path)!;

            foreach (string file in Directory.EnumerateFiles(startDir))
            {
                string fileName = Path.GetFileNameWithoutExtension(file);
                builder.AppendLine($"- [{fileName}]({fileName}.md)");
            }

            foreach (string dir in Directory.EnumerateDirectories(startDir))
                buildDir(dir, string.Empty);

            void buildDir(string dir, string offset)
            {
                string relativeDir = Path.GetRelativePath(startDir, dir).Replace('\\', '/');

                builder.AppendLine(offset + $"- [{Path.GetFileName(dir)}]({relativeDir}/README.md)");

                offset += "    ";

                foreach (string file in Directory.EnumerateFiles(dir))
                {
                    string fileName = Path.GetFileNameWithoutExtension(file);
                    builder.AppendLine(offset + $"- [{fileName}]({relativeDir}/{fileName}.md)");
                }

                foreach (string subDir in Directory.EnumerateDirectories(dir))
                    buildDir(subDir, offset);
            }
        }
        #endregion

        #region Utils
        private string parse(string str, Func<string, bool>? plinkValidator = null)
        {
            Parser parser = new Parser(str);
            MdBuilder builder = new MdBuilder(parser.Parse(), path);

            if (plinkValidator is not null)
                builder.plinkValidator = plinkValidator;

            return builder.Build();
        }

        private void appendType(string typeName)
        {
            // TODO: link
            TypeSymbol type = TypeSymbol.GetTypeInternal(typeName);
            builder.Append(type);
        }

        private string getArg(string name)
        {
            if (args.TryGetValue(name, out var value))
                return value;
            else
                throw new Exception($"Required arg '{name}' missing.");
        }
        private bool getBoolArg(string name)
        {
            if (args.TryGetValue(name, out var value))
                return bool.Parse(value);
            else
                return false;
        }
        private string? getOptionalArg(string name)
        {
            if (args.TryGetValue(name, out var value))
                return value;
            else
                return null;
        }
        #endregion
    }
}
