using System;
using System.Collections.Generic;
using System.Linq;

namespace cmd.Internals.Scripting
{
    public static class VariableCache
    {
        public static Dictionary<string, string> data = new Dictionary<string, string>();

        public static void Set(string name, string value)
        {
            if (data.ContainsKey(name))
            {
                data[name] = value;
            }
            else
            {
                data.Add(name, value);
            }
        }

        public static string Get(string name)
        {
            return data[name];
        }
    }
}
