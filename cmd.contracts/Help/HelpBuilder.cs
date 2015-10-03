using System;
using System.Collections;
using System.Collections.Generic;

namespace cmd.contracts.Help
{
    public class HelpBuilder : IEnumerable<HelpItem>
    {
        private List<HelpItem> _items = new List<HelpItem>();

        public static HelpBuilder Empty => new HelpBuilder();

        public void Add(string command, string description, string helptext)
        {
            _items.Add(new HelpItem { Command = command, Description = description, HelpText = helptext });
        }

        public IEnumerator<HelpItem> GetEnumerator()
        {
            return _items.GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return _items.GetEnumerator();
        }
    }
}