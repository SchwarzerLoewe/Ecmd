using cmd.contracts;
using Creek.Extensibility.Plugins;

namespace ArchivePlugin
{
    [PlugIn("archive")]
    public class Plugin : PlugIn<ICmdApplication>, ICmdPlugIn
    {
        public ICommand[] GetCommands()
        {
            return new ICommand[] { new UnzipCommand(), new ZipCommand() };
        }
    }
}