using FanScript.DocumentationGenerator.Tokens;
using FanScript.DocumentationGenerator.Tokens.Links;
using System.Collections.Immutable;
using System.Text;

namespace FanScript.DocumentationGenerator.Builders
{
    public sealed class TextBuilder : Builder
    {
        private StringBuilder builder = new();

        public TextBuilder(ImmutableArray<Token> tokens) : base(tokens)
        {
        }

        public override string Build()
        {
            builder.Clear();

            build();

            return builder.ToString();
        }

        protected override void buildTab(TabToken token)
            => builder.Append('\t');

        protected override void buildNewLine(NewLineToken token)
            => builder.AppendLine();

        protected override void buildString(StringToken token)
            => builder.Append(token.Value);

        protected override void buildHeader(HeaderToken token)
            => builder.Append(new string('#', token.Level) + " " + token.Value);

        protected override void buildArg(ArgToken token)
            => builder.AppendLine("@" + token.Name + ":" + token.Value);

        protected override void buildTemplate(TemplateToken token)
            => builder.AppendLine("$template " + token.Value);

        protected override void buildLink(LinkToken token)
            => builder.Append("$link " + token.DisplayString + ";" + token.Value + ";");

        protected override void buildParamLink(ParamLinkToken token)
            => builder.Append("$plink " + token.Value + ";");

        protected override void buildConstantLink(ConstantLinkToken token)
            => builder.Append("$clink " + token.Value + ";");
        protected override void buildConstantValueLink(ConstantValueLinkToken token)
            => builder.Append("$cvlink " + token.ConstantName + ";" + token.Value + ";");

        protected override void buildFunctionLink(FunctionLinkToken token)
            => builder.Append("$flink " + token.Value + ";");

        protected override void buildEventLink(EventLinkToken token)
            => builder.Append("$elink " + token.Value + ";");

        protected override void buildCodeBlock(CodeBlockToken token)
            => builder.Append("$codeblock " + token.Lang + ";(" + token.Value + ")");
    }
}
