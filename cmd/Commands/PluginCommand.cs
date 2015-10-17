using System;
using System.IO;
using cmd.contracts;
using cmd.contracts.Help;

namespace cmd.Modules
{
    public class PluginCommand : ICommand
    {
        public override bool Accept(Command command)
        {
            return command.Name == "plugins";
        }

        public override HelpBuilder Help
        {
            get
            {
                var b = new HelpBuilder();

                b.Add("plugins", "Manage Plugins",
                    "plugins install <filename>\rplugins uninstall <pluginname>\rplugins refresh");

                return b;
            }
        }

        //ToDo: implement uninstall/install plugin
        public override bool Interact(ref DirectoryInfo di, Command c)
        {
            switch (c.Arguments[0])
            {
                case "install":

                    break;
                case "uninstall":

                    break;
                case "refresh":
                    Console.WriteLine("Refreshing Plugins..");

                    Program._plugInManager.LoadPlugIns();

                    foreach (var item in Program._plugInManager.PlugIns)
                    {
                        Program.commands.AddRange(item.PlugInProxy.GetCommands());
                    }
                    break;
            }

            return true;
        }
    }
}