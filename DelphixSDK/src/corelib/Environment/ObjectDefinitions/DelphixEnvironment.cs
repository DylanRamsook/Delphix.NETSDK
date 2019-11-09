using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DelphixLibrary.Environment
{
    [Serializable]
    public class DelphixEnvironment
        {
            public string type { get; set; }
            public string reference { get; set; }
            public object @namespace { get; set; }
            public string name { get; set; }
            public string description { get; set; }
            public string primaryUser { get; set; }
            public bool enabled { get; set; }
            public string host { get; set; }
            public object proxy { get; set; }
        }
}
