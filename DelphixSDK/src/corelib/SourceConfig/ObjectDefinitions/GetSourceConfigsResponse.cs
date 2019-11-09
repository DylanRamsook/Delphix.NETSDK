using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using DelphixLibrary.SourceConfig;

namespace DelphixLibrary
{
    class GetSourceConfigsResponse : DelphixResponse
    {
        public List<DelphixSourceConfig> result { get; set; }
    }
}
