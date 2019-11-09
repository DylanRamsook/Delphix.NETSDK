using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DelphixLibrary.Source
{

    public class Runtime2
    {
        public string type { get; set; }
        public string status { get; set; }
        public bool accessible { get; set; }
        public DateTime accessibleTimestamp { get; set; }
        public long databaseSize { get; set; }
        public object notAccessibleReason { get; set; }
        public string recoveryModel { get; set; }
    }

    [Serializable]
    public class DelphixSource
    {
        public string type { get; set; }
        public string reference { get; set; }
        public object @namespace { get; set; }
        public string name { get; set; }
        public object description { get; set; }
        public bool @virtual { get; set; }
        public bool staging { get; set; }
        public string container { get; set; }
        public string config { get; set; }
        public string status { get; set; }
        public Runtime2 runtime { get; set; }
        public object hosts { get; set; }
        public object externalFilePath { get; set; }
        public object backupLocationUser { get; set; }
        public object backupLocationCredentials { get; set; }
        public string stagingSource { get; set; }
        public string sharedBackupLocation { get; set; }
        public bool enabled { get; set; }
        public string preScript { get; set; }
        public string postScript { get; set; }
    }
}
