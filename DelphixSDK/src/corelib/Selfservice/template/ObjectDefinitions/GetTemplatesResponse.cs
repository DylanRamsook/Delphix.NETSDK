using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using DelphixLibrary.Template;

namespace DelphixLibrary
{
    class GetTemplatesResponse : DelphixResponse
    {
        public List<DelphixTemplate> result { get; set; }
    }
}
