using System;
using cmd.contracts;
using Creek.Extensibility.Plugins;

namespace MenuPlugin
{
    [PlugIn("menu")]
    public class Plugin : PlugIn<ICmdApplication>, ICmdPlugIn
    {
        public ICommand[] GetCommands()
        {
            return new ICommand[] { new MenuCommand() };
        }
    }
}