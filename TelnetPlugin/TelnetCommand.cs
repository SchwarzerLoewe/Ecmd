using System;
using System.IO;
using cmd.contracts;

namespace TelnetPlugin
{
    public class TelnetCommand : ICommand
    {
        TelnetPlugin.Internal.TelnetSocket socket = new TelnetPlugin.Internal.TelnetSocket();
        System.Text.StringBuilder response = new System.Text.StringBuilder();

        public TelnetCommand()
        {
            socket.OnDataReceived += DataReceived;
            socket.OnExceptionCaught += ExceptionCaught;
        }
  
        private void ExceptionCaught(Exception ex)
        {
            throw ex;
        }
  
        private void DataReceived(string Data)
        {
            lock (response)
            {
                response.Append(Data);
            }
        }

        public override bool Interact(ref DirectoryInfo path, Command cmd)
        {
            if (cmd.Arguments[0] == "connect")
            {
                socket.Connect(cmd.Arguments[1]);
            }
            else if (cmd.Arguments[0] == "disconnect")
            {
                socket.Close();
            }
            else if (cmd.Arguments[0] == "write")
            {
                socket.Write(cmd.Arguments[1]);
            }
            else if (cmd.Arguments[0] == "writeline")
            {
                socket.WriteLine(cmd.Arguments[1]);
            }

            return true;
        }

        public override bool Accept(Command command)
        {
            return command.Name == "telnet";
        }
    }
}