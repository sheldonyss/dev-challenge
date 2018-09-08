using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ParadiseExplorer.Models
{
    public class AddressDto
    {
        public int NodeId { get; set; }
        public string Name { get; set; }
        public string Address1 { get; set; }
        public string CountryCodes { get; set; }
        public string Countries { get; set; }
    }
}
