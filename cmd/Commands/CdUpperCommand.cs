using System;
using System.IO;
using cmd.contracts;

namespace cmd.Modules
{
    public class CdUpperCommand : ICommand
    {
        public override bool Accept(Command command)
        {
            return command.Name == "cd..";
        }

        public override bool Interact(ref DirectoryInfo di, Command c)
        {
            di = new DirectoryInfo(di.Parent.FullName);
            return true;
        }

    }
}