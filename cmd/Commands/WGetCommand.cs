using System;
using System.IO;
using System.Net;
using cmd.contracts;
using cmd.contracts.Help;

namespace cmd.Modules
{
    public class WGetCommand : ICommand
    {
        public override bool Accept(Command command)
        {
            return command.Name == "wget";
        }

        public override HelpBuilder Help
        {
            get
            {
                var b = new HelpBuilder();

                b.Add("wget", "Download a File",
                    "wget <uri>");

                return b;
            }
        }

        public override bool Interact(ref DirectoryInfo path, Command c)
        {
            var wc = new WebClient();
            var prog = new contracts.Progress.SwayBar();
            bool returns = false;

            wc.DownloadProgressChanged += (s, e) => { prog.Step(); };
            wc.DownloadFileCompleted += (s, e) => { returns = true; };
            
            wc.DownloadFileAsync(new Uri(c.Arguments[0]), path.FullName + "\\" + Path.GetFileName(c.Arguments[0]));

            while (wc.IsBusy)
            {
                
            }

            return returns;
        }
    }
}