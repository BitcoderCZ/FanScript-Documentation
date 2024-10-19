using FanScript.Documentation.DocElements;
using System.Collections.Immutable;

namespace FanScript.DocumentationGenerator.Elements
{
    public sealed class DocFileTemplate : DocElement
    {
        public DocFileTemplate(ImmutableArray<DocArg> arguments, DocString value)
            : base(arguments, value)
        {
            Value = value;
        }

        public override DocString Value { get; }
    }
}
