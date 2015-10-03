using System;
using System.Linq;
using cmd.contracts;
using Creek.Extensibility.Plugins;

namespace TelnetPlugin
{
    [PlugIn("telnet")]
    public class Plugin : PlugIn<ICmdApplication>, ICmdPlugIn
    {
        public ICommand[] GetCommands()
        {
            return new ICommand[] { new TelnetCommand() };
        }
    }
}