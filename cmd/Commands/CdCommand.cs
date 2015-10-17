using System;
using System.IO;
using cmd.contracts;
using cmd.contracts.Help;

namespace cmd.Modules
{
    public class CdCommand : ICommand
    {
        public override bool Accept(Command command)
        {
            return command.Name == "cd";
        }

        public override HelpBuilder Help
        {
            get
            {
                var b = new HelpBuilder();

                b.Add("cd", "Switch to Directory",
                    "cd <folder>");

                return b;
            }
        }

        public override bool Interact(ref DirectoryInfo di, Command c)
        {
            if (Directory.Exists(di.FullName + "\\" + c.Arguments[0]))
            {
                di = new DirectoryInfo(di.FullName + "\\" + c.Arguments[0]);
                return true;
            }
            else
            {
                Console.WriteLine("Directory '" + c.Arguments[0] + "' not found!");
                return false;
            }
        }

    }
}