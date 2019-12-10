using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DelphixLibrary.Generator
{
    [Serializable]
    public class TemplateObject
        {
            public string type { get; set; }
            public string reference { get; set; }
            public object @namespace { get; set; }
            public string name { get; set; }
            public string description { get; set; }
            public string primaryUser { get; set; }
            public bool enabled { get; set; }
            public string host { get; set; }
            public object proxy { get; set; }

            private string outputRoot { get; set; }

        private void generateFile(String name, String contents)
        {
            System.IO.File.WriteAllText(outputRoot + @"/" + name + ".cs", contents);
        }


    }


}
