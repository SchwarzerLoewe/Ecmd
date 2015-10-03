using System;
using System.IO;
using cmd.contracts;

namespace cmd.Modules
{
    public class DirCommand : ICommand
    {
        public override bool Accept(Command command)
        {
            return command.Name == "dir";
        }

        public override bool Interact(ref DirectoryInfo di, Command c)
        {
            var table = new ConsoleTable();
            foreach (var item in Directory.GetDirectories(di.FullName))
            {
                var d = new DirectoryInfo(item);

                table.PrintRow(d.Name, "<DIR>", d.CreationTime.ToShortDateString());
            }
            foreach (var item in Directory.GetFiles(di.FullName))
            {
                var d = new FileInfo(item);

                table.PrintRow(d.Name, "<FILE>", d.CreationTime.ToShortDateString());
            }
            return true;
        }

    }
}