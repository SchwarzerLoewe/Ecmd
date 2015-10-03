using System.IO;
using cmd.contracts;
using System;

namespace cmd.Scripting
{
    public class ColorCommand : ICommand
    {
        public override bool Interact(ref DirectoryInfo path, Command command)
        {
            Console.BackgroundColor = (ConsoleColor)Enum.Parse(typeof(ConsoleColor), command.Arguments[0]);
            Console.ForegroundColor = (ConsoleColor)Enum.Parse(typeof(ConsoleColor), command.Arguments[1]);

            return true;
        }

        public override bool Accept(Command command)
        {
            return command.Name == "color";
        }
    }
}