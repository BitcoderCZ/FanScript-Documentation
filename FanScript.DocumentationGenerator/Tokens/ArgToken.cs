namespace FanScript.DocumentationGenerator.Tokens
{
    public sealed class ArgToken : Token
    {
        public string Name { get; }

        public ArgToken(string name, string value) : base(value)
        {
            Name = name;
        }
    }
}
