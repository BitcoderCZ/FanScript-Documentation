using FanScript.Documentation.DocElements;
using System.Collections.Immutable;

namespace FanScript.DocumentationGenerator.Elements
{
    public sealed class DocFileArg : DocElement
    {
        public DocFileArg(ImmutableArray<DocArg> arguments, DocElement? value, string name)
            : base(arguments, value)
        {
            Name = name;
        }

        public string Name { get; }
    }
}
