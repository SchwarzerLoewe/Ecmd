using System;
using System.Diagnostics;
using System.IO;
using cmd.contracts;
using cmd.contracts.Help;

namespace cmd.Modules
{
    public class RunCommand : ICommand
    {
        public override bool Accept(Command command)
        {
            return command.Name == "run";
        }

        public override HelpBuilder Help
        {
            get
            {
                var b = new HelpBuilder();

                b.Add("run", "Run an external process",
                    "run <filename>");

                return b;
            }
        }

        public override bool Interact(ref DirectoryInfo di, Command c)
        {
            if (File.Exists(di.FullName + "\\" + c.Arguments[0]))
            {
                if (c.Arguments.Length == 1)
                {
                    Process.Start(di.FullName + "\\" + c.Arguments[0]);
                }
                else if (c.Arguments.Length == 2)
                {
                    Process.Start(di.FullName + "\\" + c.Arguments[0], c.Arguments[1]);
                }
                return true;
            }
            else
            {
                return false;
            }
        }
    }
}