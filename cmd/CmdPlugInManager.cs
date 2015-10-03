using System;
using Creek.Extensibility.Plugins;
using cmd.contracts;

namespace cmd
{
    [PlugInApplication("cmd")]
    public class CmdPlugInManager : PlugInBasedApplication<ICmdPlugIn>, ICmdApplication
    {
        public ICommand[] GetCommands()
        {
            return Program.commands.ToArray();
        }
    }
}