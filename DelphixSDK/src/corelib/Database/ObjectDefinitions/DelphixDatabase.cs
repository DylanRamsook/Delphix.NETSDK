using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DelphixLibrary.Database
{
    public class Runtime
    {
        public string type { get; set; }
        public bool logSyncActive { get; set; }
        public object preProvisioningStatus { get; set; }
        public object lastRestoredBackupSetUUID { get; set; }
    }

    public class SourcingPolicy
    {
        public string type { get; set; }
        public bool logsyncEnabled { get; set; }
        public bool loadFromBackup { get; set; }
    }

    public class Metadata
    {
        public string timeflowName { get; set; }
        
    }

    [Serializable]
    public class DelphixDatabase
    {
        public string type { get; set; }
        public string reference { get; set; }
        public object @namespace { get; set; }
        public string name { get; set; }
        public string group { get; set; }
        public string provisionContainer { get; set; }
        public string currentTimeflow { get; set; }
        public object description { get; set; }
        public Runtime runtime { get; set; }
        public string os { get; set; }
        public string processor { get; set; }
        public SourcingPolicy sourcingPolicy { get; set; }
        public bool performanceMode { get; set; }
        public bool delphixManaged { get; set; }
        public bool masked { get; set; }
        
        public Metadata metadata { get; set; }
    }
}
