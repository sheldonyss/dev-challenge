using System;
using System.Collections.Generic;

namespace ParadiseExplorer.Domains
{
    public partial class Entity
    {
        public int NodeId { get; set; }
        public string Name { get; set; }
        public string Jurisdiction { get; set; }
        public string JurisdictionDescription { get; set; }
        public string CountryCodes { get; set; }
        public string Countries { get; set; }
        public string IncorporationDate { get; set; }
        public string InactivationDate { get; set; }
        public string StruckOffDate { get; set; }
        public string ClosedDate { get; set; }
        public string IbcRuc { get; set; }
        public string Status { get; set; }
        public string CompanyType { get; set; }
        public string ServiceProvider { get; set; }
        public string SourceId { get; set; }
        public string ValidUntil { get; set; }
        public string Note { get; set; }
    }
}
