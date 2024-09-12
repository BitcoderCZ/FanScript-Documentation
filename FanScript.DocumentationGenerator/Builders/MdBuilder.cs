using FanScript.Compiler;
using FanScript.Compiler.Symbols;
using FanScript.DocumentationGenerator.Parsing;
using FanScript.DocumentationGenerator.Tokens;
using FanScript.DocumentationGenerator.Tokens.Links;
using FanScript.DocumentationGenerator.Utils;
using System.Collections.Immutable;
using System.Text;

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
                case "event":
                    buildEventTemplate();
                    break;
                case "type":
                    buildTypeTemplate();
                    break;
                case "modifier":
                    buildModifierTemplate();
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
        protected override void buildConstantValueLink(ConstantValueLinkToken token)
            => builder.Append($"[{token.Value}]({linkPrefix}Constants/{token.ConstantName}.md{StringUtils.ToHeaderRef(token.Value)})");
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
        protected override void buildEventLink(EventLinkToken token)
            => builder.Append($"[{token.Value}]({linkPrefix}Events/{token.Value}.md)");
        protected override void buildTypeLink(TypeLinkToken token)
            => appendType(token.Value);
        protected override void buildModifierLink(ModifierLinkToken token)
            => builder.Append($"[{token.Value}]({linkPrefix}Modifiers/{ModifiersE.FromKind(FanScript.Compiler.Syntax.SyntaxFacts.GetKeywordKind(token.Value))}.md)");

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

            string? remarks = getOptionalArg("remarks");
            string? examples = getOptionalArg("examples");
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
                    Console.WriteLine("Infos should end with '.' - " + info);

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

                appendType(param_types[i], false);
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
                            Console.WriteLine("Param infos should end with '.' - " + param_infos[i]);

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

                if (!string.IsNullOrEmpty(return_info))
                {
                    builder.AppendLine();

                    string builtInfo = parse(return_info);
                    if (!builtInfo.EndsWith('.'))
                        Console.WriteLine("Param infos should end with '.' - " + return_info);

                    builder.AppendLine(builtInfo);
                    builder.AppendLine();
                }
            }

            if (!string.IsNullOrEmpty(remarks))
            {
                builder.AppendLine("## Remarks");
                builder.AppendLine();

                string builtRemarks = parse(remarks);

                if (!builtRemarks.EndsWith('.'))
                    Console.WriteLine("Remarks should end with '.' - " + remarks);

                builder.AppendLine(builtRemarks);
                builder.AppendLine();
            }

            if (!string.IsNullOrEmpty(examples))
            {
                builder.AppendLine("## Examples");
                builder.AppendLine();

                builder.AppendLine(parse(examples));
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
            {
                string builtInfo = parse(info);
                if (!builtInfo.EndsWith('.'))
                    Console.WriteLine("Constant info should end with '.' - " + info);

                builder.AppendLine(builtInfo);
            }

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

                    string builtInfo = parse(infos[i]);
                    if (!builtInfo.EndsWith('.'))
                        Console.WriteLine("Constant infos should end with '.' - " + infos[i]);

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

                if (fileName == "README")
                    continue;

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

                    if (fileName == "README")
                        continue;

                    builder.AppendLine(offset + $"- [{fileName}]({relativeDir}/{fileName}.md)");
                }

                foreach (string subDir in Directory.EnumerateDirectories(dir))
                    buildDir(subDir, offset);
            }
        }
        private void buildEventTemplate()
        {
            string name = getArg("name");
            string? info = getOptionalArg("info");
            string[] param_mods = getArg("param_mods").Split(";;");
            string[] param_types = getArg("param_types").Split(";;");
            string[] param_names = getArg("param_names").Split(";;");
            bool[] param_is_constant = getArg("param_is_constant").Split(";;").Select(parseBool).ToArray();
            string[]? param_infos = getOptionalArg("param_infos")?.Split(";;");

            string? remarks = getOptionalArg("remarks");
            string? examples = getOptionalArg("examples");
            string[]? related = getOptionalArg("related")?.Split(";;");

            if (param_mods.Length != param_names.Length)
                throw new InvalidDataException("param_mods length must match param_names length");
            if (param_types.Length != param_names.Length)
                throw new InvalidDataException("param_types length must match param_names length");
            if (param_is_constant.Length != param_names.Length)
                throw new InvalidDataException("param_is_constant length must match param_names length");

            if (param_infos is not null && param_infos.Length != param_names.Length)
                throw new InvalidDataException("param_infos length must match param_names length");

            int numbParams = param_names.Length;

            if (param_names.Length == 1 && string.IsNullOrEmpty(param_names[0]))
                numbParams = 0;

            builder.Append($"# {name.ToUpperFirst()}");
            builder.AppendLine("(" + string.Join(", ", param_types) + ")");
            builder.AppendLine();

            if (!string.IsNullOrEmpty(info))
            {
                string builtInfo = parse(info);
                if (!builtInfo.EndsWith('.'))
                    Console.WriteLine("Infos should end with '.' - " + info);

                builder.AppendLine(builtInfo);
                builder.AppendLine();
            }

            builder.AppendLine("```");

            builder.Append("on ");
            builder.Append(name.ToLowerFirst());
            builder.Append('(');

            for (int i = 0; i < numbParams; i++)
            {
                if (i != 0)
                    builder.Append(", ");

                if (param_is_constant[i])
                    builder.Append("const ");

                if (!string.IsNullOrEmpty(param_mods[i]))
                {
                    builder.Append(param_mods[i]);
                    builder.Append(' ');
                }

                appendType(param_types[i], false);
                builder.Append(' ');
                builder.Append(param_names[i]);
            }

            builder.AppendLine(") { }");

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

                    if (param_is_constant[i])
                    {
                        builder.AppendLine("Value must be constant.");
                        builder.AppendLine();
                    }

                    if (param_infos is not null && !string.IsNullOrEmpty(param_infos[i]))
                    {
                        builder.AppendLine();
                        string builtInfo = parse(param_infos[i]);
                        if (!builtInfo.EndsWith('.'))
                            Console.WriteLine("Param infos should end with '.' - " + param_infos[i]);

                        builder.AppendLine(builtInfo);
                    }

                    builder.AppendLine();
                }
            }

            if (!string.IsNullOrEmpty(remarks))
            {
                builder.AppendLine("## Remarks");
                builder.AppendLine();

                string builtRemarks = parse(remarks);

                if (!builtRemarks.EndsWith('.'))
                    Console.WriteLine("Remarks should end with '.' - " + remarks);

                builder.AppendLine(builtRemarks);
                builder.AppendLine();
            }

            if (!string.IsNullOrEmpty(examples))
            {
                builder.AppendLine("## Examples");
                builder.AppendLine();

                builder.AppendLine(parse(examples));
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
        private void buildTypeTemplate()
        {
            string name = getArg("name");
            string? info = getOptionalArg("info");
            string? how_to_create = getOptionalArg("how_to_create");

            string? remarks = getOptionalArg("remarks");
            string? examples = getOptionalArg("examples");
            string[]? related = getOptionalArg("related")?.Split(";;");

            builder.AppendLine($"# {name.ToUpperFirst()}");
            builder.AppendLine();

            if (!string.IsNullOrEmpty(info))
            {
                string builtInfo = parse(info);
                if (!builtInfo.EndsWith('.'))
                    Console.WriteLine("Infos should end with '.' - " + info);

                builder.AppendLine(builtInfo);
                builder.AppendLine();
            }

            builder.AppendLine("```");
            builder.AppendLine(TypeSymbol.GetTypeInternal(name).ToString());
            builder.AppendLine("```");
            builder.AppendLine();

            if (!string.IsNullOrEmpty(how_to_create))
            {
                builder.AppendLine("## How to create");
                builder.AppendLine();
                builder.AppendLine(parse(how_to_create));
                builder.AppendLine();
            }

            if (!string.IsNullOrEmpty(remarks))
            {
                builder.AppendLine("## Remarks");
                builder.AppendLine();

                string builtRemarks = parse(remarks);

                if (!builtRemarks.EndsWith('.'))
                    Console.WriteLine("Remarks should end with '.' - " + remarks);

                builder.AppendLine(builtRemarks);
                builder.AppendLine();
            }

            if (!string.IsNullOrEmpty(examples))
            {
                builder.AppendLine("## Examples");
                builder.AppendLine();

                builder.AppendLine(parse(examples));
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
                return this.parse(str, paramName => false /*Types don't have args*/).Trim();
            }
        }
        private void buildModifierTemplate()
        {
            string name = getArg("name");
            string? info = getOptionalArg("info");

            string[] targets = getArg("targets").Split(";;");
            string[] conflicting = getArg("conflicting").Split(";;");
            string[]? required = getOptionalArg("required")?.Split(";;");

            string? remarks = getOptionalArg("remarks");
            string? examples = getOptionalArg("examples");
            string[]? related = getOptionalArg("related")?.Split(";;");

            Modifiers mod = Enum.Parse<Modifiers>(name);

            builder.AppendLine($"# {name.ToUpperFirst()}");
            builder.AppendLine();

            if (!string.IsNullOrEmpty(info))
            {
                string builtInfo = parse(info);
                if (!builtInfo.EndsWith('.'))
                    Console.WriteLine("Infos should end with '.' - " + info);

                builder.AppendLine(builtInfo);
                builder.AppendLine();
            }

            builder.AppendLine("```");
            builder.AppendLine(mod.ToSyntaxString());
            builder.AppendLine("```");
            builder.AppendLine();

            builder.AppendLine("## Targets");
            builder.AppendLine();
            builder.AppendLine("The modifier can be applied to:");

            for (int i = 0; i < targets.Length; i++)
                builder.AppendLine(" - " + targets[i]);

            builder.AppendLine();

            if (conflicting.Length > 0 && !string.IsNullOrEmpty(conflicting[0]))
            {
                builder.AppendLine("## Conflicting modifiers");
                builder.AppendLine();
                builder.AppendLine("These modifiers cannot be used with this one:");

                for (int i = 0; i < conflicting.Length; i++)
                    builder.AppendLine(" - " + conflicting[i]);

                builder.AppendLine();
            }

            if (required is not null && required.Length > 0 && !string.IsNullOrEmpty(required[0]))
            {
                builder.AppendLine("## Required modifiers");
                builder.AppendLine();
                builder.AppendLine("One of these modifiers must be used to use this one:");

                for (int i = 0; i < required.Length; i++)
                    builder.AppendLine(" - " + required[i]);

                builder.AppendLine();
            }

            if (!string.IsNullOrEmpty(remarks))
            {
                builder.AppendLine("## Remarks");
                builder.AppendLine();

                string builtRemarks = parse(remarks);

                if (!builtRemarks.EndsWith('.'))
                    Console.WriteLine("Remarks should end with '.' - " + remarks);

                builder.AppendLine(builtRemarks);
                builder.AppendLine();
            }

            if (!string.IsNullOrEmpty(examples))
            {
                builder.AppendLine("## Examples");
                builder.AppendLine();

                builder.AppendLine(parse(examples));
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
                return this.parse(str, paramName => false /*Modifiers don't have args*/).Trim();
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

        private bool parseBool(string? value)
        {
            if (string.IsNullOrEmpty(value))
                return false;
            else
                return bool.Parse(value);
        }

        private void appendType(string typeName, bool link = true)
        {
            // TODO: link
            TypeSymbol type = TypeSymbol.GetTypeInternal(typeName);
            if (!link || type == TypeSymbol.Generic || type == TypeSymbol.Error || type == TypeSymbol.Void)
                builder.Append(type.Name);
            else
                builder.Append($"[{type.Name}]({linkPrefix}Types/{type.Name.ToUpperFirst()}.md)");
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
