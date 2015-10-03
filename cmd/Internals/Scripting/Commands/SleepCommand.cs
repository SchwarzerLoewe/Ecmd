using System.IO;
using System.Threading;
using cmd.contracts;
using System;

namespace Cmd.Internals.Scripting
{
    public class SleepCommand : ICommand
    {
        public override bool Interact(ref DirectoryInfo path, Command command)
        {
            Thread.Sleep(int.Parse(command.Arguments[0]));

            return true;
        }

        public override bool Accept(Command command)
        {
            return command.Name == "sleep";
        }
    }
}