using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DelphixLibrary.Template
{

    public class Source
    {
        public Properties properties { get; set; }
        public int priority { get; set; }
        public string name { get; set; }
        public string type { get; set; }
    }

    public class DataSource
    {
        public Source source { get; set; }
        public string container { get; set; }
        public string type { get; set; }
    }

    public class Properties2
    {
    }

    [Serializable]
    public class CreateDelphixTemplateRequest
    {
        public string name { get; set; }
        public List<DataSource> dataSources { get; set; }
        public Properties2 properties { get; set; }
        public string type { get; set; }
    }
}
