using System.IO;
using cmd.Internals.Scripting;
using cmd.contracts;
using System;

namespace cmd.Scripting
{
    public class SetCommand : ICommand
    {
        public override bool Interact(ref DirectoryInfo path, Command cmd)
        {
            VariableCache.Set(cmd.Arguments[0], cmd.Arguments[1]);

            return true;
        }

        public override bool Accept(Command command)
        {
            return command.Name == "set";
        }
    }
}