using System;
using System.Diagnostics;

namespace Kopigi.Utils.Class
{
    public class InstanceWatch
    {
        public Guid Key { get; set; }
        public String Label { get; set; }
        public Stopwatch Watch { get; set; }
        public TimeSpan Elapsed { get; set; }

        public InstanceWatch(Guid key, Stopwatch watch, string label)
        {
            Key = key;
            Watch = watch;
            Label = label;
        }
    }
}
