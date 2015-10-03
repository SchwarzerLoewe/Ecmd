using Creek.Extensibility.Plugins;

namespace cmd.contracts
{
    public interface ICmdApplication : IPlugInBasedApplication
    {
        ICommand[] GetCommands();
    }
}