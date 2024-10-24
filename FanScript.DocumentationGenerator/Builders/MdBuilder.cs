using FanScript.Compiler;
using FanScript.Compiler.Binding;
using FanScript.Compiler.Symbols;
using FanScript.Compiler.Syntax;
using FanScript.Documentation.Attributes;
using FanScript.Documentation.DocElements;
using FanScript.Documentation.DocElements.Builders;
using FanScript.Documentation.DocElements.Links;
using FanScript.DocumentationGenerator.Elements;
using FanScript.DocumentationGenerator.Utils;
using FanScript.Utils;
using System.Collections.Immutable;
using System.Text;

namespace FanScript.DocumentationGenerator.Builders
{
    public sealed class MdBuilder : DocElementBuilder
    {
        private const string linkPrefix = "/MdDocs/";

        private string path;

        private readonly Dictionary<string, DocElement?> args = new();

        public MdBuilder(string path)
        {
            this.path = path;
        }

        protected override void buildString(DocString element, StringBuilder builder)
            => builder.Append(element.Text.Replace("\t", "&nbsp;&nbsp;&nbsp;&nbsp;"));

        protected override void buildHeader(DocHeader element, StringBuilder builder)
            => builder.Append(new string('#', element.Level) + " " + Build(element.Value));

        protected override void buildUnknownElement(DocElement element, StringBuilder builder)
        {
            switch (element)
            {
                case DocFileArg arg:
                    buildArg(arg);
                    break;
                case DocFileTemplate template:
                    buildTemplate(template, builder);
                    break;
                default:
                    base.buildUnknownElement(element, builder);
                    break;
            }
        }

        private void buildArg(DocFileArg element)
            => args.Add(element.Name, element.Value);

        private void buildTemplate(DocFileTemplate element, StringBuilder builder)
        {
            switch (element.Value.Text)
            {
                case "function":
                    buildFuntionTemplate(builder);
                    break;
                case "builtin_function":
                    buildBuiltinFuntionTemplate(builder);
                    break;
                case "constant":
                    buildConstantTemplate(builder);
                    break;
                case "contents":
                    buildContentsTemplate(builder);
                    break;
                case "event":
                    buildEventTemplate(builder);
                    break;
                case "type":
                    buildTypeTemplate(builder);
                    break;
                case "modifier":
                    buildModifierTemplate(builder);
                    break;
                case "operator_binary":
                    buildBinaryOperatorTemplate(builder);
                    break;
                case "operator_unary":
                    buildUnaryOperatorTemplate(builder);
                    break;
                case "build_command":
                    buildBuildCommandTemplate(builder);
                    break;
                default:
                    throw new InvalidDataException($"Unknown template '{element.Value}'.");
            }
        }

        #region Links
        protected override void buildUrlLink(UrlLink element, StringBuilder builder)
        {
            var (display, link) = element.GetStrings();
            builder.Append($"[{display}]({link})");
        }
        protected override void buildParamLink(ParamLink element, StringBuilder builder)
        {
            var (display, link) = element.GetStrings();
            builder.Append($"[{display}]({StringUtils.ToHeaderRef(link)})");
        }
        protected override void buildConstantLink(ConstantLink element, StringBuilder builder)
        {
            var (display, link) = element.GetStrings();
            builder.Append($"[{display}]({linkPrefix}Constants/{link}.md)");
        }
        protected override void buildConstantValueLink(ConstantValueLink element, StringBuilder builder)
        {
            var (display, link) = element.GetStrings();
            builder.Append($"[{display}]({linkPrefix}Constants/{link}.md{StringUtils.ToHeaderRef(display)})");
        }
        protected override void buildFunctionLink(FunctionLink element, StringBuilder builder)
        {
            FunctionSymbol func = element.Function;

            builder.Append('[');

            builder.Append(func.Name);
            if (func.IsGeneric)
                builder.Append("<>");

            builder.Append('(');
            builder.Append(string.Join(',', func.Parameters.Select(param => param.Type.ToString())));
            builder.Append(')');

            builder.Append($"]({linkPrefix}Functions/");
            if (func.Namespace.Length > 1)
            {
                builder.Append(func.Namespace.Slice(1)); // remove the builtin/
                builder.Append('/');
            }
            builder.Append(U.FuncToFile(func));
            builder.Append(".md)");
        }
        protected override void buildEventLink(EventLink element, StringBuilder builder)
        {
            var (display, link) = element.GetStrings();
            builder.Append($"[{display}]({linkPrefix}Events/{link}.md)");
        }
        protected override void buildTypeLink(TypeLink element, StringBuilder builder)
            => appendType(element.Type, builder);
        protected override void buildModifierLink(ModifierLink element, StringBuilder builder)
        {
            var (display, link) = element.GetStrings();
            builder.Append($"[{display}]({linkPrefix}Modifiers/{link}.md)");
        }
        protected override void buildBuildCommandLink(BuildCommandLink element, StringBuilder builder)
        {
            var (display, link) = element.GetStrings();
            builder.Append($"[{link}]({linkPrefix}BuildCommands/{display}.md)");
        }
        #endregion

        protected override void buildCodeBlock(DocCodeBlock element, StringBuilder builder)
        {
            builder.Append("```");
            if (!string.IsNullOrEmpty(element.Lang))
            {
                builder.Append(' ');
                builder.AppendLine(element.Lang);
            }
            else
                builder.AppendLine();

            builder.AppendLine(element.Value.Text.Trim());
            builder.AppendLine("```");
        }

        protected override void buildList(DocList element, StringBuilder builder)
        {
            if (element.Value is DocList.Item onlyItem)
                buildListItem(onlyItem, builder);
            else if (element.Value is DocBlock block)
            {
                foreach (var item in block.Elements)
                    if (item is DocList.Item listItem)
                        buildListItem(listItem, builder);
            }
        }

        protected override void buildListItem(DocList.Item element, StringBuilder builder)
        {
            if (!builder.IsCurrentLineEmpty())
                builder.AppendLine();

            builder.Append(" - ");
            buildElement(element.Value, builder);

            if (!builder.IsCurrentLineEmpty())
                builder.AppendLine();
        }

        #region Templates
        private void buildFuntionTemplate(StringBuilder builder)
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
                checkInfoEnd(info);

                builder.AppendLine(parse(info));
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

                appendType(param_types[i], builder, false);
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
                    appendType(param_types[i], builder);
                    builder.AppendLine();

                    if (param_infos is not null && !string.IsNullOrEmpty(param_infos[i]))
                    {
                        checkInfoEnd(param_infos[i]);
                        builder.AppendLine();

                        builder.AppendLine(parse(param_infos[i]));
                    }

                    builder.AppendLine();
                }
            }

            if (type != "void")
            {
                builder.AppendLine("## Returns");
                builder.AppendLine();
                appendType(type, builder);
                builder.AppendLine();

                if (!string.IsNullOrEmpty(return_info))
                {
                    builder.AppendLine();

                    checkInfoEnd(return_info);

                    builder.AppendLine(parse(return_info));
                    builder.AppendLine();
                }
            }

            if (!string.IsNullOrEmpty(remarks))
            {
                builder.AppendLine("## Remarks");
                builder.AppendLine();

                checkInfoEnd(remarks);

                builder.AppendLine(parse(remarks));
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
                return this.parse(str, null).Trim();
            }
        }
        private void buildBuiltinFuntionTemplate(StringBuilder builder)
        {
            string nameArg = getArg("name");
            string[] param_types = getArg("param_types").Split(";");

            TypeSymbol[] paramTypes;
            if (param_types.Length == 1 && string.IsNullOrEmpty(param_types[0]))
                paramTypes = Array.Empty<TypeSymbol>();
            else
                paramTypes = param_types
                    .Select(str => TypeSymbol.GetTypeInternal(str))
                    .ToArray();

            FunctionSymbol? func = BuiltinFunctions
                .GetAll()
                .FirstOrDefault(func => func.Name == nameArg && func.Parameters.Select(param => param.Type).SequenceEqual(paramTypes));

            if (func is null)
                throw new Exception($"Built in function \"{nameArg}\" with types ({string.Join(", ", param_types)}) wasn't found.");

            FunctionDocAttribute doc = BuiltinFunctions.FunctionToDoc[func];

            string name = string.IsNullOrEmpty(doc.NameOverwrite) ? func.Name : doc.NameOverwrite;

            builder.Append($"# {name.ToUpperFirst()}");
            if (func.IsGeneric)
                builder.Append("<>");
            builder.AppendLine("(" + string.Join(", ", func.Parameters.Select(param => param.Type.Name)) + ")");
            builder.AppendLine();

            if (!string.IsNullOrEmpty(doc.Info))
            {
                builder.AppendLine(parse(doc.Info));
                builder.AppendLine();
            }

            builder.AppendLine("```");

            builder.Append(func.Type.Name);
            builder.Append(' ');
            builder.Append(name);
            if (func.IsGeneric)
                builder.Append("<>");
            builder.Append('(');

            for (int i = 0; i < func.Parameters.Length; i++)
            {
                var param = func.Parameters[i];

                if (i != 0)
                    builder.Append(", ");
                else if (func.IsMethod)
                    builder.Append("this ");

                if (param.Modifiers != 0)
                {
                    builder.Append(param.Modifiers.ToSyntaxString());
                    builder.Append(' ');
                }

                appendType(param.Type, builder, false);
                builder.Append(' ');
                builder.Append(param.Name);
            }

            builder.AppendLine(")");

            builder.AppendLine("```");
            builder.AppendLine();

            if (func.Parameters.Length != 0)
            {
                builder.AppendLine("## Parameters");
                builder.AppendLine();

                for (int i = 0; i < func.Parameters.Length; i++)
                {
                    var param = func.Parameters[i];

                    builder.Append("#### `");
                    builder.Append(param.Name);
                    builder.AppendLine("`");

                    if (param.Modifiers != 0)
                    {
                        builder.Append("Modifiers: ");

                        bool first = true;

                        foreach (var mod in Enum.GetValues<Modifiers>())
                        {
                            if (param.Modifiers.HasFlag(mod))
                            {
                                if (!first)
                                    builder.Append(", ");

                                first = false;
                                buildModifierLink(new ModifierLink(ImmutableArray<DocArg>.Empty, new DocString(string.Empty), mod), builder);
                            }
                        }

                        builder.AppendLine();
                        builder.AppendLine();
                    }

                    builder.Append("Type: ");
                    appendType(param.Type, builder);
                    builder.AppendLine();

                    if (doc.ParameterInfos is not null && !string.IsNullOrEmpty(doc.ParameterInfos[i]))
                    {
                        builder.AppendLine();

                        checkInfoEnd(doc.ParameterInfos[i]);

                        builder.AppendLine(parse(doc.ParameterInfos[i]!));
                    }

                    builder.AppendLine();
                }
            }

            if (func.Type != TypeSymbol.Void)
            {
                builder.AppendLine("## Returns");
                builder.AppendLine();
                appendType(func.Type, builder);
                builder.AppendLine();

                if (!string.IsNullOrEmpty(doc.ReturnValueInfo))
                {
                    builder.AppendLine();

                    checkInfoEnd(doc.ReturnValueInfo);

                    builder.AppendLine(parse(doc.ReturnValueInfo));
                    builder.AppendLine();
                }
            }

            buildRemarksExamplesRelated(doc, parse, builder);

            string parse(string str)
            {
                return this.parse(str, func).Trim();
            }
        }
        private void buildConstantTemplate(StringBuilder builder)
        {
            string name = getArg("name");

            ConstantGroup? group = Constants.Groups.FirstOrDefault(con => con.Name == name);

            if (group is null)
                throw new Exception($"Constant group \"{name}\" doesn't exist.");

            ConstantDocAttribute doc = Constants.ConstantToDoc[group];

            builder.AppendLine($"# {name}");

            builder.Append("### Type: ");
            appendType(group.Type, builder);
            builder.AppendLine();

            if (!string.IsNullOrEmpty(doc.Info))
            {
                builder.AppendLine(parse(doc.Info));
            }

            if (doc.UsedBy is not null)
            {
                builder.AppendLine("### Used by");
                builder.AppendLine();

                foreach (var item in doc.UsedBy)
                {
                    builder.Append(" - ");
                    builder.AppendLine(parse(item));
                }
            }

            builder.AppendLine("### Values");
            builder.AppendLine("```");

            for (int i = 0; i < group.Values.Length; i++)
                builder.AppendLine(group.Name + "_" + group.Values[i].Name);

            builder.AppendLine("```");

            for (int i = 0; i < group.Values.Length; i++)
            {
                Constant value = group.Values[i];

                builder.Append("##### ");
                builder.AppendLine(group.Name + "_" + value.Name);

                builder.Append("Value: ");
                builder.AppendLine(value.Value.ToString());

                if (doc.ValueInfos is not null && !string.IsNullOrEmpty(doc.ValueInfos[i]))
                {
                    builder.AppendLine();
                    builder.AppendLine(parse(doc.ValueInfos[i]!));
                }
            }

            buildRemarksExamplesRelated(doc, parse, builder);

            string parse(string str)
            {
                return this.parse(str, null).Trim();
            }
        }
        private void buildContentsTemplate(StringBuilder builder)
        {
            string startDir = Path.GetDirectoryName(path)!;

            foreach (string file in Directory.EnumerateFiles(startDir))
            {
                string fileName = Path.GetFileNameWithoutExtension(file);

                if (fileName == "README")
                    continue;

                int dotIndex = fileName.IndexOf('.');

                builder.AppendLine($"- [{(dotIndex == -1 ? fileName : fileName.Substring(0, dotIndex))}]({fileName}.md)");
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

                    int dotIndex = fileName.IndexOf('.');

                    builder.AppendLine(offset + $"- [{(dotIndex == -1 ? fileName : fileName.Substring(0, dotIndex))}]({relativeDir}/{fileName}.md)");
                }

                foreach (string subDir in Directory.EnumerateDirectories(dir))
                    buildDir(subDir, offset);
            }
        }
        private void buildEventTemplate(StringBuilder builder)
        {
            string name = getArg("name");

            if (!Enum.TryParse(name, out EventType eventType))
                throw new Exception($"Event \"{name}\" doesn't exist.");

            EventTypeInfo info = eventType.GetInfo();
            EventDocAttribute doc = U.GetAttribute<EventType, EventDocAttribute>(eventType);

            builder.Append($"# {name.ToUpperFirst()}");
            builder.AppendLine("(" + string.Join(", ", info.Parameters.Select(param => param.Type.Name)) + ")");
            builder.AppendLine();

            if (!string.IsNullOrEmpty(doc.Info))
            {
                builder.AppendLine(parse(doc.Info));
                builder.AppendLine();
            }

            builder.AppendLine("```");

            builder.Append("on ");
            builder.Append(name.ToLowerFirst());
            builder.Append('(');

            for (int i = 0; i < info.Parameters.Length; i++)
            {
                var param = info.Parameters[i];

                if (i != 0)
                    builder.Append(", ");

                if (param.Modifiers != 0)
                {
                    builder.Append(param.Modifiers.ToSyntaxString());
                    builder.Append(' ');
                }

                appendType(param.Type, builder, false);
                builder.Append(' ');
                builder.Append(param.Name);
            }

            builder.AppendLine(") { }");

            builder.AppendLine("```");
            builder.AppendLine();

            if (info.Parameters.Length != 0)
            {
                builder.AppendLine("## Parameters");
                builder.AppendLine();

                for (int i = 0; i < info.Parameters.Length; i++)
                {
                    var param = info.Parameters[i];

                    builder.Append("#### `");
                    builder.Append(param.Name);
                    builder.AppendLine("`");

                    if (param.Modifiers != 0)
                    {
                        builder.Append("Modifiers: ");

                        bool first = true;

                        foreach (var mod in Enum.GetValues<Modifiers>())
                        {
                            if (param.Modifiers.HasFlag(mod))
                            {
                                if (!first)
                                    builder.Append(", ");

                                first = false;
                                buildModifierLink(new ModifierLink(ImmutableArray<DocArg>.Empty, new DocString(string.Empty), mod), builder);
                            }
                        }

                        builder.AppendLine();
                        builder.AppendLine();
                    }

                    builder.Append("Type: ");
                    appendType(param.Type, builder);
                    builder.AppendLine();

                    if (doc.ParamInfos is not null && !string.IsNullOrEmpty(doc.ParamInfos[i]))
                    {
                        builder.AppendLine();
                        builder.AppendLine(parse(doc.ParamInfos[i]!));
                    }

                    builder.AppendLine();
                }
            }

            buildRemarksExamplesRelated(doc, parse, builder);

            string parse(string str)
            {
                return this.parse(str, null).Trim();
            }
        }
        private void buildTypeTemplate(StringBuilder builder)
        {
            string name = getArg("name");

            TypeSymbol type = TypeSymbol.GetTypeInternal(name);
            TypeDocAttribute doc = TypeSymbol.TypeToDoc[type];

            builder.AppendLine($"# {name.ToUpperFirst()}");
            builder.AppendLine();

            if (!string.IsNullOrEmpty(doc.Info))
            {
                builder.AppendLine(parse(doc.Info));
                builder.AppendLine();
            }

            builder.AppendLine("```");
            builder.AppendLine(TypeSymbol.GetTypeInternal(name).ToString());
            builder.AppendLine("```");
            builder.AppendLine();

            if (!string.IsNullOrEmpty(doc.HowToCreate))
            {
                builder.AppendLine("## How to create");
                builder.AppendLine();
                builder.AppendLine(parse(doc.HowToCreate));
                builder.AppendLine();
            }

            buildRemarksExamplesRelated(doc, parse, builder);

            string parse(string str)
            {
                return this.parse(str, null).Trim();
            }
        }
        private void buildModifierTemplate(StringBuilder builder)
        {
            string name = getArg("name");

            if (!Enum.TryParse(name, out Modifiers mod))
                throw new Exception($"Modifier \"{name}\" doesn't exist.");

            ModifierDocAttribute doc = U.GetAttribute<Modifiers, ModifierDocAttribute>(mod);

            builder.AppendLine($"# {name.ToUpperFirst()}");
            builder.AppendLine();

            if (!string.IsNullOrEmpty(doc.Info))
            {
                builder.AppendLine(parse(doc.Info));
                builder.AppendLine();
            }

            builder.AppendLine("```");
            builder.AppendLine(mod.ToSyntaxString());
            builder.AppendLine("```");
            builder.AppendLine();

            builder.AppendLine("## Targets");
            builder.AppendLine();
            builder.AppendLine("The modifier can be applied to:");

            foreach (var target in mod.GetTargets())
                builder.AppendLine(" - " + Enum.GetName(target));

            builder.AppendLine();

            var conflicting = mod.GetConflictingModifiers();
            if (conflicting.Count > 0)
            {
                builder.AppendLine("## Conflicting modifiers");
                builder.AppendLine();
                builder.AppendLine("These modifiers cannot be used with this one:");

                foreach (var conflict in conflicting)
                    builder.AppendLine(" - " + conflict.ToSyntaxString());

                builder.AppendLine();
            }

            var required = mod.GetRequiredModifiers();
            if (required.Count > 0)
            {
                builder.AppendLine("## Required modifiers");
                builder.AppendLine();
                builder.AppendLine("One of these modifiers must be used to use this one:");

                foreach (var req in required)
                    builder.AppendLine(" - " + req.ToString());

                builder.AppendLine();
            }

            buildRemarksExamplesRelated(doc, parse, builder);

            string parse(string str)
            {
                return this.parse(str, null).Trim();
            }
        }
        private void buildBinaryOperatorTemplate(StringBuilder builder)
        {
            string name = getArg("name");

            if (!Enum.TryParse(name, out BoundBinaryOperatorKind @operator))
                throw new Exception($"Build command \"{name}\" doesn't exist.");

            BinaryOperatorDocAttribute doc = U.GetAttribute<BoundBinaryOperatorKind, BinaryOperatorDocAttribute>(@operator);

            var combinations = BoundBinaryOperator.Operators
                .Where(op => op.Kind == @operator)
                .ToArray();

            builder.AppendLine($"# {name.ToUpperFirst()}");
            builder.AppendLine();

            if (!string.IsNullOrEmpty(doc.Info))
            {
                builder.AppendLine(parse(doc.Info));
                builder.AppendLine();
            }

            builder.AppendLine("```");
            builder.AppendLine($"c = a {combinations[0].SyntaxKind.GetText()} b");
            builder.AppendLine("```");
            builder.AppendLine();

            builder.AppendLine("## Types");
            builder.AppendLine();

            for (int i = 0; i < combinations.Length; i++)
            {
                var comb = combinations[i];

                builder.Append("- Left: ");
                appendType(comb.LeftType, builder);
                builder.Append(", Right: ");
                appendType(comb.RightType, builder);
                builder.Append(", Result: ");
                appendType(comb.Type, builder);
                builder.AppendLine();
                builder.AppendLine();

                if (doc.CombinationInfos is not null && !string.IsNullOrEmpty(doc.CombinationInfos[i]))
                {
                    builder.AppendLine(parse(doc.CombinationInfos[i]!));
                    builder.AppendLine();
                }
            }

            buildRemarksExamplesRelated(doc, parse, builder);

            string parse(string str)
            {
                return this.parse(str, null).Trim();
            }
        }
        private void buildUnaryOperatorTemplate(StringBuilder builder)
        {
            string name = getArg("name");

            if (!Enum.TryParse(name, out BoundUnaryOperatorKind @operator))
                throw new Exception($"Build command \"{name}\" doesn't exist.");

            UnaryOperatorDocAttribute doc = U.GetAttribute<BoundUnaryOperatorKind, UnaryOperatorDocAttribute>(@operator);

            var combinations = BoundUnaryOperator.Operators
                .Where(op => op.Kind == @operator)
                .ToArray();

            builder.AppendLine($"# {name.ToUpperFirst()}");
            builder.AppendLine();

            if (!string.IsNullOrEmpty(doc.Info))
            {
                builder.AppendLine(parse(doc.Info));
                builder.AppendLine();
            }

            builder.AppendLine("```");
            builder.AppendLine($"b = {combinations[0].SyntaxKind.GetText()}a");
            builder.AppendLine("```");
            builder.AppendLine();

            builder.AppendLine("## Types");
            builder.AppendLine();

            for (int i = 0; i < combinations.Length; i++)
            {
                var comb = combinations[i];

                builder.Append("- Operand: ");
                appendType(comb.OperandType, builder);
                builder.Append(", Result: ");
                appendType(comb.Type, builder);
                builder.AppendLine();
                builder.AppendLine();

                if (doc.CombinationInfos is not null && !string.IsNullOrEmpty(doc.CombinationInfos[i]))
                {
                    builder.AppendLine(parse(doc.CombinationInfos[i]!));
                    builder.AppendLine();
                }
            }

            buildRemarksExamplesRelated(doc, parse, builder);

            string parse(string str)
            {
                return this.parse(str, null).Trim();
            }
        }
        private void buildBuildCommandTemplate(StringBuilder builder)
        {
            string name = getArg("name");

            if (!Enum.TryParse(name, out BuildCommand buildCommand))
                throw new Exception($"Build command \"{name}\" doesn't exist.");

            BuildCommandDocAttribute doc = U.GetAttribute<BuildCommand, BuildCommandDocAttribute>(buildCommand);

            builder.AppendLine($"# {name}");

            if (!string.IsNullOrEmpty(doc.Info))
            {
                builder.AppendLine();
                builder.AppendLine(parse(doc.Info));
            }

            builder.AppendLine("```");

            builder.AppendLine("#" + name.ToLowerFirst());

            builder.AppendLine("```");

            buildRemarksExamplesRelated(doc, parse, builder);

            string parse(string str)
            {
                return this.parse(str, null).Trim();
            }
        }
        #endregion

        private void buildRemarksExamplesRelated(DocumentationAttribute doc, Func<string, string> parseFunc, StringBuilder builder)
        {
            checkInfoEnd(doc.Info);

            if (doc.Remarks is not null)
            {
                builder.AppendLine("## Remarks");
                builder.AppendLine();

                foreach (string remark in doc.Remarks)
                {
                    checkInfoEnd(remark);

                    builder.Append(" - ");
                    builder.AppendLine(parseFunc(remark));
                }

                builder.AppendLine();
            }

            if (!string.IsNullOrEmpty(doc.Examples))
            {
                builder.AppendLine("## Examples");
                builder.AppendLine();

                builder.AppendLine(parseFunc(doc.Examples));
                builder.AppendLine();
            }

            if (doc.Related is not null)
            {
                builder.AppendLine("## Related");
                builder.AppendLine();

                for (int i = 0; i < doc.Related.Length; i++)
                {
                    builder.Append(" - ");
                    builder.AppendLine(parseFunc(doc.Related[i]));
                }
                builder.AppendLine();
            }
        }

        #region Utils
        private string parse(string str, FunctionSymbol? currentFunction)
        {
            DocElementParser parser = U.CreateParser(currentFunction);
            return Build(parser.Parse(str));
        }

        private bool parseBool(string? value)
        {
            if (string.IsNullOrEmpty(value))
                return false;
            else
                return bool.Parse(value);
        }

        private void appendType(string typeName, StringBuilder builder, bool link = true)
        {
            TypeSymbol type = TypeSymbol.GetTypeInternal(typeName);
            if (!link || type == TypeSymbol.Generic || type == TypeSymbol.Error || type == TypeSymbol.Void)
                builder.Append(type.Name);
            else
                builder.Append($"[{type.Name}]({linkPrefix}Types/{type.Name.ToUpperFirst()}.md)");
        }
        private void appendType(TypeSymbol type, StringBuilder builder, bool link = true)
        {
            if (!link || type == TypeSymbol.Generic || type == TypeSymbol.Error || type == TypeSymbol.Void)
                builder.Append(type.Name);
            else
                builder.Append($"[{type.Name}]({linkPrefix}Types/{type.Name.ToUpperFirst()}.md)");
        }

        private string getArg(string name)
        {
            if (args.TryGetValue(name, out var value))
                return Build(value);
            else
                throw new Exception($"Required arg '{name}' missing.");
        }
        private bool getBoolArg(string name)
        {
            if (args.TryGetValue(name, out var value))
                return bool.Parse(Build(value));
            else
                return false;
        }
        private string? getOptionalArg(string name)
        {
            if (args.TryGetValue(name, out var value))
                return Build(value);
            else
                return null;
        }

        private void checkInfoEnd(string? str)
        {
            if (!string.IsNullOrEmpty(str) && !str.EndsWith('.') && !str.EndsWith("</>"))
            {
                Console.WriteLine("Info strings should end with '.' or end tag (\"</>\"):");
                Console.WriteLine(str);
            }
        }
        #endregion
    }
}
