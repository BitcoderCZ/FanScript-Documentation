namespace FanScript.DocumentationGenerator.Tokens.Links
{
    public sealed class LinkToken : Token
    {
        public string DisplayString { get; }

        public LinkToken(string displayString, string value)
            : base(value)
        {
            DisplayString = displayString;
        }
    }
}
