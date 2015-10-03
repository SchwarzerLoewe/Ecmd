using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Markup;

namespace cmd
{
    [ContentProperty("Apps")]
    public class Repository
    {
        public List<App> Apps { get; set; }
        public Repository()
        {
            Apps = new List<App>();
        }

        public IEnumerable<string> GetNames()
        {
            foreach (var app in Apps)
            {
                yield return app.Name;
            }
        }
    }
  
    public class App
    {
        public string Name { get; set; }
        public List<string> Dependencies { get; set; }
        public string Archive { get; set; }

        public App()
        {
            Dependencies = new List<string>();
        }
    }
}
