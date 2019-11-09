using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DelphixLibrary.Repository
{
    [Serializable]
    public class DelphixRepository
    {
        public string type { get; set; }
        public string reference { get; set; }
        public object @namespace { get; set; }
        public string name { get; set; }
        public object version { get; set; }
        public bool linkingEnabled { get; set; }
        public bool provisioningEnabled { get; set; }
        public string environment { get; set; }
        public bool staging { get; set; }
        public string toolkit { get; set; }
    }
}
