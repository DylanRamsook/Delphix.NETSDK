using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DelphixLibrary.Job
{

    public class Event
    {
        public string type { get; set; }
        public DateTime timestamp { get; set; }
        public object state { get; set; }
        public int percentComplete { get; set; }
        public string messageCode { get; set; }
        public string messageDetails { get; set; }
        public object messageAction { get; set; }
        public object messageCommandOutput { get; set; }
        public string eventType { get; set; }
    }

    public class DelphixJob
    {
        public string type { get; set; }
        public string reference { get; set; }
        public object @namespace { get; set; }
        public object name { get; set; }
        public string actionType { get; set; }
        public string target { get; set; }
        public string targetObjectType { get; set; }
        public string jobState { get; set; }
        public DateTime startTime { get; set; }
        public DateTime updateTime { get; set; }
        public bool suspendable { get; set; }
        public bool cancelable { get; set; }
        public bool queued { get; set; }
        public string user { get; set; }
        public object emailAddresses { get; set; }
        public string title { get; set; }
        public int percentComplete { get; set; }
        public string targetName { get; set; }
        public List<Event> events { get; set; }
        public string parentActionState { get; set; }
        public string parentAction { get; set; }
    }
}
