using System;
using System.IO;
using System.Net;
using System.Xaml;
using cmd.contracts;
using cmd.contracts.Help;

namespace cmd.Modules
{
    public class AptGetCommand : ICommand
    {
        private RepositoryDatabase db = new RepositoryDatabase();

        public override HelpBuilder Help
        {
            get
            {
                var b = new HelpBuilder();

                b.Add("apt-get", "Package Manager",
                    "apt-get install <packagename>\rapt-get update\rapt-get remove <packagename>" +
                    "\rapt-get add-repository <uri>\rapt-get remove-repository <uri>");

                return b;
            }
        }

        public override bool Accept(Command command)
        {
            return command.Name == "apt-get";
        }

        public override bool Interact(ref DirectoryInfo di, Command c)
        {
            db.Load();

            switch (c.Arguments[0])
            {
                case "update":
                    Update();
                    break;
                case "install":
                        Install(c);
                    break;
                case "remove":
                        Uninstall(c);
                    break;
                case "add-repository":
                        AddRepo(c);
                    break;
                case "remove-repository":
                        RemoveRepo(c);
                    break;
            }

            return true;
        }

        void Update()
        {
            db.Cache.Clear();

            foreach (var r in db.Repositories)
            {
                var prog = new cmd.contracts.Progress.AnimatedBar();

                var wc = new WebClient();
                wc.DownloadProgressChanged += (sender, e) =>
                {
                    prog.Step();
                };

                var xml = wc.DownloadString(r);
                var rep = (Repository)XamlServices.Parse(xml);
                db.Cache.Add(rep);
                
                prog.PrintMessage("Updating Cache");
            }
        }

        void Install(Command c)
        {
            var prog = new cmd.contracts.Progress.AnimatedBar();
            prog.PrintMessage("Installing");
        }

        void Uninstall(Command c)
        {
            var prog = new cmd.contracts.Progress.AnimatedBar();
            prog.PrintMessage("Removing");
        }

        void AddRepo(Command c)
        {
            db.Repositories.Add(c.Arguments[1]);
            db.Save();
        }

        void RemoveRepo(Command c)
        {
            db.Repositories.Remove(c.Arguments[1]);
            db.Save();
        }
    }
}