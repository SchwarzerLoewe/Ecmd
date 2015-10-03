using System;
using System.Collections.Generic;
using System.Linq;

namespace cmd.contracts
{
    public static class Utilities
    {
        public static string[] Split(string src, char split, char stringchar)
        {
            var tmp = new List<string>();

            var begining = 0;
            var len = src.Length;
            var instring = false;

            for (var index = 0; index < len; index++)
            {
                if (src[index] == split)
                {
                    if (!instring)
                    {
                        tmp.Add(src.Substring(begining, index - begining));
                        begining = index + 1;
                    }
                }
                else if (src[index] == stringchar)
                {
                    instring = !instring;
                }
            }

            tmp.Add(src.Substring(begining).TrimStart(stringchar).TrimEnd(stringchar));

            return tmp.ToArray();
        }
    }
}