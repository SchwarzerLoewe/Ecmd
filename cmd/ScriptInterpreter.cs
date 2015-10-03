using System;
using System.Collections.Generic;
using System.IO;
using Cmd.Internals.Scripting;
using cmd.Scripting;
using cmd.contracts;

namespace cmd
{
    public class ScriptInterpreter
    {
        public static void Interprete(string src, ref DirectoryInfo di, List<ICommand> cmds)
        {
            cmds.Add(new PauseCommand());
            cmds.Add(new SleepCommand());
            cmds.Add(new TitleCommand());
            cmds.Add(new ColorCommand());
            cmds.Add(new SetCommand());
            cmds.Add(new EvalCommand());

            foreach (var c in cmds)
            {
                c.Commands = cmds;
            }

            foreach (var l in src.Split(new[] { '\r' }, StringSplitOptions.RemoveEmptyEntries))
            {
                var line = l.Trim();
                bool iscmd = Command.TryCreate(line);

                if (line.StartsWith("::"))
                {
                    continue;
                }
                else if (iscmd)
                {
                    var cmd = Command.Create(line);

                    RunFunc(cmd, cmds, ref di);
                }
            }
        }
  
        private static bool RunFunc(IEnumerable<Command> cmds, List<ICommand> commands, ref DirectoryInfo di)
        {
            foreach (var cmd in commands)
            {
                foreach (var c in cmds)
                {
                    if (cmd.Accept(c))
                    {
                        cmd.Interact(ref di, c);

                        return true;
                    }
                }
            }
            return false;
        }
    }
}