using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using DelphixLibrary.Group;

namespace DelphixLibrary.Group
{
    public class CreateGroupsResponse : DelphixResponse
    {
        public string result { get; set; }
        public string job { get; set; }
        public string action { get; set; }
    }
}
