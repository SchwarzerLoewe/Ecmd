using System;
using System.IO;
using ICSharpCode.SharpZipLib.Zip;
using cmd.contracts;

namespace Cmd.Modules.ArchiveCommands
{
    public class ZipCommand : ICommand
    {
        public override bool Accept(Command command)
        {
            return command.Name == "zip";
        }

        public override bool Interact(ref DirectoryInfo path, Command c)
        {
            ZipOutputStream zip = new ZipOutputStream(File.Create(c.Arguments[1]));
            zip.SetLevel(9);
            ZipFolder(path.FullName + "\\" + c.Arguments[0], path.FullName + "\\" + c.Arguments[0], zip);
            zip.Finish();
            zip.Close();

            return true;
        }

        private void ZipFolder(string RootFolder, string CurrentFolder, ZipOutputStream zStream)
        {

            string[] SubFolders = Directory.GetDirectories(CurrentFolder);
            foreach (string Folder in SubFolders)
                ZipFolder(RootFolder, Folder, zStream);

            string relativePath = CurrentFolder.Substring(RootFolder.Length) + "/";

            if (relativePath.Length > 1)
            {
                ZipEntry dirEntry;
                dirEntry = new ZipEntry(relativePath);
                dirEntry.DateTime = DateTime.Now;

            }
            foreach (string file in Directory.GetFiles(CurrentFolder))
            {
                AddFileToZip(zStream, relativePath, file);
            }
        }

        private void AddFileToZip(ZipOutputStream zStream, string relativePath, string file)
        {
            byte[] buffer = new byte[4096];
            string fileRelativePath = (relativePath.Length > 1 ? relativePath : string.Empty) + Path.GetFileName(file);
            ZipEntry entry = new ZipEntry(fileRelativePath);
            entry.DateTime = DateTime.Now;
            zStream.PutNextEntry(entry);
            using (FileStream fs = File.OpenRead(file))
            {
                int sourceBytes;
                do
                {
                    sourceBytes = fs.Read(buffer, 0, buffer.Length);
                    zStream.Write(buffer, 0, sourceBytes);

                } while (sourceBytes > 0);
            }
        }
    }
}
