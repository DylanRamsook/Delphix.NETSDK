using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DelphixLibrary.Database
{
    public class DbCredentials
    {
        public string password { get; set; }
        public string type { get; set; }
    }

    public class IngestionStrategy
    {
        public string type { get; set; }
        public string validatedSyncMode { get; set; }
    }

    public class Operations
    {
        public string postSync { get; set; }
        public string preSync { get; set; }
        public string type { get; set; }
    }

    public class SourcingPolicys
    {
        public bool logsyncEnabled { get; set; }
        public string type { get; set; }
    }

    public class SyncParameters
    {
        public string type { get; set; }
    }

    public class LinkData
    {
        public string config { get; set; }
        public DbCredentials dbCredentials { get; set; }
        public string dbUser { get; set; }
        public string encryptionKey { get; set; }
        public IngestionStrategy ingestionStrategy { get; set; }
        public object mssqlCommvaultConfig { get; set; }
        public object mssqlNetbackupConfig { get; set; }
        public Operations operations { get; set; }
        public string pptHostUser { get; set; }
        public string pptRepository { get; set; }
        public List<string> sharedBackupLocations { get; set; }
        public string sourceHostUser { get; set; }
        public SourcingPolicy sourcingPolicy { get; set; }
        public SyncParameters syncParameters { get; set; }
        public string name { get; set; }
        public string type { get; set; }
    }

    public class LinkDbRequest
    {
        public string description { get; set; }
        public string group { get; set; }
        public LinkData linkData { get; set; }
    }
}
