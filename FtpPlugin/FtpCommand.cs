using System;
using System.IO;
using System.Text.RegularExpressions;
using FtpPlugin.Internals;
using cmd.contracts;

namespace FtpPlugin
{
    public class FtpCommand : ICommand
    {
        FtpClient client = new FtpClient();

        public override bool Interact(ref DirectoryInfo path, Command command)
        {
            switch (command.Name)
            {
                case "connect":
                    client.Host = command.Arguments[0];
                    client.Credentials = new System.Net.NetworkCredential(command.Arguments[1], command.Arguments[2]);

                    client.Connect();

                    break;
                case "ls":
                    var ls = client.GetListing();
                    var t = new ConsoleTable();

                    t.PrintRow("Filename", "Size");
                    foreach (var li in ls)
                    {
                        t.PrintRow(li.Name, li.Size.ToString());
                    }
                    t.PrintLine();

                    break;
                case "exec":
                    client.Execute(command.Arguments[0]);
                    break;
                case "download":
                    MemoryStream s = null;
                    Stream ss = null;

                    try
                    {
                        ss = client.OpenRead(command.Arguments[0]);
                        
                        // perform transfer
                    }
                    finally
                    {
                        if (s != null)
                        {
                            ss.CopyTo(s);
                            s.Close();
                        }  
                    }

                    File.WriteAllBytes(command.Arguments[1], s.ToArray());

                    s.Close();

                    break;
            }

            Console.Clear();

            return true;
        }

        public override bool Accept(Command command)
        {
            return Regex.IsMatch("ftp", command.Name);
        }
    }
}