namespace FanScript.DocumentationGenerator.Tokens
{
    public class CodeBlockToken : Token
    {
        public string Lang { get; }

        public CodeBlockToken(string lang, string value) : base(value)
        {
            Lang = lang;
        }
    }
}
