using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using DelphixLibrary.Group;

namespace DelphixLibrary
{
    class GetGroupsResponse : DelphixResponse
    {
        public List<DelphixGroup> result { get; set; }
        public string job { get; set; }
        public string action { get; set; }
        public int total { get; set; }
        public bool overflow { get; set; }
    }
}
