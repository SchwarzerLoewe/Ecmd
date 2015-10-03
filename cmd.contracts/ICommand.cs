using System;
using System.Collections.Generic;
using System.IO;
using cmd.Internals.Formating;

namespace cmd.contracts
{
    public abstract class ICommand
    {
        public abstract bool Interact(ref DirectoryInfo path, Command command);
        public abstract bool Accept(Command command);

        public List<ICommand> Commands { get; set; }

        protected object Populate(Command c, string name)
        {
            return GetType().GetMethod(name).Invoke(this, c.Arguments);
        }

        protected string formatter(Command c)
        {
            var output = c.Arguments[0];

            var ff = new List<string>(c.Arguments);
            ff.RemoveAt(0);

            return Format.ToString(output, cast(ff));
        }

        protected object[] cast(List<string> src)
        {
            var r = new List<object>();

            foreach (var item in src)
            {
                r.Add((object)item);
            }

            return r.ToArray();
        }
    }
}