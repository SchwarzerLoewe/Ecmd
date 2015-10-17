using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Windows.Forms;
using cmd.Commands;
using Cmd.Internals;
using cmd.Internals.Scripting;
using cmd.Modules;
using cmd.Properties;
using cmd.contracts;

namespace cmd
{
    public static class Program
    {
        public static CmdPlugInManager _plugInManager;
        internal static List<ICommand> commands = new List<ICommand>();

        static void Main(string[] args)
        {
            var di = new DirectoryInfo(Application.StartupPath);

            commands.Add(new DirCommand());
            commands.Add(new CdCommand());
            commands.Add(new CdUpperCommand());
            commands.Add(new ClearCommand());
            commands.Add(new RunCommand());
            commands.Add(new CopyCommand());
            commands.Add(new MkDirCommand());
            commands.Add(new WGetCommand());
            commands.Add(new AptGetCommand());
            commands.Add(new EchoCommand());
            commands.Add(new HelpCommand());

            _plugInManager = new CmdPlugInManager();
            _plugInManager.PlugInFolder = Path.Combine(di.FullName, "Plugins");

            if (!Directory.Exists(_plugInManager.PlugInFolder))
            {
                Directory.CreateDirectory(_plugInManager.PlugInFolder);
            }

            _plugInManager.LoadPlugIns();

            foreach (var item in _plugInManager.PlugIns)
            {
                commands.AddRange(item.PlugInProxy.GetCommands());
            }

            Console.Title = "Command Prompt";
            ConsoleWindow.SetConsoleIcon(Resources.cmd);

            if (args.Length == 1)
            {
                ScriptInterpreter.Interprete(File.ReadAllText(args[0]), ref di, commands);
            }
            else
            {
                bool t = true;
                while (t)
                {
                    Console.Write(di.GetShortPathName() + "> ");

                    var cmd = Console.ReadLine();
                    
                    VariableCache.Set("input", cmd);

                    foreach (var item in commands)
                    {
                        var command = Command.Create(cmd);
                        foreach (var c in command)
                        {
                            item.Commands = commands;

                            if (item.Accept(c))
                            {
                                if (item.Interact(ref di, c))
                                {
                                    break;
                                }
                            }
                        }
                    }
                }
            }
        }
    }
}