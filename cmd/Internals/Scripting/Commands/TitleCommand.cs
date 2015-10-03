using System.IO;
using cmd.contracts;
using System;

namespace Cmd.Internals.Scripting
{
    public class TitleCommand : ICommand
    {
        public override bool Interact(ref DirectoryInfo path, Command command)
        {
            Console.Title = command.Arguments[0];

            return true;
        }

        public override bool Accept(Command command)
        {
            return command.Name == "title";
        }
    }
}