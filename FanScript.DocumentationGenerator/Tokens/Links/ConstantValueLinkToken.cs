namespace FanScript.DocumentationGenerator.Tokens.Links
{
    public sealed class ConstantValueLinkToken : Token
    {
        public string ConstantName { get; }

        public ConstantValueLinkToken(string constantName, string value)
            : base(value)
        {
            ConstantName = constantName;
        }
    }
}
