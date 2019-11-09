using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace DelphixLibrary.Generator
{
    class ListResultTemplateObject : DelphixResponse
    {
        public List<TemplateObject> result { get; set; }
    }
}
