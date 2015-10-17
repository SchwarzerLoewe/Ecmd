using System;
using System.IO;
using cmd.contracts;
using cmd.contracts.Help;

namespace cmd.Modules
{
    public class ClearCommand : ICommand
    {
        public override bool Accept(Command command)
        {
            return command.Name == "cls";
        }

        public override HelpBuilder Help
        {
            get
            {
                var b = new HelpBuilder();

                b.Add("cls", "Clear the screen",
                    "cls");

                return b;
            }
        }
        public override bool Interact(ref DirectoryInfo di, Command c)
        {
            Console.Clear();

            return true;
        }
    }
}