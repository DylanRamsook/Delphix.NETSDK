using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using DelphixLibrary.Source;

namespace DelphixLibrary
{
    class GetSourcesResponse : DelphixResponse
    {
        public List<DelphixSource> result { get; set; }
    }
}
