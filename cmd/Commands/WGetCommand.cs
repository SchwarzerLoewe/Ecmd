using System;
using System.IO;
using System.Net;
using System.Threading;
using cmd.contracts;

namespace cmd.Modules
{
    public class WGetCommand : ICommand
    {
        public override bool Accept(Command command)
        {
            return command.Name == "wget";
        }

        public override bool Interact(ref DirectoryInfo path, Command c)
        {
            var wc = new WebClient();
            var prog = new cmd.contracts.Progress.SwayBar();
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