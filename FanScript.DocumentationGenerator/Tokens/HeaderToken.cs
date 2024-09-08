using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FanScript.DocumentationGenerator.Tokens
{
    public sealed class HeaderToken : Token
    {
        public int Level { get; }

        public HeaderToken(int level, string value) : base(value)
        {
            Level = level;
        }
    }
}
