using System;
using System.IO;
using System.Text.RegularExpressions;
using FtpPlugin.Internals;
using cmd.contracts;
using cmd.contracts.Help;

namespace FtpPlugin
{
    public class FtpCommand : ICommand
    {
        FtpClient client = new FtpClient();

        public override HelpBuilder Help
        {
            get
            {
                var b = new HelpBuilder();

                b.Add("ftp", "Manage FTP",
                    "ftp connect <host> <username>\rftp ls");

                return b;
            }
        }

        public override bool Interact(ref DirectoryInfo path, Command command)
        {
            switch (command.Arguments[0])
            {
                case "connect":
                    client.Host = command.Arguments[1];
                    Console.Write("Password: ");
                    var pw = ConsoleWindow.Password();

                    client.Credentials = new System.Net.NetworkCredential(command.Arguments[2], pw);

                    client.Connect();

                    break;
                case "disconnect":
                    client.Disconnect();

                    break;
                case "ls":
                    var ls = client.GetListing();
                    var t = new ConsoleTable();

                    t.PrintRow("Filename", "Size", "Type");
                    foreach (var li in ls)
                    {
                        if (li.Size == -1)
                        {
                            t.PrintRow(li.Name, "", "<Folder>");
                        }
                        else
                        {
                            t.PrintRow(li.Name, li.Size.ToString(), "<File>");
                        }
                    }
                    t.PrintLine();

                    break;
                case "exec":
                    client.Execute(command.Arguments[1]);
                    break;
                case "download":
                    MemoryStream s = null;
                    Stream ss = null;

                    try
                    {
                        ss = client.OpenRead(command.Arguments[1]);
                        
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

                    File.WriteAllBytes(command.Arguments[2], s.ToArray());

                    s.Close();

                    break;
            }

            return true;
        }

        public override bool Accept(Command command)
        {
            return Regex.IsMatch("ftp", command.Name);
        }
    }
}