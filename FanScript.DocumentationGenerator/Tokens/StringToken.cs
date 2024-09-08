using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FanScript.DocumentationGenerator.Tokens
{
    public sealed class StringToken : Token
    {
        public StringToken(string value) : base(value)
        {
        }
    }
}
