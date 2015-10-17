using System;
using System.IO;
using cmd.contracts;
using cmd.contracts.Help;

namespace cmd.Modules
{
    public class DirCommand : ICommand
    {
        public override bool Accept(Command command)
        {
            return command.Name == "dir";
        }

        public override HelpBuilder Help
        {
            get
            {
                var b = new HelpBuilder();

                b.Add("dir", "List all Directories and Files",
                    "dir");

                return b;
            }
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