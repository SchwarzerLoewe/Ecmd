using System;
using System.Linq;

namespace cmd.contracts.Progress
{
    public abstract class AbstractBar
    {
        public AbstractBar()
        {

        }

        /// <summary>
        /// Prints a simple message 
        /// </summary>
        /// <param name="msg">Message to print</param>
        public void PrintMessage(string msg)
        {
            Console.WriteLine(msg);
        }

        public abstract void Step();
    }
}
