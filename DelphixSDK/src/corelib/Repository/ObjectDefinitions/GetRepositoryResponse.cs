using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using DelphixLibrary.Repository;

namespace DelphixLibrary
{
    class GetRepositoryResponse : DelphixResponse
    {
        public List<DelphixRepository> result { get; set; }
    }
}
