using System.IO;
using cmd.contracts;
using cmd.contracts.Help;

namespace WinApiPlugin
{
    public class WinApiCommand : ICommand
    {
        public override bool Accept(Command command)
        {
            return command.Name == "winapi";
        }

        public override HelpBuilder Help
        {
            get
            {
                var b = new HelpBuilder();

                b.Add("winapi", "Special WinApi functions",
                    "winapi taskbar --show\rwinapi taskbar --hide");

                return b;
            }
        }

        public override bool Interact(ref DirectoryInfo path, Command command)
        {
            switch (command.Arguments[0])
            {
                case "taskbar":
                    if (command.Arguments[1] == "--show")
                    {
                        API.ShowTasktbar();
                    }
                    else if (command.Arguments[1] == "--hide")
                    {
                        API.HideTasktbar();
                    }
                    break;
                case "window":
                    if (command.Arguments[1] == "--show")
                    {
                        API.ShowConsole();
                    }
                    else if (command.Arguments[1] == "--hide")
                    {
                        API.HideConsole();
                    }
                    break;
            }

            return true;
        }
    }
}
