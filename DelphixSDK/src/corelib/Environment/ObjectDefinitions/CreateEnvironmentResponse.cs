using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using DelphixLibrary.Environment;

namespace DelphixLibrary
{
    class CreateEnvironmentResponse : DelphixResponse
    {
        public string result { get; set; }
        public string job { get; set; }
        public string action { get; set; }
    }
}
