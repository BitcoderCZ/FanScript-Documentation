using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
