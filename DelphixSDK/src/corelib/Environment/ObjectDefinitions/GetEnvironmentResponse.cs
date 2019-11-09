using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using DelphixLibrary.Environment;

namespace DelphixLibrary
{
    class GetEnvironmentsResponse : DelphixResponse
    {
        public List<DelphixEnvironment> result { get; set; }
    }
}
