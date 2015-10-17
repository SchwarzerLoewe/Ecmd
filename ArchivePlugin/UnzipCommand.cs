using System;
using System.IO;
using ICSharpCode.SharpZipLib.Zip;
using cmd.contracts;
using cmd.contracts.Help;

namespace ArchivePlugin
{
    public class UnzipCommand : ICommand
    {
        public override bool Accept(Command command)
        {
            return command.Name == "unzip";
        }

        public override HelpBuilder Help
        {
            get
            {
                var b = new HelpBuilder();

                b.Add("unzip", "unzip an .zip file",
                    "unzip <filename>");

                return b;
            }
        }

        public override bool Interact(ref DirectoryInfo path, Command c)
        {
            if (File.Exists(c.Arguments[0]))
            {
                using (var zip = new ZipFile(path.FullName + "\\" + c.Arguments[0]))
                {
                    if (IsPasswordProtectedZipFile(path.FullName + "\\" + c.Arguments[0]))
                    {
                        Console.Write("Please type in your Zip Password: ");
                        var pw = ConsoleWindow.Password();

                        Console.WriteLine();

                        zip.Password = pw;
                    }

                    var prog = new cmd.contracts.Progress.SwayBar();
                    prog.max = int.Parse(zip.Count.ToString());

                    foreach (ZipEntry ze in zip)
                    {
                        prog.Step();

                        if (ze.IsFile)
                        {
                            using (var fs = new FileStream(path.FullName + "\\" + ze.Name, FileMode.OpenOrCreate, FileAccess.Write))
                                zip.GetInputStream(ze).CopyTo(fs);
                        }
                        else
                        {
                            Directory.CreateDirectory(path.FullName + "\\" + ze.Name);
                        }
                    }
                }
            }
            return true;
        }

        public static bool IsPasswordProtectedZipFile(string path)
        {
            using (FileStream fileStreamIn = new FileStream(path, FileMode.Open, FileAccess.Read))
            using (ZipInputStream zipInStream = new ZipInputStream(fileStreamIn))
            {
                ZipEntry entry = zipInStream.GetNextEntry();
                return entry.IsCrypted;
            }
        }
    }
}
