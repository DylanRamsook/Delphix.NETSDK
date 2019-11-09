using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using DelphixLibrary.Timeflow;

namespace DelphixLibrary
{
    class GetTimeFlowsResponse : DelphixResponse
    {
        public List<DelphixTimeFlow> result { get; set; }
    }
}
