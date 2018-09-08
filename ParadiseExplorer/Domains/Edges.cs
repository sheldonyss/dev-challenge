using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using ParadiseExplorer.Models;

namespace ParadiseExplorer.Domains
{
    public partial class Edges
    {
        public int Id { get; set; }
        public int StartId { get; set; }
        public string Type { get; set; }
        public int EndId { get; set; }
        public string Link { get; set; }
        public string StartDate { get; set; }
        public string EndDate { get; set; }
        public string SourceId { get; set; }
        public string ValidUntil { get; set; }

        [NotMapped]
        public EdgeType EdgeType => Enum.Parse<EdgeType>(Type);

        public int GetFrom => StartId;
        public int GetTo => EndId;
        public string EdgeTypeDesc => Type;
    }
}
