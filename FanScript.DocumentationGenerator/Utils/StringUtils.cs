using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace FanScript.DocumentationGenerator.Utils
{
    public static class StringUtils
    {
        public static string ToUpperFirst(this string str)
        {
            if (str.Length == 0)
                return string.Empty;
            else if (str.Length == 1)
                return char.ToUpperInvariant(str[0]).ToString();
            else
                return char.ToUpperInvariant(str[0]) + str.Substring(1);
        }
        public static string ToLowerFirst(this string str)
        {
            if (str.Length == 0)
                return string.Empty;
            else if (str.Length == 1)
                return char.ToLowerInvariant(str[0]).ToString();
            else
                return char.ToLowerInvariant(str[0]) + str.Substring(1);
        }

        public static string ToHeaderRef(string headerText)
        {
            StringBuilder builder = new StringBuilder(headerText.Length);

            for (int i = 0; i < headerText.Length; i++)
            {
                if (char.IsLetterOrDigit(headerText[i]))
                    builder.Append(headerText[i]);
                else if (headerText[i] == ' ')
                    builder.Append('-');
            }

            return "#" + builder;
        }

        public static string[] BlockSplit(this string str, char c)
        {
            if (c == '(' || c == ')')
                throw new ArgumentException(nameof(c));

            List<int> splits = new List<int>();

            int depth = 0;

            for (int i = 0; i < str.Length; i++)
            {
                switch (str[i])
                {
                    case '(':
                        depth++;
                        break;
                    case ')':
                        {
                            depth--;
                            if (depth < 0)
                                throw new ArgumentException("Closed block, when no block is open.", nameof(str));
                        }
                        break;
                    default:
                        {
                            if (depth == 0 && str[i] == c)
                                splits.Add(i);
                        }
                        break;
                }
            }

            if (depth != 0)
                throw new ArgumentException($"Unclosed block{(depth == 1 ? string.Empty : "s")}.", nameof(str));

            if (splits.Count == 0)
                return [str];

            string[] arr = new string[splits.Count + 1];

            int prevIndex = 0;

            for (int i = 0; i < splits.Count; i++)
            {
                int splitIndex = splits[i];
                arr[i] = str.Substring(prevIndex, splitIndex - prevIndex);
                prevIndex = splitIndex + 1;
            }

            arr[splits.Count] = str.Substring(prevIndex);

            return arr;
        }

        public static string Repeat(this string _str, int times)
        {
            char[] str = _str.ToCharArray();
            char[] res = new char[str.Length * times];

            int index = 0;
            for (int i = 0; i < times; i++)
            {
                Array.Copy(str, 0, res, index, str.Length);
                index += str.Length;
            }

            return new string(res);
        }
    }
}
