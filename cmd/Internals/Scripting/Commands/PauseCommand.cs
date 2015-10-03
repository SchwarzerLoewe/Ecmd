using System.IO;
using cmd.Internals.Scripting;
using cmd.contracts;
using System;

namespace Cmd.Internals.Scripting
{
    public class PauseCommand : ICommand
    {
        public override bool Interact(ref DirectoryInfo path, Command command)
        {
            VariableCache.Set("input", Console.ReadLine());

            return true;
        }

        public override bool Accept(Command command)
        {
            return command.Name == "pause";
        }
    }
}