using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Giddy.Utility
{
    public static class StringExtensionMethods
    {
        /// <summary>
        /// returns the substring between two found text locations. E.g. If text = "abcdef" then string.Between("bc","f") will return "de";
        /// </summary>
        /// <param name="text">The strig the search</param>
        /// <param name="fromFirstInstanceOf">Text that defines the start position</param>
        /// <param name="toFirstInstanceOf">Text that defines the end position</param>
        /// <returns></returns>
        public static string Between(this string text, string fromFirstInstanceOf, string toFirstInstanceOf)
        {
            var start = text.IndexOf(fromFirstInstanceOf) + fromFirstInstanceOf.Length;
            var end = text.IndexOf(toFirstInstanceOf, start);

            var substring = text.Substring(start, end - start);

            return substring;
        }
    }
}
