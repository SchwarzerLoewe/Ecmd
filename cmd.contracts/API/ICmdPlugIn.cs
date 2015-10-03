using Creek.Extensibility.Plugins;

namespace cmd.contracts
{
    public interface ICmdPlugIn : IPlugIn
    {
        ICommand[] GetCommands();
    }
}