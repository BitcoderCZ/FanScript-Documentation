using FanScript.DocumentationGenerator.Tokens;
using FanScript.DocumentationGenerator.Tokens.Links;
using System.Collections.Immutable;

namespace FanScript.DocumentationGenerator.Builders
{
    public abstract class Builder
    {
        protected readonly ImmutableArray<Token> Tokens;

        protected Builder(ImmutableArray<Token> tokens)
        {
            Tokens = tokens;
        }

        public abstract string Build();

        protected void build()
        {
            foreach (var token in Tokens)
            {
                switch (token)
                {
                    case NewLineToken newLine:
                        buildNewLine(newLine);
                        break;
                    case TabToken tab:
                        buildTab(tab);
                        break;
                    case StringToken str:
                        buildString(str);
                        break;
                    case HeaderToken header:
                        buildHeader(header);
                        break;
                    case ArgToken arg:
                        buildArg(arg);
                        break;
                    case TemplateToken template:
                        buildTemplate(template);
                        break;
                    case LinkToken link:
                        buildLink(link);
                        break;
                    // TODO: $tLink - TypeSymbol link
                    case ParamLinkToken paramLink:
                        buildParamLink(paramLink);
                        break;
                    case ConstantLinkToken constantLink:
                        buildConstantLink(constantLink);
                        break;
                    case ConstantValueLinkToken constantValueLink:
                        buildConstantValueLink(constantValueLink);
                        break;
                    case FunctionLinkToken functionLink:
                        buildFunctionLink(functionLink);
                        break;
                    case EventLinkToken eventLink:
                        buildEventLink(eventLink);
                        break;
                    case CodeBlockToken codeBlock:
                        buildCodeBlock(codeBlock);
                        break;
                    default:
                        throw new InvalidDataException($"Unknown token '{token.GetType()}'.");
                }
            }
        }

        protected abstract void buildNewLine(NewLineToken token);
        protected abstract void buildTab(TabToken token);
        protected abstract void buildString(StringToken token);
        protected abstract void buildHeader(HeaderToken token);
        protected abstract void buildArg(ArgToken token);
        protected abstract void buildTemplate(TemplateToken token);
        protected abstract void buildLink(LinkToken token);
        protected abstract void buildParamLink(ParamLinkToken token);
        protected abstract void buildConstantLink(ConstantLinkToken token);
        protected abstract void buildConstantValueLink(ConstantValueLinkToken token);
        protected abstract void buildFunctionLink(FunctionLinkToken token);
        protected abstract void buildEventLink(EventLinkToken token);
        protected abstract void buildCodeBlock(CodeBlockToken token);
    }
}
