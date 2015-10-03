using System;
using System.Diagnostics;
using System.IO;
using cmd.contracts;

namespace cmd.Modules
{
    public class RunCommand : ICommand
    {
        public override bool Accept(Command command)
        {
            return command.Name == "run";
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