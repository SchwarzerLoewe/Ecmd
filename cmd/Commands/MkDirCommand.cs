using System;
using System.IO;
using cmd.contracts;
using cmd.contracts.Help;

namespace cmd.Modules
{
    public class MkDirCommand : ICommand
    {
        public override bool Accept(Command command)
        {
            return command.Name == "mkdir";
        }

        public override HelpBuilder Help
        {
            get
            {
                var b = new HelpBuilder();

                b.Add("mkdir", "Create a Directory",
                    "mkdir <name>");

                return b;
            }
        }

        public override bool Interact(ref DirectoryInfo path, Command c)
        {
            Directory.CreateDirectory(path.FullName + "\\" + c.Arguments[0]);
            return true;
        }

    }
}