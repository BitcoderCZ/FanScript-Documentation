using FanScript.DocumentationGenerator.Tokens;
using FanScript.DocumentationGenerator.Tokens.Links;
using System.Collections.Immutable;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using System.Text;

namespace FanScript.DocumentationGenerator.Parsing
{
    public class Parser
    {
        private string text;
        private int position;

        public Parser(string text)
        {
            this.text = text.Replace("\r\n", "\n");
        }

        private char current => text[position];
        private char peek(int offset) => text[position + offset];
        private ReadOnlySpan<char> read(int length)
        {
            ReadOnlySpan<char> span = text.AsSpan(position, length);
            position += length;
            return span;
        }
        private ReadOnlySpan<char> readUntilChar(char c)
        {
            int start = position;

            while (position < text.Length && current != c)
                position++;

            return text.AsSpan(start, position - start);
        }
        private ReadOnlySpan<char> readBlocksUntil(char c)
        {
            int start = position;

            int depth = 0;

            List<ReadOnlyMemory<char>> spans = new();

            while (position < text.Length && (depth > 0 || current != c))
            {
                switch (current)
                {
                    case '(' when position > 0 && peek(-1) == '\\':
                        position--;
                        addSpan();
                        start = ++position;
                        break;
                    case '(':
                        {
                            if (depth == 0)
                                addSpan();

                            depth++;
                        }
                        break;
                    case ')' when position > 0 && peek(-1) == '\\':
                        position--;
                        addSpan();
                        start = ++position;
                        break;
                    case ')':
                        {
                            depth--;
                            Debug.Assert(depth >= 0);
                            if (depth == 0)
                                addSpan();
                        }
                        break;
                }
                position++;
            }

            if (depth > 0)
                throw new Exception($"Unclosed block.");

            addSpan();

            return string.Concat(spans);

            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            void addSpan()
            {
                if (position - start > 0)
                    spans.Add(text.AsMemory(start, position - start));
                start = position + 1;
            }
        }
        private ReadOnlySpan<char> readBlock()
        {
            if (current == '\n')
                position++;

            if (current != '(')
                return ReadOnlySpan<char>.Empty;

            int start = ++position;

            int depth = 1;

            while (position < text.Length && depth > 0)
            {
                switch (current)
                {
                    case '(':
                        depth++;
                        break;
                    case ')':
                        depth--;
                        Debug.Assert(depth >= 0);
                        break;
                }
                position++;
            }

            if (depth > 0)
                throw new Exception($"Unclosed block.");

            return text.AsSpan(start, (position - 1) - start);
        }

        public ImmutableArray<Token> Parse()
        {
            ImmutableArray<Token>.Builder builder = ImmutableArray.CreateBuilder<Token>();
            position = 0;

            while (position < text.Length)
            {
                switch (current)
                {
                    case '\n':
                        builder.Add(new NewLineToken(new string(read(1))));
                        break;
                    case '\t':
                        builder.Add(new TabToken(new string(read(1))));
                        break;
                    case '\\':
                        {
                            position++;
                            switch (current)
                            {
                                case 'n':
                                    builder.Add(new NewLineToken("\n"));
                                    position++;
                                    break;
                                case 't':
                                    builder.Add(new TabToken("\t"));
                                    position++;
                                    break;
                                default:
                                    builder.Add(new StringToken(new string(read(1))));
                                    break;
                            }
                        }
                        break;
                    case '#':
                        builder.Add(parseHeader());
                        break;
                    case '@':
                        builder.Add(parseArg());
                        break;
                    case '$':
                        builder.Add(parseDolar());
                        break;
                    default:
                        {
                            var read = readText();
                            if (read.Length != 0)
                                builder.Add(new StringToken(new string(read)));
                            else
                                position++; // prevent infinite loop
                        }
                        break;
                }
            }

            return builder.ToImmutable();
        }

        private ReadOnlySpan<char> readText()
        {
            StringBuilder builder = new StringBuilder();

            while (position < text.Length && current != '#' && current != '$' && current != '@' && current != '\n' && current != '\t')
            {
                switch (current)
                {
                    case '\\':
                        {
                            if (peek(1) == 'n' || peek(1) == 't')
                                return builder.ToString();

                            position++;
                            builder.Append(current);
                            position++;
                        }
                        break;
                    default:
                        {
                            builder.Append(current);
                            position++;
                        }
                        break;
                }
            }

            return builder.ToString();
        }

        private ReadOnlySpan<char> readLine()
        {
            int start = position;

            while (position < text.Length && current != '\n')
                position++;

            var res = text.AsSpan(start, position - start);

            if (position < text.Length)
                position++; // skip \n

            return res;
        }

        private HeaderToken parseHeader()
        {
            int headerLevel = 0;

            while (position < text.Length && current == '#')
            {
                headerLevel++;
                position++;
            }

            var value = readText();

            return new HeaderToken(headerLevel, new string(value.Trim()));
        }

        private ArgToken parseArg()
        {
            position++;
            string name = new string(readUntilChar(':'));
            position++;
            string value = new string(readBlocksUntil('\n'));
            position++;

            return new ArgToken(name, value);
        }

        private Token parseDolar()
        {
            position++;

            string s = new string(readUntilChar(' '));
            position++;

            switch (s)
            {
                case "template":
                    return parseTemplate();
                case "link":
                    return parseLink();
                case "plink":
                    return parsePLink();
                case "clink":
                    return parseCLink();
                case "cvlink":
                    return parseCVLink();
                case "flink":
                    return parseFLink();
                case "elink":
                    return parseELink();
                case "tlink":
                    return parseTLink();
                case "mlink":
                    return parseMLink();
                case "codeblock":
                    return parseCodeBlock();
                default:
                    throw new InvalidDataException($"Unknown $ expression '{s}'.");
            }
        }

        private TemplateToken parseTemplate()
        {
            string value = new string(readLine());

            return new TemplateToken(value);
        }
        private LinkToken parseLink()
        {
            string name = new string(readUntilChar(';'));
            position++;
            string url = new string(readUntilChar(';'));
            position++;
            return new LinkToken(name, url);
        }
        private ParamLinkToken parsePLink()
        {
            string paramName = new string(readUntilChar(';'));
            position++;
            return new ParamLinkToken(paramName);
        }
        private ConstantLinkToken parseCLink()
        {
            string constantName = new string(readUntilChar(';'));
            position++;
            return new ConstantLinkToken(constantName);
        }
        private ConstantValueLinkToken parseCVLink()
        {
            string constantName = new string(readUntilChar(';'));
            position++;
            string constantValueName = new string(readUntilChar(';'));
            position++;
            return new ConstantValueLinkToken(constantName, constantValueName);
        }
        private FunctionLinkToken parseFLink()
        {
            string functionSpecification = new string(readUntilChar(';'));
            position++;
            return new FunctionLinkToken(functionSpecification);
        }
        private EventLinkToken parseELink()
        {
            string eventName = new string(readUntilChar(';'));
            position++;
            return new EventLinkToken(eventName);
        }
        private TypeLinkToken parseTLink()
        {
            string type = new string(readUntilChar(';'));
            position++;
            return new TypeLinkToken(type);
        }
        private ModifierLinkToken parseMLink()
        {
            string modifier = new string(readUntilChar(';'));
            position++;
            return new ModifierLinkToken(modifier);
        }
        private CodeBlockToken parseCodeBlock()
        {
            string lang = new string(readUntilChar(';'));
            position++;
            string text = new string(readBlock());
            return new CodeBlockToken(lang, text);
        }
    }
}