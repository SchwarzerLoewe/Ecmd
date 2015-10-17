using System;
using System.IO;
using cmd.contracts;
using cmd.contracts.Help;

namespace TelnetPlugin
{
    public class TelnetCommand : ICommand
    {
        TelnetPlugin.Internal.TelnetSocket socket = new TelnetPlugin.Internal.TelnetSocket();
        System.Text.StringBuilder response = new System.Text.StringBuilder();

        public override HelpBuilder Help
        {
            get
            {
                var b = new HelpBuilder();

                b.Add("telnet", "Telnet Client",
                    "telnet connect <host>\rtelnet disconnect\rtelnet write <text>\rtelnet writeline <text>" +
                    "\rapt-get add-repository <uri>\rapt-get remove-repository <uri>");

                return b;
            }
        }

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