using System;
using System.IO;
using cmd.contracts;

namespace cmd.Modules
{
    public class ClearCommand : ICommand
    {
        public override bool Accept(Command command)
        {
            return command.Name == "cls";
        }

        public override bool Interact(ref DirectoryInfo di, Command c)
        {
            Console.Clear();

            return true;
        }
    }
}