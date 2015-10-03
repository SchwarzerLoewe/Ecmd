using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using cmd.Internals.Formating;
using cmd.contracts;

namespace cmd.Commands
{
    public class EchoCommand : ICommand
    {
        public override bool Accept(Command command)
        {
            return command.Name == "echo";
        }

        public override bool Interact(ref DirectoryInfo di, Command c)
        {
            var output = c.Arguments[0];

            var ff = new List<string>(c.Arguments);
            ff.RemoveAt(0);

            Console.WriteLine(Format.ToString(output, cast(ff)));

            return true;
        }
    }
}