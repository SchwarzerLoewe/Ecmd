using System;
using cmd.contracts;
using Creek.Extensibility.Plugins;

namespace FtpPlugin
{
    [PlugIn("ftp")]
    public class Plugin : PlugIn<ICmdApplication>, ICmdPlugIn
    {
        public ICommand[] GetCommands()
        {
            return new ICommand[] { new FtpCommand() };
        }
    }
}