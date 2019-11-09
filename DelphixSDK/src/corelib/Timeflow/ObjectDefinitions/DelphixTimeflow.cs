using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DelphixLibrary.Timeflow
{

    public class ParentPoint
    {
        public string type { get; set; }
        public object location { get; set; }
        public DateTime timestamp { get; set; }
        public string timeflow { get; set; }
    }

    [Serializable]
    public class DelphixTimeFlow
    {
        public string type { get; set; }
        public string reference { get; set; }
        public object @namespace { get; set; }
        public string name { get; set; }
        public string container { get; set; }
        public ParentPoint parentPoint { get; set; }
        public string parentSnapshot { get; set; }
        public object databaseGuid { get; set; }
    }
}
