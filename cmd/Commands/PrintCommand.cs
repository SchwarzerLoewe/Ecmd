using System;
using System.IO;
using cmd.contracts;

namespace cmd.Commands
{
    public class PrintCommand : ICommand
    {
        public override bool Accept(Command command)
        {
            return command.Name == "print";
        }

        public override bool Interact(ref DirectoryInfo di, Command c)
        {
            Console.Write(c.Arguments[0]);

            return true;
        }
    }
}