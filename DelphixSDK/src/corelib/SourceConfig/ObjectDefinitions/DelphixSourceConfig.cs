using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DelphixLibrary.SourceConfig
{
    public class Credentials
    {
        public string type { get; set; }
        public string password { get; set; }
    }

    public class Instance
    {
        public string type { get; set; }
        public string host { get; set; }
    }

    [Serializable]
    public class DelphixSourceConfig
    {
        public string type { get; set; }
        public string reference { get; set; }
        public object @namespace { get; set; }
        public string name { get; set; }
        public bool linkingEnabled { get; set; }
        public bool discovered { get; set; }
        public string environmentUser { get; set; }
        public string repository { get; set; }
        public string databaseName { get; set; }
        public string user { get; set; }
        public Credentials credentials { get; set; }
        public Instance instance { get; set; }
    }
}
