using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using cmd.Internals.Scripting;

namespace cmd.contracts
{
    public class Command
    {
        public string Name { get; set; }
        public string[] Arguments { get; set; }

        private Command()
        {
        }

        public static IEnumerable<Command> Create(string cmd)
        {
            if (cmd != "")
            {
                var cmds = Regex.Split(cmd, "&&");

                foreach (var item in cmds)
                {
                    var spl = Utilities.Split(item.Trim(), ' ', '"');
                    var c = new Command();
                    c.Name = spl[0];

                    var args = spl.ToList();
                    args.RemoveAt(0);

                    for (int i = 0; i < args.Count; i++)
                    {
                        foreach (var v in VariableCache.data)
                        {
                           args[i] = args[i].Replace("%" + v.Key + "%", v.Value);
                        }
                    }

                    c.Arguments = args.ToArray();

                    yield return c;
                }
            }
        }

        public static bool TryCreate(string cmd)
        {
            if (cmd.Trim().StartsWith("##"))
            {
                return false;
            }

            try
            {
                Create(cmd);
            }
            catch (Exception)
            {
                return false;
            }
            return true;
        }
    }
}