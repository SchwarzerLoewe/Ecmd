using System;
using System.IO;
using cmd.contracts;
using cmd.contracts.Help;

namespace cmd.Modules
{
    public class CopyCommand : ICommand
    {
        public override bool Accept(Command command)
        {
            return command.Name == "copy";
        }

        public override HelpBuilder Help
        {
            get
            {
                var b = new HelpBuilder();

                b.Add("copy", "Copy a File",
                    "copy <filename> <newfilename>");

                return b;
            }
        }

        public override bool Interact(ref DirectoryInfo path, Command c)
        {
            File.Copy(path.FullName + "\\" + c.Arguments[0], c.Arguments[1]);
            return true;
        }

    }
}