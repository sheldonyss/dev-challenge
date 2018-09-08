using System;
using System.Collections.Generic;

namespace ParadiseExplorer.Domains
{
    public partial class Address
    {
        public int NodeId { get; set; }
        public string Name { get; set; }
        public string Address1 { get; set; }
        public string CountryCodes { get; set; }
        public string Countries { get; set; }
        public string SourceId { get; set; }
        public string ValidUntil { get; set; }
        public string Note { get; set; }
    }
}
