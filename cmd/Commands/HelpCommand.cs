using System.IO;
using cmd.contracts;
using System;
using cmd.contracts.Help;

namespace cmd.Commands
{
    public class HelpCommand : ICommand
    {
        public override bool Accept(Command command)
        {
            return command.Name == "help";
        }

        public override HelpBuilder Help
        {
            get
            {
                var b = new HelpBuilder();

                b.Add("help", "How to use the commands",
                    "help <command>");

                return b;
            }
        }

        public override bool Interact(ref DirectoryInfo path, Command command)
        {
            if (command.Arguments.Length == 0)
            {
                foreach (var c in Program.commands)
                {
                    foreach (var h in c.Help)
                    {
                        if (!string.IsNullOrEmpty(h.Command))
                        {
                            Console.WriteLine(h.Command + " " + h.Description);
                        }
                        else
                        {
                            continue;
                        }
                    }
                }
            }
            else
            {
                foreach (var c in Program.commands)
                {
                    foreach (var h in c.Help)
                    {
                        if(h.Command == command.Arguments[0])
                        {
                            foreach (var item in h.HelpText.Split(new[] { '\r' }, StringSplitOptions.RemoveEmptyEntries ))
                            {
                                Console.WriteLine(item);
                            }
                        }
                    }
                }
            }
            return true;
        }
    }
}