using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Xaml;

namespace cmd
{
    public class RepositoryDatabase
    {
        public List<string> Repositories { get; set; }

        public List<Repository> Cache { get; set; }

        public RepositoryDatabase()
        {
            Repositories = new List<string>();
            Cache = new List<Repository>();
        }

        public void Save()
        {
            XamlServices.Save("reps", Repositories);
        }

        public void Load()
        {
            if (File.Exists("reps"))
            {
                Repositories = (List<string>)XamlServices.Load("reps");
            }
        }
    }
}
