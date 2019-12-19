using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DelphixLibrary
{
    public class Error
    {
        public string type { get; set; }
        public string details { get; set; }
        public string action { get; set; }
        public string id { get; set; }
        public object commandOutput { get; set; }
    }

    public class DelphixResponseError
    {
        public string type { get; set; }
        public string status { get; set; }
        public Error error { get; set; }
    }
}
