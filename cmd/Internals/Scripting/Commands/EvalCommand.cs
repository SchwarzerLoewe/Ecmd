using System.IO;
using cmd;
using cmd.contracts;
using System;

namespace Cmd.Internals.Scripting
{
    public class EvalCommand : ICommand
    {
        public override bool Interact(ref DirectoryInfo path, Command command)
        {
            ScriptInterpreter.Interprete(command.Arguments[0], ref path, Commands);

            return true;
        }

        public override bool Accept(Command command)
        {
            return command.Name == "eval";
        }
    }
}