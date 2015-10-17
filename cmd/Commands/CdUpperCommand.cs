using System;
using System.IO;
using cmd.contracts;
using cmd.contracts.Help;

namespace cmd.Modules
{
    public class CdUpperCommand : ICommand
    {
        public override bool Accept(Command command)
        {
            return command.Name == "cd..";
        }

        public override HelpBuilder Help
        {
            get
            {
                var b = new HelpBuilder();

                b.Add("cd..", "Switch to parent Directory",
                    "cd..");

                return b;
            }
        }

        public override bool Interact(ref DirectoryInfo di, Command c)
        {
            di = new DirectoryInfo(di.Parent.FullName);
            return true;
        }

    }
}