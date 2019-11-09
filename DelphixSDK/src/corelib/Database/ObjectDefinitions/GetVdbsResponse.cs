using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using DelphixLibrary.Database;

namespace DelphixLibrary
{
    class GetVdbsResponse : DelphixResponse
    {
        public List<DelphixDatabase> result { get; set; }
    }
}
