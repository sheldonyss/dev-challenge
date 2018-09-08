using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ParadiseExplorer.Models
{
    public enum EdgeType
    {
        connected_to,
        intermediary_of,
        officer_of,
        registered_address,
        same_as,
        same_id_as,
        same_name_as
    }
}
