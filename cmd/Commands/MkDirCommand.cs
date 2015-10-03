using System;
using System.IO;
using cmd.contracts;

namespace cmd.Modules
{
    public class MkDirCommand : ICommand
    {
        public override bool Accept(Command command)
        {
            return command.Name == "mkdir";
        }

        public override bool Interact(ref DirectoryInfo path, Command c)
        {
            Directory.CreateDirectory(path.FullName + "\\" + c.Arguments[0]);
            return true;
        }

    }
}