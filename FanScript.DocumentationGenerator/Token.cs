using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FanScript.DocumentationGenerator
{
    public abstract class Token
    {
        public string Value { get; }

        protected Token(string value)
        {
            Value = value;
        }
    }
}
