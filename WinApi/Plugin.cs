using System;
using Creek.Extensibility.Plugins;
using cmd.contracts;

namespace WinApiPlugin
{
    [PlugIn("winapi")]
    public class Plugin : PlugIn<ICmdApplication>, ICmdPlugIn
    {
        public ICommand[] GetCommands()
        {
            return new ICommand[] { new WinApiCommand() };
        }
    }
}