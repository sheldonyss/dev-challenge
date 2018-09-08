using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ParadiseExplorer.Models
{
    public class EdgeDto
    {
        public int Id { get; set; }
        public int From { get; set; }
        public string Type { get; set; }
        public int To { get; set; }
        public string Link { get; set; }
        public EdgeType EdgeType { get; set; }
        public string EdgeTypeDesc { get; set; }
    }
}
