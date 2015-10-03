using System;
using System.IO;
using cmd.contracts;

namespace cmd.Modules
{
    public class CopyCommand : ICommand
    {
        public override bool Accept(Command command)
        {
            return command.Name == "copy";
        }

        public override bool Interact(ref DirectoryInfo path, Command c)
        {
            File.Copy(path.FullName + "\\" + c.Arguments[0], c.Arguments[1]);
            return true;
        }

    }
}