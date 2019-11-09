using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DelphixLibrary.Template
{
    public class Properties
    {
    }

    public class DelphixTemplate
    {
        public string type { get; set; }
        public string reference { get; set; }
        public object @namespace { get; set; }
        public string name { get; set; }
        public object notes { get; set; }
        public Properties properties { get; set; }
        public string activeBranch { get; set; }
        public DateTime lastUpdated { get; set; }
        public string firstOperation { get; set; }
        public string lastOperation { get; set; }
    }
}
